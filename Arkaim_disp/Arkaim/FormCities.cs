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
    public struct _Cities
    {
        public _Cities(string id, string name)
        {
            this.id_val = id;
            this.name_val = name;
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

        public override string ToString()
        {
            return name_val;
        }

        private string id_val;
        private string name_val;
    }

    public partial class FormCities : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueCities = new Queue();
        _Cities m_cities;
        
        public FormCities(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewCities.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewCities.Columns.Add("Название", -2, HorizontalAlignment.Left);

            listViewCities.Columns[0].Width = 25;
            listViewCities.Columns[1].Width = 150;
        }

        private void FormCities_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formCities = null;
        }


        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;
            //knopki
            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxCity.Text = "";
            textBoxCity.Enabled = true;
            
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();
                    if (textBoxCity.Text.Trim() != "")
                    {
                        string sql = String.Format("INSERT INTO `city` (`name`) VALUES ('{0}')", textBoxCity.Text);
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    else throw new System.InvalidOperationException("Хреновина с названием города не может быть пустой!"); 



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
                if (listViewCities.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `city` SET `name`='{0}' WHERE `city_id`='{1}'", textBoxCity.Text, m_cities.id);
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

            textBoxCity.Text = "";
            textBoxCity.Enabled = false;
            
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshCities();
        }

        private void listViewCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxCity.Enabled = true;

            bNew = false;

            if (listViewCities.FocusedItem == null)
                return;

            int k = queueCities.Count;
            for (int i = 0; i < k; i++)
            {
                m_cities = (_Cities)queueCities.Dequeue();
                if (m_cities.id == (string)listViewCities.Items[listViewCities.FocusedItem.Index].Tag)
                {
                    textBoxCity.Text = m_cities.name;
                    
                    queueCities.Enqueue(m_cities);

                    break;
                };

                queueCities.Enqueue(m_cities);
            }
        }


        public void refreshCities()
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
                DataTable dataTable = dataSet.Tables[0];

                listViewCities.Items.Clear();
                queueCities.Clear();
                _Cities t = new _Cities();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["city_id"].ToString().Trim());
                    t.id = dataRow["city_id"].ToString().Trim();
                    item1.SubItems.Add(dataRow["name"].ToString().Trim());
                    t.name = dataRow["name"].ToString().Trim();
                   

                    listViewCities.Items.Add(item1);
                    listViewCities.Items[listViewCities.Items.Count - 1].Tag = dataRow["city_id"].ToString();
                    queueCities.Enqueue(t);
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
            if (listViewCities.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `city` WHERE `city_id`='{0}'", m_cities.id);
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

            textBoxCity.Text = "";
            textBoxCity.Enabled = false;
            
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshCities();
        }

        private void textBoxCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonApply.PerformClick();
        }

        private void FormCities_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;
            textBoxCity.Enabled = false;

            refreshCities();
            
        }

       


    }

}
