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
    public struct _Ekskursovody
    {
        public _Ekskursovody(string N_ekskursovoda, string FiO, string category, string rozhd, string passport, string inn, string strax, string tel, string address, string activ)
        {
            this.id_val = N_ekskursovoda;
            this.name_val = FiO;
            this.cat_val = category;
            this.rozhd_val = rozhd;
            this.pass_val = passport;
            this.inn_val = inn;
            this.strax_val = strax;
            this.tel_val = tel;
            this.address_val = address;
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
        public string category
        {
            get
            {
                return cat_val;
            }
            set
            {
                cat_val = value;
            }
        }
        public string rozhd
        {
            get
            {
                return rozhd_val;
            }
            set
            {
                rozhd_val = value;
            }
        }
        public string passport
        {
            get
            {
                return pass_val;
            }
            set
            {
                pass_val = value;
            }
        }
        public string inn
        {
            get
            {
                return inn_val;
            }
            set
            {
                inn_val = value;
            }
        }
        public string strax
        {
            get
            {
                return strax_val;
            }
            set
            {
                strax_val = value;
            }
        }
        public string tel
        {
            get
            {
                return tel_val;
            }
            set
            {
                tel_val = value;
            }
        }
        public string address
        {
            get
            {
                return address_val;
            }
            set
            {
                address_val = value;
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
        public override string ToString()
        {
            return name_val;
        }

        private string id_val;
        private string name_val;
        private string cat_val;
        private string rozhd_val;
        private string pass_val;
        private string inn_val;
        private string strax_val;
        private string tel_val;
        private string address_val;
        private string activ_val;
    }

    public partial class FormEkskursovody : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueEkskursovody = new Queue();
        _Ekskursovody m_account;

        public FormEkskursovody(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewEkskursovody.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("Ф.И.О.", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("Категория", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("Дата рождения", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("Паспорт", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("ИНН", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("Страховое", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("Телефон", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("Адрес", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns.Add("Активность", -2, HorizontalAlignment.Left);
            listViewEkskursovody.Columns[0].Width = 50;
            listViewEkskursovody.Columns[1].Width = 150;
            listViewEkskursovody.Columns[2].Width = 150;
            listViewEkskursovody.Columns[3].Width = 150;
            listViewEkskursovody.Columns[4].Width = 150;
            listViewEkskursovody.Columns[5].Width = 150;
            listViewEkskursovody.Columns[6].Width = 150;
            listViewEkskursovody.Columns[7].Width = 150;
            listViewEkskursovody.Columns[8].Width = 200;
            listViewEkskursovody.Columns[9].Width = 200;
        }

        private void FormAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formEkskursovody = null;
        }


        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxName.Text = "";
            textBoxName.Enabled = true;
            comboBoxCat.SelectedIndex = -1;
            comboBoxCat.Enabled = true;
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
            textBox6.Text = "";
            textBox6.Enabled = true;
            textBox7.Text = "";
            textBox7.Enabled = true;
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

                    string sql = String.Format("INSERT INTO `ekskursovody` (`N_ekskursovoda`, `FiO`, `category`, `rozhd`, `passport`, `inn`, `strax`, `tel`, `address`, `activ`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')", textBox5.Text, textBoxName.Text, comboBoxCat.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, comboBoxActivity.Text);
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
                if (listViewEkskursovody.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `ekskursovody` SET `FiO`='{0}',`category`='{1}',`rozhd`='{2}', `passport`='{3}', `inn`='{4}', `strax`='{5}', `N_ekskursovoda`='{6}', `tel`='{7}', `address`='{8}', `activ`='{9}' WHERE `N_ekskursovoda`='{10}'", textBoxName.Text, comboBoxCat.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, comboBoxActivity.Text, m_account.id);
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

            comboBoxCat.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            comboBoxActivity.Enabled = false;
            refreshEkskursovody();
        }

        private void listViewEkskursovody_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxName.Enabled = true;

            comboBoxCat.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            comboBoxActivity.Enabled = true;
            bNew = false;

            if (listViewEkskursovody.FocusedItem == null)
                return;

            int k = queueEkskursovody.Count;
            for (int i = 0; i < k; i++)
            {
                m_account = (_Ekskursovody)queueEkskursovody.Dequeue();
                if (m_account.id == (string)listViewEkskursovody.Items[listViewEkskursovody.FocusedItem.Index].Tag)
                {
                    textBoxName.Text = m_account.name;
                    comboBoxCat.Text = m_account.category;
                    textBox1.Text = m_account.rozhd;
                    textBox2.Text = m_account.passport;
                    textBox3.Text = m_account.inn;
                    textBox4.Text = m_account.strax;
                    textBox6.Text = m_account.tel;
                    textBox5.Text = m_account.id;
                    textBox7.Text = m_account.address;
                    comboBoxActivity.Text = m_account.activ;
                    queueEkskursovody.Enqueue(m_account);
                    break;
                };

                queueEkskursovody.Enqueue(m_account);
            }
        }


        public void refreshEkskursovody()
        {
            listViewEkskursovody.Items.Clear();
            queueEkskursovody.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `N_ekskursovoda`, `FiO`, `category`, `rozhd`, `passport`, `inn`, `strax`, `tel`, `address`, `activ` FROM `ekskursovody` ORDER BY `activ`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewEkskursovody.Items.Clear();
                queueEkskursovody.Clear();
                _Ekskursovody t = new _Ekskursovody();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["N_ekskursovoda"].ToString().Trim());
                    t.id = dataRow["N_ekskursovoda"].ToString().Trim();
                    item1.SubItems.Add(dataRow["FiO"].ToString().Trim());
                    t.name = dataRow["FiO"].ToString().Trim();
                    item1.SubItems.Add(dataRow["category"].ToString().Trim());
                    t.category = dataRow["category"].ToString().Trim();
                    item1.SubItems.Add(dataRow["rozhd"].ToString().Trim());
                    t.rozhd = dataRow["rozhd"].ToString().Trim();
                    item1.SubItems.Add(dataRow["passport"].ToString().Trim());
                    t.passport = dataRow["passport"].ToString().Trim();
                    item1.SubItems.Add(dataRow["inn"].ToString().Trim());
                    t.inn = dataRow["inn"].ToString().Trim();
                    item1.SubItems.Add(dataRow["strax"].ToString().Trim());
                    t.strax = dataRow["strax"].ToString().Trim();
                    item1.SubItems.Add(dataRow["tel"].ToString().Trim());
                    t.tel = dataRow["tel"].ToString().Trim();
                    item1.SubItems.Add(dataRow["address"].ToString().Trim());
                    t.address = dataRow["address"].ToString().Trim();
                    item1.SubItems.Add(dataRow["activ"].ToString().Trim());
                    t.activ = dataRow["activ"].ToString().Trim();

                    listViewEkskursovody.Items.Add(item1);
                    listViewEkskursovody.Items[listViewEkskursovody.Items.Count - 1].Tag = dataRow["N_ekskursovoda"].ToString();
                    queueEkskursovody.Enqueue(t);
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
            if (listViewEkskursovody.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `ekskursovody` WHERE `N_ekskursovoda`='{0}'", m_account.id);
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

            comboBoxCat.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;   
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            comboBoxActivity.Enabled = false;
            refreshEkskursovody();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonApply.PerformClick();
        }

        private void FormEkskursovody_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;

            textBoxName.Enabled = false;

            comboBoxCat.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;   
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            comboBoxActivity.Enabled = false;
            refreshEkskursovody();
        }


    }
}

