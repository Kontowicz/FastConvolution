using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    class test
    {
        public Bitmap bitmap = null;
        private static float[][] image1;
        private static float[][] image2;
        private static float[][] image3;
        private static float[][] image4;
        public float[][] array;

        public test(string path)
        {
            bitmap = new Bitmap(path);

            array = new float[bitmap.Height][];
            for(int i = 0; i < bitmap.Height; i++)
            {
                array[i] = new float[bitmap.Width];
            }

            int a = bitmap.Height - 1;
            int b = 0;
            for (int i = 0; i < bitmap.Height ; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    var p = bitmap.GetPixel(j, i);
                    int[] tmp = new int[4];
                    tmp[0] = p.R;
                    tmp[1] = p.G;
                    tmp[2] = p.B;
                    tmp[3] = p.A;
                    int avg = (int)tmp.Average();
                    avg = avg > 128 ? 1 : 0;
                    array[a][b] = (float)avg;
                    b += 1;
                }
                a -= 1;
                b = 0;
            }
        }

        public void divide()
        {
            image1 = new float[512][];
            image2 = new float[512][];
            image3 = new float[512][];
            image4 = new float[512][];



            for (int i = 0; i < 512; ++i)
            {
                image1[i] = new float[512];
                image2[i] = new float[512];
                image3[i] = new float[512];
                image4[i] = new float[512];
            }

            for (int i = 0; i < bitmap.Height / 2; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image1[i][j] = array[i][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image2[i - bitmap.Height / 2][j] = array[i][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image3[i- bitmap.Height / 2][j ] = array[i][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = bitmap.Width / 2; j < bitmap.Width; ++j)
                {
                    image4[i - bitmap.Height / 2][j - bitmap.Width / 2] = array[i][j];
                }
            }
        }

        public void connect()
        {
            for (int i = 0; i < bitmap.Height / 2; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    array[i][j] =  image1[i][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    array[i][j] = image2[i - bitmap.Height / 2][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    array[i][j] = image3[i - bitmap.Height / 2][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = bitmap.Width / 2; j < bitmap.Width; ++j)
                {
                    array[i][j] = image4[i - bitmap.Height / 2][j - bitmap.Width / 2];
                }
            }
        }

        public void save(string path)
        {
            var toSave = new Bitmap(array.Length, array[0].Length);
            int a = array.Length - 1;
            int b = 0;
            
            for (int i = 0; i < toSave.Width; i++)
            {
                for (int j = 0; j < toSave.Height; j++)
                {
                    var rgb = (int)(array[i][j] * 255);
                    var color = Color.FromArgb(rgb, rgb, rgb);

                    toSave.SetPixel(i, j, color);
                }

            }
            toSave.RotateFlip(RotateFlipType.Rotate90FlipXY);

            toSave.Save(path);
        }
    }
}
