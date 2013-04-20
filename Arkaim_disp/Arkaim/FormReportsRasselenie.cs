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

    public struct _ReportsRasselenie
    {
        public _ReportsRasselenie(string date, string date2, string zhitie, string city, string kol_czel, string money, string proc)
        {
            this.date_val = date;
            this.zhitie_val = zhitie;
            this.city_val = city;
            this.kol_czel_val = kol_czel;
            this.money_val = money;
            this.proc_val = proc;
            this.date2_val = date2;
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
        public string zhitie
        {
            get
            {
                return zhitie_val;
            }
            set
            {
                zhitie_val = value;
            }
        }
        public string city
        {
            get
            {
                return city_val;
            }
            set
            {
                city_val = value;
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
        public string money
        {
            get
            {
                return money_val;
            }
            set
            {
                money_val = value;
            }
        }
        public string proc
        {
            get
            {
                return proc_val;
            }
            set
            {
                proc_val = value;
            }
        }
        public string date2
        {
            get
            {
                return date2_val;
            }
            set
            {
                date2_val = value;
            }
        }

        public override string ToString()
        {
            return city_val;
        }

        private string date_val;
        private string zhitie_val;
        private string city_val;
        private string kol_czel_val;
        private string money_val;
        private string proc_val;
        private string date2_val;
    }

    public partial class FormReportsRasselenie : Form
    {
        private FormMain mainWin;
        Queue queueReportsRasselenie = new Queue();
        _ReportsRasselenie m_reportsrasselenie;
        Queue queueZhitie = new Queue();
        _Zhitie m_zhitie;

        public FormReportsRasselenie(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

          /*  listViewReportsRasselenie.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Способ жития", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Город", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Кол-во денег", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Проценты", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns[0].Width = 100;
            listViewReportsRasselenie.Columns[1].Width = 200;
            listViewReportsRasselenie.Columns[2].Width = 200;
            listViewReportsRasselenie.Columns[3].Width = 200;
            listViewReportsRasselenie.Columns[4].Width = 200;
            listViewReportsRasselenie.Columns[5].Width = 200;*/

        }

        private void FormReportsRasselenie_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formReportsRasselenie = null;

        }

        private void FormReportsRasselenie_Load(object sender, EventArgs e)
        {
            
            DateTime pickedDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            dateTimePicker1.Value = pickedDate;
                        
            textBoxCity.Text = "";

            //refreshReportsRasselenie();
            refreshZhitie();
            refreshYear();

        }

   
        
        private void listViewReportsRasselenie_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            if (listViewReportsRasselenie.FocusedItem == null)
                return;

            int k = queueReportsRasselenie.Count;
            for (int i = 0; i < k; i++)
            {
                m_reportsrasselenie = (_ReportsRasselenie)queueReportsRasselenie.Dequeue();
                if (m_reportsrasselenie.date.ToString() == (string)listViewReportsRasselenie.Items[listViewReportsRasselenie.FocusedItem.Index].Tag)
                {
                   // textBoxName.Text = m_reportsrasselenie.date;
                  //  textBoxCity.Text = m_reportsrasselenie.city;

                    _Zhitie c;
                    int k2 = queueZhitie.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        c = (_Zhitie)queueZhitie.Dequeue();
                        if (c.nazvanie.ToString() == m_reportsrasselenie.zhitie.ToString())
                        {
                            for (int iii = 0; iii < comboBoxZhitie.Items.Count; iii++)
                            {
                                if (((_Zhitie)comboBoxZhitie.Items[iii]).nazvanie == c.nazvanie)
                                {
                                    comboBoxZhitie.SelectedIndex = iii;
                                    m_zhitie = c;
                                    break;
                                }
                            }
                        };
                        queueZhitie.Enqueue(c);
                    }
                    queueReportsRasselenie.Enqueue(m_reportsrasselenie);
                    break;
                };

                queueReportsRasselenie.Enqueue(m_reportsrasselenie);
            }

        }

        public void refreshReportsRasselenie()
        {
            listViewReportsRasselenie.Items.Clear();
            queueReportsRasselenie.Clear();


            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `date`, `zhitie`  FROM `rasselenie` ORDER BY `date`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewReportsRasselenie.Items.Clear();
                queueReportsRasselenie.Clear();
                _ReportsRasselenie t = new _ReportsRasselenie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["date"].ToString().Trim(), 0);
                    t.date= dataRow["date"].ToString();
                    item1.SubItems.Add(dataRow["zhitie"].ToString().Trim());
                    t.zhitie = dataRow["zhitie"].ToString().Trim();
                    listViewReportsRasselenie.Items.Add(item1);
                    listViewReportsRasselenie.Items[listViewReportsRasselenie.Items.Count - 1].Tag = dataRow["date"].ToString();
                    queueReportsRasselenie.Enqueue(t);
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
        //всякие отчеты
        //житие
        public void refreshReportsZhitie()
        {
            listViewReportsRasselenie.Items.Clear();
            queueReportsRasselenie.Clear();

            listViewReportsRasselenie.Columns.Clear();
            listViewReportsRasselenie.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Житие в", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Кол-во денег", -2, HorizontalAlignment.Left);

            listViewReportsRasselenie.Columns[0].Width = 150;
            listViewReportsRasselenie.Columns[1].Width = 150;
            listViewReportsRasselenie.Columns[2].Width = 150;
           
            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("select 'наличные' AS `date`, `rasselenie`.`zhitie`,sum(`rasselenie`.`Kol_czel`) AS `Kol_czel`,sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where `zhitie`.`nazvanie` = '{0}' and `rasselenie`.`date` >= '{1}' and `rasselenie`.`date` <= '{2}' union all select 'безналичность' AS `date`, `rasselenie`.`zhitie_bez`, sum(`rasselenie`.`Kol_czel`) AS `Kol_czel`, sum(((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) where `zhitie`.`nazvanie` = '{0}' and `rasselenie`.`date` >= '{1}' and `rasselenie`.`date` <= '{2}'", comboBoxZhitie.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                if (comboBoxZhitie.SelectedIndex == -1)
                    sql = String.Format("select `rasselenie`.`date` AS `date`,`rasselenie`.`zhitie` AS `zhitie`,sum(`rasselenie`.`Kol_czel`) AS `Kol_czel`,sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where `rasselenie`.`date` >= '{0}' and `rasselenie`.`date` <= '{1}' group by `rasselenie`.`zhitie` union all select `rasselenie`.`date` AS `date`,concat(`rasselenie`.`zhitie_bez`,'(б/н)') AS `zhitie`,sum(`rasselenie`.`Kol_czel`) AS `Kol_czel`,sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0))) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) where `rasselenie`.`date` >= '{0}' and `rasselenie`.`date` <= '{1}' group by `rasselenie`.`zhitie_bez`", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);

                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewReportsRasselenie.Items.Clear();
                queueReportsRasselenie.Clear();
                _ReportsRasselenie v = new _ReportsRasselenie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["date"].ToString().Trim());
                    v.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["zhitie"].ToString().Trim());
                    v.zhitie= dataRow["zhitie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    v.kol_czel = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    v.money = dataRow["cena"].ToString().Trim();
             

                    listViewReportsRasselenie.Items.Add(item1);
                    listViewReportsRasselenie.Items[listViewReportsRasselenie.Items.Count - 1].Tag = dataRow["date"].ToString();
                    queueReportsRasselenie.Enqueue(v);
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
        private void buttonZhitie_Click(object sender, EventArgs e)
        {
            refreshReportsZhitie();
        }
        //деньги
        public void refreshReportsMoney()
        {
            listViewReportsRasselenie.Items.Clear();
            queueReportsRasselenie.Clear();
                        
            listViewReportsRasselenie.Columns.Clear();
            listViewReportsRasselenie.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Дата 2", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Кол-во денег", -2, HorizontalAlignment.Left);
           

            listViewReportsRasselenie.Columns[0].Width = 150;
            listViewReportsRasselenie.Columns[1].Width = 150;
            listViewReportsRasselenie.Columns[2].Width = 150;
            listViewReportsRasselenie.Columns[3].Width = 150;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("select '{0}' AS `date`, '{1}' AS `date2`, sum(`rasselenie`.`Kol_czel`) AS `Kol_czel`, sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where `rasselenie`.`date` >= '{0}' and `rasselenie`.`date` <= '{1}' union all select '{0}' AS `date`, '{1}' AS `date2`, sum(`rasselenie`.`Kol_czel`) AS `Kol_czel`,sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0))) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) where `rasselenie`.`date` >= '{0}' and `rasselenie`.`date` <= '{1}'", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
            
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewReportsRasselenie.Items.Clear();
                queueReportsRasselenie.Clear();
                _ReportsRasselenie v = new _ReportsRasselenie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["date"].ToString().Trim());
                    v.date= dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["date2"].ToString().Trim());
                    v.zhitie = dataRow["date2"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    v.kol_czel = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    v.money = dataRow["cena"].ToString().Trim();
                    
                    
                    listViewReportsRasselenie.Items.Add(item1);
                    listViewReportsRasselenie.Items[listViewReportsRasselenie.Items.Count - 1].Tag = dataRow["date"].ToString();
                    queueReportsRasselenie.Enqueue(v);
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
        private void buttonMoney_Click(object sender, EventArgs e)
        {
            refreshReportsMoney();

        }
        
        //города
        public void refreshReportsCity()
        {
            listViewReportsRasselenie.Items.Clear();
            queueReportsRasselenie.Clear();
            
            listViewReportsRasselenie.Columns.Clear();
            listViewReportsRasselenie.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Город", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
            listViewReportsRasselenie.Columns.Add("Кол-во денег", -2, HorizontalAlignment.Left);

            listViewReportsRasselenie.Columns[0].Width = 150;
            listViewReportsRasselenie.Columns[1].Width = 150;
            listViewReportsRasselenie.Columns[2].Width = 150;
            listViewReportsRasselenie.Columns[3].Width = 150;
            
            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("select `rasselenie`.`date` AS `date`,`rasselenie`.`City` AS `City`,sum(`rasselenie`.`Kol_czel`) AS `kol_czel`,sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where `rasselenie`.`City`='{0}' group by `rasselenie`.`City` union all select `rasselenie`.`date` AS `date`,concat(`rasselenie`.`City`,' б/н') AS `City`,sum(`rasselenie`.`Kol_czel`) AS `kol_czel`,sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0))) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) where `rasselenie`.`City`='{0}' group by `rasselenie`.`City` order by `cena` desc", textBoxCity.Text);
                if(textBoxCity.Text.ToString() == "")
                    sql = "select `rasselenie`.`date` AS `date`,`rasselenie`.`City` AS `City`,sum(`rasselenie`.`Kol_czel`) AS `kol_czel`,sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) group by `rasselenie`.`City` union all select `rasselenie`.`date` AS `date`,concat(`rasselenie`.`City`,' б/н') AS `City`,sum(`rasselenie`.`Kol_czel`) AS `kol_czel`,sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0))) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) group by `rasselenie`.`City` order by `cena` desc";
                    
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewReportsRasselenie.Items.Clear();
                queueReportsRasselenie.Clear();
                _ReportsRasselenie v = new _ReportsRasselenie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["date"].ToString().Trim());
                    v.date= dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["City"].ToString().Trim());
                    v.zhitie = dataRow["City"].ToString().Trim();
                    item1.SubItems.Add(dataRow["kol_czel"].ToString().Trim());
                    v.kol_czel = dataRow["kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    v.money = dataRow["cena"].ToString().Trim();
                    //item1.SubItems.Add(dataRow["zhitie"].ToString().Trim());
                    //v.zhitie = dataRow["zhitie"].ToString().Trim();

                    listViewReportsRasselenie.Items.Add(item1);
                    listViewReportsRasselenie.Items[listViewReportsRasselenie.Items.Count - 1].Tag = dataRow["date"].ToString();
                    queueReportsRasselenie.Enqueue(v);
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
        private void buttonCity_Click(object sender, EventArgs e)
        {
            refreshReportsCity();

        }
        private void buttonExcel_Click(object sender, EventArgs e)
        {
            if (listViewReportsRasselenie.Items.Count == 0)
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


            foreach (ColumnHeader col in listViewReportsRasselenie.Columns)
            {
                excelApp.Cells[i2 - 1, i3] = col.Text; i3++;
            }


            foreach (ListViewItem lvi in listViewReportsRasselenie.Items)
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
        //
        //
        public void refreshZhitie()
        {
            string Zhitie = "";
            if (comboBoxZhitie.SelectedIndex != -1)
            {
                Zhitie = m_zhitie.nazvanie.ToString();
            }

            comboBoxZhitie.Items.Clear();
            queueZhitie.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `nazvanie` FROM `zhitie` ORDER BY `id`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                _Zhitie c = new _Zhitie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    c.id = dataRow["id"].ToString();
                    c.nazvanie = dataRow["nazvanie"].ToString().Trim();

                    comboBoxZhitie.Items.Add(c);


                    if (Zhitie != "" && Zhitie == c.nazvanie.ToString())
                    {
                        comboBoxZhitie.SelectedItem = c;
                    }


                    queueZhitie.Enqueue(c);
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


        private void comboBoxZhitie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxZhitie.SelectedIndex == -1)
                return;
            m_zhitie = (_Zhitie)comboBoxZhitie.SelectedItem;
        }




        //year reports
        private void refreshYear()
        {
            DateTime dt = DateTime.Now;

            mainWin.m_dbConnector.Lock();
            MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

            string sql = String.Format("SELECT YEAR(`date`) AS `date` FROM `rasselenie` order by `date` asc limit 1");
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            myAdapter.SelectCommand = new MySqlCommand(sql, conn);
            DataSet dataSet = new DataSet();
            myAdapter.Fill(dataSet);
            DataTable dataTable = dataSet.Tables[0];
            mainWin.m_dbConnector.Unlock();
            DataRow dataRow = dataTable.Rows[0];
            int dat1 = dt.Year;
            int dat2 = int.Parse(string.Format("{0}", dataRow["date"]));
            for (; dat2 <= dat1; dat2++)
            {
                comboBoxYear.Items.Add(dat2);
            }
            comboBoxYear.SelectedIndex = comboBoxYear.Items.Count - 1;
        }
        private void buttonYearReport1_Click(object sender, EventArgs e)
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
                    sql += string.Format("select \"{1}\" as month, ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) AS `cena_nal`, ifnull(sum(`rasselenie`.`zhitie_bez`),0) AS `cena_bez`, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) AS `Kol_czel`  from `rasselenie` where (`rasselenie`.`date` >= \"{0}-{2}-01\") and (`rasselenie`.`date` < \"{0}-{3}-01\") and (`rasselenie`.`bron` = \"\" and `rasselenie`.`bron_bez` = \"\"))  AS `Kol_czel_nal`, ifnull(sum(`rasselenie`.`bron`),0) AS `bron_nal`, ifnull(sum(`rasselenie`.`bron_bez`),0) AS `bron_bez`, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) AS `Kol_czel` from `rasselenie` where (`rasselenie`.`date` >= \"{0}-{2}-01\") and (`rasselenie`.`date` < \"{0}-{3}-01\") and (`rasselenie`.`bron` != \"\" OR `rasselenie`.`bron_bez` != \"\"))  AS `Kol_czel_bron`, ifnull(sum(parkovka),0) AS `park`, ifnull(sum(`rasselenie`.`Kol_czel`),0) AS `Sum_czel`, (ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) AS `Sum_nal`, (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) AS `Sum_bez`, ((ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) + (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) + ifnull(sum(parkovka),0)) AS `Sum_all` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{0}-{2}-01\") and (`rasselenie`.`date` < \"{0}-{3}-01\")", comboBoxYear.Text.ToString().Trim(), arr[x, 0], arr[x, 1], arr[x, 2]);
                    ini = false;

                }
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                listViewReportsRasselenie.Items.Clear();


                listViewReportsRasselenie.Columns.Clear();
                listViewReportsRasselenie.Columns.Add("Месяц", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Проживание нал", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Проживание без", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Проживание чел", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Бронь нал", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Бронь без", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Бронь чел", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Парковка", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Итого чел", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Итого нал", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Итого без", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Итого сумм", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns[0].Width = 150;
                listViewReportsRasselenie.Columns[1].Width = 150;
                listViewReportsRasselenie.Columns[2].Width = 150;
                listViewReportsRasselenie.Columns[3].Width = 150;
                listViewReportsRasselenie.Columns[4].Width = 150;
                listViewReportsRasselenie.Columns[5].Width = 150;
                listViewReportsRasselenie.Columns[6].Width = 150;
                listViewReportsRasselenie.Columns[7].Width = 150;
                listViewReportsRasselenie.Columns[8].Width = 150;
                listViewReportsRasselenie.Columns[9].Width = 150;
                listViewReportsRasselenie.Columns[10].Width = 150;
                listViewReportsRasselenie.Columns[11].Width = 150;

                queueReportsRasselenie.Clear();
                _ReportsRasselenie cv = new _ReportsRasselenie();
               
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["month"].ToString().Trim());
                    cv.date = dataRow["month"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena_nal"].ToString().Trim());
                    cv.money = dataRow["cena_nal"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena_bez"].ToString().Trim());
                    cv.money = dataRow["cena_bez"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_nal"].ToString().Trim());
                    cv.kol_czel = dataRow["Kol_czel_nal"].ToString().Trim();
                    item1.SubItems.Add(dataRow["bron_nal"].ToString().Trim());
                    cv.money = dataRow["bron_nal"].ToString().Trim();
                    item1.SubItems.Add(dataRow["bron_bez"].ToString().Trim());
                    cv.money = dataRow["bron_bez"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_bron"].ToString().Trim());
                    cv.kol_czel = dataRow["Kol_czel_bron"].ToString().Trim();

                    item1.SubItems.Add(dataRow["park"].ToString().Trim());
                    cv.zhitie = dataRow["park"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_czel"].ToString().Trim());
                    cv.kol_czel = dataRow["Sum_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_nal"].ToString().Trim());
                    cv.money = dataRow["Sum_nal"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_bez"].ToString().Trim());
                    cv.money = dataRow["Sum_bez"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_all"].ToString().Trim());
                    cv.money = dataRow["Sum_all"].ToString().Trim();


                    listViewReportsRasselenie.Items.Add(item1);
                    listViewReportsRasselenie.Items[listViewReportsRasselenie.Items.Count - 1].Tag = dataRow["month"].ToString();
                    queueReportsRasselenie.Enqueue(cv);
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

        private void buttonYearReport2_Click(object sender, EventArgs e)
        {

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("select distinct `nazvanie` from `zhitie`");
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);         
                DataTable dataTable = dataSet.Tables[0];
                mainWin.m_dbConnector.Unlock();
                
                List<string> zhitie = new List<string>();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    zhitie.Add(dataRow["nazvanie"].ToString().Trim());
                }
                string[] arr = zhitie.ToArray();

                bool ini = true;
                sql = "";
                for (int x = 0; x < arr.Length; x++)
                {
                        if (ini == false) { sql += " union all "; }
                        sql += string.Format("select \"{0}\" AS name, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-01-01\") and (`rasselenie`.`date` < \"{1}-04-01\") and `zhitie` = \"{0}\") AS `Kol_czel_jan`, (select ((ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) + (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) + ifnull(sum(parkovka),0)) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-01-01\") and (`rasselenie`.`date` < \"{1}-04-01\") and `zhitie` = \"{0}\" ) AS `Sum_jan`, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-04-01\") and (`rasselenie`.`date` < \"{1}-06-01\") and `zhitie` = \"{0}\") AS `Kol_czel_apr`, (select ((ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) + (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) + ifnull(sum(parkovka),0)) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-04-01\") and (`rasselenie`.`date` < \"{1}-06-01\") and `zhitie` = \"{0}\" ) AS `Sum_apr`, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-06-01\") and (`rasselenie`.`date` < \"{1}-07-01\") and `zhitie` = \"{0}\") AS `Kol_czel_may`, (select ((ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) + (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) + ifnull(sum(parkovka),0)) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-06-01\") and (`rasselenie`.`date` < \"{1}-07-01\") and `zhitie` = \"{0}\" ) AS `Sum_may`, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-07-01\") and (`rasselenie`.`date` < \"{1}-08-01\") and `zhitie` = \"{0}\" ) AS `Kol_czel_jun`, (select ((ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) + (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) + ifnull(sum(parkovka),0)) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-07-01\") and (`rasselenie`.`date` < \"{1}-08-01\") and `zhitie` = \"{0}\" ) AS `Sum_jun`, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-08-01\") and (`rasselenie`.`date` < \"{1}-09-01\") and `zhitie` = \"{0}\" ) AS `Kol_czel_jul`, (select ((ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) + (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) + ifnull(sum(parkovka),0)) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-08-01\") and (`rasselenie`.`date` < \"{1}-09-01\") and `zhitie` = \"{0}\" ) AS `Sum_jul`, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-09-01\") and (`rasselenie`.`date` < \"{1}-10-01\") and `zhitie` = \"{0}\" ) AS `Kol_czel_aug`, (select ((ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) + (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) + ifnull(sum(parkovka),0)) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-09-01\") and (`rasselenie`.`date` < \"{1}-10-01\") and `zhitie` = \"{0}\" ) AS `Sum_aug`, (select ifnull(sum(`rasselenie`.`Kol_czel`),0) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-10-01\") and (`rasselenie`.`date` < \"{1}-12-01\") and `zhitie` = \"{0}\") AS `Kol_czel_oct`, (select ((ifnull(sum((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`),0) + ifnull(sum(`rasselenie`.`bron`),0)) + (ifnull(sum(`rasselenie`.`zhitie_bez`),0) + ifnull(sum(`rasselenie`.`bron_bez`),0)) + ifnull(sum(parkovka),0)) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where (`rasselenie`.`date` >= \"{1}-10-01\") and (`rasselenie`.`date` < \"{1}-12-01\") and `zhitie` = \"{0}\" ) AS `Sum_oct`", arr[x], comboBoxYear.Text.ToString().Trim());
                        ini = false;
                }


                //throw new System.InvalidOperationException(sql);
                
                myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
                
                listViewReportsRasselenie.Items.Clear();


                listViewReportsRasselenie.Columns.Clear();
                listViewReportsRasselenie.Columns.Add("Виды расселения", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Янв-Март Туристы", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Янв-Март Сумма", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Апр-Май Туристы", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Апр-Май Сумма", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Июнь Туристы", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Июнь Сумма", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Июль Туристы", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Июль Сумма", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Август Туристы", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Август Сумма", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Сент Туристы", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Сент Сумма", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Окт-Дек Туристы", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns.Add("Окт-Дек Сумма", -2, HorizontalAlignment.Left);
                listViewReportsRasselenie.Columns[0].Width = 150;
                listViewReportsRasselenie.Columns[1].Width = 150;
                listViewReportsRasselenie.Columns[2].Width = 150;
                listViewReportsRasselenie.Columns[3].Width = 150;
                listViewReportsRasselenie.Columns[4].Width = 150;
                listViewReportsRasselenie.Columns[5].Width = 150;
                listViewReportsRasselenie.Columns[6].Width = 150;
                listViewReportsRasselenie.Columns[7].Width = 150;
                listViewReportsRasselenie.Columns[8].Width = 150;
                listViewReportsRasselenie.Columns[9].Width = 150;
                listViewReportsRasselenie.Columns[10].Width = 150;
                listViewReportsRasselenie.Columns[11].Width = 150;
                listViewReportsRasselenie.Columns[12].Width = 150;
                listViewReportsRasselenie.Columns[13].Width = 150;
                listViewReportsRasselenie.Columns[14].Width = 150;

                queueReportsRasselenie.Clear();
                _ReportsRasselenie cv = new _ReportsRasselenie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["name"].ToString().Trim());
                    cv.date= dataRow["name"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_jan"].ToString().Trim());
                    cv.money = dataRow["Kol_czel_jan"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_jan"].ToString().Trim());
                    cv.money = dataRow["Sum_jan"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_apr"].ToString().Trim());
                    cv.kol_czel = dataRow["Kol_czel_apr"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_apr"].ToString().Trim());
                    cv.money = dataRow["Sum_apr"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_may"].ToString().Trim());
                    cv.money = dataRow["Kol_czel_may"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_may"].ToString().Trim());
                    cv.kol_czel = dataRow["Sum_may"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_jun"].ToString().Trim());
                    cv.zhitie = dataRow["Kol_czel_jun"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_jun"].ToString().Trim());
                    cv.kol_czel = dataRow["Sum_jun"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_jul"].ToString().Trim());
                    cv.money = dataRow["Kol_czel_jul"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_jul"].ToString().Trim());
                    cv.money = dataRow["Sum_jul"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_aug"].ToString().Trim());
                    cv.money = dataRow["Kol_czel_aug"].ToString().Trim();

                    item1.SubItems.Add(dataRow["Sum_aug"].ToString().Trim());
                    cv.money = dataRow["Sum_aug"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel_oct"].ToString().Trim());
                    cv.money = dataRow["Kol_czel_oct"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Sum_oct"].ToString().Trim());
                    cv.money = dataRow["Sum_oct"].ToString().Trim();
                    
                    listViewReportsRasselenie.Items.Add(item1);
                    listViewReportsRasselenie.Items[listViewReportsRasselenie.Items.Count - 1].Tag = dataRow["name"].ToString();
                    queueReportsRasselenie.Enqueue(cv);
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

    }
}