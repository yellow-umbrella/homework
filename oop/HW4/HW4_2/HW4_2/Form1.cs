using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4_2
{
    public partial class Form1 : Form
    {
        public Form1(double maxSide)
        {
            InitializeComponent();
            this.maxSide = maxSide;
            rand = new Random();
        }

        private double maxSide;
        private const double maxRhombAngle = 89;
        private Random rand;

        private void buttonTriangle_Click(object sender, EventArgs e)
        {
            double a = rand.NextDouble() * (maxSide - 1) + 1;
            double b = rand.NextDouble() * (maxSide - 1) + 1;
            double maxC = Math.Min(a + b, maxSide);
            double minC = Math.Abs(a - b);
            double c = minC + rand.NextDouble() * (maxC - minC);

            Triangle f = new Triangle(a, b, c);

            labelTriangle.Text = f.Info();
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            double r = 1 + rand.NextDouble() * (maxSide - 1);

            Circle f = new Circle(r);

            labelCircle.Text = f.Info();
        }

        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            double a = rand.NextDouble() * (maxSide - 1) + 1;
            double b = rand.NextDouble() * (maxSide - 1) + 1;

            Rectangle f = new Rectangle(a, b);

            labelRectangle.Text = f.Info();
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            double a = rand.NextDouble() * (maxSide - 1) + 1;

            Square f = new Square(a);

            labelSquare.Text = f.Info();
        }

        private void buttonRhomb_Click(object sender, EventArgs e)
        {
            double a = rand.NextDouble() * (maxSide - 1) + 1;
            double angle = (1 + rand.NextDouble() * (maxRhombAngle - 1)) * Math.PI / 180;

            Rhomb f = new Rhomb(a, angle);

            labelRhomb.Text = f.Info();
        }
    }
}
