using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab2 {
    class Program {

        static void mainMenu(List<Shape> shapes) {
            ConsoleKeyInfo key;
            char keyChar;

            while (true) {
                key = Console.ReadKey();
                keyChar = key.KeyChar;

                switch (keyChar) {
                    case 'c': {
                            createMenu(shapes);
                            break;
                        }
                    case 'l': {
                            printShapes(shapes);
                            break;
                        }
                    case 'h': {
                            printHelp();
                            break;
                        }
                    case 'e': {
                            return;
                        }
                    default: {
                            Console.WriteLine("Некорректный вход");
                            break;
                        }
                }
            }
        }

        private static void printHelp() {
            Console.WriteLine();
            Console.WriteLine("С - cоздать фигуру");
            Console.WriteLine("L - печать списка фигур с их аттрибутами");
            Console.WriteLine("H - помощь");
            Console.WriteLine("У - выход из программы");
        }

        private static void printShapes(List<Shape> shapes) {
            Console.WriteLine();
            foreach (Shape item in shapes)
                Console.WriteLine(item.ToString());
        }

        private static void createMenu(List<Shape> shapes) {
            Console.WriteLine();
            Console.WriteLine("Выберите фигуру для создания: У - Эллипс, С - круг, З - многоугольник, нажмите Й для отмены");
            ConsoleKeyInfo key;
            char keyChar;

            while (true) {
                key = Console.ReadKey();
                keyChar = key.KeyChar;

                switch (keyChar) {
                    case 'e': {
                            createEllipse(shapes);
                            return;
                        }
                    case 'c': {
                            createCircle(shapes);
                            return;
                        }
                    case 'p': {
                            createPolygon(shapes);
                            return;
                        }
                    case 'q': {
                            return;
                        }
                    default: {
                            Console.WriteLine("Wrong input");
                            break;
                        }
                }
            }

        }

        private static void createEllipse(List<Shape> shapes) {
            Point focus1, focus2;
            Console.WriteLine("Введите координаты фокусов в формате x1 y1 x2 y2");
            string line = Console.ReadLine();
            Regex regForPoint = new Regex(@"(\d*\.?\d+)\s+(\d*\.?\d+)");
            MatchCollection matches = regForPoint.Matches(line);
            if(matches.Count == 2) {
                GroupCollection group1 = matches[0].Groups;
                GroupCollection group2 = matches[1].Groups;
                focus1 = new Point(Convert.ToDouble(group1[1].Value), Convert.ToDouble(group1[2].Value));
                focus2 = new Point(Convert.ToDouble(group2[1].Value), Convert.ToDouble(group2[2].Value));
                Console.WriteLine("Введите длину большой полуоси");
                double axis = Convert.ToDouble(Console.ReadLine());
                shapes.Add( new Ellipse(focus1, focus2, axis));
            }
            else {
                Console.WriteLine("Неудалось создать эллипс из-за неправильного ввода");
            }
        }

        private static void createCircle(List<Shape> shapes) {
            Point center;
            Console.WriteLine("Введите координаты центра круга в формате x y");
            string line = Console.ReadLine();
            Regex regForPoint = new Regex(@"(\d*\.?\d+)\s+(\d*\.?\d+)");
            MatchCollection matches = regForPoint.Matches(line);
            if (matches.Count == 1) {
                GroupCollection group1 = matches[0].Groups;
                center = new Point(Convert.ToDouble(group1[1].ToString()), Convert.ToDouble(group1[2].ToString()));
                Console.WriteLine("Введите радиус");
                double radius = Convert.ToDouble(Console.ReadLine().ToString());
                shapes.Add(new Circle(center, radius));
            }
            else {
                Console.WriteLine("Неудалось создать круг из-за неправильного ввода");
            }
        }

        private static void createPolygon(List<Shape> shapes) {
            int n;
            List<Point> points = new List<Point>();
            Console.WriteLine("Введите число точек многоугольника");
            n = Convert.ToInt32(Console.ReadLine());
            if(n <= 2) {
                Console.WriteLine("Многоугольник из менее трёх точек не сделаешь");
                return;
            }
            Console.WriteLine("Введите {0} точек в формате x1 y1 x2 y2 .... xn yn", n);
            string line = Console.ReadLine();
            Regex regForPoint = new Regex(@"(\d*\.?\d+)\s+(\d*\.?\d+)");
            MatchCollection matches = regForPoint.Matches(line);
            if (matches.Count == n) {
                foreach(Match match in matches) {
                    GroupCollection group = match.Groups;
                    points.Add(new Point(Convert.ToDouble(group[1].Value), Convert.ToDouble(group[2].Value)));
                }
                shapes.Add(new Polygon(n, points));
            }
            else {
                Console.WriteLine("Неудалось создать круг из-за неправильного ввода");
            }
        }

        static void Main(string[] args) {
            List<Shape> shapes = new List<Shape>();

            mainMenu(shapes);
        }
    }
}
