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
    public struct _Ekskursii
    {
        public _Ekskursii(string N_ekskursii, string nazvanie, string stoimost, string P_kat, string S_kat, string T_kat, string activ)
        {
            this.id_val = N_ekskursii;
            this.nazvanie_val = nazvanie;
            this.stoimost_val = stoimost;
            this.P_kat_val = P_kat;
            this.S_kat_val = S_kat;
            this.T_kat_val = T_kat;
            this.activ_val = activ;
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
                
        public string nazvanie
        {
            get
            {
                return nazvanie_val;
            }
            set
            {
                nazvanie_val = value;
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
        public string P_kat
        {
            get
            {
                return P_kat_val;
            }
            set
            {
                P_kat_val = value;
            }
        }
        public string S_kat
        {
            get
            {
                return S_kat_val;
            }
            set
            {
                S_kat_val = value;
            }
        }
        public string T_kat
        {
            get
            {
                return T_kat_val;
            }
            set
            {
                T_kat_val = value;
            }
        }
        public string activ
        {
            get
            {
                return activ_val;
            }
            set
            {
                activ_val = value;
            }
        }
       
        public override string ToString(){
            return nazvanie_val;
        }

        private string id_val;
        private string nazvanie_val;
        private string stoimost_val;
        private string P_kat_val;
        private string S_kat_val;
        private string T_kat_val;
        private string activ_val;
    }

    public partial class FormEkskursii : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueEkskursii = new Queue();
        _Ekskursii m_ekskursii;

        public FormEkskursii(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewEkskursii.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewEkskursii.Columns.Add("Название", -2, HorizontalAlignment.Left);
            listViewEkskursii.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);
            listViewEkskursii.Columns.Add("Оплата по 1 кат.", -2, HorizontalAlignment.Left);
            listViewEkskursii.Columns.Add("Оплата по 2 кат.", -2, HorizontalAlignment.Left);
            listViewEkskursii.Columns.Add("Оплата по 3 кат.", -2, HorizontalAlignment.Left);
            listViewEkskursii.Columns.Add("Активность", -2, HorizontalAlignment.Left);
            listViewEkskursii.Columns[0].Width = 25;
            listViewEkskursii.Columns[1].Width = 150;
            listViewEkskursii.Columns[2].Width = 150;
            listViewEkskursii.Columns[3].Width = 150;
            listViewEkskursii.Columns[4].Width = 150;
            listViewEkskursii.Columns[5].Width = 150;
            listViewEkskursii.Columns[6].Width = 150;
        }

        private void FormEkskursii_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formEkskursii = null;
        }


        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

           textBoxNumber.Text = "";
            textBoxNumber.Enabled = true;
            textBox1.Text = "";
            textBox1.Enabled = true;
            textBox2.Text = "";
            textBox2.Enabled = true;
            textBox3.Text = "";
            textBox3.Enabled = true;
            textBox4.Text = "";
            textBox4.Enabled = true;
            textBox5.Text = "";
            textBox5.Enabled = true;
            comboBoxActivity.SelectedIndex = -1;
            comboBoxActivity.Enabled = true;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("INSERT INTO `ekskursii` (`nazvanie`, `stoimost`, `1_kat`, `2_kat`, `3_kat`, `N_ekskursii`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBoxNumber.Text);
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
                if (listViewEkskursii.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `ekskursii` SET `nazvanie`='{0}',`stoimost`='{1}',`1_kat`='{2}',`2_kat`='{3}', `3_kat`='{4}', `N_ekskursii`='{5}' WHERE `N_ekskursii`='{6}'", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBoxNumber.Text, m_ekskursii.id);
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

            textBoxNumber.Text = "";
            textBoxNumber.Enabled = false;
            textBox1.Text = "";
            textBox1.Enabled = false;
            textBox2.Text = "";
            textBox2.Enabled = false;
            textBox3.Text = "";
            textBox3.Enabled = false;
            textBox4.Text = "";
            textBox4.Enabled = false;
            textBox5.Text = "";
            textBox5.Enabled = false;
            comboBoxActivity.SelectedIndex = -1;
            comboBoxActivity.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshEkskursii();
        }

        private void listViewEkskursii_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxNumber.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            comboBoxActivity.Enabled = true;
            bNew = false;

            if (listViewEkskursii.FocusedItem == null)
                return;

            int k = queueEkskursii.Count;
            for (int i = 0; i < k; i++)
            {
                m_ekskursii = (_Ekskursii)queueEkskursii.Dequeue();
                if (m_ekskursii.id.ToString() == (string)listViewEkskursii.Items[listViewEkskursii.FocusedItem.Index].Tag)
                {
                    textBoxNumber.Text = m_ekskursii.id;
                    textBox1.Text = m_ekskursii.nazvanie;
                    textBox2.Text = m_ekskursii.stoimost;
                    textBox3.Text = m_ekskursii.P_kat;
                    textBox4.Text = m_ekskursii.S_kat;
                    textBox5.Text = m_ekskursii.T_kat;
                    comboBoxActivity.Text = m_ekskursii.activ;
                    
                    queueEkskursii.Enqueue(m_ekskursii);
                    break;
                };

                queueEkskursii.Enqueue(m_ekskursii);
            }

        }


        public void refreshEkskursii()
        {
            listViewEkskursii.Items.Clear();
            queueEkskursii.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `N_ekskursii`, `nazvanie`, `stoimost`, `1_kat`, `2_kat`, `3_kat` FROM `ekskursii` ORDER BY `N_ekskursii`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewEkskursii.Items.Clear();
                queueEkskursii.Clear();
                _Ekskursii t = new _Ekskursii();

              foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["N_ekskursii"].ToString().Trim());
                    t.id = dataRow["N_ekskursii"].ToString().Trim();
                    item1.SubItems.Add(dataRow["nazvanie"].ToString().Trim());
                    t.nazvanie = dataRow["nazvanie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoimost"].ToString().Trim());
                    t.stoimost = dataRow["stoimost"].ToString().Trim();
                    item1.SubItems.Add(dataRow["1_kat"].ToString().Trim());
                    t.P_kat = dataRow["1_kat"].ToString().Trim();
                    item1.SubItems.Add(dataRow["2_kat"].ToString().Trim());
                    t.S_kat = dataRow["2_kat"].ToString().Trim();
                    item1.SubItems.Add(dataRow["3_kat"].ToString().Trim());
                    t.T_kat = dataRow["3_kat"].ToString().Trim();
 
                    
                    listViewEkskursii.Items.Add(item1);
                    listViewEkskursii.Items[listViewEkskursii.Items.Count - 1].Tag = dataRow["N_ekskursii"].ToString();
                    queueEkskursii.Enqueue(t);
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
            if (listViewEkskursii.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `ekskursii` WHERE `N_ekskursii`='{0}'", m_ekskursii.id);
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

            textBoxNumber.Text = "";
            textBoxNumber.Enabled = false;
            
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshEkskursii();
        }

        private void textBoxAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonApply.PerformClick();
        }

        private void FormAccounts_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;

            textBoxNumber.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            refreshEkskursii();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


    }
}