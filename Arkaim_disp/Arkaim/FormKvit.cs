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
    public struct _Kvit
    {
        public _Kvit(string id, string date, string KvN, string KvK, string kol)
        {
            this.id_val = id;
            this.date_val = date;
            this.KvN_val = KvN;
            this.KvK_val = KvK;
            this.kol_val = kol;
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
        
        public override string ToString()
        {
            return date_val;
        }

        private string id_val;
        private string date_val;
        private string KvN_val;
        private string KvK_val;
        private string kol_val;
    }

    public partial class FormKvit : Form
    {
        private FormMain mainWin;
        private bool bNew = false;
        Queue queueKvit = new Queue();
        _Kvit m_kvit;
        
        public FormKvit (FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            this.MdiParent = mainWin;
            this.WindowState = FormWindowState.Maximized;

            listViewKvit.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Дата поступления", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Количество", -2, HorizontalAlignment.Left);

            listViewKvit.Columns[0].Width = 25;
            listViewKvit.Columns[1].Width = 150;
            listViewKvit.Columns[2].Width = 300;
            listViewKvit.Columns[3].Width = 150;
            listViewKvit.Columns[4].Width = 150;
 
        }

        private void FormKvit_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formKvit = null;
        }
        
        private void buttonNew_Click(object sender, EventArgs e)
        {
            bNew = true;
            //knopki
            buttonApply.Enabled = true;
            buttonDelete.Enabled = false;
            
            textBoxKvN.Text = "";
            textBoxKvN.Enabled = true;
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

                    string sql = String.Format("INSERT INTO `kvit` (`N_kvit_nach`, `N_kvit_koniec`, `date`) VALUES ('{0}', '{1}', '{2}')", textBoxKvN.Text, textBoxKvK.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day);
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
                if (listViewKvit.FocusedItem == null)
                    return;

                try
                {
                    mainWin.m_dbConnector.Lock();
                    MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                    string sql = String.Format("UPDATE `kvit` SET `N_kvit_nach`='{0}',`N_kvit_koniec`='{1}', `date`='{2}' WHERE `id`='{3}'", textBoxKvN.Text, textBoxKvK.Text, DateTime.Parse(dateTimePicker1.Text).Year + "-" + DateTime.Parse(dateTimePicker1.Text).Month + "-" + DateTime.Parse(dateTimePicker1.Text).Day, m_kvit.id);
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
            textBoxKvK.Enabled = false;
            dateTimePicker1.Enabled = false;
           
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshKvit();
        }

        private void listViewKvit_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
            buttonDelete.Enabled = true;

            textBoxKvN.Enabled = true;

            textBoxKvK.Enabled = true;
            dateTimePicker1.Enabled = true;
                        
            bNew = false;
                       

            if (listViewKvit.FocusedItem == null)
                return;

            int k = queueKvit.Count;
            for (int i = 0; i < k; i++)
            {
                m_kvit = (_Kvit)queueKvit.Dequeue();
                
                if (m_kvit.id == (string)listViewKvit.Items[listViewKvit.FocusedItem.Index].Tag)
                {
                    textBoxKvN.Text = m_kvit.KvN;
                    textBoxKvK.Text = m_kvit.KvK;
                    dateTimePicker1.Value = DateTime.Parse(m_kvit.date);
                    queueKvit.Enqueue(m_kvit);
                    break;
                };

                queueKvit.Enqueue(m_kvit);
            }
        }


        public void refreshKvit()
        {
            listViewKvit.Items.Clear();
            queueKvit.Clear();

            listViewKvit.Columns.Clear();
            listViewKvit.Columns.Add("№", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Дата поступления", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Количество", -2, HorizontalAlignment.Left);
            
            listViewKvit.Columns[0].Width = 25;
            listViewKvit.Columns[1].Width = 150;
            listViewKvit.Columns[2].Width = 300;
            listViewKvit.Columns[3].Width = 150;
            listViewKvit.Columns[4].Width = 150;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "SELECT `id`, `N_kvit_nach`, `N_kvit_koniec`, ((`N_kvit_koniec` - `N_kvit_nach`) + 1) AS `kol_vo`, cast(`date` as char) AS `date` FROM `kvit` ORDER BY `id`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewKvit.Items.Clear();
                queueKvit.Clear();
                _Kvit t = new _Kvit();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ListViewItem item1 = new ListViewItem(dataRow["id"].ToString().Trim());
                    t.id = dataRow["id"].ToString().Trim();
                    item1.SubItems.Add(dataRow["date"].ToString().Trim());
                    t.date = dataRow["date"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_nach"].ToString().Trim());
                    t.KvN = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.KvK = dataRow["N_kvit_koniec"].ToString().Trim();
                    item1.SubItems.Add(dataRow["kol_vo"].ToString().Trim());
                    t.kol = dataRow["kol_vo"].ToString().Trim();
  
                    listViewKvit.Items.Add(item1);
                    listViewKvit.Items[listViewKvit.Items.Count - 1].Tag = dataRow["id"].ToString();
                    queueKvit.Enqueue(t);
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
            if (listViewKvit.FocusedItem == null)
                return;

            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = String.Format("DELETE FROM `kvit` WHERE `id`='{0}'", m_kvit.id);
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

            textBoxKvK.Enabled = false;
            dateTimePicker1.Enabled = false;
            
            buttonDelete.Enabled = false;
            buttonApply.Enabled = false;
            refreshKvit();
        }

  
        private void FormKvit_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;
            buttonNew.Enabled = true;
            buttonApply.Enabled = false;
            textBoxKvN.Enabled = false;
            textBoxKvK.Enabled = false;
            dateTimePicker1.Enabled = false;
           
            refreshKvit();
        }

        /*
         * Отчет по остатку билетов
         * начало  
         *   
         * 
         *
         * */
        //начальные и конечные номера билетов из табл. квитанции
        public void refreshOst()
        {
            listViewKvit.Items.Clear();
            queueKvit.Clear();

            listViewKvit.Columns.Clear();
            listViewKvit.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewKvit.Columns.Add("Номер конец", -2, HorizontalAlignment.Left);

            listViewKvit.Columns[0].Width = 150;
            listViewKvit.Columns[1].Width = 150;
          
            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "select `kvit`.`N_kvit_nach` AS `N_kvit_nach`,`kvit`.`N_kvit_koniec` AS `N_kvit_koniec` from `kvit` order by `N_kvit_nach`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewKvit.Items.Clear();
                queueKvit.Clear();
                _Bilety t = new _Bilety();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    
                    ListViewItem item1 = new ListViewItem(dataRow["N_kvit_nach"].ToString().Trim());
                    t.id = dataRow["N_kvit_nach"].ToString().Trim();
                    item1.SubItems.Add(dataRow["N_kvit_koniec"].ToString().Trim());
                    t.name = dataRow["N_kvit_koniec"].ToString().Trim();
                                       

                    listViewKvit.Items.Add(item1);
                    listViewKvit.Items[listViewKvit.Items.Count - 1].Tag = dataRow["N_kvit_nach"].ToString();
                    queueKvit.Enqueue(t);
                    
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
        //начальные и конечные номера билетов из журнала
        public void refreshOst1()
        {
            listViewKvit.Items.Clear();
            queueKvit.Clear();

            listViewKvit.Columns.Clear();
            listViewKvit.Columns.Add("Номер начало", -2, HorizontalAlignment.Left);
            listViewKvit.Columns[0].Width = 150;
            
            try
            {
                mainWin.m_dbConnector.Lock();
                MySqlConnection conn = mainWin.m_dbConnector.getMySqlConnection();

                string sql = "select `rasselenie`.`N_kvit` AS `N_kvit` from `rasselenie` order by `N_kvit`";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(sql, conn);
                DataSet dataSet = new DataSet();
                myAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];

                listViewKvit.Items.Clear();
                queueKvit.Clear();
                _Bilety t = new _Bilety();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    ListViewItem item1 = new ListViewItem(dataRow["N_kvit"].ToString().Trim());
                    t.id = dataRow["N_kvit"].ToString().Trim();
                    
                    listViewKvit.Items.Add(item1);
                    listViewKvit.Items[listViewKvit.Items.Count - 1].Tag = dataRow["N_kvit"].ToString();
                    queueKvit.Enqueue(t);

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
            
            //массив0 квитанции
            refreshOst();
            int[,] arr = new int [listViewKvit.Items.Count, listViewKvit.Columns.Count];
            for (int x = 0, maxX = listViewKvit.Items.Count; x < maxX; x++)
            {
                for (int y = 0, maxY = listViewKvit.Columns.Count; y < maxY; y++)
                {
                    arr[x, y] = int.Parse(listViewKvit.Items[x].SubItems[y].Text.Trim());

                }
            }

            //массив1 журнал
            refreshOst1();
            int[] arr1 = new int[listViewKvit.Items.Count];
            for (int x = 0, maxX = listViewKvit.Items.Count; x < maxX; x++)
            {
                arr1[x] = int.Parse(listViewKvit.Items[x].Text.Trim());
            }

            string[,] b = new string [10,10];
            
            refreshKvit();

                List<int> ARR = new List<int>();
                List<int> ARR1 = new List<int>();
                List<int> ARR2 = new List<int>();
                List<int> ARR3 = new List<int>();
                List<object> unique = new List<object>();
                List<object> unique2 = new List<object>();

                //диапазоны из нач/кон значений квитанции
                for (int x = 0; x <= ((arr.Length / 2) - 1); x++)
                {
                    for (int i = arr[x, 0], maxi = arr[x, 1]; i <= maxi; i++)
                    {

                        ARR.Add(i);
                    }
                }
                //журнал
                foreach (int g in arr1) ARR1.Add(g);
                                
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
                            unique.Add(x+1);
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
                        /*for (int y = 0; y < b.Length/3; y++)
                        {
                            if (unique[x + 1].Equals(int.Parse(b[y, 0])))
                            {
                                unique2.Add(b[y, 1]);
                                unique2.Add(b[y, 2]);
                            }
                        }*/
                   }



                }

                //массив для екселя
                object[,] c = new object[unique2.Count, 3];

                for (int i = 0, k = 0; i < unique2.Count; i = i + 3, k++)
                {
                    c[k, 0] = unique2[i];
                }
                for (int i = 1, k = 0; i < unique2.Count; i = i + 3, k++)
                {
                    c[k, 1] = unique2[i];
                }
                for (int i = 2, k = 0; i < unique2.Count; i = i + 3, k++)
                {
                    c[k, 2] = unique2[i];
                }
                
                /*for (int i = 3, k = 0; i < unique2.Count; i = i + 5, k++)
                {
                    c[k, 3] = unique2[i];
                }
            
                for (int i = 4, k = 0; i < unique2.Count; i = i + 5, k++)
                {
                    c[k, 4] = unique2[i];
                }*/
            
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
            Microsoft.Office.Interop.Excel.Range excelCellName = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.get_Range("A4", "C"+unique2.Count);
            
            excelCellName.Value2 = c;
            
                                  
        }

  /*
 * Отчет по остатку билетов конец
 * 
 *
 * */

       


    }

}
