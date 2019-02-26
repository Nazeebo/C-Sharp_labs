using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab0 {
    class Program {
        static void mergeSort(ref int[] x, int l, int r) {
            if (l >= r)
                return;
            int m = (l + r) / 2;
            mergeSort(ref x, l, m);
            mergeSort(ref x, m + 1, r);
            int i = l, j = m + 1;
            int[] y = new int[x.Length];
            for(int k = l; k <= r; ++k) {
                if (j > r || (i <= m && x[i] < x[j]) ) {
                    y[k] = x[i];
                    ++i;
                }
                else {
                    y[k] = x[j];
                    ++j;
                }
            }
            for (int k = l; k <= r; ++k)
                x[k] = y[k];
        }

        static void Main(string[] args) {
            Console.WriteLine("Введите число");
            int N = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[N];
            Random rand = new Random();
            for (int i = 0; i < arr.Length; ++i)
                arr[i] = rand.Next(1000);
            for (int i = 0; i < arr.Length; ++i)
                Console.Write("{0} ", arr[i]);
            mergeSort(ref arr, 0, N - 1);
            Console.WriteLine();
            for (int i = 0; i < arr.Length; ++i)
                Console.Write("{0} ", arr[i]);
        }
    }
}
