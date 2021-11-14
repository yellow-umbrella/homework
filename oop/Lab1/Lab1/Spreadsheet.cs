using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Antlr4.Runtime;

namespace Lab1
{
    public class Spreadsheet
    {
        private Dictionary<string, MyCell> dictionary;
        private int columns = 0;
        public int Columns { get => columns; }
        private int rows = 0;
        public int Rows { get => rows; }

        public Spreadsheet(int columns, int rows)
        {
            dictionary = new Dictionary<string, MyCell>();
            this.columns = columns;
            this.rows = rows;

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    MyCell cell = new MyCell(i, j);
                    dictionary.Add(cell.Name, cell);
                }
            }
        }

        public bool Recalculate(string startCellName)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    string cellName = MyCell.CreateCellName(j, i);
                    clearColors();
                    try
                    {
                        CalculateCell(cellName);
                    }
                    catch (Exception ex)
                    {
                        dictionary[startCellName].Val = 0;
                        dictionary[startCellName].Exp = "";
                        return false;
                    }
                }
            }
            return true;
        }

        private void clearColors()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    string cellName = MyCell.CreateCellName(j, i);
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
                dictionary[cellName].Val = 0;
                dictionary[cellName].Color = 2;
                return 0;
            }

            dictionary[cellName].Val = Evaluate(dictionary[cellName].Exp, this);
            dictionary[cellName].Color = 2;
            return dictionary[cellName].Val;
        }

        public bool DeleteColumn()
        {
            if (columns == 1)
            {
                return false;
            }

            columns--;

            for (int i = 0; i < rows; i++)
            {
                string cellName = MyCell.CreateCellName(columns, i);
                dictionary.Remove(cellName);
            }
            return true;
        }

        public bool DeleteRow()
        {
            if (rows == 1)
            {
                return false;
            }

            rows--;

            for (int i = 0; i < columns; i++)
            {
                string cellName = MyCell.CreateCellName(i, rows);
                dictionary.Remove(cellName);
            }
            return true;
        }

        public void AddRow()
        {
            for (int i = 0; i < columns; ++i)
            {
                MyCell cell = new MyCell(rows, i);
                dictionary.Add(cell.Name, cell);
            }
            rows++;
        }

        public void AddColumn()
        {
            for (int i = 0; i < rows; ++i)
            {
                MyCell cell = new MyCell(i, columns);
                dictionary.Add(cell.Name, cell);
            }
            columns++;
        }

        public bool Load(string filename)
        {
            Dictionary<string, MyCell> _dictionary = dictionary;
            int _rows = rows;
            int _columns = columns;
            try
            {
                using (TextFieldParser parser = new TextFieldParser(filename))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    dictionary.Clear();
                    rows = 0;
                    columns = 0;
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        int ind = 0;
                        foreach (string field in fields)
                        {
                            MyCell cell = new MyCell(rows, ind);
                            dictionary.Add(cell.Name, cell);
                            dictionary[cell.Name].Exp = field;
                            ind++;
                        }
                        if (rows == 0)
                        {
                            columns = ind;
                        }
                        else if (columns != ind)
                        {
                            throw new Exception();
                        }
                        rows++;
                    }
                    if (!Recalculate("A0"))
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                rows = _rows;
                columns = _columns;
                dictionary = _dictionary;
                return false;
            }
            return true;
        }

        public void Save(string filename)
        {
            using (StreamWriter streamWriter = new StreamWriter(filename))
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        string cellName = MyCell.CreateCellName(j, i);
                        streamWriter.Write(dictionary[cellName].Exp);
                        if (j != columns - 1)
                        {
                            streamWriter.Write(",");
                        }
                    }
                    streamWriter.WriteLine();
                }
            }
        }

        public double getCellVal(int row, int column)
        {
            return dictionary[MyCell.CreateCellName(column, row)].Val;
        }

        public string getCellExp(int row, int column)
        {
            return dictionary[MyCell.CreateCellName(column, row)].Exp;
        }

        public void setCellExp(string cellName, string exp) 
        {
            dictionary[cellName].Exp = exp;
        }

        public static double Evaluate(string expression, Spreadsheet spreadsheet)
        {
            var lexer = new LabCalculatorLexer(new AntlrInputStream(expression));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowExceptionErrorListener());

            var tokens = new CommonTokenStream(lexer);
            var parser = new LabCalculatorParser(tokens);
            parser.ErrorHandler = new BailErrorStrategy();
            var tree = parser.compileUnit();

            var visitor = new LabCalculatorVisitor(spreadsheet);

            return visitor.Visit(tree);
        }
    }
}
