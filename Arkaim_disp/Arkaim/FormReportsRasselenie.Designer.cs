namespace Ark
{
    partial class FormReportsRasselenie
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.buttonZhitie = new System.Windows.Forms.Button();
            this.buttonCity = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonMoney = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxZhitie = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.listViewReportsRasselenie = new System.Windows.Forms.ListView();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.buttonYearReport1 = new System.Windows.Forms.Button();
            this.buttonYearReport2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonYearReport2);
            this.groupBox1.Controls.Add(this.buttonYearReport1);
            this.groupBox1.Controls.Add(this.comboBoxYear);
            this.groupBox1.Controls.Add(this.buttonExcel);
            this.groupBox1.Controls.Add(this.buttonZhitie);
            this.groupBox1.Controls.Add(this.buttonCity);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxZhitie);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxCity);
            this.groupBox1.Location = new System.Drawing.Point(12, 360);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(802, 136);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // buttonExcel
            // 
            this.buttonExcel.Location = new System.Drawing.Point(479, 104);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(75, 23);
            this.buttonExcel.TabIndex = 12;
            this.buttonExcel.Text = "Excel";
            this.buttonExcel.UseVisualStyleBackColor = true;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // buttonZhitie
            // 
            this.buttonZhitie.Location = new System.Drawing.Point(479, 37);
            this.buttonZhitie.Name = "buttonZhitie";
            this.buttonZhitie.Size = new System.Drawing.Size(75, 23);
            this.buttonZhitie.TabIndex = 11;
            this.buttonZhitie.Text = "житиехрясь";
            this.buttonZhitie.UseVisualStyleBackColor = true;
            this.buttonZhitie.Click += new System.EventHandler(this.buttonZhitie_Click);
            // 
            // buttonCity
            // 
            this.buttonCity.Location = new System.Drawing.Point(479, 71);
            this.buttonCity.Name = "buttonCity";
            this.buttonCity.Size = new System.Drawing.Size(75, 23);
            this.buttonCity.TabIndex = 10;
            this.buttonCity.Text = "городхрясь";
            this.buttonCity.UseVisualStyleBackColor = true;
            this.buttonCity.Click += new System.EventHandler(this.buttonCity_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonMoney);
            this.groupBox2.Controls.Add(this.dateTimePicker2);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Location = new System.Drawing.Point(20, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 108);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Кол-во денег и человек";
            // 
            // buttonMoney
            // 
            this.buttonMoney.Location = new System.Drawing.Point(142, 79);
            this.buttonMoney.Name = "buttonMoney";
            this.buttonMoney.Size = new System.Drawing.Size(75, 23);
            this.buttonMoney.TabIndex = 2;
            this.buttonMoney.Text = "деньги";
            this.buttonMoney.UseVisualStyleBackColor = true;
            this.buttonMoney.Click += new System.EventHandler(this.buttonMoney_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(17, 53);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(17, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Житие";
            // 
            // comboBoxZhitie
            // 
            this.comboBoxZhitie.FormattingEnabled = true;
            this.comboBoxZhitie.Location = new System.Drawing.Point(334, 39);
            this.comboBoxZhitie.Name = "comboBoxZhitie";
            this.comboBoxZhitie.Size = new System.Drawing.Size(121, 21);
            this.comboBoxZhitie.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "город";
            // 
            // textBoxCity
            // 
            this.textBoxCity.Location = new System.Drawing.Point(334, 73);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(121, 20);
            this.textBoxCity.TabIndex = 5;
            // 
            // listViewReportsRasselenie
            // 
            this.listViewReportsRasselenie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewReportsRasselenie.FullRowSelect = true;
            this.listViewReportsRasselenie.GridLines = true;
            this.listViewReportsRasselenie.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewReportsRasselenie.HideSelection = false;
            this.listViewReportsRasselenie.Location = new System.Drawing.Point(12, 12);
            this.listViewReportsRasselenie.MultiSelect = false;
            this.listViewReportsRasselenie.Name = "listViewReportsRasselenie";
            this.listViewReportsRasselenie.Size = new System.Drawing.Size(802, 342);
            this.listViewReportsRasselenie.TabIndex = 5;
            this.listViewReportsRasselenie.UseCompatibleStateImageBehavior = false;
            this.listViewReportsRasselenie.View = System.Windows.Forms.View.Details;
            this.listViewReportsRasselenie.SelectedIndexChanged += new System.EventHandler(this.listViewReportsRasselenie_SelectedIndexChanged);
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(616, 42);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(121, 21);
            this.comboBoxYear.TabIndex = 13;
            // 
            // buttonYearReport1
            // 
            this.buttonYearReport1.Location = new System.Drawing.Point(616, 72);
            this.buttonYearReport1.Name = "buttonYearReport1";
            this.buttonYearReport1.Size = new System.Drawing.Size(121, 23);
            this.buttonYearReport1.TabIndex = 14;
            this.buttonYearReport1.Text = "Общая динамика";
            this.buttonYearReport1.UseVisualStyleBackColor = true;
            this.buttonYearReport1.Click += new System.EventHandler(this.buttonYearReport1_Click);
            // 
            // buttonYearReport2
            // 
            this.buttonYearReport2.Location = new System.Drawing.Point(616, 104);
            this.buttonYearReport2.Name = "buttonYearReport2";
            this.buttonYearReport2.Size = new System.Drawing.Size(121, 23);
            this.buttonYearReport2.TabIndex = 15;
            this.buttonYearReport2.Text = "По видам расселения";
            this.buttonYearReport2.UseVisualStyleBackColor = true;
            this.buttonYearReport2.Click += new System.EventHandler(this.buttonYearReport2_Click);
            // 
            // FormReportsRasselenie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 508);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewReportsRasselenie);
            this.Name = "FormReportsRasselenie";
            this.Text = "отчеты (расселение)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormReportsRasselenie_FormClosed);
            this.Load += new System.EventHandler(this.FormReportsRasselenie_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewReportsRasselenie;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Button buttonMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxZhitie;
        private System.Windows.Forms.Button buttonZhitie;
        private System.Windows.Forms.Button buttonCity;
        private System.Windows.Forms.Button buttonExcel;
        private System.Windows.Forms.Button buttonYearReport2;
        private System.Windows.Forms.Button buttonYearReport1;
        private System.Windows.Forms.ComboBox comboBoxYear;
    }
}