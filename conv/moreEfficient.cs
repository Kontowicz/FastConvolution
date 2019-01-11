using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    class moreEfficient
    {
        private static float[][] image;
        private int size;
        public long time;
        public moreEfficient(int size)
        {
            this.size = size;
            image = new float[size][];
            for(int i = 0; i < 1024; i++)
            {
                image[i] = new float[1024];
            }
            fill();
        }

        private void fill()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    image[i][j] = i;
                }
            }
        }
        public void convolution()
        {
            float[][] destination = new float[1024][];
            for (int i = 0; i < 1024; i++)
            {
                destination[i] = new float[1024];
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100; i++)
            {
                //var p5 = procesPartFirst(destination, image);
                //var p6 = procesPartSecond(destination, image);
                var p = corners(destination, image);
                var p1 = top(destination, image);
                var p2 = bottom(destination, image);
                var p3 = left(destination, image);
                var p4 = right(destination, image);
                proces(destination, image);
                p.Wait();
                p1.Wait();
                p2.Wait();
                p3.Wait();
                p4.Wait();
                //p5.Wait();
                //p6.Wait();

                //var pp5 = procesPartFirst(image, destination);
                //var pp6 = procesPartSecond(image, destination);
                var pp = corners(image, destination);
                var pp1 = top(image, destination);
                var pp2 = bottom(image, destination);
                var pp3 = left(image, destination);
                var pp4 = right(image, destination);
                proces(image, destination);
                pp.Wait();
                pp1.Wait();
                pp2.Wait();
                pp3.Wait();
                pp4.Wait();
                //pp5.Wait();
                //pp6.Wait();
            }
            stopwatch.Stop();
            time = (long)stopwatch.ElapsedMilliseconds;
        }
        public void proces(float[][] dest, float[][] source)
        {
            Parallel.For(1, 1023, i =>
            {
                for (int j = 1; j < 1023; ++j)
                {
                    dest[i][ j] = (source[i][j] * (float)0.6) + ((source[i - 1][j] + source[i + 1][j] + source[i][j - 1] + source[i][j + 1]) * (float)0.1);
                }
            });
        }

        public Task procesPartFirst(float[][] dest, float[][] source)
        {
            return Task.Run(() =>
            {
                Parallel.For(1, 512, i =>
                {
                    for (int j = 1; j < 1023; ++j)
                    {
                        dest[i][j] = (source[i][j] * (float)0.6) + ((source[i - 1][j] + source[i + 1][j] + source[i][j - 1] + source[i][j + 1]) * (float)0.1);
                    }
                });
            });
        }

        public Task procesPartSecond(float[][] dest, float[][] source)
        {
            return Task.Run(() =>
            {
                Parallel.For(512, 1023, i =>
                {
                    for (int j = 1; j < 1023; ++j)
                    {
                        dest[i][j] = (source[i][j] * (float)0.6) + ((source[i - 1][j] + source[i + 1][j] + source[i][j - 1] + source[i][j + 1]) * (float)0.1);
                    }
                });
            });
        }
        public Task left(float[][] dest, float[][] source)
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    dest[i][0] = (source[i][0] * (float)0.6) + ((source[i - 1][0] + source[i + 1][0] + source[i][1]) * (float)0.1);
                }
            });
        }

        public Task right(float[][] dest, float[][] source)
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    dest[i][1023] = (source[i][1023] * (float)0.6) + ((source[i - 1][1023] + source[i + 1][1023] + source[i][1022]) * (float)0.1);
                }
            });
        }

        public Task top(float [][] dest, float [][] source)
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    dest[0][i] = (source[0][i] * (float)0.6) + ((source[0][i - 1] + source[0][i + 1] + source[1][i]) * (float)0.1);
                }
            });
        }

        public Task bottom(float [][] dest, float [][] source)
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    float newValue = (source[1023][i - 1] + source[1023][i + 1] + source[1022][i]) * (float)0.1;
                    dest[1023][i] = (source[1023][i] * (float)0.6) + newValue;
                }
            });
        }

        public Task corners(float[][] dest, float[][] source)
        {
            return Task.Run(() =>
            {
                dest[0][0] = (source[0][0] * (float)0.6) + ((source[0][1] + source[1][0]) * (float)0.1);

                dest[0][1023] = (source[0][1023] * (float)0.6) + ((source[0][1022] + source[1][1023]) * (float) 0.1);

                dest[1023][0] = (source[1023][0] * (float)0.6) + ((source[1023][1] + source[1022][0]) * (float) 0.1);

                dest[1023][1023] = (source[1023][1023] * (float)0.6) + ((source[1023][1022] + source[1022][1023]) * (float)0.1);
            });
        }
    }
}

