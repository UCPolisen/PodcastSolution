﻿namespace PodcastProjekt
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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            comboBox1 = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            listBox3 = new ListBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label7 = new Label();
            listBox4 = new ListBox();
            button8 = new Button();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            Column2 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 100);
            label1.Name = "label1";
            label1.Size = new Size(109, 32);
            label1.TabIndex = 0;
            label1.Text = "RSS/URL:";
            label1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(147, 100);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(289, 39);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 177);
            label2.Name = "label2";
            label2.Size = new Size(84, 32);
            label2.TabIndex = 2;
            label2.Text = "Namn:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(146, 170);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(290, 39);
            textBox2.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 257);
            label3.Name = "label3";
            label3.Size = new Size(108, 32);
            label3.TabIndex = 4;
            label3.Text = "Kategori:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(146, 257);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(290, 40);
            comboBox1.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label4.Location = new Point(32, 503);
            label4.Name = "label4";
            label4.Size = new Size(119, 32);
            label4.TabIndex = 7;
            label4.Text = "Podcasts";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label5.Location = new Point(921, 503);
            label5.Name = "label5";
            label5.Size = new Size(100, 32);
            label5.TabIndex = 8;
            label5.Text = "Avsnitt";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label6.Location = new Point(507, 37);
            label6.Name = "label6";
            label6.Size = new Size(137, 32);
            label6.TabIndex = 10;
            label6.Text = "Kategorier";
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.Location = new Point(507, 100);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(323, 292);
            listBox3.TabIndex = 11;
            // 
            // button1
            // 
            button1.Location = new Point(881, 100);
            button1.Name = "button1";
            button1.Size = new Size(150, 46);
            button1.TabIndex = 12;
            button1.Text = "Lägg till";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(881, 170);
            button2.Name = "button2";
            button2.Size = new Size(150, 46);
            button2.TabIndex = 13;
            button2.Text = "Ta bort";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(881, 243);
            button3.Name = "button3";
            button3.Size = new Size(150, 46);
            button3.TabIndex = 14;
            button3.Text = "Uppdatera";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(117, 345);
            button4.Name = "button4";
            button4.Size = new Size(197, 47);
            button4.TabIndex = 15;
            button4.Text = "Lägg till podcast";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(32, 957);
            button5.Name = "button5";
            button5.Size = new Size(185, 46);
            button5.TabIndex = 16;
            button5.Text = "Ändra Namn";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(247, 957);
            button6.Name = "button6";
            button6.Size = new Size(150, 46);
            button6.TabIndex = 17;
            button6.Text = "Ta bort";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(881, 318);
            button7.Name = "button7";
            button7.Size = new Size(150, 74);
            button7.TabIndex = 18;
            button7.Text = "Ändra namn";
            button7.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label7.Location = new Point(1200, 40);
            label7.Name = "label7";
            label7.Size = new Size(153, 32);
            label7.TabIndex = 19;
            label7.Text = "Beskrivning";
            // 
            // listBox4
            // 
            listBox4.FormattingEnabled = true;
            listBox4.Location = new Point(1200, 100);
            listBox4.Name = "listBox4";
            listBox4.Size = new Size(407, 292);
            listBox4.TabIndex = 20;
            // 
            // button8
            // 
            button8.Location = new Point(427, 957);
            button8.Name = "button8";
            button8.Size = new Size(150, 46);
            button8.TabIndex = 21;
            button8.Text = "Uppdatera";
            button8.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column2, Column1, Column3, Column4 });
            dataGridView1.Location = new Point(32, 565);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(835, 361);
            dataGridView1.TabIndex = 22;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { Column5 });
            dataGridView2.Location = new Point(921, 565);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 82;
            dataGridView2.Size = new Size(686, 361);
            dataGridView2.TabIndex = 23;
            // 
            // Column2
            // 
            Column2.HeaderText = "URL";
            Column2.MinimumWidth = 10;
            Column2.Name = "Column2";
            Column2.Width = 150;
            // 
            // Column1
            // 
            Column1.HeaderText = "Namn";
            Column1.MinimumWidth = 10;
            Column1.Name = "Column1";
            Column1.Width = 200;
            // 
            // Column3
            // 
            Column3.HeaderText = "Kategori";
            Column3.MinimumWidth = 10;
            Column3.Name = "Column3";
            Column3.Width = 200;
            // 
            // Column4
            // 
            Column4.HeaderText = "Avsnitt";
            Column4.MinimumWidth = 10;
            Column4.Name = "Column4";
            Column4.Width = 200;
            // 
            // Column5
            // 
            Column5.HeaderText = "Namn";
            Column5.MinimumWidth = 10;
            Column5.Name = "Column5";
            Column5.Width = 600;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(32, 18);
            label8.Name = "label8";
            label8.Size = new Size(299, 45);
            label8.TabIndex = 24;
            label8.Text = "Grupp 25 Podcast";
            label8.Click += label8_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1654, 1045);
            Controls.Add(label8);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(button8);
            Controls.Add(listBox4);
            Controls.Add(label7);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox3);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private Label label3;
        private ComboBox comboBox1;
        private Label label4;
        private Label label5;
        private Label label6;
        private ListBox listBox3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label7;
        private ListBox listBox4;
        private Button button8;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private Label label8;
    }
}
