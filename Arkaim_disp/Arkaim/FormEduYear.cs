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

    public struct _EduYears
    {
        public _EduYears(int id, string name)
        {
            this.id_val = id;
            this.name_val = name;
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

        private int id_val;
        private string name_val;
    }


    public partial class FormEduYear : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueEduYears = new Queue();
        _EduYears m_EduYears;

        public FormEduYear(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewEduYears.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewEduYears.Columns.Add("Поставщик", -2, HorizontalAlignment.Left);
            listViewEduYears.Columns[0].Width = 25;
            listViewEduYears.Columns[1].Width = 200;
        }

        private void FormReports_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formReports = null;

        }

        private void FormProviders_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;

            textBoxEduYears.Enabled = false;


            refreshEduYears();

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxEduYears.Text = "";
            textBoxEduYears.Enabled = true;

        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("INSERT INTO `tbl_edu_years` (`name`) VALUES ('{0}')", textBoxEduYears.Text);
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
                if (listViewEduYears.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `tbl_edu_years` SET `name`='{0}' WHERE `id`='{1}'", textBoxEduYears.Text, m_EduYears.id);
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

            textBoxEduYears.Text = "";
            textBoxEduYears.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshEduYears();

        }

        private void listViewProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxEduYears.Enabled = true;

            bNew = false;

            if (listViewEduYears.FocusedItem == null)
                return;

            int k = queueEduYears.Count;
            for (int i = 0; i < k; i++)
            {
                m_EduYears = (_EduYears)queueEduYears.Dequeue();
                if (m_EduYears.id.ToString() == (string)listViewEduYears.Items[listViewEduYears.FocusedItem.Index].Tag)
                {
                    textBoxEduYears.Text = m_EduYears.name;
                    queueEduYears.Enqueue(m_EduYears);
                    break;
                };

                queueEduYears.Enqueue(m_EduYears);
            }

        }


        public void refreshEduYears()
        {
            listViewEduYears.Items.Clear();
            queueEduYears.Clear();


            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `name` FROM `tbl_edu_years` ORDER BY `name`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewEduYears.Items.Clear();
                queueEduYears.Clear();
                _EduYears p = new _EduYears();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim(), 0);
                    p.id = int.Parse(dataRow["id"].ToString());
                    item1.SubItems.Add(dataRow["name"].ToString().Trim());
                    p.name = dataRow["name"].ToString().Trim();
                    listViewEduYears.Items.Add(item1);
                    listViewEduYears.Items[listViewEduYears.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueEduYears.Enqueue(p);
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
            if (listViewEduYears.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `tbl_edu_years` WHERE `id`='{0}'", m_EduYears.id);
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

            textBoxEduYears.Text = "";
            textBoxEduYears.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshEduYears();
        }
    }
}