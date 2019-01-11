using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    class mosteftest
    {
        private static float[] image;
        private static float[] destination;
        private static long size;
        public static long time;
        public mosteftest()
        {
            image = new float[1024 * 1024];
            destination = new float[1024 * 1024];
            size = 1024 * 1024;
        }

        public static void convolution()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100; i++)
            {
                System.Console.WriteLine(i);
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
        public static void procesA()
        {
            Parallel.For(1025, size - 2025, i =>
            {
                //System.Console.WriteLine(i);
                if (i % 1024 != 0 && i % 1024 != 1) {
                    for (long j = i; j < i + 1024 && j < size - 1024; ++j)
                    {
                        try
                        {
                            image[j] = (destination[j] * (float)0.6) + ((destination[j - 1] + destination[j + 1] + destination[j - 1024] + destination[j + 1024]) * (float)0.1);
                        } catch (Exception ex)
                        {
                            System.Console.WriteLine(j);
                        }
                    }
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void procesB()
        {
            Parallel.For(1025, size, i =>
            {
                if (i % 1024 != 0 && i % 1024 != 1)
                {
                    for (long j = i; j < i + 1024 && j < size - 1024; ++j)
                    {
                        destination[j] = (image[j] * (float)0.6) + ((image[j - 1] + image[j + 1] + image[j - 1024] + image[j + 1024]) * (float)0.1);
                    }
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task leftA()
        {
            return Task.Run(() =>
            {
                for (int i = 2046; i < size - 1025; i += 1024)
                {
                     destination[i] = (image[i]*(float)0.6) + ((image[i+1] + image[i + 1024] + image[i - 1024]) *(float)0.1);
                     //destination[i][0] = (image[i][0] * (float)0.6) + ((image[i - 1][0] + image[i + 1][0] + image[i][1]) * (float)0.1);
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task leftB()
        {
            return Task.Run(() =>
            {
                for (int i = 2046; i < size  - 1024; i += 1024)
                {
                    image[i] = (destination[i] * (float)0.6) + ((destination[i + 1] + destination[i + 1024] + destination[i - 1024]) * (float)0.1);
                    //destination[i][0] = (image[i][0] * (float)0.6) + ((image[i - 1][0] + image[i + 1][0] + image[i][1]) * (float)0.1);
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task rightA()
        {
            return Task.Run(() =>
            {
                for (int i = 1024; i < size - 1024; i += 1024)
                {
                    destination[i] = (image[i] * (float)0.6) + ((image[i + 1] + image[i + 1024] + image[i - 1024]) * (float)0.1);
                    //destination[i][1023] = (image[i][1023] * (float)0.6) + ((image[i - 1][1023] + image[i + 1][1023] + image[i][1022]) * (float)0.1);
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task rightB()
        {
            return Task.Run(() =>
            {
                for (int i = 1024; i < size - 1024; i += 1024)
                {
                    image[i] = (destination[i] * (float)0.6) + ((destination[i + 1] + destination[i + 1024] + destination[i - 1024]) * (float)0.1);
                    //destination[i][1023] = (image[i][1023] * (float)0.6) + ((image[i - 1][1023] + image[i + 1][1023] + image[i][1022]) * (float)0.1);
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task topA()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1024; ++i)
                {
                    image[i] = (destination[i] * (float)0.6) + ((destination[i - 1] + destination[i + 1] + destination[i+1024]) * (float)0.1);
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task topB()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i < 1024; ++i)
                {
                    destination[i] = (image[i] * (float)0.6) + ((image[i - 1] + image[i + 1] + image[i + 1024]) * (float)0.1);
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task bottomA()
        {
            return Task.Run(() =>
            {
                for (long i = size - 1024; i < size - 1024; ++i)
                {
                    image[i] = (destination[i] * (float)0.6) + ((destination[i - 1] + destination[i + 1] + destination[1024 + i]) * (float)0.1);
                }
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task bottomB()
        {
            return Task.Run(() =>
            {
                for (long i = size - 1024; i < size - 1024; ++i)
                {
                    destination[i] = (image[i] * (float)0.6) + ((image[i - 1] + image[i + 1] + image[1024 + i]) * (float)0.1);
                }
            });
        }




        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task cornersA()
        {
            return Task.Run(() =>
            {

                image[0] = (destination[0] * (float)0.6) + ((destination[1] + destination[1024]) * (float)0.1);
                image[1023] = (destination[1023] * (float)0.6) + ((destination[1022] + destination[2046]) * (float)0.1);
                image[size - 1023] = (destination[size - 1023] * (float)0.6) + ((destination[size - 2046] + destination[size - 1022]) * (float)0.1);
                image[size - 1] = (destination[size - 1] * (float)0.6) + ((destination[size - 2] + destination[size - 1024]) * (float)0.1);

            });
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task cornersB()
        {
            return Task.Run(() =>
            {
                destination[0] = (image[0] * (float)0.6) + ((image[1] + image[1024]) * (float)0.1);
                destination[1023] = (image[1023] * (float)0.6) + ((image[1022] + image[2046]) * (float)0.1);
                destination[size - 1023] = (image[size - 1023] * (float)0.6) + ((image[size - 2046] + image[size - 1022]) * (float)0.1);
                destination[size - 1] = (image[size - 1] * (float)0.6) + ((image[size - 1] + image[size - 1024]) * (float)0.1);
            });
        }
    }
}
