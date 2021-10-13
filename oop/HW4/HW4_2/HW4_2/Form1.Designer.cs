
namespace HW4_2
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
            this.buttonTriangle = new System.Windows.Forms.Button();
            this.buttonCircle = new System.Windows.Forms.Button();
            this.buttonRectangle = new System.Windows.Forms.Button();
            this.buttonSquare = new System.Windows.Forms.Button();
            this.buttonRhomb = new System.Windows.Forms.Button();
            this.labelTriangle = new System.Windows.Forms.Label();
            this.labelCircle = new System.Windows.Forms.Label();
            this.labelRectangle = new System.Windows.Forms.Label();
            this.labelSquare = new System.Windows.Forms.Label();
            this.labelRhomb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonTriangle
            // 
            this.buttonTriangle.Location = new System.Drawing.Point(0, 0);
            this.buttonTriangle.Name = "buttonTriangle";
            this.buttonTriangle.Size = new System.Drawing.Size(157, 34);
            this.buttonTriangle.TabIndex = 0;
            this.buttonTriangle.Text = "create triangle";
            this.buttonTriangle.UseVisualStyleBackColor = true;
            this.buttonTriangle.Click += new System.EventHandler(this.buttonTriangle_Click);
            // 
            // buttonCircle
            // 
            this.buttonCircle.Location = new System.Drawing.Point(419, 5);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(157, 34);
            this.buttonCircle.TabIndex = 1;
            this.buttonCircle.Text = "create circle";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            // 
            // buttonRectangle
            // 
            this.buttonRectangle.Location = new System.Drawing.Point(0, 122);
            this.buttonRectangle.Name = "buttonRectangle";
            this.buttonRectangle.Size = new System.Drawing.Size(157, 34);
            this.buttonRectangle.TabIndex = 2;
            this.buttonRectangle.Text = "create rectangle";
            this.buttonRectangle.UseVisualStyleBackColor = true;
            this.buttonRectangle.Click += new System.EventHandler(this.buttonRectangle_Click);
            // 
            // buttonSquare
            // 
            this.buttonSquare.Location = new System.Drawing.Point(419, 118);
            this.buttonSquare.Name = "buttonSquare";
            this.buttonSquare.Size = new System.Drawing.Size(157, 34);
            this.buttonSquare.TabIndex = 3;
            this.buttonSquare.Text = "create square";
            this.buttonSquare.UseVisualStyleBackColor = true;
            this.buttonSquare.Click += new System.EventHandler(this.buttonSquare_Click);
            // 
            // buttonRhomb
            // 
            this.buttonRhomb.Location = new System.Drawing.Point(0, 251);
            this.buttonRhomb.Name = "buttonRhomb";
            this.buttonRhomb.Size = new System.Drawing.Size(157, 34);
            this.buttonRhomb.TabIndex = 4;
            this.buttonRhomb.Text = "create rhomb";
            this.buttonRhomb.UseVisualStyleBackColor = true;
            this.buttonRhomb.Click += new System.EventHandler(this.buttonRhomb_Click);
            // 
            // labelTriangle
            // 
            this.labelTriangle.AutoSize = true;
            this.labelTriangle.Location = new System.Drawing.Point(163, 5);
            this.labelTriangle.Name = "labelTriangle";
            this.labelTriangle.Size = new System.Drawing.Size(52, 25);
            this.labelTriangle.TabIndex = 5;
            this.labelTriangle.Text = "none";
            // 
            // labelCircle
            // 
            this.labelCircle.AutoSize = true;
            this.labelCircle.Location = new System.Drawing.Point(582, 9);
            this.labelCircle.Name = "labelCircle";
            this.labelCircle.Size = new System.Drawing.Size(52, 25);
            this.labelCircle.TabIndex = 6;
            this.labelCircle.Text = "none";
            // 
            // labelRectangle
            // 
            this.labelRectangle.AutoSize = true;
            this.labelRectangle.Location = new System.Drawing.Point(163, 127);
            this.labelRectangle.Name = "labelRectangle";
            this.labelRectangle.Size = new System.Drawing.Size(52, 25);
            this.labelRectangle.TabIndex = 7;
            this.labelRectangle.Text = "none";
            // 
            // labelSquare
            // 
            this.labelSquare.AutoSize = true;
            this.labelSquare.Location = new System.Drawing.Point(582, 127);
            this.labelSquare.Name = "labelSquare";
            this.labelSquare.Size = new System.Drawing.Size(52, 25);
            this.labelSquare.TabIndex = 8;
            this.labelSquare.Text = "none";
            // 
            // labelRhomb
            // 
            this.labelRhomb.AutoSize = true;
            this.labelRhomb.Location = new System.Drawing.Point(163, 256);
            this.labelRhomb.Name = "labelRhomb";
            this.labelRhomb.Size = new System.Drawing.Size(52, 25);
            this.labelRhomb.TabIndex = 9;
            this.labelRhomb.Text = "none";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelRhomb);
            this.Controls.Add(this.labelSquare);
            this.Controls.Add(this.labelRectangle);
            this.Controls.Add(this.labelCircle);
            this.Controls.Add(this.labelTriangle);
            this.Controls.Add(this.buttonRhomb);
            this.Controls.Add(this.buttonSquare);
            this.Controls.Add(this.buttonRectangle);
            this.Controls.Add(this.buttonCircle);
            this.Controls.Add(this.buttonTriangle);
            this.Name = "Form1";
            this.Text = "HW4_2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTriangle;
        private System.Windows.Forms.Button buttonCircle;
        private System.Windows.Forms.Button buttonRectangle;
        private System.Windows.Forms.Button buttonSquare;
        private System.Windows.Forms.Button buttonRhomb;
        private System.Windows.Forms.Label labelTriangle;
        private System.Windows.Forms.Label labelCircle;
        private System.Windows.Forms.Label labelRectangle;
        private System.Windows.Forms.Label labelSquare;
        private System.Windows.Forms.Label labelRhomb;
    }
}

