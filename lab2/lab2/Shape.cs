using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2 {
    struct Point {
        public double x;
        public double y;
        public Point(double x, double y) {
            this.x = x;
            this.y = y;
        }
    }

    abstract class Shape {
        abstract public double calcArea();
        abstract public double calcPerimeter();
        abstract public Point massCenter();
        protected double square { get; set; }
        protected double perimeter{ get; set;}
        protected Point centerMass { get; set; }
    }
}
