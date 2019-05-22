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
        private static int gcd(int a, int b) {
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
            int k = gcd(r1.denominator,r2.denominator);
            return new Ratio(r1.numerator * (r2.denominator / k) + r2.numerator * (r1.denominator / k), r1.denominator * r2.denominator / k);
        }

        public static Ratio operator -(Ratio r1, Ratio r2) {
            int k = gcd(r1.denominator, r2.denominator);
            return new Ratio(r1.numerator * (r2.denominator / k) - r2.numerator * (r1.denominator / k), r1.denominator * r2.denominator / k);
        }

        public static Ratio operator +(Ratio r) { 
            return new Ratio(r.numerator, r.denominator);
        }

        public static Ratio operator -(Ratio r) {
            return new Ratio(-r.numerator, r.denominator);
        }

        public static Ratio operator *(Ratio r1, Ratio r2) {
            int k1 = gcd(r1.numerator, r2.denominator);
            int k2 = gcd(r2.numerator, r1.denominator);

            return new Ratio(r1.numerator/k1 * r2.numerator/k2, r1.denominator/k2 * r2.denominator/k1);
        }

        public static Ratio operator /(Ratio r1, Ratio r2) {
            if(r2.numerator == 0) throw new DenominatorException("В результате деление у дроби был бы нулевой знаменатель");
            int num = gcd(r1.numerator, r2.numerator);
            int den = gcd(r1.denominator, r2.denominator);

            return new Ratio(r1.numerator/num * r2.denominator/den, r1.denominator/den * r2.numerator/num);
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
