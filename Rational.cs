using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exemQ4A
{
    class Rational
    {
        public int p { get; set; }
        public double q { get; set; }
        public double rational { get; set; }

        public Rational(double rational)
        {
            this.rational = rational;
        }

        public Rational(int p, int q)
        {
            this.p = p;
            this.q = q;
            if (q <= 0 || p % 1 != 0)
            {
                this.rational = 0;
            }
            else
            {
                this.rational = p / q;
            }
        }

        public bool greaterThan(double TMP)
        {
            if(this.rational > TMP)
                return true;

            return false;
        }

        public bool Equals(double TMP)
        {
            if (this.rational == TMP)
                return true;

            return false;
        }

        public static double operator + (Rational r, double TMP)
        {
            return r.rational + TMP;
        }

        public static double operator - (Rational r, double TMP)
        {
            return r.rational - TMP;
        }

        public static double operator * (Rational r, double TMP)
        {
            return r.rational * TMP;
        }

        public int getNumerator()
        {
            return this.p;
        }

        public double getDenominator()
        {
            return this.q;
        }
    }
}
