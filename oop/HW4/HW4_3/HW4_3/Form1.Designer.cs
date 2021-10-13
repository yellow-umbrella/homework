
namespace HW4_3
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
            this.buttonRightTriangle = new System.Windows.Forms.Button();
            this.buttonIsoscelesTriangle = new System.Windows.Forms.Button();
            this.labelRightTriangle = new System.Windows.Forms.Label();
            this.labelIsoscelesTriangle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonRightTriangle
            // 
            this.buttonRightTriangle.Location = new System.Drawing.Point(12, 12);
            this.buttonRightTriangle.Name = "buttonRightTriangle";
            this.buttonRightTriangle.Size = new System.Drawing.Size(216, 34);
            this.buttonRightTriangle.TabIndex = 0;
            this.buttonRightTriangle.Text = "create right triangle";
            this.buttonRightTriangle.UseVisualStyleBackColor = true;
            this.buttonRightTriangle.Click += new System.EventHandler(this.buttonRightTriangle_Click);
            // 
            // buttonIsoscelesTriangle
            // 
            this.buttonIsoscelesTriangle.Location = new System.Drawing.Point(402, 12);
            this.buttonIsoscelesTriangle.Name = "buttonIsoscelesTriangle";
            this.buttonIsoscelesTriangle.Size = new System.Drawing.Size(216, 34);
            this.buttonIsoscelesTriangle.TabIndex = 1;
            this.buttonIsoscelesTriangle.Text = "create isosceles triangle";
            this.buttonIsoscelesTriangle.UseVisualStyleBackColor = true;
            this.buttonIsoscelesTriangle.Click += new System.EventHandler(this.buttonIsoscelesTriangle_Click);
            // 
            // labelRightTriangle
            // 
            this.labelRightTriangle.AutoSize = true;
            this.labelRightTriangle.Location = new System.Drawing.Point(13, 53);
            this.labelRightTriangle.Name = "labelRightTriangle";
            this.labelRightTriangle.Size = new System.Drawing.Size(52, 25);
            this.labelRightTriangle.TabIndex = 2;
            this.labelRightTriangle.Text = "none";
            // 
            // labelIsoscelesTriangle
            // 
            this.labelIsoscelesTriangle.AutoSize = true;
            this.labelIsoscelesTriangle.Location = new System.Drawing.Point(402, 53);
            this.labelIsoscelesTriangle.Name = "labelIsoscelesTriangle";
            this.labelIsoscelesTriangle.Size = new System.Drawing.Size(52, 25);
            this.labelIsoscelesTriangle.TabIndex = 3;
            this.labelIsoscelesTriangle.Text = "none";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelIsoscelesTriangle);
            this.Controls.Add(this.labelRightTriangle);
            this.Controls.Add(this.buttonIsoscelesTriangle);
            this.Controls.Add(this.buttonRightTriangle);
            this.Name = "Form1";
            this.Text = "HW4_3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRightTriangle;
        private System.Windows.Forms.Button buttonIsoscelesTriangle;
        private System.Windows.Forms.Label labelRightTriangle;
        private System.Windows.Forms.Label labelIsoscelesTriangle;
    }
}

