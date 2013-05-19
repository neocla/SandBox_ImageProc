using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenCvSharp;


namespace DepthCompositionSample
{
    public partial class Form1 : Form
    {
        private Bitmap _image;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var filename = textBox1.Text;
            if (filename == null)
            {
                MessageBox.Show("ファイル名なし");
            }

            using (IplImage image = Cv.LoadImage(filename))
            using(IplImage filteredImage = image.Clone())
            using(IplImage differenceImage = Cv.CreateImage(image.Size, image.Depth, image.NChannels))
            using(IplImage normalizedImage = Cv.CreateImage(image.Size, image.Depth, image.NChannels))
            {

                ShowCvWindow(image);

                //using (IplImage halfImage = Cv.CreateImage(new CvSize(image.Width / 2, image.Height / 2), image.Depth, image.NChannels))
                //{
                //    Cv.PyrDown(image, halfImage, CvFilter.Gaussian5x5);
                //    Cv.PyrUp(halfImage, filteredImage);

                //}
                Cv.Smooth(image, filteredImage, SmoothType.Gaussian, 25, 25);
                ShowCvWindow(filteredImage);

                image.Sub(filteredImage, differenceImage);
                ShowCvWindow(differenceImage);

                differenceImage.Normalize(normalizedImage, 0, 255, NormType.MinMax);
                ShowCvWindow(normalizedImage);

                image.SaveImage("original.bmp");
                filteredImage.SaveImage("filterd.bmp");
                differenceImage.SaveImage("difference.bmp");
                normalizedImage.SaveImage("normalized.bmp");

                //Cv.ReleaseImage(image);
            }
        }

        private static void ShowCvWindow(IplImage image)
        {
            Cv.NamedWindow("window");
            Cv.ShowImage("window", image);
            Cv.WaitKey();
            Cv.DestroyWindow("window");
        }
    }
}
