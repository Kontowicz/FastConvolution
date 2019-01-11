using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    class mostEfficient
    {
        private static float[][] image;
        private static float[][] destination;
        private int size;
        public long time;
        public mostEfficient(int size)
        {
            this.size = size;
            image = new float[size][];
            for (int i = 0; i < 1024; i++)
            {
                image[i] = new float[1024];
            }
            destination = new float[size][];
            for (int i = 0; i < 1024; i++)
            {
                destination[i] = new float[1024];
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100; i++)
            {

                var p = cornersA();
                var p1 = topA();
                var p2 = bottomA();
                var p3 = leftA();
                var p4 = rightA();
                procesA();
                p.Wait();
                p1.Wait();
                p2.Wait();
                p3.Wait();
                p4.Wait();

                var pp = cornersB();
                var pp1 = topB();
                var pp2 = bottomB();
                var pp3 = leftB();
                var pp4 = rightB();
                procesB();
                pp.Wait();
                pp1.Wait();
                pp2.Wait();
                pp3.Wait();
                pp4.Wait();

            }
            stopwatch.Stop();
            time = (long)stopwatch.ElapsedMilliseconds;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void procesA()
        {
            Parallel.For(1, 1023, i =>
            {
                for (int j = 1; j < 1023; ++j)
                {
                    destination[i][j] = (image[i][j] * (float)0.6) + ((image[i - 1][j] + image[i + 1][j] + image[i][j - 1] + image[i][j + 1]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void procesB()
        {
            Parallel.For(1, 1023, i =>
            {
                for (int j = 1; j < 1023; ++j)
                {
                    image[i][j] = (destination[i][j] * (float)0.6) + ((destination[i - 1][j] + destination[i + 1][j] + destination[i][j - 1] + destination[i][j + 1]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task leftA()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    destination[i][0] = (image[i][0] * (float)0.6) + ((image[i - 1][0] + image[i + 1][0] + image[i][1]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task leftB()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    image[i][0] = (destination[i][0] * (float)0.6) + ((destination[i - 1][0] + destination[i + 1][0] + destination[i][1]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task rightA()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    destination[i][1023] = (image[i][1023] * (float)0.6) + ((image[i - 1][1023] + image[i + 1][1023] + image[i][1022]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task rightB()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    image[i][1023] = (destination[i][1023] * (float)0.6) + ((destination[i - 1][1023] + destination[i + 1][1023] + destination[i][1022]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task topA()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    destination[0][i] = (image[0][i] * (float)0.6) + ((image[0][i - 1] + image[0][i + 1] + image[1][i]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task topB()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    image[0][i] = (destination[0][i] * (float)0.6) + ((destination[0][i - 1] + destination[0][i + 1] + destination[1][i]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task bottomA()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    float newValue = (image[1023][i - 1] + image[1023][i + 1] + image[1022][i]) * (float)0.1;
                    destination[1023][i] = (image[1023][i] * (float)0.6) + newValue;
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task bottomB()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1023; ++i)
                {
                    image[1023][i] = (destination[1023][i] * (float)0.6) + ((destination[1023][i - 1] + destination[1023][i + 1] + destination[1022][i]) * (float)0.1);
                }
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task cornersA()
        {
            return Task.Run(() =>
            {
                destination[0][0] = (image[0][0] * (float)0.6) + ((image[0][1] + image[1][0]) * (float)0.1);
                destination[0][1023] = (image[0][1023] * (float)0.6) + ((image[0][1022] + image[1][1023]) * (float)0.1);
                destination[1023][0] = (image[1023][0] * (float)0.6) + ((image[1023][1] + image[1022][0]) * (float)0.1);
                destination[1023][1023] = (image[1023][1023] * (float)0.6) + ((image[1023][1022] + image[1022][1023]) * (float)0.1);
            });
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task cornersB()
        {
            return Task.Run(() =>
            {
                image[0][0] = (destination[0][0] * (float)0.6) + ((destination[0][1] + destination[1][0]) * (float)0.1);
                image[0][1023] = (destination[0][1023] * (float)0.6) + ((destination[0][1022] + destination[1][1023]) * (float)0.1);
                image[1023][0] = (destination[1023][0] * (float)0.6) + ((destination[1023][1] + destination[1022][0]) * (float)0.1);
                image[1023][1023] = (destination[1023][1023] * (float)0.6) + ((destination[1023][1022] + destination[1022][1023]) * (float)0.1);
            });
        }
    }
}
