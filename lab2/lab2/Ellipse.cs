using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2 {
    class Ellipse : Shape {
        private Point focus1 { get; set; }
        private Point focus2 { get; set; }
        private double big_half_axis { get; set; }
        private double small_half_axis { get; set; }
        private double half_focus_distance { get; set; }
        private double e { get; set; }

        public Ellipse(Point focus1, Point focus2, double axis) {
            this.focus1 = focus1;
            this.focus2 = focus2;
            big_half_axis = axis;

            half_focus_distance = Math.Sqrt(Math.Pow(Math.Abs(focus1.x - focus2.x), 2) + Math.Pow(Math.Abs(focus1.y - focus2.y), 2))/2;
            e = half_focus_distance / big_half_axis;
            small_half_axis = big_half_axis * Math.Sqrt(1 - Math.Pow(e, 2));

            perimeter = calcPerimeter();
            square = calcArea();
            centerMass = massCenter();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Ellipse: Area = {0}, Perimeter = {1}, center of mass in ({2},{3})", square, perimeter, centerMass.x, centerMass.y);
            return sb.ToString();
        }

        public override double calcPerimeter() {
            double tmp = Math.Pow(big_half_axis - small_half_axis, 2);
            return 4 * (Math.PI * big_half_axis * small_half_axis + tmp) / (big_half_axis + small_half_axis);
        }

        public override double calcArea() {
            return Math.PI * big_half_axis * small_half_axis;
        }

        public override Point massCenter() {
            return new Point(focus1.x + (focus2.x - focus1.x) / 2, focus1.y + (focus2.y - focus1.y) / 2);
        }
    }
}
