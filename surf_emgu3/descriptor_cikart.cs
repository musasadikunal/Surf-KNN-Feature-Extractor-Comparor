using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using SURF_DENEME;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace surf_emgu3
{
    public partial class descriptor_cikart : Form
    {
        OpenFileDialog dlg;

        public descriptor_cikart()
        {
            InitializeComponent();
        }
        private void button_resim_yukle_Click(object sender, EventArgs e)
        {
            using (dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";


                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    pictureBox1.ImageLocation = dlg.FileName;
                }
            }
        }

        private void button_des_cikart_Click(object sender, EventArgs e)
        {
            Mat modelImage = CvInvoke.Imread(dlg.FileName, LoadImageType.Grayscale);
            VectorOfKeyPoint features = DrawMatches.DESC_CIKAR(modelImage);

            var featur2 = features.ToArray();
            
            
            /*
            for (int i = 0; i < featur2.Length; i++)
            {
                featur2[i].(ass => ass.)
            }

            foreach (var item in featur2.)
            {

            }
                /*
            Image<Gray, byte> myImage = new Image<Gray, byte>(resim);
            pictureBox1.Image = myImage.ToBitmap();
            Matrix<float> veriler = surf.Descriptor_Cikart(myImage);

            foreach (var item in veriler.Data)
            {
                File.AppendAllText("data.txt", item.ToString() + '*');
            }
            //surf.Descriptor_Cikart.modelDescriptors.
            File.AppendAllText("data.txt", textBox1.Text.ToString() + Environment.NewLine);
            */
        }
        private void descriptor_cikart_Load(object sender, EventArgs e)
        {

        }
    }
}
