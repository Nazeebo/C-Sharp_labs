using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatioLibrary{
    public class DenominatorException : ArgumentException {
        public DenominatorException(string message) : base(message) { }
    }

    public class Ratio {
        private int gcd(int a, int b) {
            if (b == 0)
                return a;
            return gcd(b, a % b);
        }

        private int numerator { get; set; }
        private int denominator { get; set; }

        public Ratio(int num, int den) {
            if (den == 0) throw new DenominatorException("Знаменатель не может равняться нулю");
            int k = gcd(num, den);

            if (k > 1) {
                num /= k;
                den /= k;
            }

            numerator = num;
            denominator = den;
        }


        public static Ratio operator +(Ratio r1, Ratio r2) {
            return new Ratio(r1.numerator * r2.denominator + r2.numerator * r1.denominator, r1.denominator * r2.denominator);
        }

        public static Ratio operator -(Ratio r1, Ratio r2) {
            return new Ratio(r1.numerator * r2.denominator - r2.numerator * r1.denominator, r1.denominator * r2.denominator);
        }

        public static Ratio operator +(Ratio r) { 
            return new Ratio(r.numerator, r.denominator);
        }

        public static Ratio operator -(Ratio r) {
            return new Ratio(-r.numerator, r.denominator);
        }

        public static Ratio operator *(Ratio r1, Ratio r2) {
            return new Ratio(r1.numerator * r2.numerator, r1.denominator * r2.denominator);
        }

        public static Ratio operator /(Ratio r1, Ratio r2) {
            if(r2.numerator == 0) throw new DenominatorException("В результате деление у дроби был бы нулевой знаменатель");
            return new Ratio(r1.numerator * r2.denominator, r1.denominator * r2.numerator);
        }

        public double ToDouble() {
            if (denominator == 0) throw new DenominatorException("Знаменатель не должен быть равен нулю");
            return (double) numerator / denominator;
        }

        public override string ToString() {
            return string.Format("{0}/{1}", numerator, denominator);
        }
    }
}
