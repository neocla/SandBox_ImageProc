using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace SandBox_ImageProc
{
    class Program
    {
        static void Main(string[] args)
        {
            IplImage img = Cv.CreateImage(new CvSize(128, 128), BitDepth.U8, 1);

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Cv.Set2D(img, y, x, x + y);
                }
            }

            Cv.NamedWindow("window");
            Cv.ShowImage("window", img);
            Cv.WaitKey();
            Cv.DestroyWindow("window");

            Cv.ReleaseImage(img);
        }
    }
}
