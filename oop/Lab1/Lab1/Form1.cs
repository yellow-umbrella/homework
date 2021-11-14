using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            createDataGridView(4, 4);
            spreadsheet = new Spreadsheet(dgv.ColumnCount, dgv.RowCount);
            this.ActiveControl = dgv;
        }

        private Spreadsheet spreadsheet;

        private void createDataGridView(int columns, int rows)
        {
            for (int i = 0; i < columns; ++i)
            {
                DataGridViewColumn col = new DataGridViewColumn();
                col.HeaderText = MyCell.CreateColumnName(i);
                col.Name = col.HeaderText;
                MyCell cell = new MyCell();
                col.CellTemplate = cell;
                dgv.Columns.Add(col);
            }

            for (int i = 0; i < rows; ++i)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.HeaderCell.Value = i.ToString();
                dgv.Rows.Add(row);
            }
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.HeaderCell.Value = dgv.RowCount.ToString();
            dgv.Rows.Add(row);
            spreadsheet.AddRow();
        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            DataGridViewColumn col = new DataGridViewColumn();
            col.HeaderText = MyCell.CreateColumnName(dgv.ColumnCount);
            col.Name = col.HeaderText;
            col.CellTemplate = new MyCell();
            dgv.Columns.Add(col);
            spreadsheet.AddColumn();
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            if (spreadsheet.getCellExp(row, col) != "")
            {
                textBox1.Text = spreadsheet.getCellExp(row, col);
                dgv[e.ColumnIndex, e.RowIndex].Value = spreadsheet.getCellVal(row, col).ToString();
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
            string cellName = MyCell.CreateCellName(col, row);
            spreadsheet.setCellExp(cellName, textBox1.Text);
            refresh(cellName);
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
                spreadsheet.Save(filename);
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
            if (!spreadsheet.Load(filename))
            {
                MessageBox.Show("Помилка читання з файлу.");
            }
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            createDataGridView(spreadsheet.Columns, spreadsheet.Rows);
            dgv.Refresh();
            this.ActiveControl = dgv;
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            textBox1.Text = spreadsheet.getCellExp(row, col);
            refresh("A0");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Програму виконала: Нємкевич Дар'я, студентка групи К-25.\n" +
                "Щоб ввести вираз клікніть на потрібну клітинку та почніть друкувати, щоб побачити результат клікніть на буль-яку іншу клітинку.\n" +
                "Доступні операції: +, -, *, /; inc(), dec(); ^; унарні +, -.");
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            if (!spreadsheet.DeleteRow())
            {
                MessageBox.Show("Неможливо видалити рядок.");
                return;
            }

            
            dgv.Rows.RemoveAt(dgv.RowCount - 1);
            refresh("A0");
        }

        private void buttonDeleteColumn_Click(object sender, EventArgs e)
        {
            if (!spreadsheet.DeleteColumn())
            {
                MessageBox.Show("Неможливо видалити стовпчик.");
                return;
            }
           
            dgv.Columns.RemoveAt(dgv.ColumnCount - 1);
            refresh("A0");
        }

        private void refresh(string cellName)
        {
            if (!spreadsheet.Recalculate(cellName))
            {
                MessageBox.Show("Некоректний ввід.");
                spreadsheet.Recalculate(cellName);
            }

            for (int i = 0; i < dgv.RowCount; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    cellName = MyCell.CreateCellName(j, i);
                    if (spreadsheet.getCellExp(i, j) == "")
                    {
                        dgv[j, i].Value = null;
                    }
                    else
                    {
                        dgv[j, i].Value = spreadsheet.getCellVal(i, j);
                    }
                }
            }
        }

    }
}
