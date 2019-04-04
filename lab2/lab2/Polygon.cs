using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2 {
    class Polygon : Shape {
        private int count { get; set; }
        private List<Point> list;

        public Polygon(int count, List<Point> points) {
            this.count = count;
            list = points;
            list.Add(list.First());

            perimeter = calcPerimeter();
            square = calcArea();
            centerMass = massCenter();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Polygon: Area = {0}, Perimeter = {1}, center of mass in ({2},{3})", square, perimeter, centerMass.x, centerMass.y);
            return sb.ToString();
        }

        public override double calcPerimeter() {
            double res = 0;
            Point[] arr = list.ToArray();
            for(int i = 0; i < list.Count-1; ++i)
                res += Math.Sqrt(Math.Pow(Math.Abs(arr[i+1].x - arr[i].x), 2) + Math.Pow(Math.Abs(arr[i+1].y - arr[i].y), 2));
            return res;
        }

        public override double calcArea() {
            double res = 0;
            Point[] arr = list.ToArray();
            for(int i = 0; i < list.Count-1; ++i)
                res += ((arr[i+1].x - arr[i].x) * (arr[i+1].y + arr[i].y) );
            return Math.Abs(res / 2);
        }

        public override Point massCenter() {
            Point[] arr = list.ToArray();
            double tr_area;
            Point tr_center;
            Point result = new Point(0,0);
            for (int i = 2; i < list.Count - 1; ++i) {
                tr_area = triangle_area(arr[i - 2], arr[i - 1], arr[i]);
                tr_center = triangle_center(arr[i - 2], arr[i - 1], arr[i]);
                result.x += (tr_area / square) * tr_center.x;
                result.y += (tr_area / square) * tr_center.y;
            }
            return result;
        }

        public double triangle_area(Point p1, Point p2, Point p3) {
            return Math.Abs((p2.x - p1.x) * (p3.y - p1.y) - (p2.y - p1.y) * (p3.x - p1.x)) / 2.0;
        }

        public Point triangle_center(Point p1, Point p2, Point p3) {
            return new Point((p1.x + p2.x + p3.x) / 3.0, (p1.y + p2.y + p3.y) / 3.0);
        }
    }
}
