using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    class basicImprove
    {
        Bitmap bitmap;
        private static float[][] image;
        private static float[][] destination;
        private static float[][] image1;
        private static float[][] image2;
        private static float[][] image3;
        private static float[][] image4;

        private static float[][] imageD1;
        private static float[][] imageD2;
        private static float[][] imageD3;
        private static float[][] imageD4;


        public basicImprove(string path)
        {
            bitmap = new Bitmap(path);

            image = new float[bitmap.Height][];
            for(int i = 0; i < bitmap.Height; i++)
            {
                image[i] = new float[bitmap.Width];
            }

            //for (int i = 0; i < 1024; i++)
            //{
            //    var imod = i % (128 * 2);

            //    for (int j = 0; j < 1024; j++)
            //    {
            //        var jmod = j % (128 * 2);

            //        if (imod < 128 && jmod < 128)
            //            image[i][j] = 0;
            //        else if (imod >= 128 && jmod >= 128)
            //            image[i][j] = 0;
            //        else
            //            image[i][j] = 1;
            //    }
            //}

            destination = new float[bitmap.Height][];
            for (int i = 0; i < bitmap.Height; i++)
            {
                destination[i] = new float[bitmap.Width];
            }

            int a = bitmap.Height - 1;
            int b = 0;
            for (int i = 0; i < bitmap.Height; i++)
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
                    image[a][b] = (float)avg;
                    b += 1;
                }
                a -= 1;
                b = 0;
            }

        }



        private void divide()
        {
            image1 = new float[512][];
            image2 = new float[512][];
            image3 = new float[512][];
            image4 = new float[512][];

            imageD1 = new float[512][];
            imageD2 = new float[512][];
            imageD3 = new float[512][];
            imageD4 = new float[512][];

            for (int i = 0; i < 512; ++i)
            {
                image1[i] = new float[512];
                image2[i] = new float[512];
                image3[i] = new float[512];
                image4[i] = new float[512];

                imageD1[i] = new float[512];
                imageD2[i] = new float[512];
                imageD3[i] = new float[512];
                imageD4[i] = new float[512];
            }

            for (int i = 0; i < bitmap.Height / 2; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image1[i][j] = image[i][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image2[i - bitmap.Height / 2][j] = image[i][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image3[i - bitmap.Height / 2][j] = image[i][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = bitmap.Width / 2; j < bitmap.Width; ++j)
                {
                    image4[i - bitmap.Height / 2][j - bitmap.Width / 2] = image[i][j];
                }
            }
        }

        private static Task procesImage1()
        {
            return Task.Run(() =>
            {
                float up = 0;
                float down = 0;
                float left = 0;
                float right = 0;
                float center = 0;

                for (int i = 0; i < 512; i++)
                {
                    for (int j = 0; j < 512; j++)
                    {
                        up = 0;
                        down = 0;
                        left = 0;
                        right = 0;
                        center = 0;

                        if (i - 1 >= 0 && i - 1 < 512 && j >= 0 && j < 512)
                            up = image1[i - 1][j] * 0.1f;

                        if (i + 1 >= 0 && i + 1 < 512 && j >= 0 && j < 512)
                            down = image1[i + 1][j] * 0.1f;

                        if (i >= 0 && i < 512 && j - 1 >= 0 && j - 1 < 512)
                            left = image1[i][j - 1] * 0.1f;

                        if (i >= 0 && i < 512 && j + 1 >= 0 && j + 1 < 512)
                            right = image1[i][j + 1] * 0.1f;

                        if (i >= 0 && i < 512 && j >= 0 && j < 512)
                            center = image1[i][j] * 0.6f;

                        image1[i][j] = up + down + left + right + center;
                    }
                }
            });
        }

        private static Task procesImage2()
        {
            return Task.Run(() =>
            {
                float up = 0;
                float down = 0;
                float left = 0;
                float right = 0;
                float center = 0;

                for (int i = 0; i < 512; i++)
                {
                    for (int j = 0; j < 512; j++)
                    {
                        up = 0;
                        down = 0;
                        left = 0;
                        right = 0;
                        center = 0;

                        if (i - 1 >= 0 && i - 1 < 512 && j >= 0 && j < 512)
                            up = image2[i - 1][j] * 0.1f;

                        if (i + 1 >= 0 && i + 1 < 512 && j >= 0 && j < 512)
                            down = image2[i + 1][j] * 0.1f;

                        if (i >= 0 && i < 512 && j - 1 >= 0 && j - 1 < 512)
                            left = image2[i][j - 1] * 0.1f;

                        if (i >= 0 && i < 512 && j + 1 >= 0 && j + 1 < 512)
                            right = image2[i][j + 1] * 0.1f;

                        if (i >= 0 && i < 512 && j >= 0 && j < 512)
                            center = image2[i][j] * 0.6f;

                        image2[i][j] = up + down + left + right + center;
                    }

                }
            });
        }

        private static Task procesImage3()
        {
            return Task.Run(() =>
            {
                float up = 0;
                float down = 0;
                float left = 0;
                float right = 0;
                float center = 0;

                for (int i = 0; i < 512; i++)
                {
                    for (int j = 0; j < 512; j++)
                    {
                        up = 0;
                        down = 0;
                        left = 0;
                        right = 0;
                        center = 0;

                        if (i - 1 >= 0 && i - 1 < 512 && j >= 0 && j < 512)
                            up = image3[i - 1][j] * 0.1f;

                        if (i + 1 >= 0 && i + 1 < 512 && j >= 0 && j < 512)
                            down = image3[i + 1][j] * 0.1f;

                        if (i >= 0 && i < 512 && j - 1 >= 0 && j - 1 < 512)
                            left = image3[i][j - 1] * 0.1f;

                        if (i >= 0 && i < 512 && j + 1 >= 0 && j + 1 < 512)
                            right = image3[i][j + 1] * 0.1f;

                        if (i >= 0 && i < 512 && j >= 0 && j < 512)
                            center = image3[i][j] * 0.6f;

                        image3[i][j] = up + down + left + right + center;
                    }
                }
            });
        }

        private static Task procesImage4()
        {
            return Task.Run(() =>
           {
               float up = 0;
               float down = 0;
               float left = 0;
               float right = 0;
               float center = 0;

               for (int i = 0; i < 512; i++)
               {
                   for (int j = 0; j < 512; j++)
                   {
                       up = 0;
                       down = 0;
                       left = 0;
                       right = 0;
                       center = 0;

                       if (i - 1 >= 0 && i - 1 < 512 && j >= 0 && j < 512)
                           up = image4[i - 1][j] * 0.1f;

                       if (i + 1 >= 0 && i + 1 < 512 && j >= 0 && j < 512)
                           down = image4[i + 1][j] * 0.1f;

                       if (i >= 0 && i < 512 && j - 1 >= 0 && j - 1 < 512)
                           left = image4[i][j - 1] * 0.1f;

                       if (i >= 0 && i < 512 && j + 1 >= 0 && j + 1 < 512)
                           right = image4[i][j + 1] * 0.1f;

                       if (i >= 0 && i < 512 && j >= 0 && j < 512)
                           center = image4[i][j] * 0.6f;

                       image4[i][j] = up + down + left + right + center;
                   }
               }
           });
        }

        public void connect()
        {
            for (int i = 0; i < bitmap.Height / 2; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image[i][j] = image1[i][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image[i][j] = image2[i - bitmap.Height / 2][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image[i][j] = image3[i - bitmap.Height / 2][j];
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = bitmap.Width / 2; j < bitmap.Width; ++j)
                {
                    image[i][j] = image4[i - bitmap.Height / 2][j - bitmap.Width / 2];
                }
            }
        }

        private static void repair()
        {
            for (int i = 0; i < 512; i++)
            {
                image1[i][511] += (image2[i][0] * (float)0.1); // pionowo 
                image2[i][0] += (image1[i][511] * (float)0.1); // pionowo 

                image1[511][i] += (image2[0][i] * (float)0.1); // srodek
                image2[0][i] += (image1[511][i] * (float)0.1); // srodek
            }
            //for (int i = 512; i < 1024; i++)
            //{
            //    image3[i - 512][511] += (image4[i - 512][0] * (float)0.1); //pionowo
            //    image4[i - 512][0] += (image3[i - 512][511] * (float)0.1); //pionowo
            //    image4[i - 512][0] += (image3[i - 512][511] * (float)0.1); //srodek
            //    image3[i - 512][511] += (image4[i - 512][0] * (float)0.1); //srodek
            //}
        }
        public static void convolution()
        {
            var p = procesImage1();
            var p1 = procesImage2();
            var p2 = procesImage3();
            var p3 = procesImage4();
            p.Wait();
            p1.Wait();
            p2.Wait();
            p3.Wait();
            repair();
        }

        public void test()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            divide();
            for (int i = 0; i < 200; i++)
            {
                convolution();
            }
            connect();
            stop.Stop();
            System.Console.WriteLine(stop.Elapsed);
        }

        public void save(string path)
        {
            var toSave = new Bitmap(1024, 1024);

            for (int i = 0; i < toSave.Width; i++)
            {
                for (int j = 0; j < toSave.Height; j++)
                {
                    var rgb = (int)(image[i][j] * 255);
                    var color = Color.FromArgb(rgb, rgb, rgb);

                    toSave.SetPixel(i, j, color);
                }
            }
            toSave.RotateFlip(RotateFlipType.Rotate90FlipXY);
            toSave.Save(path);
        }
    }
}
