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

    public struct _Bilety2
    {
        public _Bilety2(int id, string date, string KvN, string KvK, string name, string cena, string FiO)
        {
            this.id_val = id;
            this.date_val = date;
            this.KvN_val = KvN;
            this.KvK_val = KvK;
            this.name_val = name;
            this.cena_val = cena;
            this.FiO_val = FiO;
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

        public override string ToString()
        {
            return name_val;
        }

        private int id_val;
        private string date_val;
        private string KvN_val;
        private string KvK_val;
        private string name_val;
        private string cena_val;
        private string FiO_val;
    }

    public partial class FormBilety2 : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        private bool bVydano = false;
        private bool bSdano = false;
        Queue queueBilety2 = new Queue();
        _Bilety2 m_bilety2;

        public FormBilety2(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;
            listViewBilety2.Items.Clear();

        }

        private void FormBilety2_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formBilety2 = null;

        }

        private void FormBilety2_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = false;
            buttonApply.Enabled = false;
            buttonVydano.Enabled = true;
            buttonSdano.Enabled = true;
            dateTimePicker1.Enabled = false;
            textBoxCena.Enabled = false;
            textBoxFiO.Enabled = false;
            textBoxKvN.Enabled = false;
            textBoxKvK.Enabled = false;
            textBoxName.Enabled = false;
            DateTime pickedDate = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, 1);
            dateTimePicker2.Value = pickedDate;
                     
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;
            
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                if (bVydano == true)
                {
                    try
                    {
                        mainWin.m_dbConnector.Lock();
                        MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                        if (textBoxKvN.Text.Trim() != "")
                        {
                        string sql = String.Format("INSERT INTO `bilety2_vydano` (`date`, `N_kvit_nach`, `N_kvit_koniec`, `naimenovanie`, `cena`) VALUES ('{0}','{1}','{2}','{3}','{4}')", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, textBoxKvN.Text, textBoxKvK.Text, textBoxName.Text, textBoxCena.Text);
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
                        refreshVydano();
                    }

                }
                else if (bSdano == true)
                {
                    try
                    {
                        mainWin.m_dbConnector.Lock();
                        MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                        //табл выдано
                        if (textBoxKvN.Text.Trim() != "")
                        {
                        string sql = String.Format("INSERT INTO `bilety2_sdano` (`date`, `N_kvit_nach`, `N_kvit_koniec`, `FiO`) VALUES ('{0}','{1}','{2}','{3}')", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, textBoxKvN.Text, textBoxKvK.Text, textBoxFiO.Text);
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
                        refreshSdano();
                    }
                }

            }
            else
            {
                if (listViewBilety2.FocusedItem == null)
                    return;
                if (bVydano == true)
                {
                    try
                    {
                        mainWin.m_dbConnector.Lock();
                        MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                        string sql = String.Format("UPDATE `bilety2_vydano` SET `date`='{0}', `N_kvit_nach`='{1}', `N_kvit_koniec`='{2}',`naimenovanie`='{3}', `cena`='{4}' WHERE `id`='{5}'", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, textBoxKvN.Text, textBoxKvK.Text, textBoxName.Text, textBoxCena.Text, m_bilety2.id);
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
                        refreshVydano();
                    }

                }
                else if (bSdano == true)
                {
                    try
                    {
                        mainWin.m_dbConnector.Lock();
                        MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                        string sql = String.Format("UPDATE `bilety2_sdano` SET `date`='{0}', `N_kvit_nach`='{1}', `N_kvit_koniec`='{2}',`FiO`='{3}' WHERE `id`='{4}'", DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, textBoxKvN.Text, textBoxKvK.Text, textBoxFiO.Text, m_bilety2.id);
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
                        refreshSdano();
                    }

                }
            }

            textBoxName.Text = "";
            textBoxKvN.Text = "";
            textBoxKvK.Text = "";
            textBoxFiO.Text = "";
            textBoxCena.Text = "";
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            
        }

        private void listViewBilety2_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxName.Enabled = true;

            bNew = false;

            if (listViewBilety2.FocusedItem == null)
                return;

            int k = queueBilety2.Count;
            for (int i = 0; i < k; i++)
            {
                m_bilety2 = (_Bilety2)queueBilety2.Dequeue();
                if (m_bilety2.id.ToString() == (string)listViewBilety2.Items[listViewBilety2.FocusedItem.Index].Tag)
                {
                    textBoxName.Text = m_bilety2.name;
                    textBoxCena.Text = m_bilety2.cena;
                    textBoxFiO.Text = m_bilety2.FiO;
                    textBoxKvK.Text = m_bilety2.KvK;
                    textBoxKvN.Text = m_bilety2.KvN;
                    dateTimePicker1.Value = DateTime.Parse(m_bilety2.date);
                    queueBilety2.Enqueue(m_bilety2);
                    break;
                };

                queueBilety2.Enqueue(m_bilety2);
            }

        }

        public void refreshBilety2()
        {
            listViewBilety2.Items.Clear();
            queueBilety2.Clear();


            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `name` FROM `tbl_hostel` ORDER BY `name`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety2.Items.Clear();
                queueBilety2.Clear();
                _Bilety2 t = new _Bilety2();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim(), 0);
                    t.id = int.Parse(dataRow["id"].ToString());
                    item1.SubItems.Add(dataRow["name"].ToString().Trim());
                    t.name = dataRow["name"].ToString().Trim();
                    listViewBilety2.Items.Add(item1);
                    listViewBilety2.Items[listViewBilety2.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueBilety2.Enqueue(t);
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

        public void refreshVydano()
        {
            listViewBilety2.Items.Clear();
            queueBilety2.Clear();
            listViewBilety2.Columns.Clear();
            listViewBilety2.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Наименование", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Цена", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns[0].Width = 25;
            listViewBilety2.Columns[1].Width = 200;
            listViewBilety2.Columns[2].Width = 200;
            listViewBilety2.Columns[3].Width = 200;
            listViewBilety2.Columns[4].Width = 200;
            listViewBilety2.Columns[5].Width = 200;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format ("SELECT `id`, cast(`date` as char) AS `date`, `N_kvit_nach`, `N_kvit_koniec`, `naimenovanie`, `cena` FROM `bilety2_vydano` WHERE `date` >= '{0}' and `date` <= '{1}' ORDER BY `date`", DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day, DateTime.Parse(dateTimePicker3.Text).Year + "-" + DateTime.Parse(dateTimePicker3.Text).Month + "-" + DateTime.Parse(dateTimePicker3.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety2.Items.Clear();
                queueBilety2.Clear();
                _Bilety2 t = new _Bilety2();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim(), 0);
                    t.id = int.Parse(dataRow["id"].ToString());
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    t.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_nach"].ToString().Trim());
                    t.KvN = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.KvK = dataRow["N_kvit_koniec"].ToString().Trim();
                    item1.SubItems.Add(dataRow["naimenovanie"].ToString().Trim());
                    t.name = dataRow["naimenovanie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    t.cena = dataRow["cena"].ToString().Trim();
                    listViewBilety2.Items.Add(item1);
                    listViewBilety2.Items[listViewBilety2.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueBilety2.Enqueue(t);
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
        public void refreshSdano()
        {
            listViewBilety2.Items.Clear();
            queueBilety2.Clear();
            listViewBilety2.Columns.Clear();
            listViewBilety2.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Ф.И.О.", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns[0].Width = 25;
            listViewBilety2.Columns[1].Width = 200;
            listViewBilety2.Columns[2].Width = 200;
            listViewBilety2.Columns[3].Width = 200;
            listViewBilety2.Columns[4].Width = 200;
            
            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format ("SELECT `id`, cast(`date` as char) AS `date`, `N_kvit_nach`, `N_kvit_koniec`, `FiO` FROM `bilety2_sdano` WHERE `date` >= '{0}' and `date` <= '{1}' ORDER BY `date`", DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day, DateTime.Parse(dateTimePicker3.Text).Year + "-" + DateTime.Parse(dateTimePicker3.Text).Month + "-" + DateTime.Parse(dateTimePicker3.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety2.Items.Clear();
                queueBilety2.Clear();
                _Bilety2 t = new _Bilety2();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim(), 0);
                    t.id = int.Parse(dataRow["id"].ToString());
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    t.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_nach"].ToString().Trim());
                    t.KvN = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.KvK = dataRow["N_kvit_koniec"].ToString().Trim();
                    item1.SubItems.Add(dataRow["FiO"].ToString().Trim());
                    t.FiO = dataRow["FiO"].ToString().Trim();
                    
                    listViewBilety2.Items.Add(item1);
                    listViewBilety2.Items[listViewBilety2.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueBilety2.Enqueue(t);
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
            if (listViewBilety2.FocusedItem == null)
                return;
            if (bVydano == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("DELETE FROM `bilety2_vydano` WHERE `id`='{0}'", m_bilety2.id);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись,\r\nт.к. с ней связаны данные из других таблиц");
                }
                finally
                {
                    mainWin.m_dbConnector.Unlock();
                    refreshVydano();
                }
            }
            else if (bSdano == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("DELETE FROM `bilety2_sdano` WHERE `id`='{0}'", m_bilety2.id);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись,\r\nт.к. с ней связаны данные из других таблиц");
                }
                finally
                {
                    mainWin.m_dbConnector.Unlock();
                    refreshSdano();
                }
            }
            textBoxName.Text = "";
            textBoxKvN.Text = "";
            textBoxKvK.Text = "";
            textBoxFiO.Text = "";
            textBoxCena.Text = "";
            
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;

        }

        private void buttonVydano_Click(object sender, EventArgs e)
        {
            bVydano = true;
            bSdano = false;
            DateTime pickedDate = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, 1);
            dateTimePicker2.Value = pickedDate;
            dateTimePicker3.Value = DateTime.Now;
            refreshVydano();
            buttonVydano.Enabled = false;
            buttonSdano.Enabled = true;
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;
            dateTimePicker1.Enabled = true;
            textBoxCena.Text = "50";
            textBoxCena.Enabled = true;
            textBoxFiO.Enabled = false;
            textBoxKvN.Enabled = true;
            textBoxKvK.Enabled = true;
            textBoxName.Enabled = true;
            
            textBoxName.Text = "";
            textBoxKvN.Text = "";
            textBoxKvK.Text = "";
            textBoxFiO.Text = "";
            textBoxCena.Text = "";
            
        }
        private void buttonSdano_Click(object sender, EventArgs e)
        {
            bSdano = true;
            bVydano = false;
            DateTime pickedDate = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, 1);
            dateTimePicker2.Value = pickedDate;
            dateTimePicker3.Value = DateTime.Now;
            refreshSdano();
            buttonSdano.Enabled = false;
            buttonVydano.Enabled = true;
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;
            dateTimePicker1.Enabled = true;
            textBoxCena.Enabled = false;
            textBoxFiO.Enabled = true;
            textBoxKvN.Enabled = true;
            textBoxKvK.Enabled = true;
            textBoxName.Enabled = false;
            

            textBoxName.Text = "";
            textBoxKvN.Text = "";
            textBoxKvK.Text = "";
            textBoxFiO.Text = "";
            textBoxCena.Text = "";

        }
        /*
         * Отчет по остатку билетов
         * начало  
         *   
         * 
         *
         * */
        //начальные и конечные номера билетов выдано
        public void refreshOst()
        {

            listViewBilety2.Items.Clear();
            queueBilety2.Clear();

            listViewBilety2.Columns.Clear();
            listViewBilety2.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);

            listViewBilety2.Columns[0].Width = 150;
            listViewBilety2.Columns[1].Width = 150;



            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "select `bilety2_vydano`.`N_kvit_nach` AS `N_kvit_nach`,`bilety2_vydano`.`N_kvit_koniec` AS `N_kvit_koniec` from `bilety2_vydano` order by `N_kvit_nach`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety2.Items.Clear();
                queueBilety2.Clear();
                _Bilety2 t = new _Bilety2();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    ListViewItem item1 = new ListViewItem(dataRow["N_kvit_nach"].ToString().Trim());
                    t.KvN = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.KvK = dataRow["N_kvit_koniec"].ToString().Trim();
                    
                    listViewBilety2.Items.Add(item1);
                    listViewBilety2.Items[listViewBilety2.Items.Count - 1].Tag = dataRow["N_kvit_nach"].ToString();
                    queueBilety2.Enqueue(t);

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
        //начальные и конечные номера билетов сдано
        public void refreshOst1()
        {

            listViewBilety2.Items.Clear();
            queueBilety2.Clear();

            listViewBilety2.Columns.Clear();
            listViewBilety2.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);

            listViewBilety2.Columns[0].Width = 150;
            listViewBilety2.Columns[1].Width = 150;



            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "select `bilety2_sdano`.`N_kvit_nach` AS `N_kvit_nach`,`bilety2_sdano`.`N_kvit_koniec` AS `N_kvit_koniec` from `bilety2_sdano` order by `N_kvit_nach`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety2.Items.Clear();
                queueBilety2.Clear();
                _Bilety2 t = new _Bilety2();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    ListViewItem item1 = new ListViewItem(dataRow["N_kvit_nach"].ToString().Trim());
                    t.KvN = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.KvK = dataRow["N_kvit_koniec"].ToString().Trim();

                    listViewBilety2.Items.Add(item1);
                    listViewBilety2.Items[listViewBilety2.Items.Count - 1].Tag = dataRow["N_kvit_nach"].ToString();
                    queueBilety2.Enqueue(t);

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
        //конечные номера билетов и названия экскурсий
        public void refreshOst2()
        {

            listViewBilety2.Items.Clear();
            queueBilety2.Clear();

            listViewBilety2.Columns.Clear();
            listViewBilety2.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Название", -2, HorizontalAlignment.Left);
            listViewBilety2.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);

            listViewBilety2.Columns[0].Width = 150;
            listViewBilety2.Columns[1].Width = 150;
            listViewBilety2.Columns[2].Width = 150;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "select `bilety2_vydano`.`N_kvit_koniec` AS `N_kvit_koniec`,`bilety2_vydano`.`naimenovanie` AS `naimenovanie`,`bilety2_vydano`.`cena` AS `stoimost` from `bilety2_vydano` order by `bilety2_vydano`.`N_kvit_koniec`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewBilety2.Items.Clear();
                queueBilety2.Clear();
                _Bilety2 t = new _Bilety2();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    ListViewItem item1 = new ListViewItem(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.KvN = dataRow["N_kvit_koniec"].ToString().Trim();
                    item1.SubItems.Add(dataRow["naimenovanie"].ToString().Trim());
                    t.name = dataRow["naimenovanie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoimost"].ToString().Trim());
                    t.cena = dataRow["stoimost"].ToString().Trim();

                    listViewBilety2.Items.Add(item1);
                    listViewBilety2.Items[listViewBilety2.Items.Count - 1].Tag = dataRow["N_kvit_koniec"].ToString();
                    queueBilety2.Enqueue(t);

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
            //массив0 выдано
            refreshOst();
            int[,] arr = new int[listViewBilety2.Items.Count, listViewBilety2.Columns.Count];
            for (int x = 0, maxX = listViewBilety2.Items.Count; x < maxX; x++)
            {
                for (int y = 0, maxY = listViewBilety2.Columns.Count; y < maxY; y++)
                {
                    arr[x, y] = int.Parse(listViewBilety2.Items[x].SubItems[y].Text.Trim());

                }
            }
            //массив1 сдано
            refreshOst1();
            int[,] arr1 = new int[listViewBilety2.Items.Count, listViewBilety2.Columns.Count];
            for (int x = 0, maxX = listViewBilety2.Items.Count; x < maxX; x++)
            {
                for (int y = 0, maxY = listViewBilety2.Columns.Count; y < maxY; y++)
                {
                    arr1[x, y] = int.Parse(listViewBilety2.Items[x].SubItems[y].Text.Trim());

                }
            }
            //массив2 названия
            refreshOst2();
            string[,] b = new string[listViewBilety2.Items.Count, listViewBilety2.Columns.Count];
            for (int x = 0, maxX = listViewBilety2.Items.Count; x < maxX; x++)
            {
                for (int y = 0, maxY = listViewBilety2.Columns.Count; y < maxY; y++)
                {
                    b[x, y] = listViewBilety2.Items[x].SubItems[y].Text;

                }
            }
                       
            //refreshBilety2();
            listViewBilety2.Items.Clear();
            listViewBilety2.Columns.Clear();
            
            List<int> ARR = new List<int>();
            List<int> ARR1 = new List<int>();
            List<int> ARR2 = new List<int>();
            List<int> ARR3 = new List<int>();
            List<int> unique = new List<int>();
            List<object> unique2 = new List<object>();

            //диапазоны из нач/кон значений выдано
            for (int x = 0; x <= ((arr.Length / 2) - 1); x++)
            {
                for (int i = arr[x, 0], maxi = arr[x, 1]; i <= maxi; i++)
                {
                    ARR.Add(i);
                }
            }
            //диапазоны из нач/кон значений сдано
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
                    for (int y = 0; y < b.Length / 3; y++)
                    {
                        if (unique[x + 1].Equals(int.Parse(b[y, 0])))
                        {
                            unique2.Add(b[y, 1]);
                            unique2.Add(b[y, 2]);
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
       */
        
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (bVydano == true) refreshVydano();

            else if (bSdano == true) refreshSdano();
        }

    }
}