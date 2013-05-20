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
    public struct _Bilety
    {
        public _Bilety(string id, string date, string KvN, string KvK, string name, string kol, string cena)
        {
            this.id_val = id;
            this.date_val = date;
            this.name_val = name;
            this.KvN_val = KvN;
            this.KvK_val = KvK;
            this.kol_val = kol;
            this.cena_val = cena;
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

        public string name
        {
            get
            {
                return name_val;
            }
            set
            {
                name_val = value;
            }
        }
        public string KvN
        {
            get
            {
                return KvN_val;
            }
            set
            {
                KvN_val = value;
            }
        }
        public string KvK
        {
            get
            {
                return KvK_val;
            }
            set
            {
                KvK_val = value;
            }
        }
        public string kol
        {
            get
            {
                return kol_val;
            }
            set
            {
                kol_val = value;
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
            return name_val;
        }

        private string id_val;
        private string date_val;
        private string name_val;
        private string KvN_val;
        private string KvK_val;
        private string kol_val;
        private string cena_val;
    }

    public partial class FormBilety : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueBilety = new Queue();
        _Bilety m_bilety;
        _Ekskursii m_ekskursii;
        Queue queueEkskursii = new Queue();

        public FormBilety(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewBilety.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Дата поступления", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Наименование", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Количество", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Цена", -2, HorizontalAlignment.Left);

            listViewBilety.Columns[0].Width = 25;
            listViewBilety.Columns[1].Width = 150;
            listViewBilety.Columns[2].Width = 300;
            listViewBilety.Columns[3].Width = 150;
            listViewBilety.Columns[4].Width = 150;
            listViewBilety.Columns[5].Width = 150;
            listViewBilety.Columns[6].Width = 150;

        }

        private void FormBilety_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formBilety = null;
        }


        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;
            //knopki
            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxKvN.Text = "";
            textBoxKvN.Enabled = true;
            comboBoxEkskursii.Enabled = true;
            textBoxKvK.Text = "";
            textBoxKvK.Enabled = true;
            dateTimePicker1.Enabled = true;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                    if (textBoxKvN.Text.Trim() != "")
                    {
                        string sql = String.Format("INSERT INTO `bilety` (`N_kvit_nach`, `N_kvit_koniec`, `naimenovanie`, `date`) VALUES ('{0}', '{1}', '{2}', '{3}')", textBoxKvN.Text, textBoxKvK.Text, comboBoxEkskursii.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day);
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    else throw new System.InvalidOperationException("Хреновина с номером билета не может быть пустой!"); 



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
                if (listViewBilety.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `bilety` SET `N_kvit_nach`='{0}',`N_kvit_koniec`='{1}',`naimenovanie`='{2}', `date`='{3}' WHERE `id`='{4}'", textBoxKvN.Text, textBoxKvK.Text, comboBoxEkskursii.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, m_bilety.id);
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
            comboBoxEkskursii.Enabled = false;
            textBoxKvK.Enabled = false;
            dateTimePicker1.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshBilety();
        }

        private void listViewBilety_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxKvN.Enabled = true;

            comboBoxEkskursii.Enabled = true;
            textBoxKvK.Enabled = true;
            dateTimePicker1.Enabled = true;

            bNew = false;


            if (listViewBilety.FocusedItem == null)
                return;

            int k = queueBilety.Count;
            for (int i = 0; i < k; i++)
            {
                m_bilety = (_Bilety)queueBilety.Dequeue();
                if (m_bilety.id == (string)listViewBilety.Items[listViewBilety.FocusedItem.Index].Tag)
                {
                    textBoxKvN.Text = m_bilety.KvN;
                    comboBoxEkskursii.Text = m_bilety.name;
                    textBoxKvK.Text = m_bilety.KvK;
                    dateTimePicker1.Value = DateTime.Parse(m_bilety.date);
                    queueBilety.Enqueue(m_bilety);

                    _Ekskursii c;
                    int k2 = queueEkskursii.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        c = (_Ekskursii)queueEkskursii.Dequeue();
                        if (c.nazvanie.ToString() == m_bilety.name.ToString())
                        {
                            for (int iii = 0; iii < comboBoxEkskursii.Items.Count; iii++)
                            {
                                if (((_Ekskursii)comboBoxEkskursii.Items[iii]).id == c.id)
                                {
                                    comboBoxEkskursii.SelectedIndex = iii;
                                    m_ekskursii = c;
                                    break;
                                }
                            }
                        };
                        queueEkskursii.Enqueue(c);
                    }


                    break;
                };

                queueBilety.Enqueue(m_bilety);
            }
        }


        public void refreshBilety()
        {
            listViewBilety.Items.Clear();
            queueBilety.Clear();

            listViewBilety.Columns.Clear();
            listViewBilety.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Дата поступления", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Наименование", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Количество", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Цена", -2, HorizontalAlignment.Left);

            listViewBilety.Columns[0].Width = 25;
            listViewBilety.Columns[1].Width = 150;
            listViewBilety.Columns[2].Width = 300;
            listViewBilety.Columns[3].Width = 150;
            listViewBilety.Columns[4].Width = 150;
            listViewBilety.Columns[5].Width = 150;
            listViewBilety.Columns[6].Width = 150;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("SELECT `bilety`.`id`, `bilety`.`N_kvit_nach`, `bilety`.`N_kvit_koniec`, `bilety`.`naimenovanie`, ((`bilety`.`N_kvit_koniec` - `bilety`.`N_kvit_nach`) + 1) AS `kol_vo`, `ekskursii`.`stoimost` as`cena`, cast(`bilety`.`date` as char) AS `date` FROM `bilety` join `ekskursii` ON `bilety`.`naimenovanie`=`ekskursii`.`nazvanie` WHERE `date` like \"{0}%\" ORDER BY `id`", comboBoxYear.Text);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety.Items.Clear();
                queueBilety.Clear();
                _Bilety t = new _Bilety();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim());
                    t.id = dataRow["id"].ToString().Trim();
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    t.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["naimenovanie"].ToString().Trim());
                    t.name = dataRow["naimenovanie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_nach"].ToString().Trim());
                    t.KvN = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.KvK = dataRow["N_kvit_koniec"].ToString().Trim();
                    item1.SubItems.Add(dataRow["kol_vo"].ToString().Trim());
                    t.kol = dataRow["kol_vo"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    t.cena = dataRow["cena"].ToString().Trim();

                    listViewBilety.Items.Add(item1);
                    listViewBilety.Items[listViewBilety.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueBilety.Enqueue(t);
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewBilety.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `bilety` WHERE `id`='{0}'", m_bilety.id);
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

            textBoxKvN.Text = "";
            textBoxKvN.Enabled = false;

            comboBoxEkskursii.Enabled = false;
            textBoxKvK.Enabled = false;
            dateTimePicker1.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshBilety();
        }

        private void textBoxBilety_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonApply.PerformClick();
        }

        private void FormBilety_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;
            textBoxKvN.Enabled = false;
            comboBoxEkskursii.Enabled = false;
            textBoxKvK.Enabled = false;
            dateTimePicker1.Enabled = false;

            refreshYear();
            refreshBilety();
            refreshEkskursii();
            
        }


        public void refreshEkskursii()
        {
            string N_ekskursii = "";
            if (comboBoxEkskursii.SelectedIndex != -1)
            {
                N_ekskursii = m_ekskursii.id.ToString();
            }

            comboBoxEkskursii.Items.Clear();
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

                    comboBoxEkskursii.Items.Add(c);


                    if (N_ekskursii != "" && N_ekskursii == c.id.ToString())
                    {
                        comboBoxEkskursii.SelectedItem = c;
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


        private void comboBoxEkskursii_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEkskursii.SelectedIndex == -1)
                return;
            m_ekskursii = (_Ekskursii)comboBoxEkskursii.SelectedItem;
        }

        private void refreshYear()
        {
            DateTime dt = DateTime.Now;

            mainWin.m_dbConnector.Lock();
            MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

            string sql = String.Format("SELECT YEAR(`date`) AS `date` FROM `bilety` order by `date` asc limit 1");
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
            
            comboBoxYear.SelectedIndex = comboBoxYear.Items.Count-1;
        }
        private void buttonYear_Click(object sender, EventArgs e)
        {
            refreshBilety();
        }
        /*
         * Отчет по остатку билетов
         * начало  
         * сделано ужасно, но переделывать лень  
         * куча всяких массивов и списков
         *
         * */
        //обновление listview ==начальные и конечные номера билетов для массива
        //массив из "билетов"
        public void refreshOst()
        {

            listViewBilety.Items.Clear();
            queueBilety.Clear();

            listViewBilety.Columns.Clear();
            listViewBilety.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);

            listViewBilety.Columns[0].Width = 150;
            listViewBilety.Columns[1].Width = 150;



            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("select `bilety`.`N_kvit_nach` AS `N_kvit_nach`,`bilety`.`N_kvit_koniec` AS `N_kvit_koniec` from `bilety` WHERE `date` like \"{0}%\" order by `N_kvit_nach`", comboBoxYear.Text);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety.Items.Clear();
                queueBilety.Clear();
                _Bilety t = new _Bilety();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    ListViewItem item1 = new ListViewItem(dataRow["N_kvit_nach"].ToString().Trim());
                    t.id = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.name = dataRow["N_kvit_koniec"].ToString().Trim();


                    listViewBilety.Items.Add(item1);
                    listViewBilety.Items[listViewBilety.Items.Count - 1].Tag = dataRow["N_kvit_nach"].ToString();
                    queueBilety.Enqueue(t);

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
        //массив из журнала
        public void refreshOst1()
        {

            listViewBilety.Items.Clear();
            queueBilety.Clear();

            listViewBilety.Columns.Clear();
            listViewBilety.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);

            listViewBilety.Columns[0].Width = 150;
            listViewBilety.Columns[1].Width = 150;



            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("select `zhurnal`.`N_kvit_nach` AS `N_kvit_nach`,`zhurnal`.`N_kvit_koniec` AS `N_kvit_koniec` from `zhurnal` WHERE `date` like \"{0}%\" order by `N_kvit_nach`", comboBoxYear.Text);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety.Items.Clear();
                queueBilety.Clear();
                _Bilety t = new _Bilety();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    ListViewItem item1 = new ListViewItem(dataRow["N_kvit_nach"].ToString().Trim());
                    t.id = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.name = dataRow["N_kvit_koniec"].ToString().Trim();


                    listViewBilety.Items.Add(item1);
                    listViewBilety.Items[listViewBilety.Items.Count - 1].Tag = dataRow["N_kvit_nach"].ToString();
                    queueBilety.Enqueue(t);

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
        //конечные номера билетов и названия экскурсий для массива 2
        public void refreshOst2()
        {

            listViewBilety.Items.Clear();
            queueBilety.Clear();

            listViewBilety.Columns.Clear();
            listViewBilety.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Название", -2, HorizontalAlignment.Left);
            listViewBilety.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);

            listViewBilety.Columns[0].Width = 150;
            listViewBilety.Columns[1].Width = 150;
            listViewBilety.Columns[2].Width = 150;
            listViewBilety.Columns[3].Width = 150;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                //string sql = "select `bilety`.`N_kvit_koniec` AS `N_kvit_koniec`,`bilety`.`naimenovanie` AS `naimenovanie`,`ekskursii`.`stoimost` AS `stoimost` from (`bilety` join `ekskursii` on((`bilety`.`naimenovanie` = `ekskursii`.`nazvanie`))) order by `bilety`.`N_kvit_koniec`";
                string sql = String.Format("select `bilety`.`N_kvit_nach` AS `N_kvit_nach`, `bilety`.`N_kvit_koniec` AS `N_kvit_koniec`,`bilety`.`naimenovanie` AS `naimenovanie`,`ekskursii`.`stoimost` AS `stoimost` from (`bilety` join `ekskursii` on((`bilety`.`naimenovanie` = `ekskursii`.`nazvanie`)))  WHERE `date` like \"{0}%\" order by `bilety`.`N_kvit_koniec`", comboBoxYear.Text);
                
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety.Items.Clear();
                queueBilety.Clear();
                _Bilety t = new _Bilety();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    //ListViewItem item1 = new ListViewItem(dataRow["N_kvit_koniec"].ToString().Trim());
                    //t.id = dataRow["N_kvit_koniec"].ToString().Trim();
                    ListViewItem item1 = new ListViewItem(dataRow["N_kvit_nach"].ToString().Trim());
                    t.id = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.date = dataRow["N_kvit_koniec"].ToString().Trim();
                    item1.SubItems.Add(dataRow["naimenovanie"].ToString().Trim());
                    t.name = dataRow["naimenovanie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoimost"].ToString().Trim());
                    t.cena = dataRow["stoimost"].ToString().Trim();

                    listViewBilety.Items.Add(item1);
                    listViewBilety.Items[listViewBilety.Items.Count - 1].Tag = dataRow["N_kvit_koniec"].ToString();
                    queueBilety.Enqueue(t);

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

        private void buttonOst_Click(object sender, EventArgs e)
        {

            //массив0 билеты
            refreshOst();
            int[,] arr = new int[listViewBilety.Items.Count, listViewBilety.Columns.Count];
            for (int x = 0, maxX = listViewBilety.Items.Count; x < maxX; x++)
            {
                for (int y = 0, maxY = listViewBilety.Columns.Count; y < maxY; y++)
                {
                    arr[x, y] = int.Parse(listViewBilety.Items[x].SubItems[y].Text.Trim());

                }
            }

            //массив1 журнал
            refreshOst1();
            int[,] arr1 = new int[listViewBilety.Items.Count, listViewBilety.Columns.Count];
            for (int x = 0, maxX = listViewBilety.Items.Count; x < maxX; x++)
            {
                for (int y = 0, maxY = listViewBilety.Columns.Count; y < maxY; y++)
                {
                    arr1[x, y] = int.Parse(listViewBilety.Items[x].SubItems[y].Text.Trim());

                }
            }

            //массив2 названия экскурсий
            refreshOst2();
            string[,] b = new string[listViewBilety.Items.Count, listViewBilety.Columns.Count];
            for (int x = 0, maxX = listViewBilety.Items.Count; x < maxX; x++)
            {
                for (int y = 0, maxY = listViewBilety.Columns.Count; y < maxY; y++)
                {
                    b[x, y] = listViewBilety.Items[x].SubItems[y].Text;

                }
            }

            refreshBilety();
            //listViewBilety.Items.Clear();
            //listViewBilety.Columns.Clear();

            List<int> ARR = new List<int>();
            List<int> ARR1 = new List<int>();
            List<int> ARR2 = new List<int>();
            List<int> ARR3 = new List<int>();
            List<int> unique = new List<int>();
            List<object> unique2 = new List<object>();

            //диапазоны из нач/кон значений из таблицы билеты
            for (int x = 0; x <= ((arr.Length / 2) - 1); x++)
            {
                for (int i = arr[x, 0], maxi = arr[x, 1]; i <= maxi; i++)
                {
                    ARR.Add(i);
                }
            }
            //диапазоны из нач/кон значений из журнала
            for (int x = 0; x <= ((arr1.Length / 2) - 1); x++)
            {
                for (int i = arr1[x, 0], maxi = arr1[x, 1]; i <= maxi; i++)
                {
                    ARR1.Add(i);
                }
            }
              
            //удаление того, что есть в журнале, но нет в билетах

            for (int x = 0; x < ARR.Count; x++)
            {

                for (int y = 0; y < ARR1.Count; y++)
                {
                    if (ARR[x].Equals(ARR1[y]))
                        ARR3.Add(ARR[x]);
                    
                }

            }

            //объединение списков
            ARR2.AddRange(ARR.ToArray());
            ARR2.AddRange(ARR3.ToArray());

            ARR2.Sort();

            //удаление двойных
            for (int m = 0; m < ARR2.Count; m++)
            {
                for (int j = m + 1; j < ARR2.Count; j++)
                {
                    if (ARR2[j].Equals(ARR2[m]))
                    {
                        ARR2.RemoveAt(j--);
                        ARR2.RemoveAt(m);
                    }
                }
            }

            //приведение к начальным
            for (int x = 0; x <= ((arr.Length / 2) - 1); x++)
            {
                for (int i = arr[x, 0], maxi = arr[x, 1]; i <= maxi; i++)
                {

                    if (ARR2.Contains(i))
                    {
                        unique.Add(x + 1);
                        unique.Add(i);
                    }

                }

            }

            //собственно диапазоны остатка
            for (int x = 0; x < unique.Count; x = x + 2)
            {
                int m;
                if (x == 0) m = x + 1; else m = x - 2;
                if (!unique[x].Equals(unique[m]))
                {
                    unique2.Add(unique[x]);
                    unique2.Add(unique[x + 1]);
                }

                int f;
                if (x < unique.Count - 2) f = x + 2; else f = x - 1;
                if (!unique[x].Equals(unique[f]))
                {

                    unique2.Add(unique[x + 1]);
                    //for (int y = 0; y < b.Length / 3; y++)
                    for (int y = 0; y < b.Length / 4; y++)
                    {

                            for (int i = int.Parse(b[y, 0]), maxi = int.Parse(b[y, 1]); i <= maxi; i++)
                            {
                                if (unique[x+1].Equals(i))
                                {
                                    
                                    unique2.Add(b[y, 2]);
                                    unique2.Add(b[y, 3]);

                                }
                            }
                        

                    }
                }
                
            }

            //массив для екселя
            
            object[,] c = new object[unique2.Count, unique2.Count / 3];

            for (int i = 0, k = 0; i < unique2.Count; i = i + 5, k++)
            {
                c[k, 0] = unique2[i];
            }
            for (int i = 1, k = 0; i < unique2.Count; i = i + 5, k++)
            {
                c[k, 1] = unique2[i];
            }
            for (int i = 2, k = 0; i < unique2.Count; i = i + 5, k++)
            {
                c[k, 2] = unique2[i];
            }
            for (int i = 3, k = 0; i < unique2.Count; i = i + 5, k++)
            {
                c[k, 3] = unique2[i];
            }

            for (int i = 4, k = 0; i < unique2.Count; i = i + 5, k++)
            {
                c[k, 4] = unique2[i];
            }
            //тестовый объект,чтоб в ексель вставлять всякое
            object[] a = unique2.ToArray();
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelApp.Visible = true;
            BringToFront();
            string workbookPath = Application.StartupPath + "\\templates/bilety.xls";
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath, 0,
              true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true,
              false, 0, true, false, false);

            Microsoft.Office.Interop.Excel.Sheets excelSheets = excelWorkbook.Worksheets;

            string currentSheet = "Бланк";
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
            Microsoft.Office.Interop.Excel.Range excelCellName = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("A4", "E" + unique2.Count);

            excelCellName.Value2 = c;


        }


        /*
       * Отчет по остатку билетов конец
       * 
       *
       * */




    }

}
