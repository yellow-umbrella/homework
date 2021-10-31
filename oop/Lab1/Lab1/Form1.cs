using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            createDataGridView(4, 4);
            dictionary = new Dictionary<string, MyCell>();

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    string cellName = createColumnName(j) + i.ToString();
                    MyCell cell = new MyCell(cellName, i, j);
                    dictionary.Add(cellName, cell);
                }
            }

            this.ActiveControl = dgv;
        }

        private int columns = 0;
        private int rows = 0;
        public Dictionary<string, MyCell> dictionary;

        private void createDataGridView(int _columns, int _rows)
        {
            for (int i = 0; i < _columns; ++i)
            {
                DataGridViewColumn col = new DataGridViewColumn();
                col.HeaderText = createColumnName(i);
                col.Name = col.HeaderText;
                MyCell cell = new MyCell();
                col.CellTemplate = cell;
                dgv.Columns.Add(col);
                columns++;

            }

            for (int i = 0; i < _rows; ++i)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.HeaderCell.Value = i.ToString();
                dgv.Rows.Add(row);
                rows++;
            }
        }

        const int maxLetter = 26;
        private string createColumnName(int i)
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

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.HeaderCell.Value = rows.ToString();
            dgv.Rows.Add(row);
            for (int i = 0; i < columns; ++i)
            {
                string cellName = createCellName(i, rows);
                MyCell cell = new MyCell(cellName, rows, i);
                dictionary.Add(cellName, cell);
            }
            rows++;
        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            if (columns - 1 >= maxLetter*maxLetter)
            {
                return;
            }
            DataGridViewColumn col = new DataGridViewColumn();
            col.HeaderText = createColumnName(columns);
            col.Name = col.HeaderText;
            col.CellTemplate = new MyCell();
            dgv.Columns.Add(col);
            for (int i = 0; i < rows; ++i)
            {
                string cellName = createCellName(columns, i);
                MyCell cell = new MyCell(cellName, i, columns);
                dictionary.Add(cellName, cell);
            }
            columns++;
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            string cellName = createCellName(col, row);
            if (dictionary[cellName].Exp != "")
            {
                textBox1.Text = dictionary[cellName].Exp;
                dgv[e.ColumnIndex, e.RowIndex].Value = dictionary[cellName].Val.ToString();
            }
            else
            {
                textBox1.Text = "";
            }
            this.ActiveControl = textBox1;
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            string cellName = createCellName(col, row);
            dictionary[cellName].Exp = textBox1.Text;
            recalculate(cellName);
        }

        private void recalculate(string startCellName)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    string cellName = createCellName(j, i);
                    clearColors();
                    try
                    {
                        CalculateCell(cellName);
                    }
                    catch (Exception ex)
                    {
                        dictionary[startCellName].Val = 0;
                        dictionary[startCellName].Exp = "";
                        MessageBox.Show("Invalid input. " + ex.Message);
                        clearColors();
                        recalculate(startCellName);
                    }
                }
            }
        }

        private void clearColors()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    string cellName = createCellName(j, i);
                    dictionary[cellName].Color = 0;
                }
            }
        }

        public double CalculateCell(string cellName)
        {
            if (!dictionary.ContainsKey(cellName))
            {
                return 0;
            }

            if (dictionary[cellName].Color == 1)
            {
                throw new Exception("Recursion is not allowed.");
            }
            if (dictionary[cellName].Color == 2)
            {
                return dictionary[cellName].Val;
            }

            dictionary[cellName].Color = 1;
            if (dictionary[cellName].Exp == "")
            {
                dgv[dictionary[cellName].Column, dictionary[cellName].Row].Value = null;
                dictionary[cellName].Val = 0;
                dictionary[cellName].Color = 2;
                return 0;
            }

            dictionary[cellName].Val = Calculator.Evaluate(dictionary[cellName].Exp, this);
            dgv[dictionary[cellName].Column, dictionary[cellName].Row].Value = dictionary[cellName].Val;
            dictionary[cellName].Color = 2;
            return dictionary[cellName].Val;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = saveFileDialog1.FileName;
            
            try
            {
                using(StreamWriter streamWriter = new StreamWriter(filename))
                {
                    streamWriter.WriteLine(columns);
                    streamWriter.WriteLine(rows);
                    for (int i = 0; i < columns; i++)
                    {
                        for (int j = 0; j < rows; j++)
                        {
                            string cellName = createCellName(i, j);
                            streamWriter.WriteLine(dictionary[cellName].Exp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving the file. " + ex.Message);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialog1.FileName;
            string cellName;
            Dictionary<string, MyCell> _dictionary = new Dictionary<string, MyCell>();
            int _rows;
            int _columns;
            try
            {
                using (StreamReader streamReader = new StreamReader(filename))
                {
                    string line;
                    line =  streamReader.ReadLine();
                    _columns = int.Parse(line);
                    line = streamReader.ReadLine();
                    _rows = int.Parse(line);
                    for (int i = 0; i < _columns; i++)
                    {
                        for (int j = 0; j < _rows; j++)
                        {
                            cellName = createCellName(i, j);
                            _dictionary[cellName].Exp = streamReader.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while reading from file. " + ex.Message);
                return;
            }
            rows = _rows;
            columns = _columns;
            dictionary = _dictionary;
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            cellName = createCellName(col, row);
            textBox1.Text = dictionary[cellName].Exp;
            recalculate("A0");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Програму виконала: Нємкевич Дар'я, студентка групи К-25.\n" + 
                "Щоб ввести вираз клікніть на потрібну клітинку та почніть друкувати, щоб побачити результат клікніть на буль-яку іншу клітинку.");
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            if (rows == 1)
            {
                MessageBox.Show("Unable to delete row");
                return;
            }

            rows--;

            for (int i = 0; i < columns; i++)
            {
                string cellName = createCellName(i, rows);
                dictionary.Remove(cellName);
            }
            dgv.Rows.RemoveAt(rows);
            recalculate("A0");
        }

        private string createCellName(int col, int row)
        {
            return createColumnName(col) + row.ToString();
        }

        private void buttonDeleteColumn_Click(object sender, EventArgs e)
        {
            if (columns == 1)
            {
                MessageBox.Show("Unable to delete column");
                return;
            }
           
            columns--;

            for (int i = 0; i < rows; i++)
            {
                string cellName = createCellName(columns, i);
                dictionary.Remove(cellName);
            }
            dgv.Columns.RemoveAt(columns);
            recalculate("A0");
        }
    }
}
