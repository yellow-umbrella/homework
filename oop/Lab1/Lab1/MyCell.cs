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

        public MyCell(int row, int column)
        {
            this.name = CreateCellName(column, row);
            this.row = row;
            col = column;
            val = 0;
            exp = "";
        }

        public static string CreateColumnName(int i)
        {
            string name = "";
            const int codeA = 65;
            if (i < maxLetter)
            {
                char c = (char)(codeA + i);
                return name + c;
            }
            name += (char)(i / maxLetter + codeA - 1);
            name += (char)(i % maxLetter + codeA);

            return name;
        }

        public static string CreateCellName(int col, int row)
        {
            return CreateColumnName(col) + row.ToString();
        }

        const int maxLetter = 26;

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
