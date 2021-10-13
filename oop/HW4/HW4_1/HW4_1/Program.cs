using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4_1
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

    class EquilateralTriangle : Triangle
    {
        private double area;

        public EquilateralTriangle(double a) : base(a, a, a) { }

        public double Area
        {
            get => area = Math.Sqrt(3) * A * A / 4;
        }

        public void ChangeSides(double a)
        {
            ChangeSides(a, a, a);
        }
    }

    class Triangle
    {
        private double a;
        public double A { get => a; }

        private double b;
        public double B { get => b; }

        private double c;
        public double C { get => c; }

        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public void ChangeSides(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double Perimetr { get => a + b + c; }

        public double Alfa { get => Math.Acos((b * b + c * c - a * a) / (2 * b * c)); }
        public double Beta { get => Math.Acos((a * a + c * c - b * b) / (2 * a * c)); }
        public double Gamma { get => Math.Acos((b * b + a * a - b * b) / (2 * b * a)); }
    }
}
