using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
            context = new Context();
            context.SetStrategy(new DomStrategy(context.File));
            radioButtonDom.Checked = true;
            fillComboBoxes();
        }

        private void fillComboBoxes()
        {
            HashSet<string> attributes = context.GetAttributes("ClassName");
            foreach (string atrr in attributes)
            {
                comboBoxName.Items.Add(atrr);
            }

            attributes = context.GetAttributes("SeatsNum");
            foreach (string atrr in attributes)
            {
                comboBoxSeats.Items.Add(atrr);
            }

            attributes = context.GetAttributes("DayName");
            foreach (string atrr in attributes)
            {
                comboBoxDay.Items.Add(atrr);
            }

            attributes = context.GetAttributes("PairNum");
            foreach (string atrr in attributes)
            {
                comboBoxPair.Items.Add(atrr);
            }

            attributes = context.GetAttributes("Professor");
            foreach (string atrr in attributes)
            {
                comboBoxProfessor.Items.Add(atrr);
            }
        }

        Context context;

        private void radioButtonDom_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDom.Checked)
            {
                context.SetStrategy(new DomStrategy(context.File));
            }
        }

        private void radioButtonSax_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSax.Checked)
            {
                context.SetStrategy(new SaxStrategy(context.File));
            }
        }

        private void radioButtonLinq_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLinq.Checked)
            {
                context.SetStrategy(new LinqStrategy(context.File));
            }
        }

        private void makeQuery()
        {
            List<string> queryList = new List<string>()
            {
                comboBoxName.Text, comboBoxSeats.Text, comboBoxDay.Text, comboBoxPair.Text, comboBoxProfessor.Text
            };
            richTextBox1.Text = context.Find(queryList);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            makeQuery();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            context.ConvertToHtml();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            comboBoxName.Text = "";
            comboBoxSeats.Text = "";
            comboBoxDay.Text = "";
            comboBoxPair.Text = "";
            comboBoxProfessor.Text = "";
        }
    }
}
