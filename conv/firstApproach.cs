using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace conv
{
    class firstApproach
    {
        private float[,] image;
        private int size;
        public long time;
        public firstApproach(int size)
        {
            this.size = size;
            this.image = new float[size, size];
            fill();
        }

        private void fill()
        {

            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    image[i, j] = i;
                }
            }
        }

        public void convolution()
        {
            float[,] destination = new float[1024, 1024];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for(int i = 0; i < 100; i++)
            {
                var p1 = top(destination, image);
                var p2 = bottom(destination, image);
                var p3 = left(destination, image);
                var p4 = right(destination, image);
                var p5 = corners(destination, image);
                proces(destination, image);
                p1.Wait();
                p2.Wait();
                p3.Wait();
                p4.Wait();
                p4.Wait();

                var pp1 = top(destination, image);
                var pp2 = bottom(destination, image);
                var pp3 = left(destination, image);
                var pp4 = right(destination, image);
                var pp5 = corners(destination, image);
                proces(destination, image);
                pp1.Wait();
                pp2.Wait();
                pp3.Wait();
                pp4.Wait();
                pp4.Wait();
            }
            stopwatch.Stop();
            time = (long)stopwatch.ElapsedMilliseconds;
        }
        public void proces(float[,] dest, float[,] source)
        {
            Parallel.For(1, 1023, i =>
            {
                for (int j = 1; j < 1023; ++j)
                {
                    var pixel = source[i,j] * 0.6f;

                    pixel += source[i - 1,j] * 0.1f;
                    pixel += source[i + 1,j] * 0.1f;
                    pixel += source[i,j - 1] * 0.1f;
                    pixel += source[i,j + 1] * 0.1f;

                    dest[i,j] = pixel;
                }
            });
        }
        public Task left(float[,] dest, float[,] source)
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    float newValue = source[i,0] * (float)0.6;
                    newValue += source[i - 1,0] * (float)0.1;
                    newValue += source[i + 1,0] * (float)0.1;
                    newValue += source[i,1] * (float)0.1;
                    dest[i,0] = newValue;
                }
            });
        }

        public Task right(float[,] dest, float[,] source)
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    float newValue = source[i,1023] * (float)0.6;
                    newValue += source[i - 1,1023] * (float)0.1;
                    newValue += source[i + 1,1023] * (float)0.1;
                    newValue += source[i,1022] * (float)0.1;
                    dest[i,1023] = newValue;
                }
            });
        }

        public Task top(float[,] dest, float[,] source)
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    float newValue = source[0,i] * (float)0.6;
                    newValue += source[0,i - 1] * (float)0.1;
                    newValue += source[0,i + 1] * (float)0.1;
                    newValue += source[1,i] * (float)0.1;
                    dest[0,i] = newValue;
                }
            });
        }

        public Task bottom(float[,] dest, float[,] source)
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    float newValue = source[1023,i] * (float)0.6;
                    newValue += source[1023,i - 1] * (float)0.1;
                    newValue += source[1023,i + 1] * (float)0.1;
                    newValue += source[1022,i] * (float)0.1;
                    dest[1023,i] = newValue;
                }
            });
        }

        public Task corners(float[,] dest, float[,] source)
        {
            return Task.Run(() =>
            {
                float p = source[0,0] * (float)0.6;
                p += source[0,1] * (float)0.1;
                p += source[1,0] * (float)0.1;
                dest[0,0] = p;

                p = source[0,1023] * (float)0.6;
                p += source[0,1022] * (float)0.1;
                p += source[1,1023] * (float)0.1;
                dest[0,1023] = p;

                p = source[1023,0] * (float)0.6;
                p += source[1023,1] * (float)0.1;
                p += source[1022,0] * (float)0.1;
                dest[1023,0] = p;

                p = source[1023,1023] * (float)0.6;
                p += source[1023,1022] * (float)0.1;
                p += source[1023 - 1,1023] * (float)0.1;
                dest[1023,1023] = p;
            });
        }

        //public void convolution()
        //{
        //    float[,] destination = new float[1024, 1024];
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    for(int i = 0; i < 100; i++)
        //    {
        //        var taskFirst_1 = procesRow(image, destination, 0, 128);
        //        var taskFirst_2 = procesRow(image, destination, 128, 256);
        //        taskFirst_1.Wait();
        //        taskFirst_2.Wait();

        //        var taskFirst_3 = procesRow(image, destination, 256, 384);
        //        var taskSecond_1 = procesRow(destination, image, 0, 128);
        //        taskFirst_3.Wait();
        //        taskSecond_1.Wait();

        //        var taskFirst_4 = procesRow(image, destination, 384, 512);
        //        var taskSecond_2 = procesRow(destination, image, 128, 256);
        //        taskFirst_4.Wait();
        //        taskSecond_2.Wait();

        //        var taskFirst_5 = procesRow(image, destination, 512, 640);
        //        var taskSecond_3 = procesRow(destination, image, 256, 384);
        //        taskFirst_5.Wait();
        //        taskSecond_3.Wait();

        //        var taskFirst_6 = procesRow(image, destination, 640, 786);
        //        var taskSecond_4 = procesRow(destination, image, 384, 512);
        //        taskFirst_6.Wait();
        //        taskSecond_4.Wait();

        //        var taskFirst_7 = procesRow(image, destination, 786, 914);
        //        var taskSecond_5 = procesRow(destination, image, 512, 640);
        //        taskFirst_7.Wait();
        //        taskSecond_5.Wait();

        //        var taskFirst_8 = procesRow(image, destination, 914, 1024);
        //        var taskSecond_6 = procesRow(destination, image, 640, 786);
        //        taskFirst_7.Wait();
        //        taskSecond_5.Wait();

        //        var taskSecond_7 = procesRow( destination,image, 786, 914);
        //        var taskSecond_8 = procesRow( destination, image, 914, 1024);
        //        taskSecond_7.Wait();
        //        taskSecond_8.Wait();
        //    }
        //    stopwatch.Stop();
        //    System.Console.WriteLine(stopwatch.Elapsed);
        //}

        //private Task procesRow(float[,] source, float [,] destination, int begin, int end)
        //{
        //    return Task.Run(() =>
        //    {
        //        float up = 0;
        //        float down = 0;
        //        float left = 0;
        //        float right = 0;
        //        float center = 0;

        //        for (int i = begin; i < end; i++)
        //        {
        //            for (int j = 0; j < this.size; j++)
        //            {
        //                up = 0;
        //                down = 0;
        //                left = 0;
        //                right = 0;
        //                center = 0;

        //                if (i - 1 >= 0 && i - 1 < this.size && j >= 0 && j < this.size)
        //                    up = source[i - 1, j] * 0.1f;

        //                if (i + 1 >= 0 && i + 1 < this.size && j >= 0 && j < this.size)
        //                    down = source[i + 1, j] * 0.1f;

        //                if (i >= 0 && i < this.size && j - 1 >= 0 && j - 1 < this.size)
        //                    left = source[i, j - 1] * 0.1f;

        //                if (i >= 0 && i < this.size && j + 1 >= 0 && j + 1 < this.size)
        //                    right = source[i, j + 1] * 0.1f;

        //                if (i >= 0 && i < this.size && j >= 0 && j < this.size)
        //                    center = source[i, j] * 0.6f;

        //                destination[i, j] = up + down + left + right + center;
        //            }
        //        }
        //    });
        //}
    }
}
