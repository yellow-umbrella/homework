using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4_3
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
        private const double maxAngle = 179;
        private Random rand;

        private void buttonRightTriangle_Click(object sender, EventArgs e)
        {
            double a = rand.NextDouble() * (maxSide - 1) + 1;
            double b = rand.NextDouble() * (maxSide - 1) + 1;

            Triangle tr = new RightTriangle(a, b);

            labelRightTriangle.Text = tr.Info();
        }

        private void buttonIsoscelesTriangle_Click(object sender, EventArgs e)
        {
            double a = rand.NextDouble() * (maxSide - 1) + 1;
            double angle = (1 + rand.NextDouble() * (maxAngle - 1)) * Math.PI / 180;

            Triangle tr = new IsoscelesTriangle(a, angle);

            labelIsoscelesTriangle.Text = tr.Info();
        }
    }
}
