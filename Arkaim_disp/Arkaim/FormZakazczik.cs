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
    public struct _Zakazczik
    {
        public _Zakazczik(string id, string name, string ownership, string contact_person, string phone, string email, string icq, string address, string site, string date, string eksn, string imia, string stoimost, string kol_czel)
        {
            this.id_val = id;
            this.name_val = name;
            this.ownership_val = ownership;
            this.contact_person_val = contact_person;
            this.phone_val = phone;
            this.email_val = email;
            this.icq_val = icq;
            this.address_val = address;
            this.site_val = site;
            this.date_val = date;
            this.eksn_val = eksn;
            this.imia_val = imia;
            this.stoimost_val = stoimost;
            this.kol_czel_val = kol_czel;
   
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
        public string ownership
        {
            get
            {
                return ownership_val;
            }
            set
            {
                ownership_val = value;
            }
        }
        public string contact_person
        {
            get
            {
                return contact_person_val;
            }
            set
            {
                contact_person_val = value;
            }
        }
        public string phone
        {
            get
            {
                return phone_val;
            }
            set
            {
                phone_val = value;
            }
        }
        public string email
        {
            get
            {
                return email_val;
            }
            set
            {
                email_val = value;
            }
        }
        public string icq
        {
            get
            {
                return icq_val;
            }
            set
            {
                icq_val = value;
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
        public string site
        {
            get
            {
                return site_val;
            }
            set
            {
                site_val = value;
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
        public string eksn
        {
            get
            {
                return eksn_val;
            }
            set
            {
                eksn_val = value;
            }
        }
        public string imia
        {
            get
            {
                return imia_val;
            }
            set
            {
                imia_val = value;
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
        public string kol_czel
        {
            get
            {
                return kol_czel_val;
            }
            set
            {
                kol_czel_val = value;
            }
        }
        
        public override string ToString()
        {
            return name_val;
        }

        private string id_val;
        private string name_val;
        private string ownership_val;
        private string contact_person_val;
        private string phone_val;
        private string email_val;
        private string icq_val;
        private string address_val;
        private string site_val;
        private string date_val;
        private string eksn_val;
        private string imia_val;
        private string stoimost_val;
        private string kol_czel_val;
    }

    public partial class FormZakazczik : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueZakazczik = new Queue();
        _Zakazczik m_zakazczik;

        public FormZakazczik(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;
            					
            listViewZakazczik.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("Наименование", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("форма собственности", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("Контактное лицо", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("Телефон", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("e-mail", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("ICQ", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("Адрес", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("Сайт", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns[0].Width = 25;
            listViewZakazczik.Columns[1].Width = 150;
            listViewZakazczik.Columns[2].Width = 150;
            listViewZakazczik.Columns[3].Width = 150;
            listViewZakazczik.Columns[4].Width = 150;
            listViewZakazczik.Columns[5].Width = 150;
            listViewZakazczik.Columns[6].Width = 150;
            listViewZakazczik.Columns[7].Width = 150;
            listViewZakazczik.Columns[8].Width = 150;
        }

        private void FormZakazczik_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formZakazczik = null;
        }


        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;

            
            textBoxName.Text = "";
            textBoxName.Enabled = true;
            textBoxOwnership.Text = "";
            textBoxOwnership.Enabled = true;

            textBoxTel.Text = "";
            textBoxTel.Enabled = true;

            textBoxMail.Text = "";
            textBoxMail.Enabled = true;
            textBoxIcq.Text = "";
            textBoxIcq.Enabled = true;
            textBoxAddress.Text = "";
            textBoxAddress.Enabled = true;
            textBoxContactPerson.Text = "";
            textBoxContactPerson.Enabled = true;
            textBoxSite.Text = "";
            textBoxSite.Enabled = true;

            textBoxZakazczik.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("INSERT INTO `zakazczik` (`name`, `ownership`, `contact_person`, `phone`, `email`, `icq`, `address`, `site`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", textBoxName.Text, textBoxOwnership.Text, textBoxContactPerson.Text, textBoxTel.Text, textBoxMail.Text, textBoxIcq.Text, textBoxAddress.Text, textBoxSite.Text);
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
                if (listViewZakazczik.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `zakazczik` SET `name`='{0}',`ownership`='{1}',`contact_person`='{2}', `phone`='{3}', `email`='{4}', `icq`='{5}', `address`='{6}', `site`='{7}' WHERE `id`='{8}'", textBoxName.Text, textBoxOwnership.Text, textBoxContactPerson.Text, textBoxTel.Text, textBoxMail.Text, textBoxIcq.Text, textBoxAddress.Text, textBoxSite.Text, m_zakazczik.id);
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
            textBoxOwnership.Enabled = false;
            textBoxMail.Enabled = false;
            textBoxIcq.Enabled = false;
            textBoxAddress.Enabled = false;
            textBoxContactPerson.Enabled = false;
            textBoxSite.Enabled = false;
            textBoxTel.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            textBoxZakazczik.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            refreshZakazczik();
        }

        private void listViewZakazczik_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxName.Enabled = true;
            textBoxTel.Enabled = true;        
            textBoxOwnership.Enabled = true;
            textBoxMail.Enabled = true;
            textBoxIcq.Enabled = true;
            textBoxAddress.Enabled = true;
            textBoxContactPerson.Enabled = true;
            textBoxSite.Enabled = true;
            textBoxZakazczik.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;

            bNew = false;

            if (listViewZakazczik.FocusedItem == null)
                return;

            int k = queueZakazczik.Count;
            for (int i = 0; i < k; i++)
            {
                m_zakazczik = (_Zakazczik)queueZakazczik.Dequeue();
                if (m_zakazczik.id == (string)listViewZakazczik.Items[listViewZakazczik.FocusedItem.Index].Tag)
                {
                    textBoxName.Text = m_zakazczik.name;
                    textBoxTel.Text = m_zakazczik.phone;
                    textBoxOwnership.Text = m_zakazczik.ownership;
                    textBoxMail.Text = m_zakazczik.email;
                    textBoxIcq.Text = m_zakazczik.icq;
                    textBoxAddress.Text = m_zakazczik.address;
                    textBoxSite.Text = m_zakazczik.site;
                    textBoxContactPerson.Text = m_zakazczik.contact_person;
                    queueZakazczik.Enqueue(m_zakazczik);
                    break;
                };

                queueZakazczik.Enqueue(m_zakazczik);
            }
        }


        public void refreshZakazczik()
        {
            listViewZakazczik.Items.Clear();
            queueZakazczik.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `name`, `ownership`, `contact_person`, `phone`, `email`, `icq`, `address`, `site` FROM `zakazczik` ORDER BY `name`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewZakazczik.Items.Clear();
                queueZakazczik.Clear();
                _Zakazczik t = new _Zakazczik();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim());
                    t.id = dataRow["id"].ToString().Trim();
                    item1.SubItems.Add(dataRow["name"].ToString().Trim());
                    t.name = dataRow["name"].ToString().Trim();
                    item1.SubItems.Add(dataRow["ownership"].ToString().Trim());
                    t.ownership = dataRow["ownership"].ToString().Trim();
                    item1.SubItems.Add(dataRow["contact_person"].ToString().Trim());
                    t.contact_person = dataRow["contact_person"].ToString().Trim();
                    item1.SubItems.Add(dataRow["phone"].ToString().Trim());
                    t.phone = dataRow["phone"].ToString().Trim();
                    item1.SubItems.Add(dataRow["email"].ToString().Trim());
                    t.email = dataRow["email"].ToString().Trim();
                    item1.SubItems.Add(dataRow["icq"].ToString().Trim());
                    t.icq = dataRow["icq"].ToString().Trim();
                    item1.SubItems.Add(dataRow["address"].ToString().Trim());
                    t.address = dataRow["address"].ToString().Trim();
                    item1.SubItems.Add(dataRow["site"].ToString().Trim());
                    t.site = dataRow["site"].ToString().Trim();

                    listViewZakazczik.Items.Add(item1);
                    listViewZakazczik.Items[listViewZakazczik.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueZakazczik.Enqueue(t);
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
            if (listViewZakazczik.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `zakazczik` WHERE `id`='{0}'", m_zakazczik.id);
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
            textBoxTel.Enabled = false;
            textBoxOwnership.Enabled = false;
            textBoxMail.Enabled = false;
            textBoxIcq.Enabled = false;
            textBoxAddress.Enabled = false;
            textBoxContactPerson.Enabled = false;
            textBoxSite.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshZakazczik();
        }

        private void textBoxZakazczik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonApply.PerformClick();
        }

        private void FormZakazczik_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;

            textBoxName.Enabled = false;
            textBoxTel.Enabled = false;
            textBoxOwnership.Enabled = false;
            textBoxMail.Enabled = false;
            textBoxIcq.Enabled = false;
            textBoxAddress.Enabled = false;
            textBoxContactPerson.Enabled = false;
            textBoxSite.Enabled = false;
            DateTime pickedDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            dateTimePicker1.Value = pickedDate;

            refreshZakazczik();
        }
        //отчеты по заказчикам

        public void refreshReportsZakaz()
        {
            listViewZakazczik.Items.Clear();
            queueZakazczik.Clear();

            listViewZakazczik.Columns.Clear();
            listViewZakazczik.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("№ экскурсии/проживание", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("Заказчик", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("Стоимость", -2, HorizontalAlignment.Left);
            listViewZakazczik.Columns.Add("Кол-во человек", -2, HorizontalAlignment.Left);

            listViewZakazczik.Columns[0].Width = 150;
            listViewZakazczik.Columns[1].Width = 150;
            listViewZakazczik.Columns[2].Width = 150;
            listViewZakazczik.Columns[3].Width = 150;
            listViewZakazczik.Columns[4].Width = 150;



            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = string.Format("select `zhurnal`.`date` AS `date`,`zhurnal`.`N_ekskursii` AS `eksn`,`zhurnal`.`zakazczik` AS `imia`,(`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))AS `stoimost`,((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1) AS `Kol_czel` from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`zakazczik` = '{0}') and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}')) union all select `plategki`.`date` AS `date`,`plategki`.`N_ekskursii` AS `eksn`,`plategki`.`zakazczik` AS `imia`,(`ekskursii`.`stoimost` * `plategki`.`Kol_czel`) AS `stoimost`,`plategki`.`Kol_czel` AS `Kol_czel` from (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`plategki`.`zakazczik` = '{0}') and (`plategki`.`date` >= '{1}') and (`plategki`.`date` <= '{2}')) union all select `rasselenie`.`date` AS `date`,`rasselenie`.`zhitie` AS `eksn`,`rasselenie`.`zakazczik` AS `imia`,(((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`) AS `stoimost`,`rasselenie`.`Kol_czel` AS `Kol_czel` from (`rasselenie` join `zhitie` on((`zhitie`.`nazvanie` = `rasselenie`.`zhitie`))) where ((`rasselenie`.`zakazczik` = '{0}') and (`rasselenie`.`date` >= '{1}') and (`rasselenie`.`date` <= '{2}')) union all select '' AS `date`,'' AS `eksn`,'общая сумма' AS `imia`,(((select ifnull(sum((`ekskursii`.`stoimost` * ((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1))),0) from (`zhurnal` join `ekskursii` on((`zhurnal`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`zhurnal`.`zakazczik` = '{0}')and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}'))) + (select ifnull(sum((`ekskursii`.`stoimost` * `plategki`.`Kol_czel`)),0) from (`plategki` join `ekskursii` on((`plategki`.`N_ekskursii` = `ekskursii`.`N_ekskursii`))) where ((`plategki`.`zakazczik` = '{0}') and (`plategki`.`date` >= '{1}') and (`plategki`.`date` <= '{2}')))) + (select ifnull(sum((((`rasselenie`.`Kol_czel` * `zhitie`.`cena`) * `rasselenie`.`Kol_dney`) + `rasselenie`.`parkovka`)),0) from (`rasselenie` join `zhitie` on((`rasselenie`.`zhitie` = `zhitie`.`nazvanie`))) where ((`rasselenie`.`zakazczik` = '{0}')and (`rasselenie`.`date` >= '{1}') and (`rasselenie`.`date` <= '{2}')))) AS `stoimost`,(((select ifnull(sum(((`zhurnal`.`N_kvit_koniec` - `zhurnal`.`N_kvit_nach`) + 1)),0) from `zhurnal` where ((`zhurnal`.`zakazczik` = '{0}')and (`zhurnal`.`date` >= '{1}') and (`zhurnal`.`date` <= '{2}')))+ (select ifnull(sum(`plategki`.`Kol_czel`),0) from `plategki` where ((`plategki`.`zakazczik` = '{0}')and (`plategki`.`date` >= '{1}') and (`plategki`.`date` <= '{2}')))) + (select ifnull(sum(`rasselenie`.`Kol_czel`),0) from `rasselenie` where ((`rasselenie`.`zakazczik` = '{0}')and (`rasselenie`.`date` >= '{1}') and (`rasselenie`.`date` <= '{2}')))) AS `Kol_czel`", textBoxZakazczik.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, DateTime.Parse(dateTimePicker2.Text).Year + "-" + DateTime.Parse(dateTimePicker2.Text).Month + "-" + DateTime.Parse(dateTimePicker2.Text).Day);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewZakazczik.Items.Clear();
                queueZakazczik.Clear();
                _Zakazczik v = new _Zakazczik();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["date"].ToString().Trim());
                    v.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["eksn"].ToString().Trim());
                    v.eksn = dataRow["eksn"].ToString().Trim();
                    item1.SubItems.Add(dataRow["imia"].ToString().Trim());
                    v.imia = dataRow["imia"].ToString().Trim();
                    item1.SubItems.Add(dataRow["stoimost"].ToString().Trim());
                    v.stoimost= dataRow["stoimost"].ToString().Trim();
                    item1.SubItems.Add(dataRow["Kol_czel"].ToString().Trim());
                    v.kol_czel= dataRow["Kol_czel"].ToString().Trim();

                    listViewZakazczik.Items.Add(item1);
                    listViewZakazczik.Items[listViewZakazczik.Items.Count - 1].Tag = dataRow["date"].ToString();
                    queueZakazczik.Enqueue(v);
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
            refreshReportsZakaz();

        }

    
    }
}

