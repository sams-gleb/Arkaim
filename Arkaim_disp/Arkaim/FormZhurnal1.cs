using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace Buh
{
    
    public struct _Zhurnal
    {
        public _Zhurnal(string id, string date, string FiO, string N_ekskursii, string N_kvit_nach, string N_kvit_koniec, string kol_czel, string zakazczik, string cena, string stoimost)
        {
            this.id_val = id;
            this.date_val = date;
            this.FiO_val = FiO;
            this.N_ekskursii_val = N_ekskursii;
            this.N_kvit_nach_val = N_kvit_nach;
            this.N_kvit_koniec_val = N_kvit_koniec;
            this.kol_czel_val = kol_czel;
            this.zakazczik_val = zakazczik;
            this.cena_val = cena;
            this.stoimost_val = stoimost;
        }

        public string id
        {
            get
            {
                return this.id_val;
            }
            set
            {
                this.id_val = value;
            }
        }
        public string date
        {
            get
            {
                return this.date_val;
            }
            set
            {
                this.date_val = value;
            }
        }
        public string FiO
        {
            get
            {
                return this.FiO_val;
            }
            set
            {
                this.FiO_val = value;
            }
        }

        public string N_ekskursii
        {
            get
            {
                return this.N_ekskursii_val;
            }
            set
            {
                this.N_ekskursii = value;
            }
        }

        public string N_kvit_nach
        {
            get
            {
                return this.N_kvit_nach_val;
            }
            set
            {
                this.N_kvit_nach_val = value;
            }
        }

        public string N_kvit_koniec
        {
            get
            {
                return this.N_kvit_koniec_val;
            }
            set
            {
                this.N_kvit_koniec_val = value;
            }
        }

        public string kol_czel
        {
            get
            {
                return this.kol_czel_val;
            }
            set
            {
                this.kol_czel_val = value;
            }
        }
        
        public string zakazczik
        {
            get
            {
                return this.zakazczik_val;
            }
            set
            {
                this.zakazczik_val = value;
            }
        }

        public string cena
        {
            get
            {
                return this.cena_val;
            }
            set
            {
                this.cena_val = value;
            }
        }

        public string stoimost
        {
            get
            {
                return this.stoimost_val;
            }
            set
            {
                this.stoimost_val = value;
            }
        }


        public override string ToString()
        {
            return FiO_val;
        }

        private string id_val;
        private string date_val;
        private string FiO_val;
        private string N_ekskursii_val;
        private string N_kvit_nach_val;
        private string N_kvit_koniec_val;
        private string kol_czel_val;
        private string zakazczik_val;
        private string cena_val;
        private string stoimost_val;
    }
    
       
    public partial class FormZhurnal1 : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueZhurnal = new Queue();
        _Zhurnal m_zhurnal;
     /* _Ekskursovody m_ekskursovody;
        Queue queueEkskursovod = new Queue();
        _Ekskursii m_ekskursii;
        Queue queueEkskursii = new Queue(); */
        
        public FormZhurnal1 (FormMain mainWin)
        {
            InitializeComponent();

            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewZhurnal.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Ф.И.О.", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Экскурсия", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Квитанции начало", -2, HorizontalAlignment.Left);
            listViewZhurnal.Columns.Add("Квитанции конец", -2, HorizontalAlignment.Left);
        //    listViewZhurnal.Columns.Add("Количество чеовек", -2, HorizontalAlignment.Left);
       //     listViewZhurnal.Columns.Add("Цена экскурсии", -2, HorizontalAlignment.Left);
      //      listViewZhurnal.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);
//            listViewZhurnal.Columns.Add("Заказчик", -2, HorizontalAlignment.Left);

            listViewZhurnal.Columns[0].Width = 25;
            listViewZhurnal.Columns[1].Width = 100;
            listViewZhurnal.Columns[2].Width = 100;
            listViewZhurnal.Columns[3].Width = 100;
            listViewZhurnal.Columns[4].Width = 100;
            listViewZhurnal.Columns[5].Width = 100;
      /*      listViewZhurnal.Columns[6].Width = 100;
            listViewZhurnal.Columns[7].Width = 100;
            listViewZhurnal.Columns[8].Width = 100;
            listViewZhurnal.Columns[9].Width = 100; */
            
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
   //         buttonExcel.Enabled = false;
                        
            comboBoxEkskursovod.Enabled = false;
            comboBoxNekskursii.Enabled = false;
            textBoxKvitNach.Enabled = false;
            textBoxKvitKoniec.Enabled = false;
            textBoxKolCzel.Enabled = false;
            textBoxZakaz.Enabled = false;
         //   refreshEkskursii();
        //    refreshEkskursovody();
        }
        


        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
  //          buttonExcel.Enabled = true;
            buttonDelete.Enabled = false;

            comboBoxEkskursovod.SelectedIndex = -1;
            comboBoxEkskursovod.Enabled = true;
            comboBoxNekskursii.SelectedIndex = -1;
            comboBoxNekskursii.Enabled = true;
            textBoxKvitNach.Text = "";
            textBoxKvitNach.Enabled = true;
            textBoxKvitKoniec.Text = "";
            textBoxKvitKoniec.Enabled = true;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = true;
            textBoxZakaz.Text = "";
            textBoxZakaz.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Enabled = true;
            dateTimePicker2.Value = DateTime.Today;
            dateTimePickerDate.Enabled = true;
            dateTimePickerDate.Value = DateTime.Today;
        }





        private void buttonApply_Click(object sender, EventArgs e)
        {
          /*  if (comboBoxEkskursovod.SelectedIndex == -1)
                return;
            if (comboBoxNekskursii.SelectedIndex == -1)
                return; */
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                    string sql = String.Format("INSERT INTO `plategki` (`N_ekskursii`, `date`, `FiO`, `N_platezhki`, `Kol_czel`, `zakazczik`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", comboBoxNekskursii.Text, DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, comboBoxEkskursovod.Text, textBoxKvitNach.Text, textBoxKolCzel.Text, textBoxZakaz.Text);
                                       
                    if (textBoxKolCzel.Text.Trim() == "")
                        sql = String.Format("INSERT INTO `zhurnal` (`date`, `FiO`, `N_ekskursii`, `N_kvit_nach`, `N_kvit_koniec`, `zakazczik`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, comboBoxEkskursovod.Text, comboBoxNekskursii.Text, textBoxKvitNach.Text, textBoxKvitKoniec.Text, textBoxZakaz.Text);
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
                    string sql = String.Format("UPDATE `plategki` SET `N_ekskursii`='{0}', `date`='{1}', `FiO`='{2}', `N_platezhki`='{3}', `Kol_czel`='{4}', `zakazczik`='{5}' WHERE `id` = '{6}')", comboBoxNekskursii.Text, DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, comboBoxEkskursovod.Text, textBoxKvitNach.Text, textBoxKolCzel.Text, textBoxZakaz.Text, m_zhurnal.id);
                    
                    if (textBoxKolCzel.Text.Trim() == "")
                         sql = String.Format("UPDATE `zhurnal` SET `date` = '{0}', `FiO` = '{1}', `N_ekskursii` = '{2}', `N_kvit_nach` = '{3}', `N_kvit_koniec` = '{4}', `zakazczik` = '{5}' WHERE `id` = '{6}')", DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, comboBoxEkskursovod.Text, comboBoxNekskursii.Text, textBoxKvitNach.Text, textBoxKvitKoniec.Text, textBoxZakaz.Text, m_zhurnal.id);
                            
                    //MessageBox.Show(sql);
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

            comboBoxEkskursovod.SelectedIndex = -1;
            comboBoxEkskursovod.Enabled = false;
            comboBoxNekskursii.SelectedIndex = -1;
            comboBoxNekskursii.Enabled = false;
            textBoxKvitNach.Text = "";
            textBoxKvitNach.Enabled = false;
            textBoxKvitKoniec.Text = "";
            textBoxKvitKoniec.Enabled = false;
            textBoxKolCzel.Text = "";
            textBoxKolCzel.Enabled = false;
            textBoxZakaz.Text = "";
            textBoxZakaz.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Enabled = false;
            dateTimePicker2.Value = DateTime.Today;
            dateTimePickerDate.Enabled = false;
            dateTimePickerDate.Value = DateTime.Today;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            //buttonExcel.Enabled = false;
            refreshZhurnal();
        }

        private void listViewZhurnal_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
         //   buttonExcel.Enabled = true;
            buttonDelete.Enabled = true;
                                    
            comboBoxEkskursovod.Enabled = true;
            comboBoxNekskursii.Enabled = true;
            textBoxKvitNach.Enabled = true;
            textBoxKvitKoniec.Enabled = true;
            textBoxKolCzel.Enabled = true;
            textBoxZakaz.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Enabled = true;
            dateTimePicker2.Value = DateTime.Today;
            dateTimePickerDate.Enabled = true;
            dateTimePickerDate.Value = DateTime.Today;

            bNew = false;

            if (listViewZhurnal.FocusedItem == null)
                return;

            int k = queueZhurnal.Count;
            for (int i = 0; i < k; i++)
            {
                m_zhurnal = (_Zhurnal)queueZhurnal.Dequeue();
                if (m_zhurnal.id.ToString() == (string)listViewZhurnal.Items[listViewZhurnal.FocusedItem.Index].Tag)
                {
                    
             /*      _Ekskursii c;
                    int k2 = queueEkskursii.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        c = (_Ekskursii)queueEkskursii.Dequeue();
                        if (c.id.ToString() == m_zhurnal.N_ekskursii.ToString())
                        {

                            for (int iii = 0; iii < comboBoxNekskursii.Items.Count; iii++)
                            {
                                if (((_Ekskursii)comboBoxNekskursii.Items[iii]).id == c.id)
                                {
                                    comboBoxNekskursii.SelectedIndex = iii;
                                    m_ekskursii = c;
                                    break;
                                }
                            }
                        };
                        queueEkskursii.Enqueue(c);
                    }

                    dateTimePickerDate.Value = DateTime.Parse(m_zhurnal.date);
                    dateTimePickerDate.Enabled = true;

                    _Ekskursovody a;
                    k2 = queueEkskursovod.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        a = (_Ekskursovody)queueEkskursovod.Dequeue();
                        if (a.id.ToString() == m_zhurnal.FiO.ToString())
                        {

                            for (int iii = 0; iii < comboBoxEkskursovod.Items.Count; iii++)
                            {
                                if (((_Ekskursovody) comboBoxEkskursovod.Items[iii]).id == a.id)
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
                    break;*/

                    textBoxKvitNach.Text = m_zhurnal.N_kvit_nach;
                    textBoxKvitKoniec.Text = m_zhurnal.N_kvit_koniec;
                    textBoxKolCzel.Text = m_zhurnal.kol_czel;
                    textBoxZakaz.Text = m_zhurnal.zakazczik;
      
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

                string sql = "SELECT `id`, `date`, `FiO`, `N_kvit_nach`, `N_kvit_koniec`, `N_ekskursii` FROM `zhurnal` ORDER BY `id`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                
                listViewZhurnal.Items.Clear();
                queueZhurnal.Clear();
                _Zhurnal m = new _Zhurnal();

               // int i = 1;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                   // ListViewItem item1 = new ListViewItem(i.ToString(), 0);
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim());
                    m.id = dataRow["id"].ToString();
                    m.date = dataRow["date"].ToString();
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    m.FiO = dataRow["FiO"].ToString();
                    item1.SubItems.Add(dataRow["FiO"].ToString().Trim());
                    m.N_kvit_nach = dataRow["N_kvit_nach"].ToString();
                    item1.SubItems.Add(dataRow["N_kvit_nach"].ToString().Trim());
                    m.N_kvit_koniec = dataRow["N_kvit_koniec"].ToString();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    m.N_ekskursii = dataRow["N_ekskursii"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_ekskursii"].ToString().Trim());

                    listViewZhurnal.Items.Add(item1);
                    listViewZhurnal.Items[listViewZhurnal.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueZhurnal.Enqueue(m);
                  //  ++i;
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
            if (listViewZhurnal.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `zhurnal` WHERE `id`='{0}'", m_zhurnal.id);
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

            comboBoxEkskursovod.Enabled = false;
            comboBoxNekskursii.Enabled = false;
            textBoxKvitNach.Enabled = false;
            textBoxKvitKoniec.Enabled = false;
            textBoxKolCzel.Enabled = false;
            textBoxZakaz.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Enabled = false;
            dateTimePicker2.Value = DateTime.Today;
            dateTimePickerDate.Enabled = false;
            dateTimePickerDate.Value = DateTime.Today;
            
            
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
           // buttonExcel.Enabled = false;
            refreshZhurnal();
        }





      /*  public void refreshEkskursovody()
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

                string sql = "SELECT `N_ekskursovoda`, `FiO` FROM `ekskursovody` ORDER BY `FiO`";
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
            if (comboBoxNekskursii.SelectedIndex != -1)
            {
                N_ekskursii = m_ekskursii.id.ToString();
            }

            comboBoxNekskursii.Items.Clear();
            queueEkskursii.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `N_ekskursii`, `nazvanie` FROM `ekskursii` ORDER BY `nazvanie`";
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

                    comboBoxNekskursii.Items.Add(c);


                    if (N_ekskursii != "" && N_ekskursii == c.id.ToString())
                    {
                        comboBoxNekskursii.SelectedItem = c;
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

        private void comboBoxNekskursii_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxNekskursii.SelectedIndex == -1)
                return;
            m_ekskursii = (_Ekskursii)comboBoxNekskursii.SelectedItem;
        }

        private void comboBoxEkskursovod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEkskursovod.SelectedIndex == -1)
                return;
            m_ekskursovody = (_Ekskursovody)comboBoxEkskursovod.SelectedItem;
        }

        */
        
        public FormZhurnal1()
        {
            InitializeComponent();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void zhurnal_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
