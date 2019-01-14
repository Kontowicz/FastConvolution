using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    class basic
    {
        Bitmap bitmap;
        private float[][] image;
        private float[][] destination;

        private float[][] image1;
        private float[][] image2;
        private float[][] image3;
        private float[][] image4;

        private float[][] imageD1;
        private float[][] imageD2;
        private float[][] imageD3;
        private float[][] imageD4;


        public basic(string path)
        {
            bitmap = new Bitmap(path);
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipXY);
            image = new float[bitmap.Height][];
            for (int i = 0; i < bitmap.Height; i++)
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
            int w = image.Length / 2;
            image1 = new float[w][];
            image2 = new float[w][];
            image3 = new float[w][];
            image4 = new float[w][];

            imageD1 = new float[w][];
            imageD2 = new float[w][];
            imageD3 = new float[w][];
            imageD4 = new float[w][];

            int h = image[0].Length / 2; 
            for (int i = 0; i < w; ++i)
            {
                image1[i] = new float[h];
                image2[i] = new float[h];
                image3[i] = new float[h];
                image4[i] = new float[h];

                imageD1[i] = new float[h];
                imageD2[i] = new float[h];
                imageD3[i] = new float[h];
                imageD4[i] = new float[h];
            }

            for (int i = 0; i < bitmap.Height / 2; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image1[i][j] = image[i][j];
                }
            }

            for (int i = 0; i < bitmap.Height / 2; ++i)
            {
                for (int j = bitmap.Width / 2; j < bitmap.Width; ++j)
                {
                    image3[i][j - bitmap.Width / 2] = image[i][j]; //lewy dolny
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image2[i - bitmap.Height / 2][j] = image[i][j];// ;
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

        #region image1
        private void proces1Left(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image1[0].Length - 1; i++)
                        {
                            imageD1[image1.Length - 1][i] = (image1[image1.Length - 1][i] * (float)0.6) + ((image1[image1.Length - 1][i + 1] + image1[image1.Length - 1][i - 1] + image1[image1.Length - 2][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image1[0].Length - 1; i++)
                        {
                            image1[image1.Length - 1][i] = (imageD1[imageD1.Length - 1][i] * (float)0.6) + ((imageD1[imageD1.Length - 1][i + 1] + imageD1[imageD1.Length - 1][i - 1] + imageD1[imageD1.Length - 2][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces1Right(int caseType)
        {
            switch(caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image1[0].Length - 1; i++)
                        {
                            imageD1[0][i] = (image1[0][i] * (float)0.6) + ((image1[0][i + 1] + image1[0][i - 1] + image1[1][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image1[0].Length - 1; i++)
                        {
                            image1[0][i] = (imageD1[0][i] * (float)0.6) + ((imageD1[0][i + 1] + imageD1[0][i - 1] + imageD1[1][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces1Top(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            imageD1[i][0] = (image1[i][0] * (float)0.6) + ((image1[i + 1][0] + image1[i - 1][0] + image1[i][1]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            image1[i][0] = (imageD1[i][0] * (float)0.6) + ((imageD1[i + 1][0] + imageD1[i - 1][0] + imageD1[i][1]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces1Bottom(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            imageD1[i][image1[0].Length - 1] = (image1[i][image1[0].Length - 1] * (float)0.6) + ((image1[i + 1][image1[0].Length - 1] + image1[i - 1][image1[0].Length - 1] + image1[i][image1[0].Length - 2]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            image1[i][image1[0].Length - 1] = (imageD1[i][imageD1[0].Length - 1] * (float)0.6) + ((imageD1[i + 1][imageD1[0].Length - 1] + imageD1[i - 1][imageD1[0].Length - 1] + imageD1[i][imageD1[0].Length - 2]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces1Corners(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        imageD1[0][0] = (image1[0][0] * (float)0.6) + ((image1[1][0] + image1[0][1]) * (float)0.1);

                        imageD1[0][image1[0].Length - 1] = (image1[0][image1[0].Length - 1] * (float)0.6) + ((image1[1][image1[0].Length - 1] + image1[0][image1[0].Length - 2]) * (float)0.1);

                        imageD1[image1.Length - 1][0] = (image1[image1.Length - 1][0] * (float)0.6) + ((image1[image1.Length - 2][0] + image1[image1.Length - 1][1]) * (float)0.1);

                        imageD1[image1.Length - 1][image1[0].Length - 1] = (image1[image1.Length - 1][image1[0].Length - 1] * (float)0.6) + ((image1[image1.Length - 2][image1[0].Length - 1] + image1[image1.Length - 1][image1[0].Length - 2]) * (float)0.1);
                        break;
                    }
                case 2:
                    {
                        image1[0][0] = (imageD1[0][0] * (float)0.6) + ((imageD1[1][0] + imageD1[0][1]) * (float)0.1);

                        image1[0][image1[0].Length - 1] = (imageD1[0][imageD1[0].Length - 1] * (float)0.6) + ((imageD1[1][imageD1[0].Length - 1] + imageD1[0][imageD1[0].Length - 2]) * (float)0.1);

                        image1[image1.Length - 1][0] = (imageD1[imageD1.Length - 1][0] * (float)0.6) + ((imageD1[imageD1.Length - 2][0] + imageD1[imageD1.Length - 1][1]) * (float)0.1);

                        image1[image1.Length - 1][image1[0].Length - 1] = (imageD1[imageD1.Length - 1][imageD1[0].Length - 1] * (float)0.6) + ((imageD1[imageD1.Length - 2][imageD1[0].Length - 1] + imageD1[imageD1.Length - 1][imageD1[0].Length - 2]) * (float)0.1);
                        break;
                    }
            }

        }

        private void proces1Inside(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image1.Length - 1; i++)
                        {
                            for (int j = 1; j < image1[0].Length - 1; j++)
                            {
                                imageD1[i][j] = (image1[i][j] * (float)0.6) + ((image1[i + 1][j] + image1[i - 1][j] + image1[i][j + 1] + image1[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image1.Length - 1; i++)
                        {
                            for (int j = 1; j < image1[0].Length - 1; j++)
                            {
                                image1[i][j] = (imageD1[i][j] * (float)0.6) + ((imageD1[i + 1][j] + imageD1[i - 1][j] + imageD1[i][j + 1] + imageD1[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
            }

        }

        private  void procesImage1(int caseType)
        {
            proces1Left(caseType);
            proces1Right(caseType);
            proces1Top(caseType);
            proces1Bottom(caseType);
            proces1Corners(caseType);
            proces1Inside(caseType);
        }
        #endregion

        #region image2
        private void proces2Left(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image2[0].Length - 1; i++)
                        {
                            imageD2[image2.Length - 1][i] = (image2[image2.Length - 1][i] * (float)0.6) + ((image2[image2.Length - 1][i + 1] + image2[image2.Length - 1][i - 1] + image2[image2.Length - 2][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image2[0].Length - 1; i++)
                        {
                            image2[image2.Length - 1][i] = (imageD2[imageD2.Length - 1][i] * (float)0.6) + ((imageD2[imageD2.Length - 1][i + 1] + imageD2[imageD2.Length - 1][i - 1] + imageD2[imageD2.Length - 2][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces2Right(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image2[0].Length - 1; i++)
                        {
                            imageD2[0][i] = (image2[0][i] * (float)0.6) + ((image2[0][i + 1] + image2[0][i - 1] + image2[1][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image2[0].Length - 1; i++)
                        {
                            image2[0][i] = (imageD2[0][i] * (float)0.6) + ((imageD2[0][i + 1] + imageD2[0][i - 1] + imageD2[1][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces2Top(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            imageD2[i][0] = (image2[i][0] * (float)0.6) + ((image2[i + 1][0] + image2[i - 1][0] + image2[i][1]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            image2[i][0] = (imageD2[i][0] * (float)0.6) + ((imageD2[i + 1][0] + imageD2[i - 1][0] + imageD2[i][1]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces2Bottom(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            imageD2[i][image2[0].Length - 1] = (image2[i][image2[0].Length - 1] * (float)0.6) + ((image2[i + 1][image2[0].Length - 1] + image2[i - 1][image2[0].Length - 1] + image2[i][image2[0].Length - 2]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            image2[i][image2[0].Length - 1] = (imageD2[i][imageD2[0].Length - 1] * (float)0.6) + ((imageD2[i + 1][imageD2[0].Length - 1] + imageD2[i - 1][imageD2[0].Length - 1] + imageD2[i][imageD2[0].Length - 2]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces2Corners(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        imageD2[0][0] = (image2[0][0] * (float)0.6) + ((image2[1][0] + image2[0][1]) * (float)0.1);

                        imageD2[0][image2[0].Length - 1] = (image2[0][image2[0].Length - 1] * (float)0.6) + ((image2[1][image2[0].Length - 1] + image2[0][image2[0].Length - 2]) * (float)0.1);

                        imageD2[image2.Length - 1][0] = (image2[image2.Length - 1][0] * (float)0.6) + ((image2[image2.Length - 2][0] + image2[image2.Length - 1][1]) * (float)0.1);

                        imageD2[image2.Length - 1][image2[0].Length - 1] = (image2[image2.Length - 1][image2[0].Length - 1] * (float)0.6) + ((image2[image2.Length - 2][image2[0].Length - 1] + image2[image2.Length - 1][image2[0].Length - 2]) * (float)0.1);
                        break;
                    }
                case 2:
                    {
                        image2[0][0] = (imageD2[0][0] * (float)0.6) + ((imageD2[1][0] + imageD2[0][1]) * (float)0.1);

                        image2[0][image2[0].Length - 1] = (imageD2[0][imageD2[0].Length - 1] * (float)0.6) + ((imageD2[1][imageD2[0].Length - 1] + imageD2[0][imageD2[0].Length - 2]) * (float)0.1);

                        image2[image2.Length - 1][0] = (imageD2[imageD2.Length - 1][0] * (float)0.6) + ((imageD2[imageD2.Length - 2][0] + imageD2[imageD2.Length - 1][1]) * (float)0.1);

                        image2[image2.Length - 1][image2[0].Length - 1] = (imageD2[imageD2.Length - 1][imageD2[0].Length - 1] * (float)0.6) + ((imageD2[imageD2.Length - 2][imageD2[0].Length - 1] + imageD2[imageD2.Length - 1][imageD2[0].Length - 2]) * (float)0.1);
                        break;
                    }
            }

        }

        private void proces2Inside(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image2.Length - 1; i++)
                        {
                            for (int j = 1; j < image2[0].Length - 1; j++)
                            {
                                imageD2[i][j] = (image2[i][j] * (float)0.6) + ((image2[i + 1][j] + image2[i - 1][j] + image2[i][j + 1] + image2[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image2.Length - 1; i++)
                        {
                            for (int j = 1; j < image2[0].Length - 1; j++)
                            {
                                image2[i][j] = (imageD2[i][j] * (float)0.6) + ((imageD2[i + 1][j] + imageD2[i - 1][j] + imageD2[i][j + 1] + imageD2[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
            }

        }
        private void procesImage2(int caseType)
        {
            proces2Left(caseType);
            proces2Right(caseType);
            proces2Top(caseType);
            proces2Bottom(caseType);
            proces2Corners(caseType);
            proces2Inside(caseType);
        }
        #endregion

        #region image3
        private void proces3Left(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3[0].Length - 1; i++)
                        {
                            imageD3[image3.Length - 1][i] = (image3[image3.Length - 1][i] * (float)0.6) + ((image3[image3.Length - 1][i + 1] + image3[image3.Length - 1][i - 1] + image3[image3.Length - 2][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3[0].Length - 1; i++)
                        {
                            image3[image3.Length - 1][i] = (imageD3[imageD3.Length - 1][i] * (float)0.6) + ((imageD3[imageD3.Length - 1][i + 1] + imageD3[imageD3.Length - 1][i - 1] + imageD3[imageD3.Length - 2][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces3Right(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3[0].Length - 1; i++)
                        {
                            imageD3[0][i] = (image3[0][i] * (float)0.6) + ((image3[0][i + 1] + image3[0][i - 1] + image3[1][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3[0].Length - 1; i++)
                        {
                            image3[0][i] = (imageD3[0][i] * (float)0.6) + ((imageD3[0][i + 1] + imageD3[0][i - 1] + imageD3[1][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces3Top(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            imageD3[i][0] = (image3[i][0] * (float)0.6) + ((image3[i + 1][0] + image3[i - 1][0] + image3[i][1]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            image3[i][0] = (imageD3[i][0] * (float)0.6) + ((imageD3[i + 1][0] + imageD3[i - 1][0] + imageD3[i][1]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces3Bottom(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            imageD3[i][image3[0].Length - 1] = (image3[i][image3[0].Length - 1] * (float)0.6) + ((image3[i + 1][image3[0].Length - 1] + image3[i - 1][image3[0].Length - 1] + image3[i][image3[0].Length - 2]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            image3[i][image3[0].Length - 1] = (imageD3[i][imageD3[0].Length - 1] * (float)0.6) + ((imageD3[i + 1][imageD3[0].Length - 1] + imageD3[i - 1][imageD3[0].Length - 1] + imageD3[i][imageD3[0].Length - 2]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces3Corners(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        imageD3[0][0] = (image3[0][0] * (float)0.6) + ((image3[1][0] + image3[0][1]) * (float)0.1);

                        imageD3[0][image3[0].Length - 1] = (image3[0][image3[0].Length - 1] * (float)0.6) + ((image3[1][image3[0].Length - 1] + image3[0][image3[0].Length - 2]) * (float)0.1);

                        imageD3[image3.Length - 1][0] = (image3[image3.Length - 1][0] * (float)0.6) + ((image3[image3.Length - 2][0] + image3[image3.Length - 1][1]) * (float)0.1);

                        imageD3[image3.Length - 1][image3[0].Length - 1] = (image3[image3.Length - 1][image3[0].Length - 1] * (float)0.6) + ((image3[image3.Length - 2][image3[0].Length - 1] + image3[image3.Length - 1][image3[0].Length - 2]) * (float)0.1);
                        break;
                    }
                case 2:
                    {
                        image3[0][0] = (imageD3[0][0] * (float)0.6) + ((imageD3[1][0] + imageD3[0][1]) * (float)0.1);

                        image3[0][image3[0].Length - 1] = (imageD3[0][imageD3[0].Length - 1] * (float)0.6) + ((imageD3[1][imageD3[0].Length - 1] + imageD3[0][imageD3[0].Length - 2]) * (float)0.1);

                        image3[image3.Length - 1][0] = (imageD3[imageD3.Length - 1][0] * (float)0.6) + ((imageD3[imageD3.Length - 2][0] + imageD3[imageD3.Length - 1][1]) * (float)0.1);

                        image3[image3.Length - 1][image3[0].Length - 1] = (imageD3[imageD3.Length - 1][imageD3[0].Length - 1] * (float)0.6) + ((imageD3[imageD3.Length - 2][imageD3[0].Length - 1] + imageD3[imageD3.Length - 1][imageD3[0].Length - 2]) * (float)0.1);
                        break;
                    }
            }

        }

        private void proces3Inside(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            for (int j = 1; j < image3[0].Length - 1; j++)
                            {
                                imageD3[i][j] = (image3[i][j] * (float)0.6) + ((image3[i + 1][j] + image3[i - 1][j] + image3[i][j + 1] + image3[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            for (int j = 1; j < image3[0].Length - 1; j++)
                            {
                                image3[i][j] = (imageD3[i][j] * (float)0.6) + ((imageD3[i + 1][j] + imageD3[i - 1][j] + imageD3[i][j + 1] + imageD3[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
            }

        }
        private void procesImage3(int caseType)
        {
            proces3Left(caseType);
            proces3Right(caseType);
            proces3Top(caseType);
            proces3Bottom(caseType);
            proces3Corners(caseType);
            proces3Inside(caseType);

        }
        #endregion

        #region image4
        private void proces4Left(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image4[0].Length - 1; i++)
                        {
                            imageD4[image4.Length - 1][i] = (image4[image4.Length - 1][i] * (float)0.6) + ((image4[image4.Length - 1][i + 1] + image4[image4.Length - 1][i - 1] + image4[image4.Length - 2][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image4[0].Length - 1; i++)
                        {
                            image4[image4.Length - 1][i] = (imageD4[imageD4.Length - 1][i] * (float)0.6) + ((imageD4[imageD4.Length - 1][i + 1] + imageD4[imageD4.Length - 1][i - 1] + imageD4[imageD4.Length - 2][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces4Right(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image4[0].Length - 1; i++)
                        {
                            imageD4[0][i] = (image4[0][i] * (float)0.6) + ((image4[0][i + 1] + image4[0][i - 1] + image4[1][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image4[0].Length - 1; i++)
                        {
                            image4[0][i] = (imageD4[0][i] * (float)0.6) + ((imageD4[0][i + 1] + imageD4[0][i - 1] + imageD4[1][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces4Top(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image4.Length - 1; i++)
                        {
                            imageD4[i][0] = (image4[i][0] * (float)0.6) + ((image4[i + 1][0] + image4[i - 1][0] + image4[i][1]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image4.Length - 1; i++)
                        {
                            image4[i][0] = (imageD4[i][0] * (float)0.6) + ((imageD4[i + 1][0] + imageD4[i - 1][0] + imageD4[i][1]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces4Bottom(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image4.Length - 1; i++)
                        {
                            imageD4[i][image4[0].Length - 1] = (image4[i][image4[0].Length - 1] * (float)0.6) + ((image4[i + 1][image4[0].Length - 1] + image4[i - 1][image4[0].Length - 1] + image4[i][image4[0].Length - 2]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image4.Length - 1; i++)
                        {
                            image4[i][image4[0].Length - 1] = (imageD4[i][imageD4[0].Length - 1] * (float)0.6) + ((imageD4[i + 1][imageD4[0].Length - 1] + imageD4[i - 1][imageD4[0].Length - 1] + imageD4[i][imageD4[0].Length - 2]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces4Corners(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        imageD4[0][0] = (image4[0][0] * (float)0.6) + ((image4[1][0] + image4[0][1]) * (float)0.1);

                        imageD4[0][image4[0].Length - 1] = (image4[0][image4[0].Length - 1] * (float)0.6) + ((image4[1][image4[0].Length - 1] + image4[0][image4[0].Length - 2]) * (float)0.1);

                        imageD4[image4.Length - 1][0] = (image4[image4.Length - 1][0] * (float)0.6) + ((image4[image4.Length - 2][0] + image4[image4.Length - 1][1]) * (float)0.1);

                        imageD4[image4.Length - 1][image4[0].Length - 1] = (image4[image4.Length - 1][image4[0].Length - 1] * (float)0.6) + ((image4[image4.Length - 2][image4[0].Length - 1] + image4[image4.Length - 1][image4[0].Length - 2]) * (float)0.1);
                        break;
                    }
                case 2:
                    {
                        image4[0][0] = (imageD4[0][0] * (float)0.6) + ((imageD4[1][0] + imageD4[0][1]) * (float)0.1);

                        image4[0][image4[0].Length - 1] = (imageD4[0][imageD4[0].Length - 1] * (float)0.6) + ((imageD4[1][imageD4[0].Length - 1] + imageD4[0][imageD4[0].Length - 2]) * (float)0.1);

                        image4[image4.Length - 1][0] = (imageD4[imageD4.Length - 1][0] * (float)0.6) + ((imageD4[imageD4.Length - 2][0] + imageD4[imageD4.Length - 1][1]) * (float)0.1);

                        image4[image4.Length - 1][image4[0].Length - 1] = (imageD4[imageD4.Length - 1][imageD4[0].Length - 1] * (float)0.6) + ((imageD4[imageD4.Length - 2][imageD4[0].Length - 1] + imageD4[imageD4.Length - 1][imageD4[0].Length - 2]) * (float)0.1);
                        break;
                    }
            }

        }

        private void proces4Inside(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image4.Length - 1; i++)
                        {
                            for (int j = 1; j < image4[0].Length - 1; j++)
                            {
                                imageD4[i][j] = (image4[i][j] * (float)0.6) + ((image4[i + 1][j] + image4[i - 1][j] + image4[i][j + 1] + image4[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image4.Length - 1; i++)
                        {
                            for (int j = 1; j < image4[0].Length - 1; j++)
                            {
                                image4[i][j] = (imageD4[i][j] * (float)0.6) + ((imageD4[i + 1][j] + imageD4[i - 1][j] + imageD4[i][j + 1] + imageD4[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
            }

        }
        private void procesImage4(int caseType)
        {
            proces4Left(caseType);
            proces4Right(caseType);
            proces4Top(caseType);
            proces4Bottom(caseType);
            proces4Corners(caseType);
            proces4Inside(caseType);

        }
        #endregion

        private void connect()
        {
            for (int i = 0; i < bitmap.Height / 2; ++i)
            {
                for (int j = 0; j < bitmap.Width / 2; ++j)
                {
                    image[i][j] = image1[i][j]; //lewy górny
                }
            }

            for (int i = 0; i < bitmap.Height / 2; ++i)
            {
                for (int j = bitmap.Width / 2; j < bitmap.Width; ++j)
                {
                    image[i][j] = image3[i][j - bitmap.Width / 2]; //lewy dolny
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width/2; ++j)
                {
                    image[i][j] = image2[i - bitmap.Height / 2][j]; // prawy górny
                }
            }

            for (int i = bitmap.Height / 2; i < bitmap.Height; ++i)
            {
                for (int j = bitmap.Width / 2; j < bitmap.Width; ++j)
                {
                    image[i][j] =  image4[i - bitmap.Height / 2][j - bitmap.Width / 2]; //prawy dolny
                }
            }
        }
        public void repair(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 0; i < image1[0].Length; i++)
                        {
                            imageD1[image1.Length - 1][i] += image2[0][i] * (float)0.1; //pionowo ok 100%
                        }
                        for (int i = 0; i < image1.Length; i++)
                        {
                            imageD1[i][image1[i].Length - 1] += image3[i][0] * (float)0.1; // 0; // dół ok 100%
                        }

                        for (int i = 0; i < image2.Length; i++)
                        {
                            imageD2[i][image2[i].Length - 1] += image4[i][0] * (float)0.1;// 0; // dół ok 100%
                        }
                        for (int i = 0; i < image2[0].Length; i++)
                        {
                            imageD2[0][i] += image1[image1.Length - 1][i] * (float)0.1;// 0; //lewo ok 100 %
                        }

                        for (int i = 0; i < image3.Length; i++)
                        {
                            imageD3[i][0] += image1[i][image1[i].Length - 1] * (float)0.1;//0; // góra
                        }
                        for (int i = 0; i < image3[0].Length; i++)
                        {
                            imageD3[image3.Length - 1][i] += image4[0][i] * (float)0.1;// 0; //prawo
                        }

                        for (int i = 0; i < image4.Length; i++)
                        {
                            imageD4[i][0] += image2[i][image2[i].Length - 1] * (float)0.1; // 0; // góra
                        }

                        for (int i = 0; i < image4[0].Length; i++)
                        {
                            imageD4[0][i] += image3[image3.Length - 1][i] * (float)0.1;// 0; //lewo
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < image1[0].Length; i++)
                        {
                            image1[image1.Length - 1][i] += imageD2[0][i]*(float)0.1; //pionowo ok 100%
                        }
                        for (int i = 0; i < image1.Length; i++)
                        {
                            image1[i][image1[i].Length - 1] += imageD3[i][0] * (float)0.1; // 0; // dół ok 100%
                        }

                        for (int i = 0; i < image2.Length; i++)
                        {
                            image2[i][image2[i].Length - 1] += imageD4[i][0] * (float)0.1;// 0; // dół ok 100%
                        }
                        for (int i = 0; i < image2[0].Length; i++)
                        {
                            //image2[0][i] += imageD3[image3.Length - 1][i] * (float)0.1;// 0; //lewo ok 100 %
                            image2[0][i] += imageD1[image1.Length - 1][i] * (float)0.1;// 0; //lewo ok 100 %
                        }

                        for (int i = 0; i < image3.Length; i++)
                        {
                            image3[i][0] += imageD1[i][image1[i].Length - 1] * (float)0.1;//0; // góra
                        }
                        for (int i = 0; i < image3[0].Length; i++)
                        {
                            image3[image3.Length - 1][i] += imageD4[0][i] * (float)0.1;// 0; //prawo
                        }

                        for (int i = 0; i < image4.Length; i++)
                        {
                            image4[i][0] += imageD2[i][image2[i].Length - 1] * (float)0.1; // 0; // góra
                        }

                        for (int i = 0; i < image4[0].Length; i++)
                        {
                            image4[0][i] += imageD3[image3.Length - 1][i] * (float)0.1;// 0; //lewo
                        }

                        break;
                    }
            }
        }
        public  void convolution()
        {
            
            procesImage1(1);
            procesImage2(1);
            procesImage3(1);
            procesImage4(1);
            repair(1);
            //repair(2);
            
            procesImage1(2);
            procesImage2(2);
            procesImage3(2);
            procesImage4(2);
            repair(2);
        }

        public void test()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            divide();
            for (int i = 0; i < 100; i++)
            {
                convolution();
            }
            connect();
            stop.Stop();
            System.Console.WriteLine(stop.Elapsed);
        }
        public void saveParts()
        {
            var toSave = new Bitmap(image4.Length, image4[0].Length);
            int a = image4.Length - 1;
            int b = 0;

            for (int i = 0; i < toSave.Width; i++)
            {
                for (int j = 0; j < toSave.Height; j++)
                {
                    var rgb = (int)(image4[i][j] * 255);
                    var color = Color.FromArgb(rgb, rgb, rgb);

                    toSave.SetPixel(i, j, color);
                }
            }
            toSave.Save("C:/Users/Piotr/Desktop/test/new.png");

            //toSave = new Bitmap(image2.Length, image2[0].Length);
            //a = image2.Length - 1;
            //b = 0;

            //for (int i = 0; i < toSave.Width; i++)
            //{
            //    for (int j = 0; j < toSave.Height; j++)
            //    {
            //        var rgb = (int)(image2[i][j] * 255);
            //        var color = Color.FromArgb(rgb, rgb, rgb);

            //        toSave.SetPixel(i, j, color);
            //    }
            //}
            //toSave.Save("C:/Users/Piotr/Desktop/parts/2.png");

            //toSave = new Bitmap(image3.Length, image3[0].Length);
            //a = image3.Length - 1;
            //b = 0;

            //for (int i = 0; i < toSave.Width; i++)
            //{
            //    for (int j = 0; j < toSave.Height; j++)
            //    {
            //        var rgb = (int)(image3[i][j] * 255);
            //        var color = Color.FromArgb(rgb, rgb, rgb);

            //        toSave.SetPixel(i, j, color);
            //    }
            //}
            //toSave.Save("C:/Users/Piotr/Desktop/parts/3.png");

            //toSave = new Bitmap(image4.Length, image4[0].Length);
            //a = image4.Length - 1;
            //b = 0;

            //for (int i = 0; i < toSave.Width; i++)
            //{
            //    for (int j = 0; j < toSave.Height; j++)
            //    {
            //        var rgb = (int)(image4[i][j] * 255);
            //        var color = Color.FromArgb(rgb, rgb, rgb);

            //        toSave.SetPixel(i, j, color);
            //    }
            //}
            //toSave.Save("C:/Users/Piotr/Desktop/parts/4.png");
        }
        public void save(string path)
        {
            var toSave = new Bitmap(image.Length, image[0].Length);
            int a = image.Length - 1;
            int b = 0;

            for (int i = 0; i < toSave.Width; i++)
            {
                for (int j = 0; j < toSave.Height; j++)
                {
                    var rgb = (int)(image[i][j] * 255);
                    var color = Color.FromArgb(rgb, rgb, rgb);

                    toSave.SetPixel(i, j, color);
                }

            }
            toSave.Save(path);
        }

    }
}
