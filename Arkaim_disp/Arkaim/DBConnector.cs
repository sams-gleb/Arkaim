using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace Ark
{
    public class DBConnector
    {
        public string connectionString;
        public MySqlConnection conn = null;

        string m_server, m_login, m_password, m_database;

        public DBConnector() 
        {
        }

        public bool setConnectionString(string s, string server, string login, string password, string database)
        {
            bool res = false;
            this.connectionString = s;
            if (conn != null)
                conn.Dispose();
            conn = new MySqlConnection(this.connectionString);
            try
            {
                conn.Open();
                conn.Close();
                m_server = server;
                m_login = login;
                m_password = password;
                m_database = database;
                res = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
                res = true;
            }
            return res;
        }

        public MySqlConnection getMySqlConnection()
        {
            return conn;
        }

        public void Lock(){
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                conn = new MySqlConnection(this.connectionString);
                conn.Open();
            }
        }

        public void Unlock(){
            conn.Close();
        }

    }
}
