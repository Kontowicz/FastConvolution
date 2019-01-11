using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Po 15 w tej sali.
// Zapisywanie obrazków 
//  Dzielenie 
// 8.00 - 16.40, fozina z wyjątkiem  9.30 - 12. 

namespace conv
{
    class classic
    {
        private float[,] image;
        private int size;
        public long time;
        public classic(int size)
        {
            this.size = size;
            this.image = new float[size, size];
            fill();
        }

        private void fill()
        {

            for (int i = 0; i < 1024; i++)
            {
                var imod = i % (128 * 2);

                for (int j = 0; j < 1024; j++)
                {
                    var jmod = j % (128 * 2);

                    if (imod < 128 && jmod < 128)
                        image[i,j] = 0;
                    else if (imod >= 128 && jmod >= 128)
                        image[i,j] = 0;
                    else
                        image[i,j] = 1;
                }
            }
        }

        private void naiveConv()
        {
            float up = 0;
            float down = 0;
            float left = 0;
            float right = 0;
            float center = 0;

            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    up = 0;
                    down = 0;
                    left = 0;
                    right = 0;
                    center = 0;

                    if (i - 1 >= 0 && i - 1 < this.size && j >= 0 && j < this.size)
                        up = image[i - 1, j] * 0.1f;

                    if (i + 1 >= 0 && i + 1 < this.size && j >= 0 && j < this.size)
                        down = image[i + 1, j] * 0.1f;

                    if (i >= 0 && i < this.size && j - 1 >= 0 && j - 1 < this.size)
                        left = image[i, j - 1] * 0.1f;

                    if (i >= 0 && i < this.size && j + 1 >= 0 && j + 1 < this.size)
                        right = image[i, j + 1] * 0.1f;

                    if (i >= 0 && i < this.size && j >= 0 && j < this.size)
                        center = image[i, j] * 0.6f;

                    image[i, j] = up + down + left + right + center;
                }
            }
        }
 
        public void test()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            for (int i = 0; i < 200; i++)
                naiveConv();

            stop.Stop();
            time = (long)stop.ElapsedMilliseconds;
        }

        public void save(string path)
        {
            var toSave = new Bitmap(1024, 1024);

            for (int i = 0; i < 1024; i++)
            {
                for (int j = 0; j < 1024; j++)
                {
                    var rgb = (int)(image[i,j] * 255);
                    var color = Color.FromArgb(rgb, rgb, rgb);

                    toSave.SetPixel(i, j, color);
                }
            }

            toSave.Save(path);
        }
    }
}

