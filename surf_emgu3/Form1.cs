using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using SURF_DENEME;
using Emgu.CV.UI;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using DirectShowLib;
using System.IO;

namespace surf_emgu3
{
    public partial class Form1 : Form
    {
        private Capture capture;  //takes images from camera as image frames
        private bool captureInProgress;
        public static Image<Bgr, Byte> ImageFrame;
        Image<Gray, Byte> grayImage;
        long a1 = 0;
        bool eslesti = false;
        string[] adresler;
        bool kontrol = false;
        
        //public static Image<Gray, Byte> resim_anahtar = new Image<Gray, Byte>("anahtar2.jpg");
        Mat modelImage = CvInvoke.Imread("a1.jpg", LoadImageType.Grayscale);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            foreach (var item in _SystemCamereas)
            {
                comboBox1.Items.Add(item.Name);
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            long matchTime;
            //using(Mat modelImage = CvInvoke.Imread("box.png", LoadImageType.Grayscale))
            //using (Mat observedImage = CvInvoke.Imread("box_in_scene.png", LoadImageType.Grayscale))

            using (Mat modelImage = CvInvoke.Imread("a1.jpg", LoadImageType.Grayscale))
            using (Mat observedImage = CvInvoke.Imread("a2.jpg", LoadImageType.Grayscale))
            {
                Mat result = DrawMatches.Draw(modelImage, observedImage, out matchTime,out eslesti);
               // pictureBox1.Image = result.Bitmap;
            }
        }

        private void button_kamera_Click(object sender, EventArgs e)
        {
            //Kameranın Başlatılması

            try
            {
                capture = new Capture(comboBox1.SelectedIndex);
                capture.SetCaptureProperty(CapProp.FrameHeight, 480);
                capture.SetCaptureProperty(CapProp.FrameWidth, 640);

            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            } 
            if (captureInProgress)
            {

                Application.Idle -= ProcessFrame;
            }
            else
            {

                Application.Idle += ProcessFrame;
            }
            captureInProgress = !captureInProgress;

        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            try
            {
                Mat ImageFrame = capture.QueryFrame(); //line 1

                grayImage = new Image<Gray, byte>(ImageFrame.Bitmap);

                //ımageBox2.Image = ImageFrame;  //line 2
                //pictureBox1.Image = ImageFrame.Bitmap;
                if(!kontrol)
                    { 
                for (int i = 0; i < adresler.Count(); i++)
                {
                    Mat dosya_resim = CvInvoke.Imread(adresler[i], LoadImageType.Grayscale);
                    ımageBox2.Image =DrawMatches.Draw(dosya_resim, ImageFrame, out a1, out eslesti);

                    if (eslesti)
                    {
                        label1.Text =  adresler[i].ToString();
                        
                            //System.Threading.Thread.Sleep(5000);
                      
                    }
                   
                }
                }
                //ımageBox2.Image = DrawMatches.Draw(modelImage, ImageFrame, out a1);


                // ImageFrame.Dispose();
                // grayImage.Dispose();
                //ImageFrame.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void button_yenile_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            foreach (var item in _SystemCamereas)
            {
                comboBox1.Items.Add(item.Name);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            descriptor_cikart DES_SAYFA = new descriptor_cikart();
            DES_SAYFA.Show();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (FileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                     adresler =File.ReadAllLines(dlg.FileName);
                    
                }
            }
        }
    }
}
