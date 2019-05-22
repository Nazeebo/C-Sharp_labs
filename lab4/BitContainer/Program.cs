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
            Console.WriteLine(array.list[0]);
            array[1] = 1;
            Console.WriteLine(array.list[0]);
        }
    }
}
