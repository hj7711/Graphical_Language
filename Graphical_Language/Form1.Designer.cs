
using System;

namespace Graphical_Language
{
    partial class Graphical_Language
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.RUN = new System.Windows.Forms.Button();
            this.SYNTEX = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 369);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(345, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 37);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(345, 305);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // RUN
            // 
            this.RUN.Location = new System.Drawing.Point(13, 415);
            this.RUN.Name = "RUN";
            this.RUN.Size = new System.Drawing.Size(75, 23);
            this.RUN.TabIndex = 2;
            this.RUN.Text = "RUN";
            this.RUN.UseVisualStyleBackColor = true;
            this.RUN.Click += new System.EventHandler(this.button1_Click);
            // 
            // SYNTEX
            // 
            this.SYNTEX.Location = new System.Drawing.Point(109, 415);
            this.SYNTEX.Name = "SYNTEX";
            this.SYNTEX.Size = new System.Drawing.Size(75, 23);
            this.SYNTEX.TabIndex = 3;
            this.SYNTEX.Text = "SYNTEX";
            this.SYNTEX.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            //
            CommandParser.Instance.pictureBox = this.pictureBox1;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.Location = new System.Drawing.Point(398, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(366, 352);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Graphical_Language
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SYNTEX);
            this.Controls.Add(this.RUN);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Graphical_Language";
            this.Text = "Graphical_Language";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button RUN;
        private System.Windows.Forms.Button SYNTEX;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

