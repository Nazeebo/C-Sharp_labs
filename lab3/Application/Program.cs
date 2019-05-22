using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatioLibrary;

namespace Application{
    class Program{
        static void Main(string[] args){
            Ratio x = new Ratio(7, 16);
            Ratio y = new Ratio(5, 24);
            Console.WriteLine("{0} или {2} и {1} или {3}", x.ToString(), y.ToString(), x.ToDouble(), y.ToDouble());
            Console.WriteLine("{0}+{1} = {2}", x.ToString(), y.ToString(), (x + y).ToString());
            Console.WriteLine("{0}-{1} = {2}", x.ToString(), y.ToString(), (x - y).ToString());
            Console.WriteLine("{0}*{1} = {2}", x.ToString(), y.ToString(), (x * y).ToString());
            Console.WriteLine("{0} / {1} = {2}", x.ToString(), y.ToString(), (x / y).ToString());
        }
    }
}
