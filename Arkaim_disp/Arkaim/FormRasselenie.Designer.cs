namespace Ark
{
    partial class FormRasselenie
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonNew = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxKvN = new System.Windows.Forms.TextBox();
            this.buttonKvit = new System.Windows.Forms.Button();
            this.textBoxKvK = new System.Windows.Forms.TextBox();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.comboBoxZakazczik = new System.Windows.Forms.ComboBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.comboBoxCity = new System.Windows.Forms.ComboBox();
            this.checkBoxBron = new System.Windows.Forms.CheckBox();
            this.checkBoxZhitie = new System.Windows.Forms.CheckBox();
            this.comboBoxZhitie2 = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxBron = new System.Windows.Forms.TextBox();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.textBoxNkvit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPark = new System.Windows.Forms.TextBox();
            this.textBoxKolCzel = new System.Windows.Forms.TextBox();
            this.textBoxKolDays = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxZhitie = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.listViewRasselenie = new System.Windows.Forms.ListView();
            this.textBoxBez = new System.Windows.Forms.TextBox();
            this.labelBez = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ФИО заказчика";
            // 
            // buttonNew
            // 
            this.buttonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNew.Location = new System.Drawing.Point(1004, 13);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(120, 23);
            this.buttonNew.TabIndex = 2;
            this.buttonNew.Text = "Новая запись";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelBez);
            this.groupBox1.Controls.Add(this.textBoxBez);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.buttonExcel);
            this.groupBox1.Controls.Add(this.comboBoxZakazczik);
            this.groupBox1.Controls.Add(this.buttonCopy);
            this.groupBox1.Controls.Add(this.comboBoxCity);
            this.groupBox1.Controls.Add(this.checkBoxBron);
            this.groupBox1.Controls.Add(this.checkBoxZhitie);
            this.groupBox1.Controls.Add(this.comboBoxZhitie2);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBoxBron);
            this.groupBox1.Controls.Add(this.buttonFilter);
            this.groupBox1.Controls.Add(this.dateTimePicker3);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.textBoxNkvit);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxPark);
            this.groupBox1.Controls.Add(this.textBoxKolCzel);
            this.groupBox1.Controls.Add(this.textBoxKolDays);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxZhitie);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.buttonNew);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.buttonApply);
            this.groupBox1.Location = new System.Drawing.Point(12, 431);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1130, 219);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.textBoxKvN);
            this.groupBox3.Controls.Add(this.buttonKvit);
            this.groupBox3.Controls.Add(this.textBoxKvK);
            this.groupBox3.Location = new System.Drawing.Point(6, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "По квитанциям";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Номер кон";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Номер нач";
            // 
            // textBoxKvN
            // 
            this.textBoxKvN.Location = new System.Drawing.Point(73, 19);
            this.textBoxKvN.Name = "textBoxKvN";
            this.textBoxKvN.Size = new System.Drawing.Size(100, 20);
            this.textBoxKvN.TabIndex = 13;
            // 
            // buttonKvit
            // 
            this.buttonKvit.Location = new System.Drawing.Point(73, 70);
            this.buttonKvit.Name = "buttonKvit";
            this.buttonKvit.Size = new System.Drawing.Size(100, 23);
            this.buttonKvit.TabIndex = 15;
            this.buttonKvit.Text = "По квитанциям";
            this.buttonKvit.UseVisualStyleBackColor = true;
            this.buttonKvit.Click += new System.EventHandler(this.buttonKvit_Click);
            // 
            // textBoxKvK
            // 
            this.textBoxKvK.Location = new System.Drawing.Point(73, 44);
            this.textBoxKvK.Name = "textBoxKvK";
            this.textBoxKvK.Size = new System.Drawing.Size(100, 20);
            this.textBoxKvK.TabIndex = 14;
            // 
            // buttonExcel
            // 
            this.buttonExcel.Location = new System.Drawing.Point(878, 13);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(120, 23);
            this.buttonExcel.TabIndex = 35;
            this.buttonExcel.Text = "to excel";
            this.buttonExcel.UseVisualStyleBackColor = true;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // comboBoxZakazczik
            // 
            this.comboBoxZakazczik.FormattingEnabled = true;
            this.comboBoxZakazczik.Location = new System.Drawing.Point(744, 84);
            this.comboBoxZakazczik.Name = "comboBoxZakazczik";
            this.comboBoxZakazczik.Size = new System.Drawing.Size(108, 21);
            this.comboBoxZakazczik.TabIndex = 34;
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(878, 42);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(120, 23);
            this.buttonCopy.TabIndex = 33;
            this.buttonCopy.Text = "Копировать";
            this.buttonCopy.UseVisualStyleBackColor = true;
            // 
            // comboBoxCity
            // 
            this.comboBoxCity.FormattingEnabled = true;
            this.comboBoxCity.Location = new System.Drawing.Point(744, 54);
            this.comboBoxCity.Name = "comboBoxCity";
            this.comboBoxCity.Size = new System.Drawing.Size(108, 21);
            this.comboBoxCity.TabIndex = 32;
            // 
            // checkBoxBron
            // 
            this.checkBoxBron.AutoSize = true;
            this.checkBoxBron.Location = new System.Drawing.Point(744, 176);
            this.checkBoxBron.Name = "checkBoxBron";
            this.checkBoxBron.Size = new System.Drawing.Size(63, 17);
            this.checkBoxBron.TabIndex = 30;
            this.checkBoxBron.Text = "Безнал";
            this.checkBoxBron.UseVisualStyleBackColor = true;
            // 
            // checkBoxZhitie
            // 
            this.checkBoxZhitie.AutoSize = true;
            this.checkBoxZhitie.Location = new System.Drawing.Point(385, 109);
            this.checkBoxZhitie.Name = "checkBoxZhitie";
            this.checkBoxZhitie.Size = new System.Drawing.Size(63, 17);
            this.checkBoxZhitie.TabIndex = 29;
            this.checkBoxZhitie.Text = "Безнал";
            this.checkBoxZhitie.UseVisualStyleBackColor = true;
            // 
            // comboBoxZhitie2
            // 
            this.comboBoxZhitie2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxZhitie2.FormattingEnabled = true;
            this.comboBoxZhitie2.Location = new System.Drawing.Point(297, 135);
            this.comboBoxZhitie2.Name = "comboBoxZhitie2";
            this.comboBoxZhitie2.Size = new System.Drawing.Size(151, 21);
            this.comboBoxZhitie2.TabIndex = 27;
            this.comboBoxZhitie2.Visible = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(297, 111);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(24, 23);
            this.buttonAdd.TabIndex = 26;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Visible = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(700, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Бронь";
            // 
            // textBoxBron
            // 
            this.textBoxBron.Location = new System.Drawing.Point(744, 113);
            this.textBoxBron.Name = "textBoxBron";
            this.textBoxBron.Size = new System.Drawing.Size(108, 20);
            this.textBoxBron.TabIndex = 24;
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(88, 77);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 23);
            this.buttonFilter.TabIndex = 23;
            this.buttonFilter.Text = "фильтр";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(24, 45);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(139, 20);
            this.dateTimePicker3.TabIndex = 22;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(24, 20);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(139, 20);
            this.dateTimePicker2.TabIndex = 21;
            // 
            // textBoxNkvit
            // 
            this.textBoxNkvit.Location = new System.Drawing.Point(744, 23);
            this.textBoxNkvit.Name = "textBoxNkvit";
            this.textBoxNkvit.Size = new System.Drawing.Size(108, 20);
            this.textBoxNkvit.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(683, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Заказчик";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(701, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Город";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(677, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Квитанция";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(499, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Парковка";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(471, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Кол-во человек";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(488, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Кол-во дней";
            // 
            // textBoxPark
            // 
            this.textBoxPark.Location = new System.Drawing.Point(562, 84);
            this.textBoxPark.Name = "textBoxPark";
            this.textBoxPark.Size = new System.Drawing.Size(100, 20);
            this.textBoxPark.TabIndex = 11;
            // 
            // textBoxKolCzel
            // 
            this.textBoxKolCzel.Location = new System.Drawing.Point(562, 55);
            this.textBoxKolCzel.Name = "textBoxKolCzel";
            this.textBoxKolCzel.Size = new System.Drawing.Size(100, 20);
            this.textBoxKolCzel.TabIndex = 10;
            // 
            // textBoxKolDays
            // 
            this.textBoxKolDays.Location = new System.Drawing.Point(562, 23);
            this.textBoxKolDays.Name = "textBoxKolDays";
            this.textBoxKolDays.Size = new System.Drawing.Size(100, 20);
            this.textBoxKolDays.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Дата";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Житие в...";
            // 
            // comboBoxZhitie
            // 
            this.comboBoxZhitie.FormattingEnabled = true;
            this.comboBoxZhitie.Location = new System.Drawing.Point(297, 84);
            this.comboBoxZhitie.Name = "comboBoxZhitie";
            this.comboBoxZhitie.Size = new System.Drawing.Size(151, 21);
            this.comboBoxZhitie.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(297, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(151, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(297, 20);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(151, 20);
            this.textBoxName.TabIndex = 3;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(1004, 71);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 23);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(1004, 42);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(120, 23);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Сохранить";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // listViewRasselenie
            // 
            this.listViewRasselenie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRasselenie.FullRowSelect = true;
            this.listViewRasselenie.GridLines = true;
            this.listViewRasselenie.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewRasselenie.HideSelection = false;
            this.listViewRasselenie.Location = new System.Drawing.Point(12, 12);
            this.listViewRasselenie.MultiSelect = false;
            this.listViewRasselenie.Name = "listViewRasselenie";
            this.listViewRasselenie.Size = new System.Drawing.Size(1130, 413);
            this.listViewRasselenie.TabIndex = 5;
            this.listViewRasselenie.UseCompatibleStateImageBehavior = false;
            this.listViewRasselenie.View = System.Windows.Forms.View.Details;
            this.listViewRasselenie.SelectedIndexChanged += new System.EventHandler(this.listViewRasselenie_SelectedIndexChanged);
            // 
            // textBoxBez
            // 
            this.textBoxBez.Location = new System.Drawing.Point(744, 146);
            this.textBoxBez.Name = "textBoxBez";
            this.textBoxBez.Size = new System.Drawing.Size(108, 20);
            this.textBoxBez.TabIndex = 37;
            // 
            // labelBez
            // 
            this.labelBez.AutoSize = true;
            this.labelBez.Location = new System.Drawing.Point(694, 149);
            this.labelBez.Name = "labelBez";
            this.labelBez.Size = new System.Drawing.Size(44, 13);
            this.labelBez.TabIndex = 38;
            this.labelBez.Text = "Безнал";
            // 
            // FormRasselenie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 652);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewRasselenie);
            this.Name = "FormRasselenie";
            this.Text = "Расселение";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRasselenie_FormClosed);
            this.Load += new System.EventHandler(this.FormRasselenie_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.ListView listViewRasselenie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxZhitie;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPark;
        private System.Windows.Forms.TextBox textBoxKolCzel;
        private System.Windows.Forms.TextBox textBoxKolDays;
        private System.Windows.Forms.TextBox textBoxNkvit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxBron;
        private System.Windows.Forms.ComboBox comboBoxZhitie2;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.CheckBox checkBoxBron;
        private System.Windows.Forms.CheckBox checkBoxZhitie;
        private System.Windows.Forms.ComboBox comboBoxCity;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.ComboBox comboBoxZakazczik;
        private System.Windows.Forms.Button buttonExcel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxKvN;
        private System.Windows.Forms.Button buttonKvit;
        private System.Windows.Forms.TextBox textBoxKvK;
        private System.Windows.Forms.Label labelBez;
        private System.Windows.Forms.TextBox textBoxBez;
    }
}