using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1 {

    enum StatusOfFiles {
        NoneEof,
        FirstEof,
        SecondEof,
        BothEof
    }

    class Program {
        private const int EOF = -1;
        private static long N{ get; set; }

        public static long SkipSame(ref FileStream stream1, ref FileStream stream2, out int a, out int b, ref StatusOfFiles status) {
            do {
                a = stream1.ReadByte();
                b = stream2.ReadByte();
            } while (a == b && a != EOF && b != EOF);

            if(a == EOF && b == EOF) {
                status = StatusOfFiles.BothEof;
            }
            else if(a == EOF) {
                status = StatusOfFiles.FirstEof;
                return stream2.Position;
            }
            else if(b == EOF) {
                status = StatusOfFiles.SecondEof;
                return stream1.Position;
            }
            return stream1.Position;
        }

        public static bool CheckNext(ref FileStream stream1, ref FileStream stream2, out int a, out int b, ref StatusOfFiles status) {
            a = stream1.ReadByte();
            b = stream2.ReadByte();
            if (a == EOF) {
                status = StatusOfFiles.FirstEof;
            }

            if (b == EOF) {
                status = StatusOfFiles.SecondEof;
            }

            return a == b ? false : true;
        }

        public static void PrintRestOfStream(ref FileStream stream, string pattern, int buf, ref int count, ref StatusOfFiles status) {
            count++;
            while (count < N || buf != EOF) {
                buf = stream.ReadByte();
                if (buf != EOF)
                    Console.Write(pattern, buf);
                count++;
            }
            status = StatusOfFiles.BothEof;
        }

        static void Main(string[] args) {
            StatusOfFiles status = StatusOfFiles.NoneEof;

            if (args.Length < 2) {
                Console.WriteLine("Program requires at least 2 arguments");
                return;
            }

            FileStream stream1 = new FileStream(args[0], FileMode.Open);
            FileStream stream2 = new FileStream(args[1], FileMode.Open);

            if (args.Length >= 3)
                N = Convert.ToInt64(args[3]);
            else {
                N = stream1.Length;
                if (stream2.Length > N)
                    N = stream2.Length;
            }

            int byte1 = 0, byte2 = 0, count = 0;
            long position = 0;
            while (status != StatusOfFiles.BothEof && count < N) {
                bool offset = false;
                position = SkipSame(ref stream1, ref stream2, out byte1, out byte2, ref status);

                if (status == StatusOfFiles.NoneEof) {
                    Console.Write("0x{0:x8}: ", position - 1);
                    offset = true;

                    Console.Write("0x{0:x}(0x{1:x}) ", byte1, byte2);
                    count++;

                    while (CheckNext(ref stream1, ref stream2, out byte1, out byte2, ref status) && status == StatusOfFiles.NoneEof) {
                        position++;
                        Console.Write("0x{0:x}(0x{1:x}) ", byte1, byte2);
                        count++;
                    }
                }

                if (!offset)
                    Console.Write("0x{0:x8}: ", position - 1);

                if (status == StatusOfFiles.SecondEof) {
                    Console.Write("0x{0:x}(<EOF>) ", byte1);
                    PrintRestOfStream(ref stream1, "0x{0:x} ", byte1, ref count, ref status);
                }
                if(status == StatusOfFiles.FirstEof) {
                    Console.Write("<EOF>(0x{0:x}) ", byte2);
                    PrintRestOfStream(ref stream2, "(0x{0:x}) ", byte2, ref count, ref status);
                }

                Console.WriteLine();

            }

            if (count == 0)
                Console.WriteLine("Files are identical");

        }
    }
}
