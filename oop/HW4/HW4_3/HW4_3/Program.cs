using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4_3
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(100));
        }
    }

    abstract class Triangle
    {
        private double a;
        protected double A => a;

        private double b;
        protected double B => b;

        private double gamma;
        protected double Gamma => gamma;

        public Triangle(double a, double b, double gamma)
        {
            this.a = a;
            this.b = b;
            this.gamma = gamma;
        }

        public abstract double Area();
        public abstract double Perimetr();

        public string Info()
        {
            String sides = "Sides: " + a.ToString("F")
                + "; " + b.ToString("F") + "\n";
            String angle = "Angle: " + (gamma * 180 / Math.PI).ToString("F") + "\n";
            String area = "Area: " + Area().ToString("F") + "\n";
            String perimetr =  "Perimetr: " + Perimetr().ToString("F") + "\n";
            return sides + angle + perimetr + area;
        }

    }

    class RightTriangle : Triangle
    {
        public RightTriangle(double leg1, double leg2) : base(leg1, leg2, Math.PI / 2) { }
        public override double Area()
        {
            return A * B / 2;
        }

        public override double Perimetr()
        {
            return A + B + Math.Sqrt(A*A + B*B);
        }
    }

    class IsoscelesTriangle : Triangle
    {
        public IsoscelesTriangle(double leg, double vertexAngle) : base(leg, leg, vertexAngle) { }

        public override double Area()
        {
            return A * A * Math.Sin(Gamma) / 2;
        }

        public override double Perimetr()
        {
            return 2 * A + Math.Sqrt(2*A*A + A*A*Math.Cos(Gamma));
        }
    }
}
