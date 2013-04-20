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
    public struct _Semester
    {
        public _Semester(int id, string name)
        {
            this.id_val = id;
            this.name_val = name;
        }

        public int id
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

        public string name
        {
            get
            {
                return this.name_val;
            }
            set
            {
                this.name_val = value;
            }
        }
        
        public override string ToString()
        {
            return name_val;
        }

        private int id_val;
        private string name_val;
    }

    public struct _Pdv
    {
        public _Pdv(int id, string name)
        {
            this.id_val = id;
            this.name_val = name;
        }

        public int id
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

        public string name
        {
            get
            {
                return this.name_val;
            }
            set
            {
                this.name_val = value;
            }
        }

        public override string ToString()
        {
            return name_val;
        }

        private int id_val;
        private string name_val;
    }

    public struct _Journal
    {
        public _Journal(int id, double sum, string student, string group, int payment_name_id, string date, int account_id, int edu_year_id, int hostel_id, int semester, int pdv)
        {
            this.id_val = id;
            this.semester_val = semester;
            this.sum_val = sum;
            this.student_val = student;
            this.group_val = group;
            this.payment_name_id_val = payment_name_id;
            this.account_id_val = account_id;
            this.date_val = date;
            this.edu_year_id_val = edu_year_id;
            this.hostel_id_val = hostel_id;
            this.pdv_val = pdv;
        }

        public int id
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
        public int pdv
        {
            get
            {
                return this.pdv_val;
            }
            set
            {
                this.pdv_val = value;
            }
        }
        public int semester
        {
            get
            {
                return this.semester_val;
            }
            set
            {
                this.semester_val = value;
            }
        }

        public double sum
        {
            get
            {
                return this.sum_val;
            }
            set
            {
                this.sum_val = value;
            }
        }

        public string student
        {
            get
            {
                return this.student_val;
            }
            set
            {
                this.student_val = value;
            }
        }

        public string group
        {
            get
            {
                return this.group_val;
            }
            set
            {
                this.group_val = value;
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

        public int payment_name_id
        {
            get
            {
                return this.payment_name_id_val;
            }
            set
            {
                this.payment_name_id_val = value;
            }
        }

        public int account_id
        {
            get
            {
                return this.account_id_val;
            }
            set
            {
                this.account_id_val = value;
            }
        }

        public int hostel_id
        {
            get
            {
                return this.hostel_id_val;
            }
            set
            {
                this.hostel_id_val = value;
            }
        }

        

        public int edu_year_id
        {
            get
            {
                return this.edu_year_id_val;
            }
            set
            {
                this.edu_year_id_val = value;
            }
        }

        public override string ToString()
        {
            return student_val;
        }

        private int id_val;
        private int semester_val;
        private double sum_val;
        private string group_val;
        private string student_val;
        private int payment_name_id_val;
        private int account_id_val;
        private int hostel_id_val;
        private string date_val;
        private int edu_year_id_val;
        private int pdv_val;
    }

    public partial class FormJournal : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueJournal = new Queue();
        _Journal m_journal;
        _PaymentNames m_payment_name;
        Queue queuePaymentNames = new Queue();
        _Ekskursovody m_account;
        Queue queueAccounts = new Queue();
       // _Reports m_edu_year;
     //   Queue queueEduYears = new Queue();
        _Hostel m_hostel;
        Queue queueHostel = new Queue();
        _Semester m_semester;
        Queue queueSemesters = new Queue();
        _Pdv m_pdv;
        Queue queuePdv = new Queue();
        

        public FormJournal(FormMain mainWin)
        {
            InitializeComponent();

            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewJournal.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewJournal.Columns.Add("Дата", -2, HorizontalAlignment.Left);
            listViewJournal.Columns.Add("Плательщик", -2, HorizontalAlignment.Left);
            listViewJournal.Columns.Add("Группа", -2, HorizontalAlignment.Left);
            listViewJournal.Columns.Add("Цена", -2, HorizontalAlignment.Left);
            listViewJournal.Columns.Add("Наименование платежа", -2, HorizontalAlignment.Left);
            
            listViewJournal.Columns[0].Width = 25;
            listViewJournal.Columns[1].Width = 100;
            listViewJournal.Columns[2].Width = 100;
            listViewJournal.Columns[3].Width = 100;
            listViewJournal.Columns[4].Width = 100;
            listViewJournal.Columns[5].Width = 200;
            
        }

        private void FormMarks_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;
            buttonExcel.Enabled = false;

            textBoxStudent.Enabled = false;
            textBoxGroup.Enabled = false;
            textBoxSum.Enabled = false;
            textBoxSum.Text = "0.00";
            comboBoxPaymentNames.SelectedIndex = -1;
            comboBoxPaymentNames.Enabled = false;
            dateTimePickerDate.Enabled = false;
            comboBoxAccounts.SelectedIndex = -1;
            comboBoxAccounts.Enabled = false;
            comboBoxEduYear.SelectedIndex = -1;
            comboBoxEduYear.Enabled = false;
            comboBoxHostel.SelectedIndex = -1;
            comboBoxHostel.Enabled = false;
            comboBoxSemester.SelectedIndex = -1;
            comboBoxSemester.Enabled = false;
            comboBoxPdv.SelectedIndex = -1;
            comboBoxPdv.Enabled = false;
            
            refreshPaymentNames();
            refreshAccounts();
            refreshHostels();
            refreshEduYears();
            refreshJournal();
            refreshSemesters();
            refreshPdv();
        }

        private void FormMarks_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formZhurnal = null;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;

            buttonApply.Enabled = true;
            buttonExcel.Enabled = true;
            buttonDelete.Enabled = false;

            textBoxStudent.Text = "";
            textBoxStudent.Enabled = true;
            textBoxGroup.Text = "";
            textBoxGroup.Enabled = true;
            textBoxSum.Text = "0.00";
            textBoxSum.Enabled = true;
            comboBoxPaymentNames.SelectedIndex = -1;
            comboBoxPaymentNames.Enabled = true;
            dateTimePickerDate.Enabled = true;
            dateTimePickerDate.Value = DateTime.Today;
            comboBoxAccounts.SelectedIndex = -1;
            comboBoxAccounts.Enabled = true;
            comboBoxEduYear.SelectedIndex = -1;
            comboBoxEduYear.Enabled = true;
            comboBoxHostel.SelectedIndex = -1;
            comboBoxHostel.Enabled = true;
            comboBoxSemester.SelectedIndex = -1;
            comboBoxSemester.Enabled = true;
            comboBoxPdv.SelectedIndex = -1;
            comboBoxPdv.Enabled = true;


        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (comboBoxPaymentNames.SelectedIndex == -1)
                return;
            if (comboBoxAccounts.SelectedIndex == -1)
                return;
            if (comboBoxEduYear.SelectedIndex == -1)
                return;
            if (comboBoxSemester.SelectedIndex == -1)
                return;
            if (bNew == true)
            {
                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("INSERT INTO `tbl_journal` (`student`, `group`, `sum`, `payment_name_id`, `date`, `account_id`, `semester`, `edu_years_id`, `hostel_id`, `pdv`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')", textBoxStudent.Text, textBoxGroup.Text, textBoxSum.Text.Replace(",", "."), m_payment_name.id.ToString(), DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, m_account.id.ToString(), m_semester.id.ToString(), m_edu_year.id.ToString(), m_hostel.id.ToString(), m_pdv.id.ToString());
                    if(m_hostel.id==0)
                        sql = String.Format("INSERT INTO `tbl_journal` (`student`, `group`, `sum`, `payment_name_id`, `date`, `account_id`, `semester`, `edu_years_id`, `hostel_id`, `pdv`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', NULL, '{8}')", textBoxStudent.Text, textBoxGroup.Text, textBoxSum.Text.Replace(",", "."), m_payment_name.id.ToString(), DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, m_semester.id.ToString(), m_edu_year.id.ToString(), m_pdv.id.ToString());
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
                if (listViewJournal.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `tbl_journal` SET `student`='{0}', `group`='{1}', `payment_name_id`='{2}', `date`='{3}', `account_id`='{4}', `edu_years_id`='{5}', `hostel_id`='{6}', `sum`='{7}', `semester`='{8}', `pdv`='{9}'  WHERE `id`='{10}' ", textBoxStudent.Text, textBoxGroup.Text, m_payment_name.id.ToString(), DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, m_account.id.ToString(), m_edu_year.id.ToString(), m_hostel.id.ToString(), textBoxSum.Text.Replace(",", "."), m_semester.id.ToString(), m_pdv.id.ToString(), m_journal.id);
                    if(m_hostel.id==0)
                        sql = String.Format("UPDATE `tbl_journal` SET `student`='{0}', `group`='{1}', `payment_name_id`='{2}', `date`='{3}', `account_id`='{4}', `edu_years_id`='{5}', `hostel_id`=NULL, `sum`='{6}', `semester`='{7}', `pdv`='{8}' WHERE `id`='{9}' ", textBoxStudent.Text, textBoxGroup.Text, m_payment_name.id.ToString(), DateTime.Parse(dateTimePickerDate.Text).Year + "." + DateTime.Parse(dateTimePickerDate.Text).Month + "." + DateTime.Parse(dateTimePickerDate.Text).Day, m_account.id.ToString(), m_edu_year.id.ToString(), textBoxSum.Text.Replace(",", "."), m_semester.id.ToString(), m_pdv.id.ToString(), m_journal.id);
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

            textBoxStudent.Text = "";
            textBoxStudent.Enabled = false;
            textBoxGroup.Text = "";
            textBoxGroup.Enabled = false;
            textBoxSum.Text = "0.00";
            textBoxSum.Enabled = false;
            comboBoxPaymentNames.SelectedIndex = -1;
            comboBoxPaymentNames.Enabled = false;
            dateTimePickerDate.Enabled = false;
            dateTimePickerDate.Value = DateTime.Today;
            comboBoxAccounts.SelectedIndex = -1;
            comboBoxAccounts.Enabled = false;
            comboBoxEduYear.SelectedIndex = -1;
            comboBoxEduYear.Enabled = false;
            comboBoxHostel.SelectedIndex = -1;
            comboBoxHostel.Enabled = false;
            comboBoxSemester.SelectedIndex = -1;
            comboBoxSemester.Enabled = false;
            comboBoxPdv.SelectedIndex = -1;
            comboBoxPdv.Enabled = false;

            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            buttonExcel.Enabled = false;
            refreshJournal();
        }

        private void listViewMarks_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonExcel.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxStudent.Enabled = true;
            textBoxGroup.Enabled = true;
            textBoxSum.Enabled = true;
            comboBoxPaymentNames.Enabled = true;
            comboBoxAccounts.Enabled = true;
            comboBoxEduYear.Enabled = true;
            comboBoxHostel.Enabled = true;
            comboBoxSemester.Enabled = true;
            comboBoxPdv.Enabled = true;

            bNew = false;

            if (listViewJournal.FocusedItem == null)
                return;

            int k = queueJournal.Count;
            for (int i = 0; i < k; i++)
            {
                m_journal = (_Journal)queueJournal.Dequeue();
                if (m_journal.id.ToString() == (string)listViewJournal.Items[listViewJournal.FocusedItem.Index].Tag)
                {
                    textBoxStudent.Text = m_journal.student;
                    //MessageBox.Show(m_journal.student);
                    textBoxGroup.Text = m_journal.group;
                    textBoxSum.Text = m_journal.sum.ToString();

                    _PaymentNames c;
                    int k2 = queuePaymentNames.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        c = (_PaymentNames)queuePaymentNames.Dequeue();
                        if (c.id.ToString() == m_journal.payment_name_id.ToString())
                        {

                            for (int iii = 0; iii < comboBoxPaymentNames.Items.Count; iii++)
                            {
                                if (((_PaymentNames)comboBoxPaymentNames.Items[iii]).id == c.id)
                                {
                                    comboBoxPaymentNames.SelectedIndex = iii;
                                    m_payment_name = c;
                                   break;
                                }
                            }
                        };
                        queuePaymentNames.Enqueue(c);
                    }

                    dateTimePickerDate.Value = DateTime.Parse(m_journal.date);
                    dateTimePickerDate.Enabled = true;

                    _Ekskursovody a;
                    k2 = queueAccounts.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        a = (_Ekskursovody)queueAccounts.Dequeue();
                        if (a.id.ToString() == m_journal.account_id.ToString())
                        {

                            for (int iii = 0; iii < comboBoxAccounts.Items.Count; iii++)
                            {
                                if (((_Ekskursovody)comboBoxAccounts.Items[iii]).id == a.id)
                                {
                                    comboBoxAccounts.SelectedIndex = iii;
                                    m_account = a;
                                    break;
                                }
                            }
                        };
                        queueAccounts.Enqueue(a);
                    }

                    _Semester s;
                    k2 = queueSemesters.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        s = (_Semester)queueSemesters.Dequeue();
                        if (s.id.ToString() == m_journal.semester.ToString())
                        {

                            for (int iii = 0; iii < comboBoxSemester.Items.Count; iii++)
                            {
                                if (((_Semester)comboBoxSemester.Items[iii]).id == s.id)
                                {
                                    comboBoxSemester.SelectedIndex = iii;
                                    m_semester = s;
                                    break;
                                }
                            }
                        };
                        queueSemesters.Enqueue(s);
                    }

                    _Pdv p;
                    k2 = queuePdv.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        p = (_Pdv)queuePdv.Dequeue();
                        if (p.id.ToString() == m_journal.pdv.ToString())
                        {

                            for (int iii = 0; iii < comboBoxPdv.Items.Count; iii++)
                            {
                                if (((_Pdv)comboBoxPdv.Items[iii]).id == p.id)
                                {
                                    comboBoxPdv.SelectedIndex = iii;
                                    m_pdv = p;
                                    break;
                                }
                            }
                        };
                        queuePdv.Enqueue(p);
                    }
                    

                    _Hostel h;
                    k2 = queueHostel.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        h = (_Hostel)queueHostel.Dequeue();
                        if (h.id.ToString() == m_journal.hostel_id.ToString())
                        {

                            for (int iii = 0; iii < comboBoxHostel.Items.Count; iii++)
                            {
                                if (((_Hostel)comboBoxHostel.Items[iii]).id == h.id)
                                {
                                    comboBoxHostel.SelectedIndex = iii;
                                    m_hostel = h;
                                    break;
                                }
                            }
                        };
                        queueHostel.Enqueue(h);
                    }

                    _EduYears ey;
                    k2 = queueEduYears.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        ey = (_EduYears)queueEduYears.Dequeue();
                        if (ey.id.ToString() == m_journal.edu_year_id.ToString())
                        {

                            for (int iii = 0; iii < comboBoxEduYear.Items.Count; iii++)
                            {
                                if (((_EduYears)comboBoxEduYear.Items[iii]).id == ey.id)
                                {
                                    comboBoxEduYear.SelectedIndex = iii;
                                    m_edu_year = ey;
                                    break;
                                }
                            }
                        };
                        queueEduYears.Enqueue(ey);
                    }
                    
                    queueJournal.Enqueue(m_journal);
                    break;
                };

                queueJournal.Enqueue(m_journal);
            }

        }

        public void refreshJournal()
        {
            listViewJournal.Items.Clear();
            queueJournal.Clear();


            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `payment_name_id`, `account_id`, `sum`, `date`, `student`, `group`, `edu_years_id`, `hostel_id`, semester, pdv FROM `tbl_journal`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                _Journal m = new _Journal();

                int i = 1;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(i.ToString(), 0);
                    m.id = int.Parse(dataRow["id"].ToString());
                    m.semester = int.Parse(dataRow["semester"].ToString());
                    m.sum = double.Parse(dataRow["sum"].ToString().Replace(".", ","));
                    m.date = dataRow["date"].ToString();
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    m.student = dataRow["student"].ToString();
                    
                    item1.SubItems.Add(dataRow["student"].ToString().Trim());
                    m.group = dataRow["group"].ToString();
                    item1.SubItems.Add(dataRow["group"].ToString().Trim());
                    item1.SubItems.Add(m.sum.ToString());

                    m.pdv = int.Parse(dataRow["pdv"].ToString());

                    m.payment_name_id = int.Parse(dataRow["payment_name_id"].ToString().Trim());
                    m.account_id = int.Parse(dataRow["account_id"].ToString().Trim());
                    m.edu_year_id = int.Parse(dataRow["edu_years_id"].ToString().Trim());
                    m.hostel_id = 0;
                    if (dataRow["hostel_id"].ToString().Trim().Length>0)
                        m.hostel_id = int.Parse(dataRow["hostel_id"].ToString().Trim());

                    _PaymentNames c;
                    int k2 = queuePaymentNames.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        c = (_PaymentNames)queuePaymentNames.Dequeue();
                        if (c.id == m.payment_name_id)
                        {
                            item1.SubItems.Add(c.name);
                            queuePaymentNames.Enqueue(c);
                            break;
                        };
                        queuePaymentNames.Enqueue(c);
                    }

                    _Semester s;
                    k2 = queueSemesters.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        s = (_Semester)queueSemesters.Dequeue();
                        if (s.id == m.semester)
                        {
                            item1.SubItems.Add(s.name);
                            queueSemesters.Enqueue(s);
                            break;
                        };
                        queueSemesters.Enqueue(s);
                    }
                    _Pdv p;
                    k2 = queuePdv.Count;
                    for (int i2 = 0; i2 < k2; i2++)
                    {
                        p = (_Pdv)queuePdv.Dequeue();
                        if (p.id == m.pdv)
                        {
                            item1.SubItems.Add(p.name);
                            queuePdv.Enqueue(p);
                            break;
                        };
                        queuePdv.Enqueue(p);
                    }


                    listViewJournal.Items.Add(item1);
                    listViewJournal.Items[listViewJournal.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueJournal.Enqueue(m);
                    ++i;
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
            if (listViewJournal.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `tbl_journal` WHERE `id`='{0}'", m_journal.id);
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

            textBoxStudent.Text = "";
            textBoxStudent.Enabled = false;
            textBoxGroup.Text = "";
            textBoxGroup.Enabled = false;
            textBoxSum.Text = "0.00";
            textBoxSum.Enabled = false;
            comboBoxPaymentNames.SelectedIndex = -1;
            comboBoxPaymentNames.Enabled = false;
            comboBoxAccounts.SelectedIndex = -1;
            comboBoxAccounts.Enabled = false;
            comboBoxEduYear.SelectedIndex = -1;
            comboBoxEduYear.Enabled = false;
            dateTimePickerDate.Enabled = false;
            dateTimePickerDate.Value = DateTime.Today;
            comboBoxHostel.SelectedIndex = -1;
            comboBoxHostel.Enabled = false;
            comboBoxSemester.SelectedIndex = -1;
            comboBoxSemester.Enabled = false;
            comboBoxPdv.SelectedIndex = -1;
            comboBoxPdv.Enabled = false;


            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            buttonExcel.Enabled = false;
            refreshJournal();
        }

        public void refreshPaymentNames()
        {
            string payment_name_id = "";
            if (comboBoxPaymentNames.SelectedIndex != -1)
            {
                payment_name_id = m_payment_name.id.ToString();
            }

            comboBoxPaymentNames.Items.Clear();
            queuePaymentNames.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `name` FROM `tbl_payment_names` ORDER BY `name`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                _PaymentNames c = new _PaymentNames();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    c.id = int.Parse(dataRow["id"].ToString());
                    c.name = dataRow["name"].ToString().Trim();

                    comboBoxPaymentNames.Items.Add(c);


                    if (payment_name_id != "" && payment_name_id == c.id.ToString())
                    {
                        comboBoxPaymentNames.SelectedItem = c;
                    }


                    queuePaymentNames.Enqueue(c);
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

        public void refreshAccounts()
        {
            string account_id = "";
            if (comboBoxAccounts.SelectedIndex != -1)
            {
                account_id = m_account.id.ToString();
            }

            comboBoxAccounts.Items.Clear();
            queueAccounts.Clear();

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `name` FROM `tbl_accounts` ORDER BY `name`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                _Ekskursovody c = new _Ekskursovody();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    c.id = dataRow["id"].ToString();
                    c.name = dataRow["name"].ToString().Trim();

                    comboBoxAccounts.Items.Add(c);


                    if (account_id != "" && account_id == c.id.ToString())
                    {
                        comboBoxAccounts.SelectedItem = c;
                    }


                    queueAccounts.Enqueue(c);
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

        public void refreshEduYears()
        {
            string edu_years_id = "";
            if (comboBoxEduYear.SelectedIndex != -1)
            {
                edu_years_id = m_edu_year.id.ToString();
            }

            comboBoxEduYear.Items.Clear();
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

                _EduYears c = new _EduYears();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    c.id = int.Parse(dataRow["id"].ToString());
                    c.name = dataRow["name"].ToString().Trim();

                    comboBoxEduYear.Items.Add(c);


                    if (edu_years_id != "" && edu_years_id == c.id.ToString())
                    {
                        comboBoxEduYear.SelectedItem = c;
                    }


                    queueEduYears.Enqueue(c);
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

        public void refreshPdv()
        {
            comboBoxPdv.Items.Clear();
            queuePdv.Clear();

            _Pdv p1 = new _Pdv();
            p1.id = 1;
            p1.name = "ПДВ";
            comboBoxPdv.Items.Add(p1);
            queuePdv.Enqueue(p1);

            _Pdv p2 = new _Pdv();
            p2.id = 2;
            p2.name = "Без ПДВ";
            comboBoxPdv.Items.Add(p2);
            queuePdv.Enqueue(p2);
        }

        public void refreshSemesters()
        {
            comboBoxSemester.Items.Clear();
            queueSemesters.Clear();

            _Semester s1 = new _Semester();
            s1.id = 1;
            s1.name = "за І семестр";
            comboBoxSemester.Items.Add(s1);
            queueSemesters.Enqueue(s1);

            _Semester s2 = new _Semester();
            s2.id = 2;
            s2.name = "за ІІ семестр";
            comboBoxSemester.Items.Add(s2);
            queueSemesters.Enqueue(s2);
            
            _Semester s3 = new _Semester();
            s3.id = 3;
            s3.name = "за ІІI семестр";
            comboBoxSemester.Items.Add(s3);
            queueSemesters.Enqueue(s3);

            _Semester s4 = new _Semester();
            s4.id = 4;
            s4.name = "за ІV семестр";
            comboBoxSemester.Items.Add(s4);
            queueSemesters.Enqueue(s4);

            _Semester s5 = new _Semester();
            s5.id = 5;
            s5.name = "за V семестр";
            comboBoxSemester.Items.Add(s5);
            queueSemesters.Enqueue(s5);

            _Semester s6 = new _Semester();
            s6.id = 6;
            s6.name = "за VІ семестр";
            comboBoxSemester.Items.Add(s6);
            queueSemesters.Enqueue(s6);

            _Semester s7 = new _Semester();
            s7.id = 7;
            s7.name = "за VІІ семестр";
            comboBoxSemester.Items.Add(s7);
            queueSemesters.Enqueue(s7);

            _Semester s8 = new _Semester();
            s8.id = 8;
            s8.name = "за VIІІ семестр";
            comboBoxSemester.Items.Add(s8);
            queueSemesters.Enqueue(s8);

            _Semester s9 = new _Semester();
            s9.id = 9;
            s9.name = "за ІX семестр";
            comboBoxSemester.Items.Add(s9);
            queueSemesters.Enqueue(s9);

            _Semester s10 = new _Semester();
            s10.id = 10;
            s10.name = "за X семестр";
            comboBoxSemester.Items.Add(s10);
            queueSemesters.Enqueue(s10);
        }

        public void refreshHostels()
        {
            string hostel_id = "";
            if (comboBoxHostel.SelectedIndex != -1)
            {
                hostel_id = m_hostel.id.ToString();
            }

            comboBoxHostel.Items.Clear();
            queueHostel.Clear();

            _Hostel s3 = new _Hostel();
            s3.id = 0;
            s3.name = "";
            comboBoxHostel.Items.Add(s3);
            queueHostel.Enqueue(s3);

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

                _Hostel c = new _Hostel();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    c.id = int.Parse(dataRow["id"].ToString());
                    c.name = dataRow["name"].ToString().Trim();

                    comboBoxHostel.Items.Add(c);


                    if (hostel_id != "" && hostel_id == c.id.ToString())
                    {
                        comboBoxHostel.SelectedItem = c;
                    }


                    queueHostel.Enqueue(c);
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

        private void comboBoxCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPaymentNames.SelectedIndex == -1)
                return;
            m_payment_name = (_PaymentNames)comboBoxPaymentNames.SelectedItem;
        }

        private void comboBoxAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAccounts.SelectedIndex == -1)
                return;
            m_account = (_Ekskursovody)comboBoxAccounts.SelectedItem;
        }

        private void comboBoxEduYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEduYear.SelectedIndex == -1)
                return;
            m_edu_year = (_EduYears)comboBoxEduYear.SelectedItem;
        }

        private void comboBoxHostells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHostel.SelectedIndex == -1)
                return;
            m_hostel = (_Hostel)comboBoxHostel.SelectedItem;

        }

        private void comboBoxSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSemester.SelectedIndex == -1)
                return;
            m_semester = (_Semester)comboBoxSemester.SelectedItem;

        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();  // Creates a new Excel Application
            excelApp.Visible = true;  // Makes Excel visible to the user.

            // The following line adds a new workbook
            Microsoft.Office.Interop.Excel.Workbook newWorkbook = excelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

            // The following code opens an existing workbook
            string workbookPath = Application.StartupPath + "\\template.xls";  // Add your own path here
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath, 0,
                false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true,
                false, 0, true, false, false);

            // The following gets the Worksheets collection
            Microsoft.Office.Interop.Excel.Sheets excelSheets = excelWorkbook.Worksheets;

            // The following gets Sheet1 for editing
            string currentSheet = "Бланк";
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);

            // The following gets cell A1 for editing
            //Microsoft.Office.Interop.Excel.Range excelCell = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("A1", "A1");
            // The following sets cell A1's value to "Hi There"
            //excelCell.Value2 = "Hi There";

            _Ekskursovody a;
            int n = queueAccounts.Count;
            for (int i = 0; i < n; i++)
            {
                a = (_Ekskursovody)queueAccounts.Dequeue();
                queueAccounts.Enqueue(a);
                if (a.id == m_journal.account_id.ToString())
                {
                    Microsoft.Office.Interop.Excel.Range excelCellAccount = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("C8", "C8");
                    excelCellAccount.Value2 = a.name;
                    break;
                }
            }

            Microsoft.Office.Interop.Excel.Range excelCellID = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("D11", "D11");
            excelCellID.Value2 = m_journal.id.ToString();

            Microsoft.Office.Interop.Excel.Range excelCellStudent = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("E14", "F14");
            excelCellStudent.Value2 = m_journal.student;

            _PaymentNames pn;
            n = queuePaymentNames.Count;
            for (int i = 0; i < n; i++)
            {
                pn = (_PaymentNames)queuePaymentNames.Dequeue();
                queuePaymentNames.Enqueue(pn);
                if (pn.id == m_journal.payment_name_id)
                {
                    Microsoft.Office.Interop.Excel.Range excelCellPaymentName = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("B19", "C19");
                    excelCellPaymentName.Value2 = pn.name;
                    break;
                }
            }

            _Semester s;
            n = queueSemesters.Count;
            for (int i = 0; i < n; i++)
            {
                s = (_Semester)queueSemesters.Dequeue();
                queueSemesters.Enqueue(s);
                if (s.id == m_journal.semester)
                {
                    Microsoft.Office.Interop.Excel.Range excelCellSemester = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("B20", "C20");
                    excelCellSemester.Value2 = s.name;
                    break;
                };
            }

            _Pdv p;
            n = queuePdv.Count;
            for (int i = 0; i < n; i++)
            {
                p = (_Pdv)queuePdv.Dequeue();
                queuePdv.Enqueue(p);
                if (p.id == m_journal.pdv)
                {
                    Microsoft.Office.Interop.Excel.Range excelCellPdv = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("E22", "E22");
                    excelCellPdv.Value2 = p.name;
                    break;
                };
            }

            _EduYears ey;
            n = queueEduYears.Count;
            for (int i = 0; i < n; i++)
            {
                ey = (_EduYears)queueEduYears.Dequeue();
                queueEduYears.Enqueue(ey);
                if (ey.id.ToString() == m_journal.edu_year_id.ToString())
                {
                    Microsoft.Office.Interop.Excel.Range excelCellSemester = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("B21", "C21");
                    excelCellSemester.Value2 = ey.name;
                    break;
                };
            }
            
            Microsoft.Office.Interop.Excel.Range excelCellSum = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("F21", "F21");
            excelCellSum.Value2 = m_journal.sum;

            String date = "";
            if(DateTime.Parse(m_journal.date).Day<10)
                date += "0";
            date += DateTime.Parse(m_journal.date).Day;
            date += "/";
            if(DateTime.Parse(m_journal.date).Month<10)
                date += "0";
            date += DateTime.Parse(m_journal.date).Month;
            date += "/";
            date += DateTime.Parse(m_journal.date).Year;

            Microsoft.Office.Interop.Excel.Range excelCellDate = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("F11", "F11");
            excelCellDate.Value2 = date;
        }

        private void comboBoxPdv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPdv.SelectedIndex == -1)
                return;
            m_pdv = (_Pdv)comboBoxPdv.SelectedItem;
        }

    }
}