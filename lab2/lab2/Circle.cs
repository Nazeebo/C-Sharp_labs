using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2 {
    class Circle : Shape {
        private Point center{ get; set;}
        private double radius{ get; set; }

        public Circle(Point center, double radius) {
            this.center = center;
            this.radius = radius;

            perimeter = calcPerimeter();
            square = calcArea();
            centerMass = massCenter();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Circle: Area = {0}, Perimeter = {1}, center of mass in ({2},{3})", square, perimeter, centerMass.x, centerMass.y);
            return sb.ToString();
        }

        public override double calcPerimeter() {
            return Math.PI * radius * 2;
        }

        public override double calcArea() {
            return Math.PI * Math.Pow(radius, 2);
        }

        public override Point massCenter() {
            return center;
        }
    }
}
