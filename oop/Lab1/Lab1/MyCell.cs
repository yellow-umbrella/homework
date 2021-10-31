using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public class MyCell : DataGridViewTextBoxCell
    {
        public MyCell() { }

        public MyCell(string name, int row, int column)
        {
            this.name = name;
            this.row = row;
            col = column;
            val = 0;
            exp = "";
        }

        private string name;
        public string Name { get => name; set => name = value; }

        private double val;
        public double Val { get => val; set => val = value; }

        private string exp;
        public string Exp { get => exp; set => exp = value; }

        private int row;
        public int Row { get => row; set => row = value; }

        private int col;
        public int Column { get => col; set => col = value; }

        private int color = 0;
        public int Color { get => color; set => color = value; }

    }
}
