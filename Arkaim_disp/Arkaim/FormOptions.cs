using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Ark
{
    public partial class FormOptions : Form
    {
        private FormMain mainWin;

        public FormOptions(FormMain mainWin)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            textBoxServer.Text = mainWin.m_DBServer;
            textBoxDBName.Text = mainWin.m_DBName;
            textBoxLogin.Text = mainWin.m_DBLogin;
            textBoxPassword.Text = mainWin.m_DBPassword;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            mainWin.formOptions = null;
            Close();
        }

        private void FormOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainWin.formOptions = null;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            string fileXML;
            fileXML = Application.StartupPath + "\\config.xml";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.Load(fileXML);
                }
                catch (System.IO.FileNotFoundException)
                {
                    XmlTextWriter xmlWriter = new XmlTextWriter(fileXML, System.Text.Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                    xmlWriter.WriteStartElement("Options");
                    xmlWriter.Close();
                    xmlDoc.Load(fileXML);
                }
                XmlNode root = xmlDoc.DocumentElement;

                XmlNodeList nodeList = xmlDoc.SelectNodes("/Options/Item");


                bool bServer = false;
                bool bDBName = false;
                bool bLogin = false;
                bool bPassword = false;
                XmlElement childNodeServer = null;
                XmlElement childNodeDBName = null;
                XmlElement childNodeLogin = null;
                XmlElement childNodePassword = null;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (((XmlElement)nodeList.Item(i)).GetAttribute("Name") == "Server")
                    {
                        childNodeServer = (XmlElement)nodeList.Item(i);
                        bServer = true;
                    };
                    if (((XmlElement)nodeList.Item(i)).GetAttribute("Name") == "DBName")
                    {
                        childNodeDBName = (XmlElement)nodeList.Item(i);
                        bDBName = true;
                    };
                    if (((XmlElement)nodeList.Item(i)).GetAttribute("Name") == "Login")
                    {
                        childNodeLogin = (XmlElement)nodeList.Item(i);
                        bLogin = true;
                    };
                    if (((XmlElement)nodeList.Item(i)).GetAttribute("Name") == "Password")
                    {
                        childNodePassword = (XmlElement)nodeList.Item(i);
                        bPassword = true;
                    };
                }

                if (bServer == false)
                {
                    childNodeServer = xmlDoc.CreateElement("Item");
                    XmlText textNodeServer = xmlDoc.CreateTextNode(textBoxServer.Text);
                    root.AppendChild(childNodeServer);
                    childNodeServer.SetAttribute("Name", "Server");
                    childNodeServer.AppendChild(textNodeServer);
                }
                else
                {
                    childNodeServer.InnerText = textBoxServer.Text;
                };

                if (bDBName == false)
                {
                    childNodeDBName = xmlDoc.CreateElement("Item");
                    XmlText textNodeDBName = xmlDoc.CreateTextNode(textBoxDBName.Text);
                    root.AppendChild(childNodeDBName);
                    childNodeDBName.SetAttribute("Name", "DBName");
                    childNodeDBName.AppendChild(textNodeDBName);
                }
                else
                {
                    childNodeDBName.InnerText = textBoxDBName.Text;
                };

                if (bLogin == false)
                {
                    childNodeLogin = xmlDoc.CreateElement("Item");
                    XmlText textNodeLogin = xmlDoc.CreateTextNode(textBoxLogin.Text);
                    root.AppendChild(childNodeLogin);
                    childNodeLogin.SetAttribute("Name", "Login");
                    childNodeLogin.AppendChild(textNodeLogin);
                }
                else
                {
                    childNodeLogin.InnerText = textBoxLogin.Text;
                };

                if (bPassword == false)
                {
                    childNodePassword = xmlDoc.CreateElement("Item");
                    XmlText textNodePassword = xmlDoc.CreateTextNode(textBoxPassword.Text);
                    root.AppendChild(childNodePassword);
                    childNodePassword.SetAttribute("Name", "Password");
                    childNodePassword.AppendChild(textNodePassword);
                }
                else
                {
                    childNodePassword.InnerText = textBoxPassword.Text;
                };

                xmlDoc.Save(fileXML);

                this.mainWin.m_DBServer = textBoxServer.Text;
                this.mainWin.m_DBName = textBoxDBName.Text;
                this.mainWin.m_DBLogin = textBoxLogin.Text;
                this.mainWin.m_DBPassword = textBoxPassword.Text;

                if (!this.mainWin.m_dbConnector.setConnectionString(String.Format("server={0};uid={1};pwd={2};database={3}", this.mainWin.m_DBServer, this.mainWin.m_DBLogin, this.mainWin.m_DBPassword, this.mainWin.m_DBName), this.mainWin.m_DBServer, this.mainWin.m_DBLogin, this.mainWin.m_DBPassword, this.mainWin.m_DBName))
                {
                    MessageBox.Show("Необходимо использовать более новую версию программы!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.mainWin.bCanClose = true;
                    this.mainWin.Close();
                }

                mainWin.formOptions = null;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}