
namespace Lab2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonDom = new System.Windows.Forms.RadioButton();
            this.radioButtonSax = new System.Windows.Forms.RadioButton();
            this.radioButtonLinq = new System.Windows.Forms.RadioButton();
            this.comboBoxName = new System.Windows.Forms.ComboBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBoxSeats = new System.Windows.Forms.ComboBox();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxPair = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxProfessor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radioButtonDom
            // 
            this.radioButtonDom.AutoSize = true;
            this.radioButtonDom.Location = new System.Drawing.Point(12, 514);
            this.radioButtonDom.Name = "radioButtonDom";
            this.radioButtonDom.Size = new System.Drawing.Size(80, 29);
            this.radioButtonDom.TabIndex = 0;
            this.radioButtonDom.TabStop = true;
            this.radioButtonDom.Text = "DOM";
            this.radioButtonDom.UseVisualStyleBackColor = true;
            this.radioButtonDom.CheckedChanged += new System.EventHandler(this.radioButtonDom_CheckedChanged);
            // 
            // radioButtonSax
            // 
            this.radioButtonSax.AutoSize = true;
            this.radioButtonSax.Location = new System.Drawing.Point(109, 514);
            this.radioButtonSax.Name = "radioButtonSax";
            this.radioButtonSax.Size = new System.Drawing.Size(70, 29);
            this.radioButtonSax.TabIndex = 1;
            this.radioButtonSax.TabStop = true;
            this.radioButtonSax.Text = "SAX";
            this.radioButtonSax.UseVisualStyleBackColor = true;
            this.radioButtonSax.CheckedChanged += new System.EventHandler(this.radioButtonSax_CheckedChanged);
            // 
            // radioButtonLinq
            // 
            this.radioButtonLinq.AutoSize = true;
            this.radioButtonLinq.Location = new System.Drawing.Point(194, 514);
            this.radioButtonLinq.Name = "radioButtonLinq";
            this.radioButtonLinq.Size = new System.Drawing.Size(77, 29);
            this.radioButtonLinq.TabIndex = 2;
            this.radioButtonLinq.TabStop = true;
            this.radioButtonLinq.Text = "LINQ";
            this.radioButtonLinq.UseVisualStyleBackColor = true;
            this.radioButtonLinq.CheckedChanged += new System.EventHandler(this.radioButtonLinq_CheckedChanged);
            // 
            // comboBoxName
            // 
            this.comboBoxName.FormattingEnabled = true;
            this.comboBoxName.Location = new System.Drawing.Point(12, 66);
            this.comboBoxName.Name = "comboBoxName";
            this.comboBoxName.Size = new System.Drawing.Size(182, 33);
            this.comboBoxName.TabIndex = 4;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(12, 560);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(112, 34);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Пошук";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(147, 560);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 34);
            this.button2.TabIndex = 6;
            this.button2.Text = "Конвертувати";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(300, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(645, 582);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // comboBoxSeats
            // 
            this.comboBoxSeats.FormattingEnabled = true;
            this.comboBoxSeats.Location = new System.Drawing.Point(12, 136);
            this.comboBoxSeats.Name = "comboBoxSeats";
            this.comboBoxSeats.Size = new System.Drawing.Size(182, 33);
            this.comboBoxSeats.TabIndex = 8;
            // 
            // comboBoxDay
            // 
            this.comboBoxDay.FormattingEnabled = true;
            this.comboBoxDay.Location = new System.Drawing.Point(12, 214);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(182, 33);
            this.comboBoxDay.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Номер кабінету";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Кількість місць";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "День тижня";
            // 
            // comboBoxPair
            // 
            this.comboBoxPair.FormattingEnabled = true;
            this.comboBoxPair.Location = new System.Drawing.Point(12, 289);
            this.comboBoxPair.Name = "comboBoxPair";
            this.comboBoxPair.Size = new System.Drawing.Size(182, 33);
            this.comboBoxPair.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "Номер пари";
            // 
            // comboBoxProfessor
            // 
            this.comboBoxProfessor.FormattingEnabled = true;
            this.comboBoxProfessor.Location = new System.Drawing.Point(12, 370);
            this.comboBoxProfessor.Name = "comboBoxProfessor";
            this.comboBoxProfessor.Size = new System.Drawing.Size(182, 33);
            this.comboBoxProfessor.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 342);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "Викладач";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 606);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxProfessor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxPair);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDay);
            this.Controls.Add(this.comboBoxSeats);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.comboBoxName);
            this.Controls.Add(this.radioButtonLinq);
            this.Controls.Add(this.radioButtonSax);
            this.Controls.Add(this.radioButtonDom);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonDom;
        private System.Windows.Forms.RadioButton radioButtonSax;
        private System.Windows.Forms.RadioButton radioButtonLinq;
        private System.Windows.Forms.ComboBox comboBoxName;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboBoxSeats;
        private System.Windows.Forms.ComboBox comboBoxDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxPair;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxProfessor;
        private System.Windows.Forms.Label label5;
    }
}

