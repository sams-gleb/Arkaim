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

    public struct _Zhitie
    {
        public _Zhitie(string id, string nazvanie, string cena)
        {
            this.id_val = id;
            this.nazvanie_val = nazvanie;
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
            return nazvanie_val;
        }

        private string id_val;
        private string nazvanie_val;
        private string cena_val;
    }

    public partial class FormZhitie : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueZhitie = new Queue();
        _Zhitie m_zhitie;

        public FormZhitie(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewZhitie.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewZhitie.Columns.Add("Название", -2, HorizontalAlignment.Left);
            listViewZhitie.Columns.Add("Цена", -2, HorizontalAlignment.Left);
            listViewZhitie.Columns[0].Width = 25;
            listViewZhitie.Columns[1].Width = 200;
            listViewZhitie.Columns[2].Width = 200;
        }

        private void FormZhitie_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formZhitie = null;

        }

        private void FormZhitie_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;

            textBoxName.Enabled = false;
            textBoxCena.Enabled = false;
            
            refreshZhitie();

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxName.Text = "";
            textBoxName.Enabled = true;
            textBoxCena.Text = "";
            textBoxCena.Enabled = true;

        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("INSERT INTO `zhitie` (`nazvanie`, `cena`) VALUES ('{0}', '{1}')", textBoxName.Text, textBoxCena.Text);
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
                if (listViewZhitie.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `zhitie` SET `nazvanie`='{0}', `cena`='{1}' WHERE `id`='{2}'", textBoxName.Text, textBoxCena.Text, m_zhitie.id);
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
            textBoxCena.Text = "";
            textBoxCena.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshZhitie();

        }

        private void listViewZhitie_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;
            
            textBoxName.Enabled = true;
            textBoxCena.Enabled = true;

            bNew = false;

            if (listViewZhitie.FocusedItem == null)
                return;

            int k = queueZhitie.Count;
            for (int i = 0; i < k; i++)
            {
                m_zhitie = (_Zhitie)queueZhitie.Dequeue();
                if (m_zhitie.id.ToString() == (string)listViewZhitie.Items[listViewZhitie.FocusedItem.Index].Tag)
                {
                    textBoxName.Text = m_zhitie.nazvanie;
                    textBoxCena.Text = m_zhitie.cena;
                    queueZhitie.Enqueue(m_zhitie);
                    break;
                };

                queueZhitie.Enqueue(m_zhitie);
            }

        }

        public void refreshZhitie()
        {
            listViewZhitie.Items.Clear();
            queueZhitie.Clear();


            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `nazvanie`, `cena` FROM `zhitie` ORDER BY `id`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewZhitie.Items.Clear();
                queueZhitie.Clear();
                _Zhitie t = new _Zhitie();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim(), 0);
                    t.id = dataRow["id"].ToString();
                    item1.SubItems.Add(dataRow["nazvanie"].ToString().Trim());
                    t.nazvanie = dataRow["nazvanie"].ToString().Trim();
                    item1.SubItems.Add(dataRow["cena"].ToString().Trim());
                    t.cena = dataRow["cena"].ToString().Trim();

                    listViewZhitie.Items.Add(item1);
                    listViewZhitie.Items[listViewZhitie.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueZhitie.Enqueue(t);
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
            if (listViewZhitie.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `zhitie` WHERE `id`='{0}'", m_zhitie.id);
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
            textBoxCena.Enabled = false;
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshZhitie();

        }

    }
}