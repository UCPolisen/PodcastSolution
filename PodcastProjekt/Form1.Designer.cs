namespace PodcastProjekt
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
            button6 = new Button();
            label7 = new Label();
            button8 = new Button();
            dataGridView1 = new DataGridView();
            Column2 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            label8 = new Label();
            textBox3 = new TextBox();
            button5 = new Button();
            button9 = new Button();
            button10 = new Button();
            textBox4 = new TextBox();
            listBox1 = new ListBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 100);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(109, 32);
            label1.TabIndex = 0;
            label1.Text = "RSS/URL:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(147, 100);
            textBox1.Margin = new Padding(4, 2, 4, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(288, 39);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 177);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(84, 32);
            label2.TabIndex = 2;
            label2.Text = "Namn:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(147, 171);
            textBox2.Margin = new Padding(4, 2, 4, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(290, 39);
            textBox2.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 256);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(108, 32);
            label3.TabIndex = 4;
            label3.Text = "Kategori:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(147, 256);
            comboBox1.Margin = new Padding(4, 2, 4, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(290, 40);
            comboBox1.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label4.Location = new Point(32, 503);
            label4.Margin = new Padding(4, 0, 4, 0);
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
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(100, 32);
            label5.TabIndex = 8;
            label5.Text = "Avsnitt";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label6.Location = new Point(507, 36);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(137, 32);
            label6.TabIndex = 10;
            label6.Text = "Kategorier";
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.Location = new Point(507, 100);
            listBox3.Margin = new Padding(4, 2, 4, 2);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(323, 292);
            listBox3.TabIndex = 11;
            listBox3.SelectedIndexChanged += listBox3_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(880, 177);
            button1.Margin = new Padding(4, 2, 4, 2);
            button1.Name = "button1";
            button1.Size = new Size(150, 47);
            button1.TabIndex = 12;
            button1.Text = "Lägg till";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(880, 258);
            button2.Margin = new Padding(4, 2, 4, 2);
            button2.Name = "button2";
            button2.Size = new Size(150, 47);
            button2.TabIndex = 13;
            button2.Text = "Ta bort";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(880, 346);
            button3.Margin = new Padding(4, 2, 4, 2);
            button3.Name = "button3";
            button3.Size = new Size(150, 47);
            button3.TabIndex = 14;
            button3.Text = "Uppdatera";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(147, 346);
            button4.Margin = new Padding(4, 2, 4, 2);
            button4.Name = "button4";
            button4.Size = new Size(197, 47);
            button4.TabIndex = 15;
            button4.Text = "Lägg till podcast";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button6
            // 
            button6.Location = new Point(193, 958);
            button6.Margin = new Padding(4, 2, 4, 2);
            button6.Name = "button6";
            button6.Size = new Size(150, 47);
            button6.TabIndex = 17;
            button6.Text = "Ta bort";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label7.Location = new Point(1105, 36);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(153, 32);
            label7.TabIndex = 19;
            label7.Text = "Beskrivning";
            // 
            // button8
            // 
            button8.Location = new Point(370, 958);
            button8.Margin = new Padding(4, 2, 4, 2);
            button8.Name = "button8";
            button8.Size = new Size(150, 47);
            button8.TabIndex = 21;
            button8.Text = "Uppdatera";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column2, Column1, Column3, Column4 });
            dataGridView1.Location = new Point(32, 565);
            dataGridView1.Margin = new Padding(4, 2, 4, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(836, 361);
            dataGridView1.TabIndex = 22;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Column2
            // 
            Column2.HeaderText = "URL";
            Column2.MinimumWidth = 10;
            Column2.Name = "Column2";
            Column2.Width = 200;
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
            Column4.Width = 80;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(32, 17);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(299, 45);
            label8.TabIndex = 24;
            label8.Text = "Grupp 25 Podcast";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(880, 94);
            textBox3.Margin = new Padding(6);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(147, 39);
            textBox3.TabIndex = 25;
            // 
            // button5
            // 
            button5.Location = new Point(605, 486);
            button5.Margin = new Padding(6);
            button5.Name = "button5";
            button5.Size = new Size(262, 49);
            button5.TabIndex = 26;
            button5.Text = "Uppdatera alla flöden";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button9
            // 
            button9.Location = new Point(32, 958);
            button9.Margin = new Padding(6);
            button9.Name = "button9";
            button9.Size = new Size(139, 49);
            button9.TabIndex = 27;
            button9.Text = "Återställ";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button10
            // 
            button10.Location = new Point(921, 958);
            button10.Margin = new Padding(6);
            button10.Name = "button10";
            button10.Size = new Size(139, 49);
            button10.TabIndex = 28;
            button10.Text = "Sortera";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(1105, 110);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(470, 365);
            textBox4.TabIndex = 29;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(921, 565);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(686, 356);
            listBox1.TabIndex = 30;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1655, 1045);
            Controls.Add(listBox1);
            Controls.Add(textBox4);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button5);
            Controls.Add(textBox3);
            Controls.Add(label8);
            Controls.Add(dataGridView1);
            Controls.Add(button8);
            Controls.Add(label7);
            Controls.Add(button6);
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
            Margin = new Padding(4, 2, 4, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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
        private Button button6;
        private Label label7;
        private Button button8;
        private DataGridView dataGridView1;
        private Label label8;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private TextBox textBox3;
        private Button button5;
        private Button button9;
        private Button button10;
        private TextBox textBox4;
        private ListBox listBox1;
    }
}
