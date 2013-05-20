namespace Ark
{
    partial class FormReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReports));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonYear4 = new System.Windows.Forms.Button();
            this.buttonYear3 = new System.Windows.Forms.Button();
            this.buttonYear2 = new System.Windows.Forms.Button();
            this.buttonYearReport = new System.Windows.Forms.Button();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.buttonBilety = new System.Windows.Forms.Button();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.buttonDog = new System.Windows.Forms.Button();
            this.buttonActual = new System.Windows.Forms.Button();
            this.buttonDengi = new System.Windows.Forms.Button();
            this.buttonAkt = new System.Windows.Forms.Button();
            this.buttonEks = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxEkskursija = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxEkskursovod = new System.Windows.Forms.ComboBox();
            this.buttonZP = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.listViewReports = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.buttonBilety);
            this.groupBox1.Controls.Add(this.buttonExcel);
            this.groupBox1.Controls.Add(this.buttonDog);
            this.groupBox1.Controls.Add(this.buttonActual);
            this.groupBox1.Controls.Add(this.buttonDengi);
            this.groupBox1.Controls.Add(this.buttonAkt);
            this.groupBox1.Controls.Add(this.buttonEks);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxEkskursija);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxEkskursovod);
            this.groupBox1.Controls.Add(this.buttonZP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Location = new System.Drawing.Point(12, 362);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(916, 193);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonYear4);
            this.groupBox2.Controls.Add(this.buttonYear3);
            this.groupBox2.Controls.Add(this.buttonYear2);
            this.groupBox2.Controls.Add(this.buttonYearReport);
            this.groupBox2.Controls.Add(this.comboBoxYear);
            this.groupBox2.Location = new System.Drawing.Point(573, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 167);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Годовой отчет";
            // 
            // buttonYear4
            // 
            this.buttonYear4.Location = new System.Drawing.Point(26, 138);
            this.buttonYear4.Name = "buttonYear4";
            this.buttonYear4.Size = new System.Drawing.Size(137, 23);
            this.buttonYear4.TabIndex = 4;
            this.buttonYear4.Text = "Мастер-классы";
            this.buttonYear4.UseVisualStyleBackColor = true;
            this.buttonYear4.Click += new System.EventHandler(this.buttonYearReport4_Click);
            // 
            // buttonYear3
            // 
            this.buttonYear3.Location = new System.Drawing.Point(26, 111);
            this.buttonYear3.Name = "buttonYear3";
            this.buttonYear3.Size = new System.Drawing.Size(137, 21);
            this.buttonYear3.TabIndex = 3;
            this.buttonYear3.Text = "Льготные";
            this.buttonYear3.UseVisualStyleBackColor = true;
            this.buttonYear3.Click += new System.EventHandler(this.buttonYearReport3_Click);
            // 
            // buttonYear2
            // 
            this.buttonYear2.Location = new System.Drawing.Point(26, 82);
            this.buttonYear2.Name = "buttonYear2";
            this.buttonYear2.Size = new System.Drawing.Size(137, 23);
            this.buttonYear2.TabIndex = 2;
            this.buttonYear2.Text = "Статистика";
            this.buttonYear2.UseVisualStyleBackColor = true;
            this.buttonYear2.Click += new System.EventHandler(this.buttonYearReport2_Click);
            // 
            // buttonYearReport
            // 
            this.buttonYearReport.Location = new System.Drawing.Point(26, 52);
            this.buttonYearReport.Name = "buttonYearReport";
            this.buttonYearReport.Size = new System.Drawing.Size(137, 23);
            this.buttonYearReport.TabIndex = 1;
            this.buttonYearReport.Text = "Динамика";
            this.buttonYearReport.UseVisualStyleBackColor = true;
            this.buttonYearReport.Click += new System.EventHandler(this.buttonYearReport_Click);
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(26, 24);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(137, 21);
            this.comboBoxYear.TabIndex = 0;
            // 
            // buttonBilety
            // 
            this.buttonBilety.Location = new System.Drawing.Point(355, 106);
            this.buttonBilety.Name = "buttonBilety";
            this.buttonBilety.Size = new System.Drawing.Size(96, 23);
            this.buttonBilety.TabIndex = 41;
            this.buttonBilety.Text = "По билетам";
            this.buttonBilety.UseVisualStyleBackColor = true;
            this.buttonBilety.Click += new System.EventHandler(this.buttonBilety_Click);
            // 
            // buttonExcel
            // 
            this.buttonExcel.Location = new System.Drawing.Point(785, 80);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(98, 23);
            this.buttonExcel.TabIndex = 39;
            this.buttonExcel.Text = "Excel";
            this.buttonExcel.UseVisualStyleBackColor = true;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // buttonDog
            // 
            this.buttonDog.Location = new System.Drawing.Point(785, 51);
            this.buttonDog.Name = "buttonDog";
            this.buttonDog.Size = new System.Drawing.Size(98, 23);
            this.buttonDog.TabIndex = 37;
            this.buttonDog.Text = "Договор";
            this.buttonDog.UseVisualStyleBackColor = true;
            this.buttonDog.Click += new System.EventHandler(this.buttonDog_Click);
            // 
            // buttonActual
            // 
            this.buttonActual.Location = new System.Drawing.Point(117, 108);
            this.buttonActual.Name = "buttonActual";
            this.buttonActual.Size = new System.Drawing.Size(100, 21);
            this.buttonActual.TabIndex = 40;
            this.buttonActual.Text = "Активные экск";
            this.buttonActual.UseVisualStyleBackColor = true;
            this.buttonActual.Click += new System.EventHandler(this.buttonActual_Click);
            // 
            // buttonDengi
            // 
            this.buttonDengi.Location = new System.Drawing.Point(117, 81);
            this.buttonDengi.Name = "buttonDengi";
            this.buttonDengi.Size = new System.Drawing.Size(98, 23);
            this.buttonDengi.TabIndex = 38;
            this.buttonDengi.Text = "Денежки";
            this.buttonDengi.UseVisualStyleBackColor = true;
            this.buttonDengi.Click += new System.EventHandler(this.buttonDengi_Click);
            // 
            // buttonAkt
            // 
            this.buttonAkt.Location = new System.Drawing.Point(785, 22);
            this.buttonAkt.Name = "buttonAkt";
            this.buttonAkt.Size = new System.Drawing.Size(98, 23);
            this.buttonAkt.TabIndex = 36;
            this.buttonAkt.Text = "Акты";
            this.buttonAkt.UseVisualStyleBackColor = true;
            this.buttonAkt.Click += new System.EventHandler(this.buttonAkt_Click);
            // 
            // buttonEks
            // 
            this.buttonEks.Location = new System.Drawing.Point(457, 106);
            this.buttonEks.Name = "buttonEks";
            this.buttonEks.Size = new System.Drawing.Size(98, 23);
            this.buttonEks.TabIndex = 33;
            this.buttonEks.Text = "Про экскурсии";
            this.buttonEks.UseVisualStyleBackColor = true;
            this.buttonEks.Click += new System.EventHandler(this.buttonEks_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(305, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Экскурсия";
            // 
            // comboBoxEkskursija
            // 
            this.comboBoxEkskursija.FormattingEnabled = true;
            this.comboBoxEkskursija.Location = new System.Drawing.Point(384, 77);
            this.comboBoxEkskursija.Name = "comboBoxEkskursija";
            this.comboBoxEkskursija.Size = new System.Drawing.Size(157, 21);
            this.comboBoxEkskursija.TabIndex = 31;
            this.comboBoxEkskursija.SelectedIndexChanged += new System.EventHandler(this.comboBoxEkskursija_SelectedIndexChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Экскурсовод";
            // 
            // comboBoxEkskursovod
            // 
            this.comboBoxEkskursovod.FormattingEnabled = true;
            this.comboBoxEkskursovod.Location = new System.Drawing.Point(384, 24);
            this.comboBoxEkskursovod.Name = "comboBoxEkskursovod";
            this.comboBoxEkskursovod.Size = new System.Drawing.Size(157, 21);
            this.comboBoxEkskursovod.TabIndex = 29;
            // 
            // buttonZP
            // 
            this.buttonZP.Location = new System.Drawing.Point(443, 51);
            this.buttonZP.Name = "buttonZP";
            this.buttonZP.Size = new System.Drawing.Size(98, 23);
            this.buttonZP.TabIndex = 28;
            this.buttonZP.Text = "про зарплату";
            this.buttonZP.UseVisualStyleBackColor = true;
            this.buttonZP.Click += new System.EventHandler(this.buttonZP_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Даты отчета";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(87, 49);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(186, 20);
            this.dateTimePicker2.TabIndex = 7;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(87, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(186, 20);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // listViewReports
            // 
            this.listViewReports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewReports.FullRowSelect = true;
            this.listViewReports.GridLines = true;
            this.listViewReports.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewReports.HideSelection = false;
            this.listViewReports.Location = new System.Drawing.Point(12, 12);
            this.listViewReports.MultiSelect = false;
            this.listViewReports.Name = "listViewReports";
            this.listViewReports.Size = new System.Drawing.Size(916, 344);
            this.listViewReports.TabIndex = 5;
            this.listViewReports.UseCompatibleStateImageBehavior = false;
            this.listViewReports.View = System.Windows.Forms.View.Details;
            this.listViewReports.SelectedIndexChanged += new System.EventHandler(this.listViewEquipmentGroups_SelectedIndexChanged);
            // 
            // FormReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 567);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewReports);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormReports";
            this.Text = "Отчеты";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormEquipmentGroups_FormClosed);
            this.Load += new System.EventHandler(this.FormEquipmentGroups_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewReports;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonZP;
        private System.Windows.Forms.ComboBox comboBoxEkskursovod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxEkskursija;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonEks;
        private System.Windows.Forms.Button buttonDog;
        private System.Windows.Forms.Button buttonAkt;
        private System.Windows.Forms.Button buttonDengi;
        private System.Windows.Forms.Button buttonExcel;
        private System.Windows.Forms.Button buttonActual;
        private System.Windows.Forms.Button buttonBilety;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonYearReport;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Button buttonYear2;
        private System.Windows.Forms.Button buttonYear4;
        private System.Windows.Forms.Button buttonYear3;
    }
}