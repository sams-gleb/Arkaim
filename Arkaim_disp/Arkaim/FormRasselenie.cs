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

    public struct _Rasselenie
    {
        public _Rasselenie(int id, string date, string N_kvit, string FiO, string City, string Kol_dney, string Kol_czel, string zhitie, string zhitie_bez, string parkovka, string zakazczik, string summa, string bron, string bron_bez)
        {
            this.id_val = id;
            this.date_val = date;
            this.N_kvit_val = N_kvit;
            this.FiO_val = FiO;
            this.City_val = City;
            this.Kol_dney_val = Kol_dney;
            this.Kol_czel_val = Kol_czel;
            this.zhitie_val = zhitie;
            this.zhitie_bez_val = zhitie_bez;
            this.parkovka_val = parkovka;
            this.zakazczik_val = zakazczik;
            this.summa_val = summa;
            this.bron_val = bron;
            this.bron_bez_val = bron_bez;
        }

        public int id
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
        public string N_kvit
        {
            get
            {
                return N_kvit_val;
            }
            set
            {
                N_kvit_val = value;
            }
        }
        public string FiO
        {
            get
            {
                return FiO_val;
            }
            set
            {
                FiO_val = value;
            }
        }
        public string City
        {
            get
            {
                return City_val;
            }
            set
            {
                City_val = value;
            }
        }
        public string Kol_dney
        {
            get
            {
                return Kol_dney_val;
            }
            set
            {
                Kol_dney_val = value;
            }
        }
        public string Kol_czel
        {
            get
            {
                return Kol_czel_val;
            }
            set
            {
                Kol_czel_val = value;
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
        public string zhitie_bez
        {
            get
            {
                return zhitie_bez_val;
            }
            set
            {
                zhitie_bez_val = value;
            }
        }
        public string parkovka
        {
            get
            {
                return parkovka_val;
            }
            set
            {
                parkovka_val = value;
            }
        }
        public string zakazczik
        {
            get
            {
                return zakazczik_val;
            }
            set
            {
                zakazczik_val = value;
            }
        }
        public string summa
        {
            get
            {
                return summa_val;
            }
            set
            {
                summa_val = value;
            }
        }
        public string bron
        {
            get
            {
                return bron_val;
            }
            set
            {
                bron_val = value;
            }
        }
        public string bron_bez
        {
            get
            {
                return bron_bez_val;
            }
            set
            {
                bron_bez_val = value;
            }
        }
        public override string ToString()
        {
            return FiO_val;
        }

        private int id_val;
        private string date_val;
        private string N_kvit_val;
        private string FiO_val;
        private string City_val;
        private string Kol_dney_val;
        private string Kol_czel_val;
        private string zhitie_val;
        private string zhitie_bez_val;
        private string parkovka_val;
        private string zakazczik_val;
        private string summa_val;
        private string bron_val;
        private string bron_bez_val;
    }

    public partial class FormRasselenie : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueRasselenie = new Queue();
        _Rasselenie m_rasselenie;
        Queue queueZhitie = new Queue();
        _Zhitie m_zhitie;

        public FormRasselenie(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewRasselenie.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Номер квитанции", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Ф.И.О.", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Город", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Кол-во дней", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Житие", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Парковка", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Бронь", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Безнал", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Заказчик", -2, HorizontalAlignment.Left);
            listViewRasselenie.Columns.Add("Сумма", -2, HorizontalAlignment.Left);
            
            listViewRasselenie.Columns[0].Width = 35;
            listViewRasselenie.Columns[1].Width = 100;
            listViewRasselenie.Columns[2].Width = 100;
            listViewRasselenie.Columns[3].Width = 150;
            listViewRasselenie.Columns[4].Width = 100;
            listViewRasselenie.Columns[5].Width = 80;
            listViewRasselenie.Columns[6].Width = 80;
            listViewRasselenie.Columns[7].Width = 150;
            listViewRasselenie.Columns[8].Width = 70;
            listViewRasselenie.Columns[9].Width = 100;
            listViewRasselenie.Columns[10].Width = 100;
            listViewRasselenie.Columns[11].Width = 150;
            listViewRasselenie.Columns[12].Width = 100;

        }

        private void FormRasselenie_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formRasselenie = null;

        }

        private void FormRasselenie_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;

            textBoxName.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBoxZhitie.Enabled = false;
            textBoxKolDays.Enabled = false;
            textBoxKolCzel.Enabled = false;
            textBoxPark.Enabled = false;
            textBoxNkvit.Enabled = false;
            comboBoxCity.Enabled = false;
            comboBoxZakazczik.Enabled = false;
            textBoxBron.Enabled = false;
            textBoxBez.Enabled = false;

            comboBoxZhitie2.Visible = false;
            DateTime pickedDate = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, 1);
            dateTimePicker2.Value = pickedDate;

            refreshRasselenie();
            refreshZhitie();
            refreshCity();
            refreshZakazczik();

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxName.Text = "";
            textBoxName.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBoxZhitie.SelectedIndex = -1;
            comboBoxZhitie.Enabled = true;
            textBoxKolDays.Text = "";
            textBoxKolDays.Enabled = true;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = true;
            textBoxPark.Text = "";
            textBoxPark.Enabled = true;
            textBoxNkvit.Text = "";
            textBoxNkvit.Enabled = true;
            textBoxBez.Text = "";
            textBoxBez.Enabled = true;
            comboBoxCity.SelectedIndex = -1;
            comboBoxCity.Enabled = true;
            comboBoxZakazczik.Enabled = true;
            comboBoxZakazczik.Text = "Физлицо";
            textBoxBron.Text = "";
            textBoxBron.Enabled = true;
            dateTimePicker1.Value = DateTime.Now;

        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                    //чекбоксы для выбора вставки в наличность, либо безналичность
                    string bron = textBoxBron.Text.Trim();
                    string zhitie = comboBoxZhitie.Text.Trim();
                    string bron_bez = textBoxBez.Text.Trim();
                    string zhitie_bez = "";
                    /*if (checkBoxBron.Checked & ! checkBoxZhitie.Checked)
                    {
                        bron = "";
                        bron_bez = textBoxBron.Text.Trim();
                    }
                    else if (checkBoxZhitie.Checked & ! checkBoxBron.Checked)
                    {
                        zhitie = "";
                        bron_bez = "";
                        zhitie_bez = comboBoxZhitie.Text.Trim();
                    }
                    else if (checkBoxBron.Checked & checkBoxZhitie.Checked)
                    {
                        zhitie = "";
                        bron = "";
                        bron_bez = textBoxBron.Text.Trim();
                        zhitie_bez = comboBoxZhitie.Text.Trim();
                    }*/
                    string sql = String.Format("INSERT INTO `rasselenie` (`date`, `N_kvit`, `FiO`, `City`, `Kol_dney`, `Kol_czel`, `zhitie`, `parkovka`, `zakazczik`, `bron`, `zhitie_bez`, `bron_bez`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, textBoxNkvit.Text, textBoxName.Text, comboBoxCity.Text, textBoxKolDays.Text, textBoxKolCzel.Text, zhitie, textBoxPark.Text, comboBoxZakazczik.Text, bron, zhitie_bez, bron_bez );
                  

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
                if (listViewRasselenie.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                    //чекбоксы для выбора вставки в наличность, либо безналичность
                    string bron = textBoxBron.Text.Trim();
                    string zhitie = comboBoxZhitie.Text.Trim();
                    string bron_bez = textBoxBez.Text.Trim();
                    string zhitie_bez = "";
                    /*if (checkBoxBron.Checked & !checkBoxZhitie.Checked)
                    {
                        bron = "";
                        bron_bez = textBoxBron.Text.Trim();
                    }
                    else if (checkBoxZhitie.Checked & !checkBoxBron.Checked)
                    {
                        zhitie = "";
                        bron_bez = "";
                        zhitie_bez = comboBoxZhitie.Text.Trim();
                    }
                    else if (checkBoxBron.Checked & checkBoxZhitie.Checked)
                    {
                        zhitie = "";
                        bron = "";
                        bron_bez = textBoxBron.Text.Trim();
                        zhitie_bez = comboBoxZhitie.Text.Trim();
                    }*/
                    string sql = String.Format("UPDATE `rasselenie` SET `date` = '{0}', `N_kvit` = '{1}', `FiO` = '{2}', `City` = '{3}', `Kol_dney` = '{4}', `Kol_czel` = '{5}', `zhitie` = '{6}', `parkovka` = '{7}', `zakazczik`= '{8}', `bron`='{9}', `zhitie_bez`='{10}', `bron_bez`='{11}' WHERE `id`='{12}'", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, textBoxNkvit.Text, textBoxName.Text, comboBoxCity.Text, textBoxKolDays.Text, textBoxKolCzel.Text, zhitie, textBoxPark.Text, comboBoxZakazczik.Text, bron, zhitie_bez, bron_bez, m_rasselenie.id);
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

            textBoxName.Text = "";
            textBoxName.Enabled = false;
            dateTimePicker1.Enabled = true;
            comboBoxZhitie.Enabled = false;
            textBoxKolDays.Enabled = false;
            textBoxKolCzel.Enabled = false;
            textBoxPark.Enabled = false;
            textBoxNkvit.Enabled = false;
            comboBoxCity.Enabled = false;
            comboBoxZakazczik.Enabled = false;
            textBoxBron.Enabled = false;
            textBoxBez.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshRasselenie();

        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            bNew = true;
                        
            //if ( listViewRasselenie.) bNew = false;

        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
       
        comboBoxZhitie2.Visible = true;


        }
        
        private void listViewRasselenie_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxName.Enabled = true;
            comboBoxZhitie.SelectedIndex = -1;
            dateTimePicker1.Enabled = true;
            comboBoxZhitie.Enabled = true;
            textBoxKolDays.Enabled = true;
            textBoxKolCzel.Enabled = true;
            textBoxPark.Enabled = true;
            textBoxNkvit.Enabled = true;
            comboBoxCity.Enabled = true;
            comboBoxZakazczik.Enabled = true;
            textBoxBron.Enabled = true;
            textBoxBez.Enabled = true;

            bNew = false;

            if (listViewRasselenie.FocusedItem == null)
                return;

            int k = queueRasselenie.Count;
            for (int i = 0; i < k; i++)
            {
                m_rasselenie = (_Rasselenie)queueRasselenie.Dequeue();
                if (m_rasselenie.id.ToString() == (string)listViewRasselenie.Items[listViewRasselenie.FocusedItem.Index].Tag)
                {
                    textBoxName.Text = m_rasselenie.FiO;
                    
                    textBoxKolDays.Text = m_rasselenie.Kol_dney;
                    textBoxKolCzel.Text = m_rasselenie.Kol_czel;
                    textBoxPark.Text = m_rasselenie.parkovka;
                    textBoxNkvit.Text = m_rasselenie.N_kvit;
                    comboBoxCity.Text = m_rasselenie.City;
                    comboBoxZakazczik.Text = m_rasselenie.zakazczik;
                    //if (m_rasselenie.zhitie_bez == "")
                    comboBoxZhitie.Text = m_rasselenie.zhitie;
                    //else comboBoxZhitie.Text = m_rasselenie.zhitie_bez;
                    //if (m_rasselenie.bron_bez == "")
                    textBoxBron.Text = m_rasselenie.bron;
                    //else textBoxBron.Text = m_rasselenie.bron_bez;
                    textBoxBez.Text = m_rasselenie.bron_bez;
                    dateTimePicker1.Value = DateTime.Parse(m_rasselenie.date);

                    _Zhitie c;
                    int k2 = queueZhitie.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        c = (_Zhitie)queueZhitie.Dequeue();
                        if (c.nazvanie.ToString() == m_rasselenie.zhitie.ToString())
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
                    queueRasselenie.Enqueue(m_rasselenie);
                    break;
                };

                queueRasselenie.Enqueue(m_rasselenie);
                
            }

        }

        public void refreshRasselenie()
        {
            listViewRasselenie.Items.Clear();
            queueRasselenie.Clear();


            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                //string sql = String.Format("select `rasselenie`.`id` AS `id`,cast(`rasselenie`.`date` as char charset utf8) AS `date`,`rasselenie`.`N_kvit` AS `N_kvit`,`rasselenie`.`FiO` AS `FIO`,`rasselenie`.`City` AS `City`,`rasselenie`.`Kol_dney` AS `Kol_dney`,`rasselenie`.`Kol_czel` AS `Kol_czel`,`rasselenie`.`zhitie` AS `zhitie`,'' AS `zhitie_bez`,`rasselenie`.`parkovka` AS `parkovka`,`rasselenie`.`zakazczik` AS `Zakazczik`,`rasselenie`.`bron` AS `bron`,'' AS `bron_bez`,((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' union all select `rasselenie`.`id` AS `id`,cast(`rasselenie`.`date` as char charset utf8) AS `date`,`rasselenie`.`N_kvit` AS `N_kvit`,`rasselenie`.`FiO` AS `FIO`,`rasselenie`.`City` AS `City`,`rasselenie`.`Kol_dney` AS `Kol_dney`,`rasselenie`.`Kol_czel` AS `Kol_czel`,'' AS `zhitie`,`rasselenie`.`zhitie_bez` AS `zhitie_bez`,'' AS `parkovka`,`rasselenie`.`zakazczik` AS `Zakazczik`,'' AS `bron`,`rasselenie`.`bron_bez` AS `bron_bez`,(((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' union all select `rasselenie`.`id` AS `id`, '{1}' AS `date`,'' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `zhitie_bez`,'' AS `parkovka`,'Итого наличность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' union all select `rasselenie`.`id` AS `id`,'{1}' AS `date`,'' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `zhitie_bez`,'' AS `parkovka`, 'Безналичность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0))) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' ORDER BY `date`", DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day, DateTime.Parse(dateTimePicker3.Text).Year + "-" + DateTime.Parse(dateTimePicker3.Text).Month + "-" + DateTime.Parse(dateTimePicker3.Text).Day);

                string sql = String.Format("select `rasselenie`.`id` AS `id`,cast(`rasselenie`.`date` as char charset utf8) AS `date`,`rasselenie`.`N_kvit` AS `N_kvit`,`rasselenie`.`FiO` AS `FIO`,`rasselenie`.`City` AS `City`,`rasselenie`.`Kol_dney` AS `Kol_dney`,`rasselenie`.`Kol_czel` AS `Kol_czel`,`rasselenie`.`zhitie` AS `zhitie`,`rasselenie`.`parkovka` AS `parkovka`,`rasselenie`.`zakazczik` AS `Zakazczik`,`rasselenie`.`bron` AS `bron`,`rasselenie`.`bron_bez` AS `bron_bez`,(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`) + ifnull(`rasselenie`.`bron_bez`,0)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' union all select `rasselenie`.`id` AS `id`, '{1}' AS `date`,'' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `parkovka`,'Итого наличность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' union all select `rasselenie`.`id` AS `id`,'{1}' AS `date`,'' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `parkovka`, 'Безналичность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum(ifnull(`rasselenie`.`bron_bez`,0)) AS `cena` from `rasselenie` WHERE `date` >= '{0}' and `date` <= '{1}' ORDER BY `date`", DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day, DateTime.Parse(dateTimePicker3.Text).Year + "-" + DateTime.Parse(dateTimePicker3.Text).Month + "-" + DateTime.Parse(dateTimePicker3.Text).Day);
               
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewRasselenie.Items.Clear();
                queueRasselenie.Clear();
                _Rasselenie t = new _Rasselenie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim(), 0);
                    t.id = int.Parse(dataRow["id"].ToString());
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    t.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit"].ToString().Trim());
                    t.N_kvit = dataRow["N_kvit"].ToString().Trim();
                    item1.SubItems.Add(dataRow["FiO"].ToString().Trim());
                    t.FiO = dataRow["FiO"].ToString().Trim();
                    item1.SubItems.Add(dataRow["City"].ToString().Trim());
                    t.City = dataRow["City"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_dney"].ToString().Trim());
                    t.Kol_dney = dataRow["Kol_dney"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    t.Kol_czel = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["zhitie"].ToString().Trim());
                    t.zhitie = dataRow["zhitie"].ToString().Trim();
                    //item1.SubItems.Add(dataRow["zhitie_bez"].ToString().Trim());
                    //t.zhitie_bez = dataRow["zhitie_bez"].ToString().Trim();
                    item1.SubItems.Add(dataRow["parkovka"].ToString().Trim());
                    t.parkovka = dataRow["parkovka"].ToString().Trim();
                    item1.SubItems.Add(dataRow["bron"].ToString().Trim());
                    t.bron = dataRow["bron"].ToString().Trim();
                    item1.SubItems.Add(dataRow["bron_bez"].ToString().Trim());
                    t.bron_bez = dataRow["bron_bez"].ToString().Trim();
                    item1.SubItems.Add(dataRow["zakazczik"].ToString().Trim());
                    t.zakazczik = dataRow["zakazczik"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    t.summa = dataRow["cena"].ToString().Trim();
                    
                    listViewRasselenie.Items.Add(item1);
                    listViewRasselenie.Items[listViewRasselenie.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueRasselenie.Enqueue(t);
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
            listViewRasselenie.Items.Clear();
            queueRasselenie.Clear();
            refreshRasselenie();
            /*
            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                
                string sql = String.Format("select `rasselenie`.`id` AS `id`,cast(`rasselenie`.`date` as char charset utf8) AS `date`,`rasselenie`.`N_kvit` AS `N_kvit`,`rasselenie`.`FiO` AS `FIO`,`rasselenie`.`City` AS `City`,`rasselenie`.`Kol_dney` AS `Kol_dney`,`rasselenie`.`Kol_czel` AS `Kol_czel`,`rasselenie`.`zhitie` AS `zhitie`,'' AS `zhitie_bez`,`rasselenie`.`parkovka` AS `parkovka`,`rasselenie`.`zakazczik` AS `Zakazczik`,`rasselenie`.`bron` AS `bron`,'' AS `bron_bez`,((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' union all select `rasselenie`.`id` AS `id`,cast(`rasselenie`.`date` as char charset utf8) AS `date`,`rasselenie`.`N_kvit` AS `N_kvit`,`rasselenie`.`FiO` AS `FIO`,`rasselenie`.`City` AS `City`,`rasselenie`.`Kol_dney` AS `Kol_dney`,`rasselenie`.`Kol_czel` AS `Kol_czel`,'' AS `zhitie`,`rasselenie`.`zhitie_bez` AS `zhitie_bez`,'' AS `parkovka`,`rasselenie`.`zakazczik` AS `Zakazczik`,'' AS `bron`,`rasselenie`.`bron_bez` AS `bron_bez`,(((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' union all select `rasselenie`.`id` AS `id`, '{1}' AS `date`,'' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `zhitie_bez`,'' AS `parkovka`,'Итого наличность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' union all select `rasselenie`.`id` AS `id`,'{1}' AS `date`,'' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `zhitie_bez`,'' AS `parkovka`, 'Безналичность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0))) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) WHERE `date` >= '{0}' and `date` <= '{1}' ORDER BY `date`", DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day, DateTime.Parse(dateTimePicker3.Text).Year + "-" + DateTime.Parse(dateTimePicker3.Text).Month + "-" + DateTime.Parse(dateTimePicker3.Text).Day);
                //SELECT `rasselenie`.`id`, cast(`date` as char) AS `date`, `N_kvit`, `FiO`, `City`, `Kol_dney`, `Kol_czel`, `zhitie`, `parkovka`, `zakazczik`, `bron`, ((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`) AS `cena` from (`rasselenie` join `zhitie` on(`rasselenie`.`zhitie` = `zhitie`.`nazvanie`)) WHERE `date` >= '{0}' and `date` <= '{1}' ORDER BY `date`", DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day, DateTime.Parse(dateTimePicker3.Text).Year + "-" + DateTime.Parse(dateTimePicker3.Text).Month + "-" + DateTime.Parse(dateTimePicker3.Text).Day);

                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewRasselenie.Items.Clear();
                queueRasselenie.Clear();
                _Rasselenie t = new _Rasselenie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim(), 0);
                    t.id = int.Parse(dataRow["id"].ToString());
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    t.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit"].ToString().Trim());
                    t.N_kvit = dataRow["N_kvit"].ToString().Trim();
                    item1.SubItems.Add(dataRow["FiO"].ToString().Trim());
                    t.FiO = dataRow["FiO"].ToString().Trim();
                    item1.SubItems.Add(dataRow["City"].ToString().Trim());
                    t.City = dataRow["City"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_dney"].ToString().Trim());
                    t.Kol_dney = dataRow["Kol_dney"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    t.Kol_czel = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["zhitie"].ToString().Trim());
                    t.zhitie = dataRow["zhitie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["parkovka"].ToString().Trim());
                    t.parkovka = dataRow["parkovka"].ToString().Trim();
                    item1.SubItems.Add(dataRow["bron"].ToString().Trim());
                    t.bron = dataRow["bron"].ToString().Trim();
                    item1.SubItems.Add(dataRow["zakazczik"].ToString().Trim());
                    t.zakazczik = dataRow["zakazczik"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    t.summa = dataRow["cena"].ToString().Trim();

                    listViewRasselenie.Items.Add(item1);
                    listViewRasselenie.Items[listViewRasselenie.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueRasselenie.Enqueue(t);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mainWin.m_dbConnector.Unlock();
            }*/
        }
        //отчет по квитанциям
        public void refreshRasselenieKvit()
        {
            listViewRasselenie.Items.Clear();
            queueRasselenie.Clear();


            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                //string sql = String.Format("select `rasselenie`.`id` AS `id`,cast(`rasselenie`.`date` as char charset utf8) AS `date`,`rasselenie`.`N_kvit` AS `N_kvit`,`rasselenie`.`FiO` AS `FIO`,`rasselenie`.`City` AS `City`,`rasselenie`.`Kol_dney` AS `Kol_dney`,`rasselenie`.`Kol_czel` AS `Kol_czel`,`rasselenie`.`zhitie` AS `zhitie`,'' AS `zhitie_bez`,`rasselenie`.`parkovka` AS `parkovka`,`rasselenie`.`zakazczik` AS `Zakazczik`,`rasselenie`.`bron` AS `bron`,'' AS `bron_bez`,((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `N_kvit` >= '{0}' and `N_kvit` <= '{1}' union all select `rasselenie`.`id` AS `id`,cast(`rasselenie`.`date` as char charset utf8) AS `date`,`rasselenie`.`N_kvit` AS `N_kvit`,`rasselenie`.`FiO` AS `FIO`,`rasselenie`.`City` AS `City`,`rasselenie`.`Kol_dney` AS `Kol_dney`,`rasselenie`.`Kol_czel` AS `Kol_czel`,'' AS `zhitie`,`rasselenie`.`zhitie_bez` AS `zhitie_bez`,'' AS `parkovka`,`rasselenie`.`zakazczik` AS `Zakazczik`,'' AS `bron`,`rasselenie`.`bron_bez` AS `bron_bez`,(((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) WHERE `N_kvit` >= '{0}' and `N_kvit` <= '{1}' union all select `rasselenie`.`id` AS `id`, '' AS `date`,'{1}' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `zhitie_bez`,'' AS `parkovka`,'Итого наличность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `N_kvit` >= '{0}' and `N_kvit` <= '{1}' union all select `rasselenie`.`id` AS `id`,'' AS `date`,'{1}' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `zhitie_bez`,'' AS `parkovka`, 'Безналичность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + ifnull(`rasselenie`.`bron_bez`,0))) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie_bez` = `zhitie`.`nazvanie`))) WHERE `N_kvit` >= '{0}' and `N_kvit` <= '{1}' ORDER BY `N_kvit`", textBoxKvN.Text, textBoxKvK.Text);
                string sql = String.Format("select `rasselenie`.`id` AS `id`,cast(`rasselenie`.`date` as char charset utf8) AS `date`,`rasselenie`.`N_kvit` AS `N_kvit`,`rasselenie`.`FiO` AS `FIO`,`rasselenie`.`City` AS `City`,`rasselenie`.`Kol_dney` AS `Kol_dney`,`rasselenie`.`Kol_czel` AS `Kol_czel`,`rasselenie`.`zhitie` AS `zhitie`,`rasselenie`.`parkovka` AS `parkovka`,`rasselenie`.`zakazczik` AS `Zakazczik`,`rasselenie`.`bron` AS `bron`,`rasselenie`.`bron_bez` AS `bron_bez`,((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `N_kvit` >= '{0}' and `N_kvit` <= '{1}' union all select `rasselenie`.`id` AS `id`, '' AS `date`,'{1}' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `parkovka`,'Итого наличность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum(((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) + `rasselenie`.`bron`)) AS `cena` from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) WHERE `N_kvit` >= '{0}' and `N_kvit` <= '{1}' union all select `rasselenie`.`id` AS `id`,'' AS `date`,'{1}' AS `N_kvit`,'' AS `FIO`,'' AS `City`,'' AS `Kol_dney`,'' AS `Kol_czel`,'' AS `zhitie`,'' AS `parkovka`, 'Безналичность:' AS `Zakazczik`,'' AS `bron`,'' AS `bron_bez`,sum(ifnull(`rasselenie`.`bron_bez`,0)) AS `cena` from `rasselenie` WHERE `N_kvit` >= '{0}' and `N_kvit` <= '{1}' ORDER BY `N_kvit`", textBoxKvN.Text, textBoxKvK.Text);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewRasselenie.Items.Clear();
                queueRasselenie.Clear();
                _Rasselenie t = new _Rasselenie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim(), 0);
                    t.id = int.Parse(dataRow["id"].ToString());
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    t.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit"].ToString().Trim());
                    t.N_kvit = dataRow["N_kvit"].ToString().Trim();
                    item1.SubItems.Add(dataRow["FiO"].ToString().Trim());
                    t.FiO = dataRow["FiO"].ToString().Trim();
                    item1.SubItems.Add(dataRow["City"].ToString().Trim());
                    t.City = dataRow["City"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_dney"].ToString().Trim());
                    t.Kol_dney = dataRow["Kol_dney"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    t.Kol_czel = dataRow["Kol_czel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["zhitie"].ToString().Trim());
                    t.zhitie = dataRow["zhitie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["parkovka"].ToString().Trim());
                    t.parkovka = dataRow["parkovka"].ToString().Trim();
                    item1.SubItems.Add(dataRow["bron"].ToString().Trim());
                    t.bron = dataRow["bron"].ToString().Trim();
                    item1.SubItems.Add(dataRow["bron_bez"].ToString().Trim());
                    t.bron_bez = dataRow["bron_bez"].ToString().Trim();
                    item1.SubItems.Add(dataRow["zakazczik"].ToString().Trim());
                    t.zakazczik = dataRow["zakazczik"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    t.summa = dataRow["cena"].ToString().Trim();

                    listViewRasselenie.Items.Add(item1);
                    listViewRasselenie.Items[listViewRasselenie.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueRasselenie.Enqueue(t);
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
        private void buttonKvit_Click(object sender, EventArgs e)
        {
            listViewRasselenie.Items.Clear();
            queueRasselenie.Clear();
            refreshRasselenieKvit();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewRasselenie.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `rasselenie` WHERE `id`='{0}'", m_rasselenie.id);
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

            textBoxName.Text = "";
            textBoxName.Enabled = false;
            dateTimePicker1.Enabled = true;
            comboBoxZhitie.Enabled = false;
            textBoxKolDays.Enabled = false;
            textBoxKolCzel.Enabled = false;
            textBoxPark.Enabled = false;
            textBoxNkvit.Enabled = false;
            comboBoxCity.Enabled = false;
            comboBoxZakazczik.Enabled = false;
            
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshRasselenie();

        }

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
        private void buttonExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelApp.Visible = true;
            BringToFront();
            excelApp.Application.Workbooks.Add(Type.Missing);
            excelApp.Columns.ColumnWidth = 15;
            excelApp.Cells[1, 1 ]= "№";
            excelApp.Cells[1, 2 ]= "Дата";
            excelApp.Cells[1, 3 ]= "Квитанция";
            excelApp.Cells[1, 4 ]= "ФИО";
            excelApp.Cells[1, 5 ]= "Город";
            excelApp.Cells[1, 6 ]= "Кол-во дней";
            excelApp.Cells[1, 7] = "Кол-во человек";
            excelApp.Cells[1, 8] = "Житие";
            excelApp.Cells[1, 9] = "Парковка";
            excelApp.Cells[1, 10] = "Бронь";
            excelApp.Cells[1, 11] = "Безнал";
            excelApp.Cells[1, 12] = "Заказчик";
            excelApp.Cells[1, 13] = "Сумма";
            
            int i = 1;
            int i2 = 4;
            foreach (ListViewItem lvi in listViewRasselenie.Items)
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
        /*
         *комбобокс с городами 
         * 
         * 
         */
        public void refreshCity()
        {
            try
            { 
            mainWin.m_dbConnector.Lock();
            MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

            string sql = "SELECT `city_id`, `name` FROM `city` ORDER BY `name`";
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            myAdapter.SelectCommand = new MySqlCommand(sql, conn);
            DataSet dataSet = new DataSet();
            myAdapter.Fill(dataSet);
            //DataTable dataTable = dataSet.Tables[0];

            comboBoxCity.Items.Clear();
            
            int i = 0;
            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                comboBoxCity.Items.Add(dataSet.Tables[0].Rows[i][1].ToString());
            }

            //comboBox1.DataSource = ds.Tables[0];
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
        /*
         *комбобокс с заказчиками 
         * 
         * 
         */
        public void refreshZakazczik()
        {
            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `name` FROM `zakazczik` ORDER BY `name`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                
                comboBoxZakazczik.Items.Clear();

                int i = 0;
                for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                   comboBoxZakazczik.Items.Add(dataSet.Tables[0].Rows[i][1].ToString());
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

     

    }
}