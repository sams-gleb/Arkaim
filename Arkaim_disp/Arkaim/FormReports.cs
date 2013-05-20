using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace Ark
{
    public struct _Reports
    {
        public _Reports(string date, string kvn, string kvk, string kol_czel, string numer, string cena)
        {
            this.date_val = date;
            this.kvn_val = kvn;
            this.kvk_val = kvk;
            this.kol_czel_val = kol_czel;
            this.numer_val = numer;
            this.cena_val = cena;
        }

        public string date
        {
            get
            {
                return date_val;
            }
            set
            {
                date_val = value;
            }
        }

        public string kvn
        {
            get
            {
                return kvn_val;
            }
            set
            {
                kvn_val = value;
            }
        }

        public string kvk
        {
            get
            {
                return kvk_val;
            }
            set
            {
                kvk_val = value;
            }
        }
        public string kol_czel
        {
            get
            {
                return kol_czel_val;
            }
            set
            {
                kol_czel_val = value;
            }
        }
        public string numer
        {
            get
            {
                return numer_val;
            }
            set
            {
                numer_val = value;
            }
        }
        public string cena
        {
            get
            {
                return cena_val;
            }
            set
            {
                cena_val = value;
            }
        }


        public override string ToString()
        {
            return date_val;
        }

        private string date_val;
        private string kvn_val;
        private string kvk_val;
        private string kol_czel_val;
        private string numer_val;
        private string cena_val;
    }

    public partial class FormReports : Form
    {
        private FormMain mainWin;
        Queue queueReports = new Queue();
        _Reports m_reports;

        _Ekskursovody m_ekskursovody; // экскурсоводы и экскурсии
        Queue queueEkskursovod = new Queue();
        _Ekskursii m_ekskursii;
        Queue queueEkskursii = new Queue();

        public FormReports(FormMain mainWin)
        {
            InitializeComponent();

            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewReports.Columns.Add("Кол-во экскурсий", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Количество человек", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Колчел", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Нумер", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Цена", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Цена", -2, HorizontalAlignment.Left);
            listViewReports.Columns[0].Width = 150;
            listViewReports.Columns[1].Width = 150;
            listViewReports.Columns[2].Width = 150;
            listViewReports.Columns[3].Width = 150;
            listViewReports.Columns[4].Width = 150;
            listViewReports.Columns[5].Width = 150;
            listViewReports.Columns[6].Width = 150;

        }

        private void FormEquipmentGroups_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formReports = null;

        }

        private void FormEquipmentGroups_Load(object sender, EventArgs e)
        {
            
            DateTime pickedDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            dateTimePicker1.Value = pickedDate;

            refreshReports();
            refreshEkskursii();
            refreshEkskursovody();
            refreshYear();

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
                        
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            comboBoxEkskursija.Enabled = true;
            comboBoxEkskursovod.Enabled = true;
            buttonZP.Enabled = true;

        }

        

        private void listViewEquipmentGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            if (listViewReports.FocusedItem == null)
                return;

            int k = queueReports.Count;
            for (int i = 0; i < k; i++)
            {
                m_reports = (_Reports)queueReports.Dequeue();
                if (m_reports.date.ToString() == (string)listViewReports.Items[listViewReports.FocusedItem.Index].Tag)
                {

                    //фигня про экскурсии и экскурсоводов
                    _Ekskursii c;
                    int k2 = queueEkskursii.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        c = (_Ekskursii)queueEkskursii.Dequeue();
                        if (c.id.ToString() == m_reports.numer.ToString())
                        {
                            for (int iii = 0; iii < comboBoxEkskursija.Items.Count; iii++)
                            {
                                if (((_Ekskursii)comboBoxEkskursija.Items[iii]).id == c.id)
                                {
                                    comboBoxEkskursija.SelectedIndex = iii;
                                    m_ekskursii = c;
                                    break;
                                }
                            }
                        };
                        queueEkskursii.Enqueue(c);
                    }



                    queueReports.Enqueue(m_reports);
                    break;

                };

            }

        }



        /*отчеты по экскурсиям
               *------------------
               *-----------------------
               *
               *
               */

        public void refreshReportsEks()
        {
            listViewReports.Items.Clear();
            queueReports.Clear();

            listViewReports.Columns.Clear();
            listViewReports.Columns.Add("Кол-во экскурсий", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);

            listViewReports.Columns[0].Width = 150;
            listViewReports.Columns[1].Width = 150;
            listViewReports.Columns[2].Width = 150;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("select count(0) AS `count`, sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))) AS `stoimost`,sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)) AS `Kol_czel` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`ekskursii`.`nazvanie` = '{0}') and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}')) union all select count(0) AS `count`, sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))) AS `stoimost`, sum(`plategki`.`Kol_czel`) AS `Kol_czel` from (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`ekskursii`.`nazvanie` = '{0}') and (`plategki`.`date` >= '{1}') and (`plategki`.`date` <= '{2}'))", comboBoxEkskursija.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                if (comboBoxEkskursija.SelectedIndex == -1)
                    sql = String.Format("select count(0) AS `count`,sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))) AS `stoimost`,sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)) AS `Kol_czel` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`FiO` <> '') and (`zhurnal`.`date` >= '{0}') and (`zhurnal`.`date` <= '{1}')) union all select count(0) AS `count`,sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))) AS `stoimost`,sum(`plategki`.`Kol_czel`) AS `Kol_czel` from (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`plategki`.`FiO` <> ''and (`plategki`.`date` >= '{0}') and (`plategki`.`date` <= '{1}')))", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewReports.Items.Clear();
                queueReports.Clear();
                _Reports v = new _Reports();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["count"].ToString().Trim());
                    v.date = dataRow["count"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoimost"].ToString().Trim());
                    v.numer = dataRow["stoimost"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    v.kol_czel = dataRow["Kol_czel"].ToString().Trim();

                    listViewReports.Items.Add(item1);
                    listViewReports.Items[listViewReports.Items.Count - 1].Tag = dataRow["count"].ToString();
                    queueReports.Enqueue(v);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
        }
        //отчет с номерами билетов

        public void refreshReportsBilety()
        {
            listViewReports.Items.Clear();
            queueReports.Clear();

            listViewReports.Columns.Clear();
            listViewReports.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("№ начало", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("№ конец", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);

            listViewReports.Columns[0].Width = 150;
            listViewReports.Columns[1].Width = 150;
            listViewReports.Columns[2].Width = 150;
            listViewReports.Columns[3].Width = 150;
            listViewReports.Columns[4].Width = 150;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("select `zhurnal`.`date` AS `Data`, `zhurnal`.`N_kvit_nach` AS `N_kvit_nach`,`zhurnal`.`N_kvit_koniec` AS `N_kvit_koniec`,((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1) AS `KolCzel`,(`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)) AS `Stoimost` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`ekskursii`.`nazvanie` = '{0}') and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}')) union all select '' AS `Data`, 'Общая стоимость' AS `N_kvit_nach`,'' AS `N_kvit_koniec`,sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)) AS `KolCzel`,sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))) AS `Stoimost` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`ekskursii`.`nazvanie` = '{0}') and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}')) order by `N_kvit_nach`", comboBoxEkskursija.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                 if (comboBoxEkskursija.SelectedIndex == -1)
                     sql = String.Format("select `zhurnal`.`date` AS `Data`, `zhurnal`.`N_kvit_nach` AS `N_kvit_nach`,`zhurnal`.`N_kvit_koniec` AS `N_kvit_koniec`,((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1) AS `KolCzel`,(`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)) AS `Stoimost` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`date` >= '{0}') and (`zhurnal`.`date` <= '{1}')) union all select '' AS `Data`, 'Общая стоимость' AS `N_kvit_nach`,'' AS `N_kvit_koniec`,sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)) AS `KolCzel`,sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))) AS `Stoimost` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`date` >= '{0}') and (`zhurnal`.`date` <= '{1}')) order by `N_kvit_nach`", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewReports.Items.Clear();
                queueReports.Clear();
                _Reports v = new _Reports();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["Data"].ToString().Trim());
                    v.date = dataRow["Data"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_nach"].ToString().Trim());
                    v.kvn = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    v.kvk = dataRow["N_kvit_koniec"].ToString().Trim();
                    item1.SubItems.Add(dataRow["KolCzel"].ToString().Trim());
                    v.kol_czel = dataRow["KolCzel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Stoimost"].ToString().Trim());
                    v.cena = dataRow["Stoimost"].ToString().Trim();

                    listViewReports.Items.Add(item1);
                    listViewReports.Items[listViewReports.Items.Count - 1].Tag = dataRow["N_kvit_nach"].ToString();
                    queueReports.Enqueue(v);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
        }

        private void buttonEks_Click(object sender, EventArgs e)
        {

            refreshReportsEks();
        }
        private void buttonDengi_Click(object sender, EventArgs e)
        {
            comboBoxEkskursija.SelectedIndex = -1;
            refreshReportsEks();
        }
        private void buttonBilety_Click(object sender, EventArgs e)
        {
            refreshReportsBilety();
        }
        /*-----------------------
        *-----------------------
        *отчеты по ЗП
        *-----------------------
        *-----------------------
        */
        public void refreshReports()
        {
            listViewReports.Items.Clear();
            queueReports.Clear();
            listViewReports.Columns.Clear();
            listViewReports.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Номер экскурсии", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Цена", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Цена", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewReports.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);

            listViewReports.Columns[0].Width = 150;
            listViewReports.Columns[1].Width = 150;
            listViewReports.Columns[2].Width = 150;
            listViewReports.Columns[3].Width = 150;
            listViewReports.Columns[4].Width = 150;
            listViewReports.Columns[5].Width = 150;
            listViewReports.Columns[6].Width = 150;

            try
            {


                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = string.Format("select `zhurnal`.`id` AS `id`,cast(`zhurnal`.`date` as date) AS `DATA`,`zhurnal`.`N_kvit_nach` AS `N_kvit_nach`,`zhurnal`.`N_kvit_koniec` AS `N_kvit_koniec`,((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1) AS `Kol_czel`,`zhurnal`.`N_ekskursii` AS `N`,'' AS `cena` from `zhurnal` where ((`zhurnal`.`FiO` = 'БДСМ') and `zhurnal`.`id` in (select (`zhurnal`.`id` + 1) AS `id` from `zhurnal` where (`zhurnal`.`FiO` = '{0}')) and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}')) union all select `zhurnal`.`id` AS `id`,cast(`zhurnal`.`date` as date) AS `DATA`,`zhurnal`.`N_kvit_nach` AS `N_kvit_nach`,`zhurnal`.`N_kvit_koniec` AS `N_kvit_koniec`,((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1) AS `Kol_czel`,`zhurnal`.`N_ekskursii` AS `N`,((case `ekskursovody`.`category` when 1 then `ekskursii`.`1_kat` when 2 then `ekskursii`.`2_kat` when 3 then `ekskursii`.`3_kat` end) * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)) AS `cena` from ((`zhurnal` join `ekskursovody` on((`zhurnal`.`FiO` = `ekskursovody`.`FiO`))) join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`FiO` = '{0}') and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}') and (`zhurnal`.`N_ekskursii` like '%а')) union all select `zhurnal`.`id` AS `id`, cast(`zhurnal`.`date` as date) AS `DATA`,`zhurnal`.`N_kvit_nach` AS `N_kvit_nach`,`zhurnal`.`N_kvit_koniec` AS `N_kvit_koniec`,((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1) AS `Kol_czel`,`zhurnal`.`N_ekskursii` AS `N`,(case `ekskursovody`.`category` when 1 then `ekskursii`.`1_kat` when 2 then `ekskursii`.`2_kat` when 3 then `ekskursii`.`3_kat` end) AS `cena` from ((`zhurnal` join `ekskursovody` on((`zhurnal`.`FiO` = `ekskursovody`.`FiO`))) join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`FiO` = '{0}') and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}') and (not((`zhurnal`.`N_ekskursii` like '%а')))) union all select `plategki`.`id` AS `id`,cast(`plategki`.`date` as date) AS `DATA`,`plategki`.`N_platezhki` AS `N_kvit_nach`,`plategki`.`N_platezhki` AS `N_kvit_koniec`,`plategki`.`Kol_czel` AS `Kol_czel`,`plategki`.`N_ekskursii` AS `N`,(case `ekskursovody`.`category` when 1 then `ekskursii`.`1_kat` when 2 then `ekskursii`.`2_kat` when 3 then `ekskursii`.`3_kat` end) AS `cena` from ((`plategki` join `ekskursovody` on((`plategki`.`FiO` = `ekskursovody`.`FiO`))) join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`plategki`.`FiO` = '{0}') and (`plategki`.`date` >= '{1}') and (`plategki`.`date` <= '{2}')) order by `DATA`", comboBoxEkskursovod.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewReports.Items.Clear();
                queueReports.Clear();
                _Reports cv = new _Reports();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["DATA"].ToString().Trim());
                    cv.date = dataRow["DATA"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N"].ToString().Trim());
                    cv.numer = dataRow["N"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    cv.kol_czel = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Cena"].ToString().Trim());
                    cv.cena = dataRow["Cena"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Cena"].ToString().Trim());
                    cv.cena = dataRow["Cena"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_nach"].ToString().Trim());
                    cv.kvn = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    cv.kvk = dataRow["N_kvit_koniec"].ToString().Trim();



                    listViewReports.Items.Add(item1);
                    listViewReports.Items[listViewReports.Items.Count - 1].Tag = dataRow["DATA"].ToString();
                    queueReports.Enqueue(cv);
                }
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
        }


        private void buttonZP_Click(object sender, EventArgs e)
        {

            refreshReports();

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelApp.Visible = true;
            BringToFront();  
            //копировать шаблон
            //System.IO.File.Copy(Application.StartupPath + "\\template.xls", Application.StartupPath + "\\template2.xls", true);
            string workbookPath = Application.StartupPath + "\\templates/template.xls";
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath, 0,
              true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true,
              false, 0, true, false, false);
            // параметры опена FileName, UpdateLinks,ReadOnly, Format, Password,WriteResPassword,  IgnoreReadOnlyRecommended, Origin,  Delimiter,        
            // Editable,   Notify,  Converter, AddToMRU        
              
            Microsoft.Office.Interop.Excel.Sheets excelSheets = excelWorkbook.Worksheets;

            // бланк для редактирования
            string currentSheet = "Бланк";
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);

            //Имя
            Microsoft.Office.Interop.Excel.Range excelCellName = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("E9", "E9");
            excelCellName.Value2 = comboBoxEkskursovod.Text;
            //Даты отчета
            Microsoft.Office.Interop.Excel.Range excelCellDate1 = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("C8", "C8");
            excelCellDate1.Value2 = dateTimePicker1.Text;
            Microsoft.Office.Interop.Excel.Range excelCellDate2 = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("F8", "F8");
            excelCellDate2.Value2 = dateTimePicker2.Text;
            //Категория
            Microsoft.Office.Interop.Excel.Range excelCellCat = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("D11", "D11");
            excelCellCat.Value2 = getCategory();
            //зп            
            Microsoft.Office.Interop.Excel.Range excelCellZP = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("A14", "G46");
            Microsoft.Office.Interop.Excel.Range excelCellZP1 = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("J14", "P46");
            Microsoft.Office.Interop.Excel.Range excelCellZP2 = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("S14", "Y46");
            Microsoft.Office.Interop.Excel.Range excelCellZP3 = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("AB14", "AH46");


            object[,] obj = new object[listViewReports.Items.Count, listViewReports.Columns.Count];
            for (int x = 0, maxX = listViewReports.Items.Count; x < maxX; x++)
            {
                for (int y = 0, maxY = listViewReports.Columns.Count; y < maxY; y++)
                {
                    obj[x, y] = listViewReports.Items[x].SubItems[y].Text;

                }

                if (listViewReports.Items.Count > 99)
                {
                    object[,] obj0 = new object[33, 7];
                    object[,] obj1 = new object[33, 7];
                    object[,] obj2 = new object[33, 7];
                    object[,] obj3 = new object[33, 7];
                    Array.Copy(obj, 0, obj0, 0, 231);
                    Array.Copy(obj, 231, obj1, 0, 231);
                    Array.Copy(obj, 462, obj2, 0, 231);
                    Array.Copy(obj, 693, obj3, 0, ((listViewReports.Items.Count * 7) - 693));
                    excelCellZP.Value2 = obj0;
                    excelCellZP1.Value2 = obj1;
                    excelCellZP2.Value2 = obj2;
                    excelCellZP3.Value2 = obj3;
                }
                else if (listViewReports.Items.Count > 66)
                {
                    object[,] obj0 = new object[33, 7];
                    object[,] obj1 = new object[33, 7];
                    object[,] obj2 = new object[33, 7];
                    Array.Copy(obj, 0, obj0, 0, 231);
                    Array.Copy(obj, 231, obj1, 0, 231);
                    Array.Copy(obj, 462, obj2, 0, ((listViewReports.Items.Count * 7) - 462));
                    excelCellZP.Value2 = obj0;
                    excelCellZP1.Value2 = obj1;
                    excelCellZP2.Value2 = obj2;
                }
                else if (listViewReports.Items.Count > 33)
                {
                    object[,] obj0 = new object[33, 7];
                    object[,] obj1 = new object[33, 7];
                    Array.Copy(obj, 0, obj0, 0, 231);
                    Array.Copy(obj, 231, obj1, 0, ((listViewReports.Items.Count * 7) - 231));
                    excelCellZP.Value2 = obj0;
                    excelCellZP1.Value2 = obj1;
                }
                else
                {
                    object[,] obj0 = new object[33, 7];
                    Array.Copy(obj, 0, obj0, 0, (listViewReports.Items.Count * 7));
                    excelCellZP.Value2 = obj0;
                }

            }
        }

        
        /*----------------
         *----------------
         *Договор
         */
        private void buttonDog_Click(object sender, EventArgs e)
        {

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            wordApp.Visible = true;
            BringToFront();  
            //System.IO.File.Copy(Application.StartupPath + "\\template_dogovor.doc", Application.StartupPath + "\\template_dogovor2.doc", true);
            Object documentPath = Application.StartupPath + "\\templates/template_dogovor.doc";
            Object confirmConversions = true;
            Object readOnly = true;
            Object addToRecentFiles = true;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = false;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing; ;
            Object oVisible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = false;
            Object xmlTransform = Type.Missing;

            Microsoft.Office.Interop.Word.Document wordDocument = wordApp.Documents.Open(ref documentPath, ref confirmConversions, ref readOnly, ref addToRecentFiles,
  ref passwordDocument, ref passwordTemplate, ref revert,
  ref writePasswordDocument, ref writePasswordTemplate,
  ref format, ref encoding, ref oVisible,
  ref openAndRepair, ref documentDirection, ref noEncodingDialog, ref xmlTransform);
            object oBookMark = "fio";
            object oBookMark1 = "fio1";
            string pass = "passport";
            string rozh = "rozhd";
            string strax = "strax";
            string tel = "tel";
            string address = "address";
            string inn = "inn";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = comboBoxEkskursovod.Text;
            wordDocument.Bookmarks.get_Item(ref oBookMark1).Range.Text = comboBoxEkskursovod.Text;
            oBookMark = "pass";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getPass(pass);
            oBookMark = "pass1";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getPass(pass);
            oBookMark = "rozhd";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getPass(rozh);
            oBookMark = "address";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getPass(address);
            oBookMark = "inn";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getPass(inn);
            oBookMark = "strax";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getPass(strax);
            oBookMark = "tel";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getPass(tel);
            oBookMark = "kat";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getCategory().ToString();

        }
        //данные из таб.экск-вод
        public string getPass(string n)
        {
            mainWin.m_dbConnector.Lock();
            MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

            string sql = String.Format("SELECT `{1}` FROM `ekskursovody` WHERE `FiO`='{0}'", comboBoxEkskursovod.Text, n);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            myAdapter.SelectCommand = new MySqlCommand(sql, conn);
            DataSet dataSet = new DataSet();
            myAdapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            mainWin.m_dbConnector.Unlock();
            DataRow dataRow = dataTable.Rows[0];
            return dataRow[n].ToString().Trim();
        }
        //Акты
        private void buttonAkt_Click(object sender, EventArgs e)
        {

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            wordApp.Visible = true;
            BringToFront();            
            //System.IO.File.Copy(Application.StartupPath + "\\template_akt.doc", Application.StartupPath + "\\template_akt2.doc", true);
            Object documentPath = Application.StartupPath + "\\templates/template_akt.doc";
            Object confirmConversions = true;
            Object readOnly = true;
            Object addToRecentFiles = true;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = false;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing; ;
            Object oVisible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = false;
            Object xmlTransform = Type.Missing;

            Microsoft.Office.Interop.Word.Document wordDocument = wordApp.Documents.Open(ref documentPath, ref confirmConversions, ref readOnly, ref addToRecentFiles,
  ref passwordDocument, ref passwordTemplate, ref revert,
  ref writePasswordDocument, ref writePasswordTemplate,
  ref format, ref encoding, ref oVisible,
  ref openAndRepair, ref documentDirection, ref noEncodingDialog, ref xmlTransform);
            object oBookMark = "fio";
            object oBookMark1 = "fio1";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = comboBoxEkskursovod.Text;
            wordDocument.Bookmarks.get_Item(ref oBookMark1).Range.Text = comboBoxEkskursovod.Text;
            oBookMark = "data";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = dateTimePicker2.Text;
            oBookMark = "date1";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = dateTimePicker1.Text;
            oBookMark = "date2";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = dateTimePicker2.Text;
            oBookMark = "koleks";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getKol_eks();
            oBookMark = "sum";
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = getSum();
            
            oBookMark = "sum1";
            decimal n = decimal.Parse(getSum());
            wordDocument.Bookmarks.get_Item(ref oBookMark).Range.Text = NumByWords.RurPhrase(n);

        }
        
        //кол-во экскурсий и деньжат
        public string getKol_eks()
        {
            mainWin.m_dbConnector.Lock();
            MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

            string sql = String.Format("select ((select count(`zhurnal`.`FiO`) from `zhurnal` where ((`zhurnal`.`FiO` = '{0}') and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}')))) + (select count(`plategki`.`FiO`) from `plategki` where ((`plategki`.`FiO` = '{0}') and (`plategki`.`date` >= '{1}') and (`plategki`.`date` <= '{2}'))) AS `kol_eks`", comboBoxEkskursovod.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            myAdapter.SelectCommand = new MySqlCommand(sql, conn);
            DataSet dataSet = new DataSet();
            myAdapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            mainWin.m_dbConnector.Unlock();
            DataRow dataRow = dataTable.Rows[0];
            return dataRow["kol_eks"].ToString().Trim();
        }
        public string getSum()
        {
            mainWin.m_dbConnector.Lock();
            MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

            string sql = String.Format("select (((select ifnull(sum(((case `ekskursovody`.`category` when 1 then `ekskursii`.`1_kat` when 2 then `ekskursii`.`2_kat` when 3 then `ekskursii`.`3_kat` end) * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) AS `Name_exp_1` from ((`zhurnal` join `ekskursovody` on((`zhurnal`.`FiO` = `ekskursovody`.`FiO`))) join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`FiO` = '{0}') and (`zhurnal`.`N_ekskursii` like '%а') and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}'))) + (select ifnull(sum((case `ekskursovody`.`category` when 1 then `ekskursii`.`1_kat` when 2 then `ekskursii`.`2_kat` when 3 then `ekskursii`.`3_kat` end)),0) AS `cena` from ((`zhurnal` join `ekskursovody` on((`zhurnal`.`FiO` = `ekskursovody`.`FiO`))) join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`FiO` = '{0}') and (not((`zhurnal`.`N_ekskursii` like '%а')))and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}')))) + (select ifnull(sum((case `ekskursovody`.`category` when 1 then `ekskursii`.`1_kat` when 2 then `ekskursii`.`2_kat` when 3 then `ekskursii`.`3_kat` end)),0) AS `Name_exp_1` from ((`plategki` join `ekskursovody` on((`plategki`.`FiO` = `ekskursovody`.`FiO`))) join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`plategki`.`FiO` = '{0}') and (`plategki`.`date` >= '{1}') and (`plategki`.`date` <= '{2}')))) AS `Name_exp_1`", comboBoxEkskursovod.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            myAdapter.SelectCommand = new MySqlCommand(sql, conn);
            DataSet dataSet = new DataSet();
            myAdapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            mainWin.m_dbConnector.Unlock();
            DataRow dataRow = dataTable.Rows[0];
            return dataRow["Name_exp_1"].ToString().Trim();
        }
        
        /*
        //Все отчеты
        private void buttonVse_Click(object sender, EventArgs e)
       {
           for (int v = 0; v < comboBoxEkskursovod.MaxLength; v++)
           {
               comboBoxEkskursovod.SelectedIndex = v;
               //this.buttonZP_Click(v,e);
           }


        }
        */

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewReports.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `tbl_payment_names` WHERE `id`='{0}'", m_reports.date);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("Невозможно удалить запись,\r\nт.к. с ней связаны данные из других таблиц");
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }



            refreshReports();

        }

        private void buttonActual_Click(object sender, EventArgs e)
        {
            refreshEkskursovody();
        }
        //комбобоксы
        public void refreshEkskursovody()
        {
            string N_ekskursovoda = "";
            if (comboBoxEkskursovod.SelectedIndex != -1)
            {
                N_ekskursovoda = m_ekskursovody.id.ToString();
            }

            comboBoxEkskursovod.Items.Clear();
            queueEkskursovod.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                //string sql = "SELECT `N_ekskursovoda`, `FiO` FROM `ekskursovody` ORDER BY `FiO`";

                string sql = String.Format("SELECT DISTINCT `ekskursovody`.`N_ekskursovoda`,`ekskursovody`.`FiO` AS `FiO` from (`ekskursovody` join `zhurnal` on((`ekskursovody`.`FiO` = `zhurnal`.`FiO`))) where ((`zhurnal`.`date` >= '{0}') and (`zhurnal`.`date` <= '{1}')) order by `ekskursovody`.`FiO`", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                _Ekskursovody c = new _Ekskursovody();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    c.id = dataRow["N_ekskursovoda"].ToString();
                    c.name = dataRow["FiO"].ToString().Trim();

                    comboBoxEkskursovod.Items.Add(c);


                    if (N_ekskursovoda != "" && N_ekskursovoda == c.id.ToString())
                    {
                        comboBoxEkskursovod.SelectedItem = c;
                    }


                    queueEkskursovod.Enqueue(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
        }

        public void refreshEkskursii()
        {
            string N_ekskursii = "";
            if (comboBoxEkskursija.SelectedIndex != -1)
            {
                N_ekskursii = m_ekskursii.id.ToString();
            }

            comboBoxEkskursija.Items.Clear();
            queueEkskursii.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `N_ekskursii`, `nazvanie` FROM `ekskursii` ORDER BY `N_ekskursii`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                _Ekskursii c = new _Ekskursii();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    c.id = dataRow["N_ekskursii"].ToString();
                    c.nazvanie = dataRow["nazvanie"].ToString().Trim();

                    comboBoxEkskursija.Items.Add(c);


                    if (N_ekskursii != "" && N_ekskursii == c.id.ToString())
                    {
                        comboBoxEkskursija.SelectedItem = c;
                    }


                    queueEkskursii.Enqueue(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }

        }


        //категория
        public object getCategory()
        {
            mainWin.m_dbConnector.Lock();
            MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

            string sql = String.Format("SELECT `category` AS `kat` FROM `ekskursovody` WHERE `FiO`='{0}'", comboBoxEkskursovod.Text);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            myAdapter.SelectCommand = new MySqlCommand(sql, conn);
            DataSet dataSet = new DataSet();
            myAdapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            mainWin.m_dbConnector.Unlock();
           // return dataTable.Rows[0].ItemArray;
            DataRow dataRow = dataTable.Rows[0];
            return dataRow["kat"].ToString().Trim();
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            if (listViewReports.Items.Count == 0)
            {
                MessageBox.Show("Нет элементов для экспорта в excel",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelApp.Visible = true;
            BringToFront();
            excelApp.Application.Workbooks.Add(Type.Missing);
            excelApp.Columns.ColumnWidth = 15;

            int i = 1;
            int i2 = 4;
            int i3 = 1;
            
            
            foreach (ColumnHeader col in listViewReports.Columns)
            {
                excelApp.Cells[i2-1, i3] = col.Text; i3++;
            }


            foreach (ListViewItem lvi in listViewReports.Items)
            {
                i = 1;
                foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                {
                    excelApp.Cells[i2, i] = lvs.Text;
                    i++;
                }
                i2++;
            }
        }
        //
        private void refreshYear()
        {
            DateTime dt = DateTime.Now;

            mainWin.m_dbConnector.Lock();
            MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

            string sql = String.Format("SELECT YEAR(`date`) AS `date` FROM `zhurnal` order by `date` asc limit 1");
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            myAdapter.SelectCommand = new MySqlCommand(sql, conn);
            DataSet dataSet = new DataSet();
            myAdapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            mainWin.m_dbConnector.Unlock();
            DataRow dataRow = dataTable.Rows[0];
            int dat1 = dt.Year;
            int dat2 = int.Parse(string.Format("{0}",dataRow["date"]));
            for (; dat2 <= dat1; dat2++)
            {
                comboBoxYear.Items.Add(dat2);
            }
            comboBoxYear.SelectedIndex = comboBoxYear.Items.Count - 1;
        }
        private void buttonYearReport2_Click(object sender, EventArgs e)
        {

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                string sql ="";
                string[,] arr = { { "Январь-Март", "1", "4" }, { "Апрель-Май", "4", "6" }, { "Июнь", "6", "7" }, { "Июль", "7", "8" }, { "Август", "8", "9" }, { "Сентябрь", "9", "10" }, { "Октябрь-Декабрь", "10", "12" }  };
                string[,] arr2 = { { "Аркаим", "сопровождение" }, { "музей Природы и Человека", "Входной билет(ЧиП)" }, { "музей Древних производств", "Входной билет(Др.Пр.)" }, { "Комплексная №1", "0" }, { "Комплексная №2", "0" }, { "Темир", "Входной билет(Темир)" }, { "Казачья усадьба", "Входной билет(каз)" }, { "ЖКВ", "Входной билет(ЖКВ)" }, { "Лекция", "0" } };
                bool ini=true;
                for ( int x = 0; x<(arr.Length/3); x++ ) {
                    for ( int i = 0; i<(arr2.Length/2); i++) {
                        if (ini == false) { sql += " union all "; }
                        sql += string.Format("SELECT \"{4}\" AS name, \"{0}\" AS eks, (count(0)+(SELECT count(0) FROM plategki JOIN ekskursii ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`)) WHERE (ekskursii.nazvanie = \"{0}\") AND (`plategki`.`date` >= \"{5}-{2}-01\") and (`plategki`.`date` < \"{5}-{3}-01\"))) AS count_eks, (ifnull(sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)),0) + (SELECT ifnull(sum(`plategki`.`Kol_czel`),0)  FROM (`plategki` JOIN `ekskursii` ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie = \"{0}\") AND (`plategki`.`date` >= \"{5}-{2}-01\") AND (`plategki`.`date` < \"{5}-{3}-01\"))) AS Kol_czel, ((SELECT ifnull(sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)),0) FROM (`zhurnal` JOIN `ekskursii` ON((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie LIKE \"{1}%\") AND (`zhurnal`.`date` >= \"{5}-{2}-01\") AND (`zhurnal`.`date` < \"{5}-{3}-01\"))+ (SELECT ifnull(sum(`plategki`.`Kol_czel`),0)  FROM (`plategki` JOIN `ekskursii` ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie like \"{1}%\") AND (`plategki`.`date` >= \"{5}-{2}-01\") AND (`plategki`.`date` < \"{5}-{3}-01\"))) AS Vhodn, (ifnull(sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) + (SELECT ifnull(sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))),0) FROM (`plategki` JOIN `ekskursii` ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie = \"{0}\") AND (`plategki`.`date` >= \"{5}-{2}-01\") AND (`plategki`.`date` < \"{5}-{3}-01\"))) AS `stoim_eks`, ((SELECT ifnull(sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) FROM (`zhurnal` JOIN `ekskursii` ON((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie LIKE \"{1}%\") AND (`zhurnal`.`date` >= \"{5}-{2}-01\") AND (`zhurnal`.`date` < \"{5}-{3}-01\")) + (SELECT ifnull(sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))),0) FROM (`plategki` JOIN `ekskursii` ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie LIKE \"{1}%\") and (`plategki`.`date` >= \"{5}-{2}-01\") and (`plategki`.`date` < \"{5}-{3}-01\"))) as `stoim_vhod` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where (ekskursii.nazvanie = \"{0}\") and (`zhurnal`.`date` >= \"{5}-{2}-01\") and (`zhurnal`.`date` < \"{5}-{3}-01\")", arr2[i, 0], arr2[i, 1], arr[x, 1], arr[x, 2], arr[x, 0], comboBoxYear.Text.ToString().Trim());
                        ini = false;
                        
                    }
                }
                //throw new System.InvalidOperationException(sql);    
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                listViewReports.Items.Clear();

                listViewReports.Columns.Clear();
                listViewReports.Columns.Add("Месяц", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Назв.экскурсии", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во экскурсий", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во чел по входным", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Стоим экскурсий", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Стоим входных билетов", -2, HorizontalAlignment.Left);
                listViewReports.Columns[0].Width = 150;
                listViewReports.Columns[1].Width = 150;
                listViewReports.Columns[2].Width = 150;
                listViewReports.Columns[3].Width = 150;
                listViewReports.Columns[4].Width = 150;
                listViewReports.Columns[5].Width = 150;
                listViewReports.Columns[6].Width = 150;

                queueReports.Clear();
                _Reports cv = new _Reports();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["name"].ToString().Trim());
                    cv.date = dataRow["name"].ToString().Trim();
                    item1.SubItems.Add(dataRow["eks"].ToString().Trim());
                    cv.date= dataRow["eks"].ToString().Trim();
                    item1.SubItems.Add(dataRow["count_eks"].ToString().Trim());
                    cv.numer = dataRow["count_eks"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    cv.cena = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Vhodn"].ToString().Trim());
                    cv.kvn = dataRow["Vhodn"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoim_eks"].ToString().Trim());
                    cv.kvk = dataRow["stoim_eks"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoim_vhod"].ToString().Trim());
                    cv.kol_czel = dataRow["stoim_vhod"].ToString().Trim();
                  

                    listViewReports.Items.Add(item1);
                    listViewReports.Items[listViewReports.Items.Count - 1].Tag = dataRow["name"].ToString();
                    queueReports.Enqueue(cv);
                }
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
        }
        private void buttonYearReport3_Click(object sender, EventArgs e)
        {

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                string sql = "";
                string[,] arr = { { "Январь-Март", "1", "4" }, { "Апрель-Май", "4", "6" }, { "Июнь", "6", "7" }, { "Июль", "7", "8" }, { "Август", "8", "9" }, { "Сентябрь", "9", "10" }, { "Октябрь-Декабрь", "10", "12" } };
                string[,] arr2 = { { "ЧиП  (л)", "Входной билет (ЧиП) (л)" }, { "Др. пр. (л)", "Входной билет (Др.Пр.)(л)" }, { "Комплексная (л)", "0" }, { "Темир (л)", "Входной билет (Темир)(л)" }, { "Казачья усадьба (л)", "Входной билет (каз)(л)" }, { "0", "Входной билет (ЖКВ)(л)" }, { "бесплатн%", "Бесплатный входной билет(л)" } };
                bool ini = true;
                for (int x = 0; x < (arr.Length / 3); x++)
                {
                    for (int i = 0; i < (arr2.Length / 2); i++)
                    {
                        if (ini == false) { sql += " union all "; }
                        sql += string.Format("SELECT \"{4}\" AS name, \"{0}\" AS eks, (count(0)+(SELECT count(0) FROM plategki JOIN ekskursii ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`)) WHERE (ekskursii.nazvanie = \"{0}\") AND (`plategki`.`date` >= \"{5}-{2}-01\") and (`plategki`.`date` < \"{5}-{3}-01\"))) AS count_eks, (ifnull(sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)),0) + (SELECT ifnull(sum(`plategki`.`Kol_czel`),0)  FROM (`plategki` JOIN `ekskursii` ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie = \"{0}\") AND (`plategki`.`date` >= \"{5}-{2}-01\") AND (`plategki`.`date` < \"{5}-{3}-01\"))) AS Kol_czel, ((SELECT ifnull(sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)),0) FROM (`zhurnal` JOIN `ekskursii` ON((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie LIKE \"{1}%\") AND (`zhurnal`.`date` >= \"{5}-{2}-01\") AND (`zhurnal`.`date` < \"{5}-{3}-01\"))+ (SELECT ifnull(sum(`plategki`.`Kol_czel`),0)  FROM (`plategki` JOIN `ekskursii` ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie like \"{1}%\") AND (`plategki`.`date` >= \"{5}-{2}-01\") AND (`plategki`.`date` < \"{5}-{3}-01\"))) AS Vhodn, (ifnull(sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) + (SELECT ifnull(sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))),0) FROM (`plategki` JOIN `ekskursii` ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie = \"{0}\") AND (`plategki`.`date` >= \"{5}-{2}-01\") AND (`plategki`.`date` < \"{5}-{3}-01\"))) AS `stoim_eks`, ((SELECT ifnull(sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) FROM (`zhurnal` JOIN `ekskursii` ON((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie LIKE \"{1}%\") AND (`zhurnal`.`date` >= \"{5}-{2}-01\") AND (`zhurnal`.`date` < \"{5}-{3}-01\")) + (SELECT ifnull(sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))),0) FROM (`plategki` JOIN `ekskursii` ON((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE (ekskursii.nazvanie LIKE \"{1}%\") and (`plategki`.`date` >= \"{5}-{2}-01\") and (`plategki`.`date` < \"{5}-{3}-01\"))) as `stoim_vhod` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where (ekskursii.nazvanie = \"{0}\") and (`zhurnal`.`date` >= \"{5}-{2}-01\") and (`zhurnal`.`date` < \"{5}-{3}-01\")", arr2[i, 0], arr2[i, 1], arr[x, 1], arr[x, 2], arr[x, 0], comboBoxYear.Text.ToString().Trim());
                        ini = false;

                    }
                }
                //throw new System.InvalidOperationException(sql);    
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                listViewReports.Items.Clear();

                listViewReports.Columns.Clear();
                listViewReports.Columns.Add("Месяц", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Назв.экскурсии", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во экскурсий", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во чел по входным", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Стоим экскурсий", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Стоим входных билетов", -2, HorizontalAlignment.Left);
                listViewReports.Columns[0].Width = 150;
                listViewReports.Columns[1].Width = 150;
                listViewReports.Columns[2].Width = 150;
                listViewReports.Columns[3].Width = 150;
                listViewReports.Columns[4].Width = 150;
                listViewReports.Columns[5].Width = 150;
                listViewReports.Columns[6].Width = 150;

                queueReports.Clear();
                _Reports cv = new _Reports();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["name"].ToString().Trim());
                    cv.date = dataRow["name"].ToString().Trim();
                    item1.SubItems.Add(dataRow["eks"].ToString().Trim());
                    cv.date = dataRow["eks"].ToString().Trim();
                    item1.SubItems.Add(dataRow["count_eks"].ToString().Trim());
                    cv.numer = dataRow["count_eks"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    cv.cena = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Vhodn"].ToString().Trim());
                    cv.kvn = dataRow["Vhodn"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoim_eks"].ToString().Trim());
                    cv.kvk = dataRow["stoim_eks"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoim_vhod"].ToString().Trim());
                    cv.kol_czel = dataRow["stoim_vhod"].ToString().Trim();


                    listViewReports.Items.Add(item1);
                    listViewReports.Items[listViewReports.Items.Count - 1].Tag = dataRow["name"].ToString();
                    queueReports.Enqueue(cv);
                }
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
        }
        private void buttonYearReport4_Click(object sender, EventArgs e)
        {

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                string sql = "";
                string[,] arr = { { "Январь-Март", "1", "4" }, { "Апрель-Май", "4", "6" }, { "Июнь", "6", "7" }, { "Июль", "7", "8" }, { "Август", "8", "9" }, { "Сентябрь", "9", "10" }, { "Октябрь-Декабрь", "10", "12" } };
                string[,] arr2 = { { "Мастер-класс по глине", "15а" }, { "Мастер-класс по куклам", "16а" } };
                bool ini = true;
                for (int x = 0; x < (arr.Length / 3); x++)
                {
                    for (int i = 0; i < (arr2.Length / 2); i++)
                    {
                        if (ini == false) { sql += " union all "; }
                        sql += string.Format("select \"{0}\" as month, \"{1}\" as name, (ifnull(sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)),0) + (select ifnull(sum(`plategki`.`Kol_czel`),0) from `plategki` where `plategki`.`N_ekskursii` = \"{2}\" and (`plategki`.`date` >= \"{5}-{3}-01\") and (`plategki`.`date` < \"{5}-{4}-01\")) ) AS `Kol_czel`, ifnull(sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) + (select ifnull(sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))),0) from (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where `plategki`.`N_ekskursii` = \"{2}\" and (`plategki`.`date` >= \"{5}-{3}-01\") and (`plategki`.`date` < \"{5}-{4}-01\")) AS `stoim` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where `zhurnal`.`N_ekskursii` = \"{2}\" and  (`zhurnal`.`date` >= \"{5}-{3}-01\") and (`zhurnal`.`date` < \"{5}-{4}-01\")", arr[x, 0], arr2[i, 0], arr2[i, 1], arr[x, 1], arr[x, 2], comboBoxYear.Text.ToString().Trim());
                        ini = false;

                    }
                }
                //throw new System.InvalidOperationException(sql);    
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                listViewReports.Items.Clear();

                listViewReports.Columns.Clear();
                listViewReports.Columns.Add("Месяц", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Назв.мастер-класса", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Сумма", -2, HorizontalAlignment.Left);

                listViewReports.Columns[0].Width = 150;
                listViewReports.Columns[1].Width = 150;
                listViewReports.Columns[2].Width = 150;
                listViewReports.Columns[3].Width = 150;

                queueReports.Clear();
                _Reports cv = new _Reports();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["month"].ToString().Trim());
                    cv.date = dataRow["month"].ToString().Trim();
                    item1.SubItems.Add(dataRow["name"].ToString().Trim());
                    cv.date = dataRow["name"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    cv.numer = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoim"].ToString().Trim());
                    cv.cena = dataRow["stoim"].ToString().Trim();
                    
                    listViewReports.Items.Add(item1);
                    listViewReports.Items[listViewReports.Items.Count - 1].Tag = dataRow["name"].ToString();
                    queueReports.Enqueue(cv);
                }
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
        }
        private void buttonYearReport_Click(object sender, EventArgs e)
        {
            
        try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                string sql = "";
                string[,] arr = { { "Январь-Март", "1", "4" }, { "Апрель-Май", "4", "6" }, { "Июнь", "6", "7" }, { "Июль", "7", "8" }, { "Август", "8", "9" }, { "Сентябрь", "9", "10" }, { "Октябрь-Декабрь", "10", "12" } };
                bool ini = true;
                for (int x = 0; x < (arr.Length / 3); x++)
                {     
                        if (ini == false) { sql += " union all "; }
                        sql += string.Format("select \"{1}\" as month,  ((count(0) + (select count(0) from (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where (`plategki`.`date` >= \"{0}-{2}-01\") and (`plategki`.`date` < \"{0}-{3}-01\"))) - (select count(0) from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where (`zhurnal`.`date` >= \"{0}-{2}-01\") and (`zhurnal`.`date` < \"{0}-{3}-01\") and (`ekskursii`.`nazvanie` like 'Входной%'))) as `Kol_eks`, ifnull(sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) AS `stoimost`, (select ifnull(sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))),0) from (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where (`plategki`.`date` >= \"{0}-{2}-01\") and (`plategki`.`date` < \"{0}-{3}-01\")) as `stoim_bez`, (ifnull(sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)),0) + (select ifnull(sum(`plategki`.`Kol_czel`),0)  from `plategki`  where (`plategki`.`date` >= \"{0}-{2}-01\") and (`plategki`.`date` < \"{0}-{3}-01\")) ) AS `Kol_czel`, (select ifnull(sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)),0) from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where `ekskursii`.`nazvanie` like \"%(л)\" and (`zhurnal`.`date` >= \"{0}-{2}-01\") and (`zhurnal`.`date` < \"{0}-{3}-01\") ) as lgot_kol, (select ifnull(sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where `ekskursii`.`nazvanie` like \"%(л)\" and (`zhurnal`.`date` >= \"{0}-{2}-01\") and (`zhurnal`.`date` < \"{0}-{3}-01\") )AS `lgot_stoimost` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where (`zhurnal`.`date` >= \"{0}-{2}-01\") and (`zhurnal`.`date` < \"{0}-{3}-01\")", comboBoxYear.Text.ToString().Trim(), arr[x, 0], arr[x, 1], arr[x, 2]);
                        ini = false;
       
                }
                //throw new System.InvalidOperationException(sql);    
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                listViewReports.Items.Clear();

                listViewReports.Columns.Clear();
                listViewReports.Columns.Add("Месяц", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Количество экскурсий", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Стоимость без", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Кол-во льготных", -2, HorizontalAlignment.Left);
                listViewReports.Columns.Add("Стоимость льготных", -2, HorizontalAlignment.Left);
                listViewReports.Columns[0].Width = 150;
                listViewReports.Columns[1].Width = 150;
                listViewReports.Columns[2].Width = 150;
                listViewReports.Columns[3].Width = 150;
                listViewReports.Columns[4].Width = 150;
                listViewReports.Columns[5].Width = 150;
                listViewReports.Columns[6].Width = 150;


                queueReports.Clear();
                _Reports cv = new _Reports();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["month"].ToString().Trim());
                    cv.date = dataRow["month"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_eks"].ToString().Trim());
                    cv.numer = dataRow["Kol_eks"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoimost"].ToString().Trim());
                    cv.cena= dataRow["stoimost"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoim_bez"].ToString().Trim());
                    cv.kvn = dataRow["stoim_bez"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    cv.kvk = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["lgot_kol"].ToString().Trim());
                    cv.kol_czel = dataRow["lgot_kol"].ToString().Trim();
                    item1.SubItems.Add(dataRow["lgot_stoimost"].ToString().Trim());
                    cv.kol_czel = dataRow["lgot_stoimost"].ToString().Trim();

                    listViewReports.Items.Add(item1);
                    listViewReports.Items[listViewReports.Items.Count - 1].Tag = dataRow["month"].ToString();
                    queueReports.Enqueue(cv);
                }
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
        }
        //
        private void comboBoxEkskursija_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEkskursija.SelectedIndex == -1)
                return;
            m_ekskursii = (_Ekskursii)comboBoxEkskursija.SelectedItem;
        }

        private void comboBoxEkskursovod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEkskursovod.SelectedIndex == -1)
                return;
            m_ekskursovody = (_Ekskursovody)comboBoxEkskursovod.SelectedItem;
        }

        private void comboBoxEkskursija_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
          
    }
      /*
         * 
         * 
         * сумма прописью
         *   
         * 
         */

    public class NumByWords
    {
        public static string RurPhrase(decimal money)
        {
            return CurPhrase(money, "рубль", "рубля", "рублей", "копейка", "копейки", "копеек");
        }

        public static string UsdPhrase(decimal money)
        {
            return CurPhrase(money, "доллар США", "доллара США", "долларов США", "цент", "цента", "центов");
        }

        public static string NumPhrase(ulong Value, bool IsMale)
        {
            if (Value == 0UL) return "Ноль";
            string[] Dek1 = { "", " од", " дв", " три", " четыре", " пять", " шесть", " семь", " восемь", " девять", " десять", " одиннадцать", " двенадцать", " тринадцать", " четырнадцать", " пятнадцать", " шестнадцать", " семнадцать", " восемнадцать", " девятнадцать" };
            string[] Dek2 = { "", "", " двадцать", " тридцать", " сорок", " пятьдесят", " шестьдесят", " семьдесят", " восемьдесят", " девяносто" };
            string[] Dek3 = { "", " сто", " двести", " триста", " четыреста", " пятьсот", " шестьсот", " семьсот", " восемьсот", " девятьсот" };
            string[] Th = { "", "", " тысяч", " миллион", " миллиард", " триллион", " квадрилион", " квинтилион" };
            string str = "";
            for (byte th = 1; Value > 0; th++)
            {
                ushort gr = (ushort)(Value % 1000);
                Value = (Value - gr) / 1000;
                if (gr > 0)
                {
                    byte d3 = (byte)((gr - gr % 100) / 100);
                    byte d1 = (byte)(gr % 10);
                    byte d2 = (byte)((gr - d3 * 100 - d1) / 10);
                    if (d2 == 1) d1 += (byte)10;
                    bool ismale = (th > 2) || ((th == 1) && IsMale);
                    str = Dek3[d3] + Dek2[d2] + Dek1[d1] + EndDek1(d1, ismale) + Th[th] + EndTh(th, d1) + str;
                };
            };
            str = str.Substring(1, 1).ToUpper() + str.Substring(2);
            return str;
        }

        #region Private members
        private static string CurPhrase(decimal money,
            string word1, string word234, string wordmore,
            string sword1, string sword234, string swordmore)
        {
            money = decimal.Round(money, 2);
            decimal decintpart = decimal.Truncate(money);
            ulong intpart = decimal.ToUInt64(decintpart);
            string str = NumPhrase(intpart, true) + " ";
            byte endpart = (byte)(intpart % 100UL);
            if (endpart > 19) endpart = (byte)(endpart % 10);
            switch (endpart)
            {
                case 1: str += word1; break;
                case 2:
                case 3:
                case 4: str += word234; break;
                default: str += wordmore; break;
            }
            byte fracpart = decimal.ToByte((money - decintpart) * 100M);
            str += " " + ((fracpart < 10) ? "0" : "") + fracpart.ToString() + " ";
            if (fracpart > 19) fracpart = (byte)(fracpart % 10);
            switch (fracpart)
            {
                case 1: str += sword1; break;
                case 2:
                case 3:
                case 4: str += sword234; break;
                default: str += swordmore; break;
            };
            return str;
        }
        private static string EndTh(byte ThNum, byte Dek)
        {
            bool In234 = ((Dek >= 2) && (Dek <= 4));
            bool More4 = ((Dek > 4) || (Dek == 0));
            if (((ThNum > 2) && In234) || ((ThNum == 2) && (Dek == 1))) return "а";
            else if ((ThNum > 2) && More4) return "ов";
            else if ((ThNum == 2) && In234) return "и";
            else return "";
        }
        private static string EndDek1(byte Dek, bool IsMale)
        {
            if ((Dek > 2) || (Dek == 0)) return "";
            else if (Dek == 1)
            {
                if (IsMale) return "ин";
                else return "на";
            }
            else
            {
                if (IsMale) return "а";
                else return "е";
            }
        }
        #endregion
    }

}

    
