using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4_1
{
    public partial class Form1 : Form
    {
        public Form1(int maxSide)
        {
            InitializeComponent();
            this.maxSide = maxSide - 1;
            rand = new Random();
        }

        private EquilateralTriangle tr;
        private int maxSide;
        private Random rand;

        private void button1_Click(object sender, EventArgs e)
        {
            tr = new EquilateralTriangle(maxSide * rand.NextDouble() + 1);

            String area = "Area: " + tr.Area.ToString("F") + "\n";
            String perimetr = "Perimetr: " + tr.Perimetr.ToString("F") + "\n";
            String sides = "Sides: " + tr.A.ToString("F") + "; " +
                tr.B.ToString("F") + "; " + tr.C.ToString("F") + "\n";
            String angles = "Angles: " + (tr.Alfa * 180 / Math.PI).ToString("F") +
                "; " + (tr.Alfa * 180 / Math.PI).ToString("F") +
                "; " + (tr.Alfa * 180 / Math.PI).ToString("F") + "\n";

            label1.Text = sides + angles + perimetr + area;
        }

    }
}
