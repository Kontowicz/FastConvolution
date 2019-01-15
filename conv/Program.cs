using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace conv
{
    class Program
    {
        static void Main(string[] args)
        {
            classic classic = new classic(1024);
            classic.test();
            classic.save("C:/Users/Piotr/Desktop/result/classic.png");

            mostEfficient mostEfficient = new mostEfficient(1024);
            mostEfficient.convolution();
            mostEfficient.save("C:/Users/Piotr/Desktop/result/mostEfficient.png");

            basic basic = new basic("c:/users/piotr/desktop/test/test/bef.png");
            basic.test();
            basic.save("C:/Users/Piotr/Desktop/result/basic.png");

            basicImprove basicImprove = new basicImprove("c:/users/piotr/desktop/test/test/bef.png");
            basicImprove.test();
            basicImprove.save("C:/Users/Piotr/Desktop/result/basicImprove.png");

            double a = (double)classic.time;
            //double b = (double)t.time;
            //double c = (double)m.time;
            double d = (double)mostEfficient.time;
            double e = (double)basic.time;
            double f = (double)basicImprove.time;
            for (int i = 0; i < 9; i++)
            {
                classic.test();
                //t.convolution();
                //m.convolution();
                mostEfficient.convolution();
                basic.test();
                basicImprove.test();

                a += (double)classic.time;
              //  b += (double)t.time;
              //  c += (double)m.time;
                d += (double)mostEfficient.time;
                e += (double)basic.time;
                f += (double)basicImprove.time;
            }

            System.Console.WriteLine("Klasyczne podejscie obiczenia na pojedynczym obrazie: {0} sekund", Math.Round(a / 1000, 2));

            //System.Console.Write(" zajelo: {0}", Math.Round(b / 1000,2));
            //System.Console.Write(" sekund, było szybsze: ");
            //System.Console.Write(Math.Round((a / 10) / (b / 10), 2));
            //System.Console.WriteLine(" razy.");

            //System.Console.Write("Klasyczne podejście z tablicą typu [][] zajelo: {0}", Math.Round(c / 1000, 2));
            //System.Console.Write(" sekund, było szybsze: ");
            //System.Console.Write(Math.Round((a / 10) / (c / 10), 2));
            //System.Console.WriteLine(" razy.");

            System.Console.Write("Podejście asynchroniczne, obliczenia z optymalizacją kompilatora, zmienne i metody statyczne : {0}", Math.Round(d / 10,2));
            System.Console.Write(" sekund, było szybsze: ");
            System.Console.Write(Math.Round((a / 10) / (d / 10), 2));
            System.Console.WriteLine(" razy.");

            System.Console.Write("Podejście synchroniczne z podziałem obrazu: {0}", Math.Round(e / 1000,2));
            System.Console.Write(" sekund, było szybsze: ");
            System.Console.Write(Math.Round((a / 10) / (e / 10), 2));
            System.Console.WriteLine(" razy.");

            System.Console.Write("Podejście synchroniczne z podziałem obrazu: {0}", Math.Round(f / 1000,2));
            System.Console.Write(" sekund, było szybsze: ");
            System.Console.Write(Math.Round((a / 10) / (f / 10), 2));
            System.Console.WriteLine(" razy.");


        }
    }
}
