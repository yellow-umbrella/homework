using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4_2
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

            Figure f1 = new Triangle(3, 4, 5);
            Figure f2 = new Circle(4);
            Figure f3 = new Rectangle(3, 4);
            Figure f4 = new Square(3);
            Figure f5 = new Rhomb(3, Math.PI / 6);
        }
    }

    abstract class Figure
    {
        public abstract double Area();
        public abstract double Perimetr();

        public virtual string Info()
        {
            String info = "Area: " + Area().ToString("F") + "\n" 
                + "Perimetr: " + Perimetr().ToString("F") + "\n";
            return info;
        }
    }

    class Triangle : Figure
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double a, double b, double c) : base()
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }


        public override double Area()
        {
            double p = Perimetr() / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public override double Perimetr()
        {
            return a + b + c;
        }

        public override string Info()
        {
            String sides = "Sides: " + a.ToString("F") + "; " +
                b.ToString("F") + "; " + c.ToString("F") + "\n";
            return sides + base.Info();
        }
    }

    class Circle : Figure
    {
        private double r;

        public Circle(double r) : base()
        {
            this.r = r;
        }

        public override double Area()
        {
            return Math.PI * r * r;
        }

        public override double Perimetr()
        {
            return 2 * Math.PI * r;
        }

        public override string Info()
        {
            String radius = "Radius: " + r.ToString("F") + "\n";
            return radius + base.Info();
        }
    }

    class Rectangle : Figure
    {
        private double a;
        private double b;

        public Rectangle(double a, double b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override double Area()
        {
            return a * b;
        }

        public override double Perimetr()
        {
            return 2 * (a + b);
        }

        public override string Info()
        {
            String sides = "Sides: " + a.ToString("F")
                + "; " + b.ToString("F") + "\n";
            return sides + base.Info();
        }
    }

    class Square : Figure
    {
        private double a;

        public Square(double a) : base()
        {
            this.a = a;
        }

        public override double Area()
        {
            return a * a;
        }

        public override double Perimetr()
        {
            return 4 * a;
        }

        public override string Info()
        {
            String sides = "Side: " + a.ToString("F") + "\n";
            return sides + base.Info();
        }
    }

    class Rhomb : Figure
    {
        private double a;
        private double angle;

        public Rhomb(double a, double angle) : base()
        {
            this.a = a;
            this.angle = angle;
        }

        public override double Area()
        {
            return a * a * Math.Sin(angle);
        }

        public override double Perimetr()
        {
            return 4 * a;
        }

        public override string Info()
        {
            String sides = "Side: " + a.ToString("F") + "\n";
            String angles = "Angle: " + (angle * 180 / Math.PI).ToString("F") + "\n";
            return sides + angles + base.Info();
        }
    }
}
