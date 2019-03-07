 using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;


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


        public static void PrintRestOfStream(ref FileStream stream, string pattern, int buf, ref int count, ref StatusOfFiles status, bool side, bool text) {
            count++;
            while (count < N || buf != EOF) {
                buf = stream.ReadByte();
                if (buf != EOF)
                    Console.Write(pattern, buf);
                count++;
            }
            status = StatusOfFiles.BothEof;
        }


        public static void PrintOne( int byte1, int byte2, bool side, bool text) {
            if (text) {
                char out1 = (char)byte1, out2 = (char)byte2;
                if (Char.IsControl(out1))
                    out1 = '.';
                if (Char.IsControl(out2))
                    out2 = '.';

                if (side)
                    Console.Write("{0}|{1} ", out1, out2);
                else
                    Console.Write("{0}|{1} ", out1, out2);
            }
            else {
                if(side)
                    Console.Write("0x{0:x}|0x{1:x} ", byte1, byte2);
                else
                    Console.Write("0x{0:x}(0x{1:x}) ", byte1, byte2);
            }
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

            bool brief = false, text = false, side = false, isDiff = false;

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(opts => {
                if (opts.Length > 0) {
                    Console.WriteLine("Parameter --length was set and equals {0}", opts.Length);
                    N = opts.Length;
                }
                if (opts.Text) {
                    Console.WriteLine("Parameter --text was set");
                    text = true;
                }
                if (opts.Brief) {
                    Console.WriteLine("Parameter --brief was set");
                    brief = true;
                }
                if (opts.Side_by_side) {
                    Console.WriteLine("Parameter --side-by-side was set");
                    side = true;
                }
            });

            int byte1 = 0, byte2 = 0, count = 0;
            long position = 0;
            while (status != StatusOfFiles.BothEof && count < N) {
                bool offset = false;
                position = SkipSame(ref stream1, ref stream2, out byte1, out byte2, ref status);

                if (status == StatusOfFiles.NoneEof) {
                    if (!isDiff)
                        isDiff = true;

                    if (!brief) {
                        Console.Write("0x{0:x8}: ", position - 1);
                        offset = true;

                        PrintOne(byte1, byte2, side, text);
                    }

                    count++;

                    while (CheckNext(ref stream1, ref stream2, out byte1, out byte2, ref status) && status == StatusOfFiles.NoneEof) {
                        position++;
                        if(!brief)
                            PrintOne(byte1, byte2, side, text);
                        count++;
                    }
                }

                if (status == StatusOfFiles.FirstEof || status == StatusOfFiles.SecondEof) {
                    if (!offset && !brief && status != StatusOfFiles.BothEof)
                        Console.Write("0x{0:x8}: ", position - 1);

                    if (!isDiff)
                        isDiff = true;

                    bool isFirst = false;
                    if (status == StatusOfFiles.FirstEof)
                        isFirst = true;

                    if (!brief) {
                        if (status == StatusOfFiles.SecondEof) {
                            Console.Write("0x{0:x}(<EOF>) ", byte1);
                            PrintRestOfStream(ref stream1, "0x{0:x} ", byte1, ref count, ref status, side, text);
                        }
                        else {
                            Console.Write("<EOF>(0x{0:x}) ", byte2);
                            PrintRestOfStream(ref stream2, "(0x{0:x}) ", byte2, ref count, ref status, side, text);
                        }
                    }
                }

                if(!brief)
                    Console.WriteLine();

            }

            if (!isDiff)
                Console.WriteLine("Files are identical");
            else if(brief && isDiff) Console.WriteLine("Files are not identical");

        }

    }
}
