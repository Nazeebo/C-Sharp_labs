using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitContainer {
    class Program {
        static void Main(string[] args) {
            BitContainer array = new BitContainer();
            array.pushBit(1);
            array.pushBit(0);
            Console.WriteLine(array.ToString() + " " + array.Lenght.ToString());
            array[1] = 1;
            Console.WriteLine(array.ToString() + " " + array.Lenght.ToString());
            array.pushBit(false);
            Console.WriteLine(array.ToString() + " " + array.Lenght.ToString());
            for (int i = 0; i < 15; ++i)
                array.pushBit(1);
            array[10] = 0;
            Console.WriteLine(array.ToString() + " " + array.Lenght.ToString());
            array.Remove(10);
            Console.WriteLine(array.ToString() + " " + array.Lenght.ToString());
            array.Insert(10, 0);
            Console.WriteLine(array.ToString() + " " + array.Lenght.ToString());

            foreach (bool b in array)
                Console.Write(b);
        }
    }
}
