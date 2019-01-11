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
            //classic tmp = new classic(1024);
            //tmp.test();
            //firstApproach t = new firstApproach(1024);
            //t.convolution();
            //moreEfficient m = new moreEfficient(1024);
            //m.convolution();
            //mostEfficient most = new mostEfficient(1024);
            //most.convolution();
            //double a = (double)tmp.time;
            //double b = (double)t.time;
            //double c = (double)m.time;
            //double d = (double)most.time;

            //for (int i = 0; i < 9; i++)
            //{
            //    tmp.test();
            //    t.convolution();
            //    m.convolution();
            //    most.convolution();

            //    a += (double)tmp.time;
            //    b += (double)t.time;
            //    c += (double)m.time;
            //    d += (double)most.time;
            //}

            //System.Console.WriteLine("Klasyczne podejscie: {0} milisekund", a/10);
            //System.Console.Write("[,] zajelo: {0}", b/10);
            //System.Console.Write(" milisekund, było szybsze: ");
            //System.Console.Write(Math.Round((a/10) / (b/10), 2));
            //System.Console.WriteLine(" razy.");
            //System.Console.Write("[][] zajelo: {0}", c/10);
            //System.Console.Write(" milisekund, było szybsze: ");
            //System.Console.Write(Math.Round((a/10) / (c/10), 2));
            //System.Console.WriteLine(" razy.");
            //System.Console.Write("najlepsza: {0}", d/10);
            //System.Console.Write(" milisekund, było szybsze: ");
            //System.Console.Write(Math.Round((a/10) / (d/10), 2));
            //System.Console.WriteLine(" razy.");
            //classic c = new classic(1024);

            //c.save("C:/Users/Piotr/Desktop/test/bef.png");
            //c.test();
            //c.save("C:/Users/Piotr/Desktop/test/new.png");

            basic b = new basic("C:/Users/Piotr/Desktop/test/bef.png");
            b.test();
            //b.saveParts();
            b.save("C:/Users/Piotr/Desktop/test/new.png");

            //test t = new test("C:/Users/Piotr/Desktop/random.png");
            //t.divide();
            //t.connect();
            //t.save("C:/Users/Piotr/Desktop/newrandom.png");
        }
    }
}
