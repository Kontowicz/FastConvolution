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

        private int imagePartLen;
        private int imagePart0Len;
        public basic(string path)
        {
            bitmap = new Bitmap(path);
            if(bitmap.Height % 2 != 0 || bitmap.Width % 2!= 0)
            {
                throw new Exception("Wrong image size");
            }
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipXY);
            image = new float[bitmap.Height][];
            for (int i = 0; i < bitmap.Height; i++)
            {
                image[i] = new float[bitmap.Width];
            }

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

            imagePartLen = (image.Length / 2) - 1;
            imagePart0Len  = (image[0].Length / 2) - 1;
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
        private void proces1LeftRight(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < imagePart0Len; ++i)
                        {
                            imageD1[imagePartLen][i] = (image1[imagePartLen][i] * (float)0.6) + ((image1[imagePartLen][i + 1] + image1[imagePartLen][i - 1] + image1[imagePartLen - 1][i]) * (float)0.1);
                            imageD1[0][i] = (image1[0][i] * (float)0.6) + ((image1[0][i + 1] + image1[0][i - 1] + image1[1][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < imagePart0Len ; ++i)
                        {
                            image1[imagePartLen][i] = (imageD1[imagePartLen][i] * (float)0.6) + ((imageD1[imagePartLen][i + 1] + imageD1[imagePartLen][i - 1] + imageD1[imagePartLen - 1][i]) * (float)0.1);
                            image1[0][i] = (imageD1[0][i] * (float)0.6) + ((imageD1[0][i + 1] + imageD1[0][i - 1] + imageD1[1][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces1TopBottom(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < imagePartLen; i++)
                        {
                            imageD1[i][0] = (image1[i][0] * (float)0.6) + ((image1[i + 1][0] + image1[i - 1][0] + image1[i][1]) * (float)0.1);
                            imageD1[i][imagePart0Len] = (image1[i][imagePart0Len] * (float)0.6) + ((image1[i + 1][imagePart0Len] + image1[i - 1][imagePart0Len] + image1[i][imagePart0Len - 1]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < imagePartLen; i++)
                        {
                            image1[i][0] = (imageD1[i][0] * (float)0.6) + ((imageD1[i + 1][0] + imageD1[i - 1][0] + imageD1[i][1]) * (float)0.1);
                            image1[i][imagePart0Len] = (imageD1[i][imagePart0Len] * (float)0.6) + ((imageD1[i + 1][imagePart0Len] + imageD1[i - 1][imagePart0Len] + imageD1[i][imagePart0Len - 1]) * (float)0.1);
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
                        imageD1[0][imagePart0Len ] = (image1[0][imagePart0Len ] * (float)0.6) + ((image1[1][imagePart0Len ] + image1[0][ imagePart0Len - 1]) * (float)0.1);
                        imageD1[imagePartLen][0] = (image1[imagePartLen][0] * (float)0.6) + ((image1[imagePartLen - 1][0] + image1[imagePartLen][1]) * (float)0.1);
                        imageD1[imagePartLen][imagePart0Len ] = (image1[imagePartLen][imagePart0Len ] * (float)0.6) + ((image1[imagePartLen - 1][imagePart0Len ] + image1[imagePartLen][ imagePart0Len - 1]) * (float)0.1);
                        break;
                    }
                case 2:
                    {
                        image1[0][0] = (imageD1[0][0] * (float)0.6) + ((imageD1[1][0] + imageD1[0][1]) * (float)0.1);
                        image1[0][image1[0].Length - 1] = (imageD1[0][ imagePart0Len ] * (float)0.6) + ((imageD1[1][ imagePart0Len ] + imageD1[0][ imagePart0Len - 1]) * (float)0.1);
                        image1[imagePartLen][0] = (imageD1[imagePartLen][0] * (float)0.6) + ((imageD1[imagePartLen - 1][0] + imageD1[imagePartLen][1]) * (float)0.1);
                        image1[imagePartLen][imagePart0Len] = (imageD1[imagePartLen][ imagePart0Len ] * (float)0.6) + ((imageD1[imagePartLen - 1][ imagePart0Len ] + imageD1[imagePartLen][ imagePart0Len - 1]) * (float)0.1);
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
                        for (int i = 1; i < imagePartLen; i++)
                        {
                            for (int j = 1; j < imagePart0Len; j++)
                            {
                                imageD1[i][j] = (image1[i][j] * (float)0.6) + ((image1[i + 1][j] + image1[i - 1][j] + image1[i][j + 1] + image1[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < imagePartLen; i++)
                        {
                            for (int j = 1; j < imagePart0Len; j++)
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
            proces1LeftRight(caseType);
            proces1TopBottom(caseType);
            proces1Corners(caseType);
            proces1Inside(caseType);
        }
        #endregion

        #region image2
        private void proces2LeftRight(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i <  imagePart0Len ; i++)
                        {
                            imageD2[imagePartLen][i] = (image2[ imagePartLen ][i] * (float)0.6) + ((image2[imagePartLen][i + 1] + image2[imagePartLen][i - 1] + image2[imagePartLen  - 1][i]) * (float)0.1);
                            imageD2[0][i] = (image2[0][i] * (float)0.6) + ((image2[0][i + 1] + image2[0][i - 1] + image2[1][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i <  imagePart0Len ; i++)
                        {
                            image2[imagePartLen][i] = (imageD2[imagePartLen][i] * (float)0.6) + ((imageD2[imagePartLen][i + 1] + imageD2[imagePartLen][i - 1] + imageD2[imagePartLen - 1][i]) * (float)0.1);
                            image2[0][i] = (imageD2[0][i] * (float)0.6) + ((imageD2[0][i + 1] + imageD2[0][i - 1] + imageD2[1][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces2TopBottom(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            imageD2[i][0] = (image2[i][0] * (float)0.6) + ((image2[i + 1][0] + image2[i - 1][0] + image2[i][1]) * (float)0.1);
                            imageD2[i][imagePart0Len] = (image2[i][imagePart0Len] * (float)0.6) + ((image2[i + 1][imagePart0Len] + image2[i - 1][imagePart0Len] + image2[i][imagePart0Len - 1]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i < image3.Length - 1; i++)
                        {
                            image2[i][0] = (imageD2[i][0] * (float)0.6) + ((imageD2[i + 1][0] + imageD2[i - 1][0] + imageD2[i][1]) * (float)0.1);
                            image2[i][imagePart0Len] = (imageD2[i][imagePart0Len] * (float)0.6) + ((imageD2[i + 1][imagePart0Len] + imageD2[i - 1][imagePart0Len] + imageD2[i][imagePart0Len - 1]) * (float)0.1);
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
                        imageD2[0][imagePart0Len] = (image2[0][imagePart0Len] * (float)0.6) + ((image2[1][imagePart0Len] + image2[0][imagePart0Len - 1]) * (float)0.1);
                        imageD2[imagePartLen][0] = (image2[imagePartLen][0] * (float)0.6) + ((image2[imagePartLen - 1][0] + image2[imagePartLen][1]) * (float)0.1);
                        imageD2[imagePartLen][imagePart0Len] = (image2[imagePartLen][imagePart0Len] * (float)0.6) + ((image2[imagePartLen - 1][imagePart0Len] + image2[imagePartLen][imagePart0Len - 1]) * (float)0.1);
                        break;
                    }
                case 2:
                    {
                        image2[0][0] = (imageD2[0][0] * (float)0.6) + ((imageD2[1][0] + imageD2[0][1]) * (float)0.1);
                        image2[0][imagePart0Len] = (imageD2[0][imagePart0Len] * (float)0.6) + ((imageD2[1][imagePart0Len] + imageD2[0][imagePart0Len - 1]) * (float)0.1);
                        image2[imagePartLen][0] = (imageD2[ imagePartLen ][0] * (float)0.6) + ((imageD2[imageD2.Length - 2][0] + imageD2[imagePartLen][1]) * (float)0.1);
                        image2[imagePartLen][imagePart0Len] = (imageD2[imagePartLen][imagePart0Len] * (float)0.6) + ((imageD2[imagePartLen - 1][imagePart0Len] + imageD2[imagePartLen][imagePart0Len - 1]) * (float)0.1);
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
                        for (int i = 1; i <  imagePartLen ; i++)
                        {
                            for (int j = 1; j <  imagePart0Len ; j++)
                            {
                                imageD2[i][j] = (image2[i][j] * (float)0.6) + ((image2[i + 1][j] + image2[i - 1][j] + image2[i][j + 1] + image2[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i <  imagePartLen ; i++)
                        {
                            for (int j = 1; j <  imagePart0Len ; j++)
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
            proces2LeftRight(caseType);
            proces2TopBottom(caseType);
            proces2Corners(caseType);
            proces2Inside(caseType);
        }
        #endregion

        #region image3
        private void proces3LeftRight(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i <  imagePart0Len; i++)
                        {
                            imageD3[imagePartLen][i] = (image3[imagePartLen][i] * (float)0.6) + ((image3[imagePartLen][i + 1] + image3[imagePartLen][i - 1] + image3[imagePartLen - 1][i]) * (float)0.1);
                            imageD3[0][i] = (image3[0][i] * (float)0.6) + ((image3[0][i + 1] + image3[0][i - 1] + image3[1][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i <  imagePart0Len; i++)
                        {
                            image3[imagePartLen][i] = (imageD3[imageD3.Length - 1][i] * (float)0.6) + ((imageD3[imagePartLen][i + 1] + imageD3[imagePartLen][i - 1] + imageD3[imagePartLen - 1][i]) * (float)0.1);
                            image3[0][i] = (imageD3[0][i] * (float)0.6) + ((imageD3[0][i + 1] + imageD3[0][i - 1] + imageD3[1][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces3TopBottom(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i <  imagePartLen; i++)
                        {
                            imageD3[i][imagePart0Len] = (image3[i][imagePart0Len] * (float)0.6) + ((image3[i + 1][imagePart0Len] + image3[i - 1][imagePart0Len] + image3[i][imagePart0Len -1]) * (float)0.1);
                            imageD3[i][0] = (image3[i][0] * (float)0.6) + ((image3[i + 1][0] + image3[i - 1][0] + image3[i][1]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i <  imagePartLen; i++)
                        {
                            image3[i][imagePart0Len] = (imageD3[i][imagePart0Len] * (float)0.6) + ((imageD3[i + 1][imagePart0Len] + imageD3[i - 1][imagePart0Len] + imageD3[i][imagePartLen - 1]) * (float)0.1);
                            image3[i][0] = (imageD3[i][0] * (float)0.6) + ((imageD3[i + 1][0] + imageD3[i - 1][0] + imageD3[i][1]) * (float)0.1);
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
                        imageD3[0][imagePart0Len] = (image3[0][imagePart0Len] * (float)0.6) + ((image3[1][imagePart0Len] + image3[0][imagePartLen - 1]) * (float)0.1);
                        imageD3[imagePartLen][0] = (image3[imagePartLen][0] * (float)0.6) + ((image3[imagePartLen  - 1][0] + image3[imagePartLen][1]) * (float)0.1);
                        imageD3[imagePartLen][imagePart0Len] = (image3[imagePartLen][imagePart0Len] * (float)0.6) + ((image3[imagePartLen  - 1][imagePart0Len] + image3[imagePartLen][imagePart0Len - 1]) * (float)0.1);
                        break;
                    }
                case 2:
                    {
                        image3[0][0] = (imageD3[0][0] * (float)0.6) + ((imageD3[1][0] + imageD3[0][1]) * (float)0.1);
                        image3[0][imagePart0Len] = (imageD3[0][imagePart0Len] * (float)0.6) + ((imageD3[1][imagePart0Len] + imageD3[0][imagePart0Len - 1]) * (float)0.1);
                        image3[imagePartLen][0] = (imageD3[imagePartLen][0] * (float)0.6) + ((imageD3[imagePartLen - 1][0] + imageD3[imagePartLen][1]) * (float)0.1);
                        image3[imagePartLen][imagePart0Len] = (imageD3[imagePartLen][ imagePart0Len] * (float)0.6) + ((imageD3[imagePartLen - 1][ imagePart0Len] + imageD3[imagePartLen][imagePart0Len - 1]) * (float)0.1);
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
                        for (int i = 1; i <  imagePartLen; i++)
                        {
                            for (int j = 1; j <  imagePart0Len; j++)
                            {
                                imageD3[i][j] = (image3[i][j] * (float)0.6) + ((image3[i + 1][j] + image3[i - 1][j] + image3[i][j + 1] + image3[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i <  imagePartLen; i++)
                        {
                            for (int j = 1; j <  imagePart0Len; j++)
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
            proces3LeftRight(caseType);
            proces3TopBottom(caseType);
            proces3Corners(caseType);
            proces3Inside(caseType);

        }
        #endregion

        #region image4
        private void proces4LeftRight(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i <  imagePart0Len; i++)
                        {
                            imageD4[ imagePartLen][i] = (image4[imagePartLen][i] * (float)0.6) + ((image4[imagePartLen][i + 1] + image4[imagePartLen][i - 1] + image4[imagePartLen - 1][i]) * (float)0.1);
                            imageD4[0][i] = (image4[0][i] * (float)0.6) + ((image4[0][i + 1] + image4[0][i - 1] + image4[1][i]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i <  imagePart0Len; i++)
                        {
                            image4[ imagePartLen][i] = (imageD4[imageD4.Length - 1][i] * (float)0.6) + ((imageD4[imageD4.Length - 1][i + 1] + imageD4[imagePartLen][i - 1] + imageD4[imagePartLen - 1][i]) * (float)0.1);
                            image4[0][i] = (imageD4[0][i] * (float)0.6) + ((imageD4[0][i + 1] + imageD4[0][i - 1] + imageD4[1][i]) * (float)0.1);
                        }
                        break;
                    }
            }

        }

        private void proces4TopBottom(int caseType)
        {
            switch (caseType)
            {
                case 1:
                    {
                        for (int i = 1; i <  imagePartLen; i++)
                        {
                            imageD4[i][ imagePart0Len] = (image4[i][imagePart0Len] * (float)0.6) + ((image4[i + 1][imagePart0Len] + image4[i - 1][imagePart0Len] + image4[i][imagePart0Len - 1]) * (float)0.1);
                            imageD4[i][0] = (image4[i][0] * (float)0.6) + ((image4[i + 1][0] + image4[i - 1][0] + image4[i][1]) * (float)0.1);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i <  imagePartLen; i++)
                        {
                            image4[i][imagePart0Len] = (imageD4[i][imagePart0Len] * (float)0.6) + ((imageD4[i + 1][imagePart0Len] + imageD4[i - 1][imagePart0Len] + imageD4[i][imagePart0Len - 1]) * (float)0.1);
                            image4[i][0] = (imageD4[i][0] * (float)0.6) + ((imageD4[i + 1][0] + imageD4[i - 1][0] + imageD4[i][1]) * (float)0.1);
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
                        imageD4[0][imagePart0Len] = (image4[0][imagePart0Len] * (float)0.6) + ((image4[1][imagePart0Len] + image4[0][imagePart0Len - 1]) * (float)0.1);
                        imageD4[imagePartLen][0] = (image4[imagePartLen][0] * (float)0.6) + ((image4[ imagePartLen - 1][0] + image4[imagePartLen][1]) * (float)0.1);
                        imageD4[imagePartLen][ imagePart0Len] = (image4[ imagePartLen][ imagePart0Len] * (float)0.6) + ((image4[imagePartLen - 1][imagePart0Len] + image4[imagePartLen][imagePart0Len - 1]) * (float)0.1);
                        break;
                    }
                case 2:
                    {
                        image4[0][0] = (imageD4[0][0] * (float)0.6) + ((imageD4[1][0] + imageD4[0][1]) * (float)0.1);
                        image4[0][imagePart0Len] = (imageD4[0][imagePart0Len] * (float)0.6) + ((imageD4[1][imagePart0Len] + imageD4[0][imagePart0Len - 1]) * (float)0.1);
                        image4[imagePartLen][0] = (imageD4[imagePartLen][0] * (float)0.6) + ((imageD4[imagePartLen -1][0] + imageD4[imagePartLen][1]) * (float)0.1);
                        image4[imagePartLen][imagePart0Len] = (imageD4[imagePartLen][imagePart0Len] * (float)0.6) + ((imageD4[imagePartLen - 1][ imagePart0Len] + imageD4[imagePartLen][imagePart0Len - 1]) * (float)0.1);
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
                        for (int i = 1; i <  imagePartLen; i++)
                        {
                            for (int j = 1; j <  imagePart0Len; j++)
                            {
                                imageD4[i][j] = (image4[i][j] * (float)0.6) + ((image4[i + 1][j] + image4[i - 1][j] + image4[i][j + 1] + image4[i][j - 1]) * (float)0.1);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 1; i <  imagePartLen; i++)
                        {
                            for (int j = 1; j <  imagePart0Len; j++)
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
            proces4LeftRight(caseType);
            proces4TopBottom(caseType);
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
