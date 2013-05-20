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
    public struct _Zhurnal
    {
        public _Zhurnal(string id, string date, string fio, string numer, string kvn, string kvk, string kolczel, string cena, string stoimost, string zakaz)
        {
            this.id_val = id;
            this.date_val = date;
            this.fio_val = fio;
            this.numer_val = numer;
            this.kvn_val = kvn;
            this.kvk_val = kvk;
            this.kolczel_val = kolczel;
            this.cena_val = cena;
            this.stoimost_val = stoimost;
            this.zakaz_val = zakaz;
          }

        public string id
        {
            get
            {
                return id_val;
            }
            set
            {
                id_val = value;
            }
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
        public string fio
        {
            get
            {
                return fio_val;
            }
            set
            {
                fio_val = value;
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
        public string kolczel
        {
            get
            {
                return kolczel_val;
            }
            set
            {
                kolczel_val = value;
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
        public string stoimost
        {
            get
            {
                return stoimost_val;
            }
            set
            {
                stoimost_val = value;
            }
        }
        public string zakaz
        {
            get
            {
                return zakaz_val;
            }
            set
            {
                zakaz_val = value;
            }
        }
       
        public override string ToString(){
            return fio_val;
        }

        private string id_val;
        private string date_val;
        private string fio_val; 
        private string numer_val;
        private string kvn_val;
        private string kvk_val;
        private string kolczel_val;
        private string cena_val;
        private string stoimost_val;
        private string zakaz_val;
    }

    public partial class FormZhurnal : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueZhurnal = new Queue();
        _Zhurnal m_zhurnal;
        // экскурсоводы и экскурсии
        _Ekskursovody m_ekskursovody;
        Queue queueEkskursovod = new Queue();
        _Ekskursii m_ekskursii;
        Queue queueEkskursii = new Queue();
        _Zakazczik m_zakazczik;
        Queue queueZakazczik = new Queue();

        public FormZhurnal(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewZhurnal.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Ф.И.О.", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Номер экскурсии", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Квитанции начало", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Квитанции конец", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Кол-во чел", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Цена", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Заказчик", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns[0].Width = 25;
            listViewZhurnal.Columns[1].Width = 150;
            listViewZhurnal.Columns[2].Width = 150;
            listViewZhurnal.Columns[3].Width = 150;
            listViewZhurnal.Columns[4].Width = 150;
            listViewZhurnal.Columns[5].Width = 150;
            listViewZhurnal.Columns[6].Width = 150;
            listViewZhurnal.Columns[7].Width = 150;
            listViewZhurnal.Columns[8].Width = 150;
            listViewZhurnal.Columns[9].Width = 150;
        }

        private void FormZhurnal_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formZhurnal = null;
        }
        
        private void FormZhurnal_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;

            DateTime pickedDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            dateTimePicker1.Value = pickedDate;

            comboBoxEkskursovod.SelectedIndex = -1;
            comboBoxEkskursovod.Enabled = false;
            comboBoxEkskursija.SelectedIndex = -1;
            comboBoxEkskursija.Enabled = false;
            comboBoxZakazczik.SelectedIndex = -1;
            comboBoxZakazczik.Enabled = false;
            textBoxKvN.Text = "";
            textBoxKvN.Enabled = false;
            textBoxKvK.Text = "";
            textBoxKvK.Enabled = false;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = false;
            dateTimePickerDate.Enabled = false;

            refreshZhurnal();
            refreshEkskursii();
            refreshEkskursovody();
            refreshZakazczik();
        }
        
        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxKvN.Text = "";
            textBoxKvN.Enabled = true;
            textBoxKvK.Text = "";
            textBoxKvK.Enabled = true;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = true;
            dateTimePickerDate.Enabled = true;
            comboBoxEkskursovod.SelectedIndex = -1;
            comboBoxEkskursovod.Enabled = true;
            comboBoxEkskursija.SelectedIndex = -1;
            comboBoxEkskursija.Enabled = true;
            comboBoxZakazczik.Text = "Физлицо";
            //comboBoxZakazczik.SelectedIndex = -1;
            comboBoxZakazczik.Enabled = true;
            
            dateTimePickerDate.Enabled = true;
            dateTimePickerDate.Value = DateTime.Now;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("INSERT INTO `plategki` (`date`, `FiO`, `N_platezhki`, `Kol_czel`, `N_ekskursii`, `zakazczik`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, comboBoxEkskursovod.Text, textBoxKvN.Text, textBoxKolCzel.Text, comboBoxEkskursija.Text, comboBoxZakazczik.Text);
                    if(textBoxKolCzel.Text.Trim() == "")
                        sql = String.Format("INSERT INTO `zhurnal` (`date`, `FiO`, `N_kvit_nach`, `N_kvit_koniec`, `N_ekskursii`, `zakazczik`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, comboBoxEkskursovod.Text, textBoxKvN.Text, textBoxKvK.Text, comboBoxEkskursija.Text, comboBoxZakazczik.Text);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();


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
            else
            {
                if (listViewZhurnal.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `zhurnal` SET `date` = '{0}', `FiO` = '{1}', `N_ekskursii` = '{2}', `N_kvit_nach` = '{3}', `N_kvit_koniec` = '{4}', `zakazczik` = '{5}' WHERE `id` = '{6}'", DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, comboBoxEkskursovod.Text, comboBoxEkskursija.Text, textBoxKvN.Text, textBoxKvK.Text, comboBoxZakazczik.Text, m_zhurnal.id);
                    if (textBoxKvK.Text.Trim() == textBoxKvN.Text.Trim())
                        sql = String.Format("UPDATE `plategki` SET `date`='{0}', `FiO`='{1}', `N_ekskursii` = '{2}', `N_platezhki`='{3}', `Kol_czel`='{4}', `zakazczik`='{5}' WHERE `id` = '{6}'", DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, comboBoxEkskursovod.Text, comboBoxEkskursija.Text, textBoxKvN.Text, textBoxKolCzel.Text, comboBoxZakazczik.Text, m_zhurnal.id);

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                   
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

            textBoxKvN.Text = "";
            textBoxKvN.Enabled = false;
            textBoxKvK.Text = "";
            textBoxKvK.Enabled = false;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = false;
            comboBoxEkskursija.Enabled = false;
            comboBoxEkskursija.SelectedIndex = -1;
            comboBoxEkskursovod.Enabled = false;
            comboBoxEkskursovod.SelectedIndex = -1;
            comboBoxZakazczik.SelectedIndex = -1;
            comboBoxZakazczik.Enabled = false;
            dateTimePickerDate.Enabled = false;
                                   
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshZhurnal();
            refreshEkskursii();
            refreshEkskursovody();
            refreshZakazczik();
        }

        private void listViewZhurnal_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            comboBoxEkskursovod.SelectedIndex = -1;
            comboBoxEkskursovod.Enabled = true;
            comboBoxEkskursija.SelectedIndex = -1;
            comboBoxEkskursija.Enabled = true;
            comboBoxZakazczik.SelectedIndex = -1;
            comboBoxZakazczik.Enabled = true;
            textBoxKvN.Text = "";
            textBoxKvN.Enabled = true;
            textBoxKvK.Text = "";
            textBoxKvK.Enabled = true;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = true;
            dateTimePickerDate.Enabled = true;
                        
            bNew = false;

            if (listViewZhurnal.FocusedItem == null)
                return;

            int k = queueZhurnal.Count;
            for (int i = 0; i < k; i++)
            {
                m_zhurnal = (_Zhurnal)queueZhurnal.Dequeue();
                if (m_zhurnal.id.ToString() == (string)listViewZhurnal.Items[listViewZhurnal.FocusedItem.Index].Tag)
                {
                    comboBoxEkskursija.Text = m_ekskursii.id;
                    comboBoxEkskursovod.Text = m_zhurnal.fio;
                    comboBoxZakazczik.Text = m_zhurnal.zakaz;
                    textBoxKvN.Text = m_zhurnal.kvn;
                    textBoxKvK.Text = m_zhurnal.kvk;
                    textBoxKolCzel.Text = m_zhurnal.kolczel;
                    dateTimePickerDate.Value = DateTime.Parse(m_zhurnal.date);
                    //фигня для comboboxов
                    _Ekskursii c;
                    int k2 = queueEkskursii.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        c = (_Ekskursii)queueEkskursii.Dequeue();
                        if (c.id.ToString() == m_zhurnal.numer.ToString())
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

                    _Zakazczik d;
                    k2 = queueZakazczik.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        d = (_Zakazczik)queueZakazczik.Dequeue();
                        if (d.id.ToString() == m_zhurnal.zakaz.ToString())
                        {
                            for (int iii = 0; iii < comboBoxZakazczik.Items.Count; iii++)
                            {
                                if (((_Zakazczik)comboBoxZakazczik.Items[iii]).id == d.id)
                                {
                                    comboBoxZakazczik.SelectedIndex = iii;
                                    m_zakazczik = d;
                                    break;
                                }
                            }
                        };
                        queueZakazczik.Enqueue(d);
                    }

                    _Ekskursovody a;
                    k2 = queueEkskursovod.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        a = (_Ekskursovody)queueEkskursovod.Dequeue();
                        if (a.name.ToString() == m_zhurnal.fio.ToString())
                        {

                            for (int iii = 0; iii < comboBoxEkskursovod.Items.Count; iii++)
                            {
                                if (((_Ekskursovody)comboBoxEkskursovod.Items[iii]).id == a.id)
                                {
                                    comboBoxEkskursovod.SelectedIndex = iii;
                                    m_ekskursovody = a;
                                    break;
                                }
                            }
                        };

                            queueEkskursovod.Enqueue(a);
                        }

                        queueZhurnal.Enqueue(m_zhurnal);
                        break;
          
                    };

                                            
                queueZhurnal.Enqueue(m_zhurnal);
                }
                }

        public void refreshZhurnal()
        {
            listViewZhurnal.Items.Clear();
            queueZhurnal.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("SELECT id, cast(`zhurnal`.`date` as char) AS `Data`,`zhurnal`.`FiO` AS `FiO`,`zhurnal`.`N_kvit_nach` AS `KvN`,`zhurnal`.`N_kvit_koniec` AS`Rdbnfywbb`,`zhurnal`.`N_ekskursii` AS `N`,((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1) AS`KolCzel`,`ekskursii`.`stoimost` AS `Cena`,(`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` -`zhurnal`.`N_kvit_nach`) + 1)) AS `Stoimost`,`zhurnal`.`zakazczik` AS `Zakaz` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`date` >= '{0}') and (`zhurnal`.`date` <= '{1}')) UNION ALL SELECT id, cast(`plategki`.`date` as char) AS`Data`,`plategki`.`FiO` AS `FiO`,`plategki`.`N_platezhki` AS `KvN`,`plategki`.`N_platezhki` AS`KvK`,`plategki`.`N_ekskursii` AS `N`,`plategki`.`Kol_czel` AS `KolCzel`,`ekskursii`.`stoimost` AS`Cena`,(`ekskursii`.`stoimost` * `plategki`.`Kol_czel`) AS `Stoimost`,`plategki`.`zakazczik` AS `Zakaz` FROM (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE ((`plategki`.`date` >= '{0}') and (`plategki`.`date` <= '{1}'))  order by `Data`", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewZhurnal.Items.Clear();
                queueZhurnal.Clear();
                _Zhurnal t = new _Zhurnal();

              foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim());
                    t.id = dataRow["id"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Data"].ToString().Trim());
                    t.date = dataRow["Data"].ToString().Trim();
                    item1.SubItems.Add(dataRow["FiO"].ToString().Trim());
                    t.fio = dataRow["FiO"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N"].ToString().Trim());
                    t.numer = dataRow["N"].ToString().Trim();
                    item1.SubItems.Add(dataRow["KvN"].ToString().Trim());
                    t.kvn = dataRow["KvN"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Rdbnfywbb"].ToString().Trim());
                    t.kvk = dataRow["Rdbnfywbb"].ToString().Trim();
                    item1.SubItems.Add(dataRow["KolCzel"].ToString().Trim());
                    t.kolczel = dataRow["KolCzel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Cena"].ToString().Trim());
                    t.cena = dataRow["Cena"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Stoimost"].ToString().Trim());
                    t.stoimost = dataRow["Stoimost"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Zakaz"].ToString().Trim());
                    t.zakaz = dataRow["Zakaz"].ToString().Trim();


                    listViewZhurnal.Items.Add(item1);
                    listViewZhurnal.Items[listViewZhurnal.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueZhurnal.Enqueue(t);
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
        
        private void buttonFilter_Click(object sender, EventArgs e)
       {
            listViewZhurnal.Items.Clear();
            queueZhurnal.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = string.Format("SELECT id, cast(`zhurnal`.`date` as char) AS `Data`,`zhurnal`.`FiO` AS `FiO`,`zhurnal`.`N_kvit_nach` AS `KvN`,`zhurnal`.`N_kvit_koniec` AS`Rdbnfywbb`,`zhurnal`.`N_ekskursii` AS `N`,((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1) AS`KolCzel`,`ekskursii`.`stoimost` AS `Cena`,(`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` -`zhurnal`.`N_kvit_nach`) + 1)) AS `Stoimost`,`zhurnal`.`zakazczik` AS `Zakaz` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE ((`zhurnal`.`date` >= '{0}') and (`zhurnal`.`date` <= '{1}')) UNION SELECT id, cast(`plategki`.`date` as char) AS`Data`,`plategki`.`FiO` AS `FiO`,`plategki`.`N_platezhki` AS `KvN`,`plategki`.`N_platezhki` AS`KvK`,`plategki`.`N_ekskursii` AS `N`,`plategki`.`Kol_czel` AS `KolCzel`,`ekskursii`.`stoimost` AS`Cena`,(`ekskursii`.`stoimost` * `plategki`.`Kol_czel`) AS `Stoimost`,`plategki`.`zakazczik` AS `Zakaz` FROM (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) WHERE ((`plategki`.`date` >= '{0}') and (`plategki`.`date` <= '{1}')) union select '' AS `id`,'{1}' AS `Data`,'' AS `FiO`,count(0) AS `KvN`,'Кол-во человек' AS `KvK`,'Кол-во экскурсий' AS `N`,sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)) AS `KolCzel`,'Стоимость' AS `Cena`,sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))) AS `Stoimost`,'' AS `Zakaz` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`date` >= '{0}') and (`zhurnal`.`date` <= '{1}')) union all select '' AS `id`,'{1}' AS `Data`,'' AS `FiO`,count(0) AS `KvN`,'Кол-во человек б/н' AS `KvK`,'Кол-во экскурсий б/н' AS `N`,sum(`plategki`.`Kol_czel`) AS `KolCzel`,'Стоимость б/н' AS `Cena`,sum((`ekskursii`.`stoimost` * (`plategki`.`Kol_czel`))) AS `Stoimost`,'' AS `Zakaz` from (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`plategki`.`date` >= '{0}') and (`plategki`.`date` <= '{1}'))", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day); 
                    //DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewZhurnal.Items.Clear();
                queueZhurnal.Clear();
                _Zhurnal x = new _Zhurnal();

              foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim());
                    x.id = dataRow["id"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Data"].ToString().Trim());
                    x.date = dataRow["Data"].ToString().Trim();
                    item1.SubItems.Add(dataRow["FiO"].ToString().Trim());
                    x.fio = dataRow["FiO"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N"].ToString().Trim());
                    x.numer = dataRow["N"].ToString().Trim();
                    item1.SubItems.Add(dataRow["KvN"].ToString().Trim());
                    x.kvn = dataRow["KvN"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Rdbnfywbb"].ToString().Trim());
                    x.kvk = dataRow["Rdbnfywbb"].ToString().Trim();
                    item1.SubItems.Add(dataRow["KolCzel"].ToString().Trim());
                    x.kolczel = dataRow["KolCzel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Cena"].ToString().Trim());
                    x.cena = dataRow["Cena"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Stoimost"].ToString().Trim());
                    x.stoimost = dataRow["Stoimost"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Zakaz"].ToString().Trim());
                    x.zakaz = dataRow["Zakaz"].ToString().Trim();


                    listViewZhurnal.Items.Add(item1);
                    listViewZhurnal.Items[listViewZhurnal.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueZhurnal.Enqueue(x);
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
        
            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxKvN.Text = "";
            textBoxKvN.Enabled = false;
            textBoxKvK.Text = "";
            textBoxKvK.Enabled = false;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = false;
            dateTimePickerDate.Enabled = false;
            comboBoxEkskursija.Enabled = false;
            comboBoxEkskursovod.Enabled = false;
            comboBoxZakazczik.Enabled = false;
        }
        
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewZhurnal.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `zhurnal` WHERE `id`='{0}'", m_zhurnal.id);
                if (textBoxKvK.Text.Trim() == textBoxKvN.Text.Trim())  
                    sql = String.Format("DELETE FROM `plategki` WHERE `id`='{0}'", m_zhurnal.id);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("Невозможно удалить запись,\r\nт.к. с ней связаны данные из других таблиц");
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }
                        
            comboBoxEkskursovod.SelectedIndex = -1;
            comboBoxEkskursovod.Enabled = false;
            comboBoxEkskursija.SelectedIndex = -1;
            comboBoxEkskursija.Enabled = false;
            comboBoxZakazczik.SelectedIndex = -1;
            comboBoxZakazczik.Enabled = false;
            textBoxKvN.Text = "";
            textBoxKvN.Enabled = false;
            textBoxKvK.Text = "";
            textBoxKvK.Enabled = false;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = false;
            dateTimePickerDate.Enabled = false;
            
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshZhurnal();
        }
        
        private void zhurnal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonApply.PerformClick();
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelApp.Visible = true;
            BringToFront();
            string workbookPath = Application.StartupPath + "\\templates/zhurnal.xls";
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath, 0,
            true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true,
            false, 0, true, false, false);
                         
            Microsoft.Office.Interop.Excel.Sheets excelSheets = excelWorkbook.Worksheets;
            string currentSheet = "Бланк";
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
            int i = 1;
            int i2 = 4;
            foreach (ListViewItem lvi in listViewZhurnal.Items)
            {
                i = 1;
                foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                {
                    excelWorksheet.Cells[i2, i] = lvs.Text;
                    i++;
                }
                i2++;
            }
        }

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

                string sql = "SELECT `N_ekskursovoda`, `FiO` FROM `ekskursovody` WHERE `activ` = \"A\" ORDER BY `FiO`";
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
                       c.nazvanie = dataRow["N_ekskursii"].ToString().Trim();

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

           public void refreshZakazczik()
           {
               string N_zakazczika = "";
               if (comboBoxZakazczik.SelectedIndex != -1)
               {
                   N_zakazczika = m_zakazczik.id.ToString();
               }

               comboBoxZakazczik.Items.Clear();
               queueZakazczik.Clear();

               try
               {
                   mainWin.m_dbConnector.Lock();
                   MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                   string sql = "SELECT `id`, `name` FROM `zakazczik` ORDER BY `name`";
                   MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                   myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                   DataSet dataSet = new DataSet();
                   myAdapter.Fill(dataSet);
                   DataTable dataTable = dataSet.Tables[0];
                   _Zakazczik c = new _Zakazczik();

                   foreach (DataRow dataRow in dataTable.Rows)
                   {
                       c.id = dataRow["id"].ToString();
                       c.name = dataRow["name"].ToString().Trim();

                       comboBoxZakazczik.Items.Add(c);


                       if (N_zakazczika != "" && N_zakazczika == c.id.ToString())
                       {
                           comboBoxZakazczik.SelectedItem = c;
                       }


                       queueZakazczik.Enqueue(c);
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

           private void comboBoxNekskursii_SelectedIndexChanged(object sender, EventArgs e)
           {
               if (comboBoxEkskursija.SelectedIndex == -1)
                   return;
               m_ekskursii = (_Ekskursii)comboBoxEkskursija.SelectedItem;
           }

           private void comboBoxZakazczik_SelectedIndexChanged(object sender, EventArgs e)
           {
               if (comboBoxZakazczik.SelectedIndex == -1)
                   return;
               m_zakazczik = (_Zakazczik)comboBoxZakazczik.SelectedItem;
           }

           private void comboBoxEkskursovod_SelectedIndexChanged(object sender, EventArgs e)
           {
               if (comboBoxEkskursovod.SelectedIndex == -1)
                   return;
               m_ekskursovody = (_Ekskursovody)comboBoxEkskursovod.SelectedItem;
           }


    }
}