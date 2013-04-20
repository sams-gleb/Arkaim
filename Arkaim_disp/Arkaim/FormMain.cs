namespace Ark
{
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using System.Data;
using System.Threading;
using System.IO;




/// <summary>
///    Summary description for Form1.
/// </summary>
public class FormMain : System.Windows.Forms.Form
{
	/// <summary> 
	///    Required designer variable
	/// </summary>
	/// 
    public FormOptions formOptions = null;
    public FormEkskursovody formEkskursovody = null;
    public FormZhurnal formZhurnal = null;
    public FormReports formReports = null;
    public FormBilety2 formBilety2 = null;
    public FormEkskursii formEkskursii = null;
    public FormZakazczik formZakazczik = null;
    public FormBilety formBilety = null;
    public FormRasselenie formRasselenie = null;
    public FormZhitie formZhitie = null;
    public FormReportsRasselenie formReportsRasselenie = null;
    public FormKvit formKvit = null;
    public FormCities formCities = null;

	public static FormMain parentWindow;
	private System.ComponentModel.IContainer components;
    private System.Windows.Forms.StatusBar statusBarMain;
    private System.Windows.Forms.MenuItem menuItem16;
    private System.Windows.Forms.MenuItem menuItemHelp;
	private System.Windows.Forms.MenuItem menuItemFile;
	private System.Windows.Forms.MainMenu mainMenu1;
	public static int documentCount; // static var which keeps track of the document count
								// This is used in the display of the form views

    public static String fileExtension = ".doc";
    private MenuItem menuItem6;
    private MenuItem menuItem1;

    public string m_DBServer;
    public string m_DBName;
    public string m_DBLogin;
    public string m_DBPassword;

    public DBConnector m_dbConnector;

    private ImageList imageList1;
    private MenuItem menuItem4;
    private MenuItem menuItemEkskursovody;
    private MenuItem menuItemBilety2;
    private MenuItem menuItemReports;

    private bool bCancelClose = false;
    private MenuItem menuItem11;
    private MenuItem menuItemJournal;
    private MenuItem menuItemSettings;
    private MenuItem menuItem2;
    private MenuItem menuItemZakazczik;
    private MenuItem menuItemBilety;
    private MenuItem menuItemRasselenie;
    private MenuItem menuItemZhitie;
    private MenuItem menuItemReportsRasselenie;
    private ToolStrip toolStrip1;
    private ToolStripButton ñîçäàòüToolStripButton;
    private ToolStripButton îòêğûòüToolStripButton;
    private ToolStripButton ñîõğàíèòüToolStripButton;
    private ToolStripButton ïå÷àòüToolStripButton;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripButton âûğåçàòüToolStripButton;
    private ToolStripButton êîïèğîâàòüToolStripButton;
    private ToolStripButton âñòàâêàToolStripButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton ñïğàâêàToolStripButton;
    private MenuItem menuKvit;
    private MenuItem menuItemCities;
    public bool bCanClose = false;


	/// <summary>
	///   Constructor
	/// </summary>
	public FormMain()
	{
		parentWindow = this;
		//
		// Required for Win Form Designer support
		//
		InitializeComponent();	
		documentCount=0;
        statusBarMain.Height += 5;
        toolStrip1.Visible  = false ;


        string fileXML = Application.StartupPath + "\\config.xml";
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

            XmlElement childNodeDBServer = null;
            XmlElement childNodeDBName = null;
            XmlElement childNodeDBLogin = null;
            XmlElement childNodeDBPassword = null;

            for (int i = 0; i < nodeList.Count; i++)
            {
                if (((XmlElement)nodeList.Item(i)).GetAttribute("Name") == "Server")
                {
                    childNodeDBServer = (XmlElement)nodeList.Item(i);
                    m_DBServer = childNodeDBServer.InnerText;
                };
                if (((XmlElement)nodeList.Item(i)).GetAttribute("Name") == "DBName")
                {
                    childNodeDBName = (XmlElement)nodeList.Item(i);
                    m_DBName = childNodeDBName.InnerText;
                };
                if (((XmlElement)nodeList.Item(i)).GetAttribute("Name") == "Login")
                {
                    childNodeDBLogin = (XmlElement)nodeList.Item(i);
                    m_DBLogin = childNodeDBLogin.InnerText;
                };
                if (((XmlElement)nodeList.Item(i)).GetAttribute("Name") == "Password")
                {
                    childNodeDBPassword = (XmlElement)nodeList.Item(i);
                    m_DBPassword = childNodeDBPassword.InnerText;
                };
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }


	 }

	/// <summary>
	///    Clean up any resources being used
	/// </summary>
	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if (components != null) 
			{
				components.Dispose();
			}
		}
		base.Dispose( disposing );
	}

	/// <summary>
	///    Required method for Designer support - do not modify
	///    the contents of this method with the code editor
	/// </summary>
	private void InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
        this.menuItemHelp = new System.Windows.Forms.MenuItem();
        this.menuItem1 = new System.Windows.Forms.MenuItem();
        this.menuItemFile = new System.Windows.Forms.MenuItem();
        this.menuItem6 = new System.Windows.Forms.MenuItem();
        this.menuItem16 = new System.Windows.Forms.MenuItem();
        this.imageList1 = new System.Windows.Forms.ImageList(this.components);
        this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
        this.menuItem4 = new System.Windows.Forms.MenuItem();
        this.menuItemEkskursovody = new System.Windows.Forms.MenuItem();
        this.menuItem11 = new System.Windows.Forms.MenuItem();
        this.menuItemReports = new System.Windows.Forms.MenuItem();
        this.menuItemJournal = new System.Windows.Forms.MenuItem();
        this.menuItemZakazczik = new System.Windows.Forms.MenuItem();
        this.menuItemBilety = new System.Windows.Forms.MenuItem();
        this.menuItemBilety2 = new System.Windows.Forms.MenuItem();
        this.menuItem2 = new System.Windows.Forms.MenuItem();
        this.menuItemRasselenie = new System.Windows.Forms.MenuItem();
        this.menuItemZhitie = new System.Windows.Forms.MenuItem();
        this.menuItemReportsRasselenie = new System.Windows.Forms.MenuItem();
        this.menuKvit = new System.Windows.Forms.MenuItem();
        this.menuItemSettings = new System.Windows.Forms.MenuItem();
        this.statusBarMain = new System.Windows.Forms.StatusBar();
        this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        this.ñîçäàòüToolStripButton = new System.Windows.Forms.ToolStripButton();
        this.îòêğûòüToolStripButton = new System.Windows.Forms.ToolStripButton();
        this.ñîõğàíèòüToolStripButton = new System.Windows.Forms.ToolStripButton();
        this.ïå÷àòüToolStripButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        this.âûğåçàòüToolStripButton = new System.Windows.Forms.ToolStripButton();
        this.êîïèğîâàòüToolStripButton = new System.Windows.Forms.ToolStripButton();
        this.âñòàâêàToolStripButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.ñïğàâêàToolStripButton = new System.Windows.Forms.ToolStripButton();
        this.menuItemCities = new System.Windows.Forms.MenuItem();
        this.toolStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // menuItemHelp
        // 
        this.menuItemHelp.Index = 4;
        this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
        this.menuItemHelp.Text = "Ñïğàâêà";
        this.menuItemHelp.Click += new System.EventHandler(this.menuItemHelp_Click);
        // 
        // menuItem1
        // 
        this.menuItem1.Index = 0;
        this.menuItem1.Text = "Î ïğîãğàììå...";
        this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click_1);
        // 
        // menuItemFile
        // 
        this.menuItemFile.Index = 0;
        this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6});
        this.menuItemFile.Text = "Ôàéë";
        this.menuItemFile.Click += new System.EventHandler(this.menuItemFile_Click);
        // 
        // menuItem6
        // 
        this.menuItem6.Index = 0;
        this.menuItem6.Text = "Âûõîä";
        this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
        // 
        // menuItem16
        // 
        this.menuItem16.Index = -1;
        this.menuItem16.Text = "-";
        // 
        // imageList1
        // 
        this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
        this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
        this.imageList1.Images.SetKeyName(0, "");
        this.imageList1.Images.SetKeyName(1, "");
        this.imageList1.Images.SetKeyName(2, "");
        this.imageList1.Images.SetKeyName(3, "");
        this.imageList1.Images.SetKeyName(4, "");
        this.imageList1.Images.SetKeyName(5, "");
        // 
        // mainMenu1
        // 
        this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItem4,
            this.menuItem2,
            this.menuItemSettings,
            this.menuItemHelp});
        // 
        // menuItem4
        // 
        this.menuItem4.Index = 1;
        this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEkskursovody,
            this.menuItem11,
            this.menuItemReports,
            this.menuItemJournal,
            this.menuItemZakazczik,
            this.menuItemBilety,
            this.menuItemBilety2});
        this.menuItem4.Text = "İêñêóğñèè";
        // 
        // menuItemEkskursovody
        // 
        this.menuItemEkskursovody.Index = 0;
        this.menuItemEkskursovody.Text = "İêñêóğñîâîäû";
        this.menuItemEkskursovody.Click += new System.EventHandler(this.menuItem2_Click_1);
        // 
        // menuItem11
        // 
        this.menuItem11.Index = 1;
        this.menuItem11.Text = "İêñêóğñèè";
        this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
        // 
        // menuItemReports
        // 
        this.menuItemReports.Index = 2;
        this.menuItemReports.Text = "Îò÷åòû";
        this.menuItemReports.Click += new System.EventHandler(this.menuItemReports_Click);
        // 
        // menuItemJournal
        // 
        this.menuItemJournal.Index = 3;
        this.menuItemJournal.Text = "Æóğíàë";
        this.menuItemJournal.Click += new System.EventHandler(this.menuItemJournal_Click);
        // 
        // menuItemZakazczik
        // 
        this.menuItemZakazczik.Index = 4;
        this.menuItemZakazczik.Text = "Çàêàç÷èê";
        this.menuItemZakazczik.Click += new System.EventHandler(this.menuItemZakazczik_Click);
        // 
        // menuItemBilety
        // 
        this.menuItemBilety.Index = 5;
        this.menuItemBilety.Text = "Áèëåòû";
        this.menuItemBilety.Click += new System.EventHandler(this.menuItemBilety_Click);
        // 
        // menuItemBilety2
        // 
        this.menuItemBilety2.Index = 6;
        this.menuItemBilety2.Text = "Áèëåòû2";
        this.menuItemBilety2.Click += new System.EventHandler(this.menuItemBilety2_Click);
        // 
        // menuItem2
        // 
        this.menuItem2.Index = 2;
        this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemRasselenie,
            this.menuItemZhitie,
            this.menuItemReportsRasselenie,
            this.menuKvit,
            this.menuItemCities});
        this.menuItem2.Text = "Ğàññåëåíèå";
        // 
        // menuItemRasselenie
        // 
        this.menuItemRasselenie.Index = 0;
        this.menuItemRasselenie.Text = "Æóğíàë";
        this.menuItemRasselenie.Click += new System.EventHandler(this.menuItemRasselenie_Click);
        // 
        // menuItemZhitie
        // 
        this.menuItemZhitie.Index = 1;
        this.menuItemZhitie.Text = "Æèòèå";
        this.menuItemZhitie.Click += new System.EventHandler(this.menuItemZhitie_Click);
        // 
        // menuItemReportsRasselenie
        // 
        this.menuItemReportsRasselenie.Index = 2;
        this.menuItemReportsRasselenie.Text = "Îò÷åòû";
        this.menuItemReportsRasselenie.Click += new System.EventHandler(this.menuItemReportsRasselenie_Click);
        // 
        // menuKvit
        // 
        this.menuKvit.Index = 3;
        this.menuKvit.Text = "Êâèòàíöèè";
        this.menuKvit.Click += new System.EventHandler(this.menuKvit_Click);
        // 
        // menuItemSettings
        // 
        this.menuItemSettings.Index = 3;
        this.menuItemSettings.Text = "Íàñòğîéêè";
        this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
        // 
        // statusBarMain
        // 
        this.statusBarMain.Font = new System.Drawing.Font("Arial", 8F);
        this.statusBarMain.Location = new System.Drawing.Point(0, 516);
        this.statusBarMain.Name = "statusBarMain";
        this.statusBarMain.Size = new System.Drawing.Size(784, 16);
        this.statusBarMain.TabIndex = 2;
        // 
        // toolStrip1
        // 
        this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ñîçäàòüToolStripButton,
            this.îòêğûòüToolStripButton,
            this.ñîõğàíèòüToolStripButton,
            this.ïå÷àòüToolStripButton,
            this.toolStripSeparator,
            this.âûğåçàòüToolStripButton,
            this.êîïèğîâàòüToolStripButton,
            this.âñòàâêàToolStripButton,
            this.toolStripSeparator1,
            this.ñïğàâêàToolStripButton});
        this.toolStrip1.Location = new System.Drawing.Point(0, 0);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.Size = new System.Drawing.Size(784, 25);
        this.toolStrip1.TabIndex = 4;
        this.toolStrip1.Text = "toolStrip1";
        // 
        // ñîçäàòüToolStripButton
        // 
        this.ñîçäàòüToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.ñîçäàòüToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ñîçäàòüToolStripButton.Image")));
        this.ñîçäàòüToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.ñîçäàòüToolStripButton.Name = "ñîçäàòüToolStripButton";
        this.ñîçäàòüToolStripButton.Size = new System.Drawing.Size(23, 22);
        this.ñîçäàòüToolStripButton.Text = "&Ñîçäàòü";
        // 
        // îòêğûòüToolStripButton
        // 
        this.îòêğûòüToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.îòêğûòüToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("îòêğûòüToolStripButton.Image")));
        this.îòêğûòüToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.îòêğûòüToolStripButton.Name = "îòêğûòüToolStripButton";
        this.îòêğûòüToolStripButton.Size = new System.Drawing.Size(23, 22);
        this.îòêğûòüToolStripButton.Text = "&Îòêğûòü";
        // 
        // ñîõğàíèòüToolStripButton
        // 
        this.ñîõğàíèòüToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.ñîõğàíèòüToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ñîõğàíèòüToolStripButton.Image")));
        this.ñîõğàíèòüToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.ñîõğàíèòüToolStripButton.Name = "ñîõğàíèòüToolStripButton";
        this.ñîõğàíèòüToolStripButton.Size = new System.Drawing.Size(23, 22);
        this.ñîõğàíèòüToolStripButton.Text = "&Ñîõğàíèòü";
        // 
        // ïå÷àòüToolStripButton
        // 
        this.ïå÷àòüToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.ïå÷àòüToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ïå÷àòüToolStripButton.Image")));
        this.ïå÷àòüToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.ïå÷àòüToolStripButton.Name = "ïå÷àòüToolStripButton";
        this.ïå÷àòüToolStripButton.Size = new System.Drawing.Size(23, 22);
        this.ïå÷àòüToolStripButton.Text = "&Ïå÷àòü";
        // 
        // toolStripSeparator
        // 
        this.toolStripSeparator.Name = "toolStripSeparator";
        this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
        // 
        // âûğåçàòüToolStripButton
        // 
        this.âûğåçàòüToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.âûğåçàòüToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("âûğåçàòüToolStripButton.Image")));
        this.âûğåçàòüToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.âûğåçàòüToolStripButton.Name = "âûğåçàòüToolStripButton";
        this.âûğåçàòüToolStripButton.Size = new System.Drawing.Size(23, 22);
        this.âûğåçàòüToolStripButton.Text = "Â&ûğåçàòü";
        // 
        // êîïèğîâàòüToolStripButton
        // 
        this.êîïèğîâàòüToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.êîïèğîâàòüToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("êîïèğîâàòüToolStripButton.Image")));
        this.êîïèğîâàòüToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.êîïèğîâàòüToolStripButton.Name = "êîïèğîâàòüToolStripButton";
        this.êîïèğîâàòüToolStripButton.Size = new System.Drawing.Size(23, 22);
        this.êîïèğîâàòüToolStripButton.Text = "&Êîïèğîâàòü";
        // 
        // âñòàâêàToolStripButton
        // 
        this.âñòàâêàToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.âñòàâêàToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("âñòàâêàToolStripButton.Image")));
        this.âñòàâêàToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.âñòàâêàToolStripButton.Name = "âñòàâêàToolStripButton";
        this.âñòàâêàToolStripButton.Size = new System.Drawing.Size(23, 22);
        this.âñòàâêàToolStripButton.Text = "Âñò&àâêà";
        // 
        // toolStripSeparator1
        // 
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
        // 
        // ñïğàâêàToolStripButton
        // 
        this.ñïğàâêàToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.ñïğàâêàToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ñïğàâêàToolStripButton.Image")));
        this.ñïğàâêàToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.ñïğàâêàToolStripButton.Name = "ñïğàâêàToolStripButton";
        this.ñïğàâêàToolStripButton.Size = new System.Drawing.Size(23, 22);
        this.ñïğàâêàToolStripButton.Text = "Ñïğ&àâêà";
        // 
        // menuItemCities
        // 
        this.menuItemCities.Index = 4;
        this.menuItemCities.Text = "Ãîğîäà";
        this.menuItemCities.Click += new System.EventHandler(this.menuItemCities_Click);
        // 
        // FormMain
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.ClientSize = new System.Drawing.Size(784, 532);
        this.Controls.Add(this.toolStrip1);
        this.Controls.Add(this.statusBarMain);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.IsMdiContainer = true;
        this.Menu = this.mainMenu1;
        this.Name = "FormMain";
        this.Text = "Àğêàèì";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        this.Closing += new System.ComponentModel.CancelEventHandler(this.ClosingMainAppHandler);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
        this.Load += new System.EventHandler(this.Form1_Load);
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

	}

 
	//Handle the Menu Item clicks
	private void MenuItemHandler(object sender, System.EventArgs e)
	{
        Application.Exit();

	}


    
	//Exit
	private void Exit()
	{
		Form[] childForm = this.MdiChildren ;
		//Make sure to ask for saving the doc before exiting the app
		for(int i=0; i < childForm.Length ; i++)
			childForm[i].Close();

		Application.Exit();
		
	}

	//Tile
	private void Tile()
	{
		this.LayoutMdi(MdiLayout.TileHorizontal);
		
	}

	//Cascade
	private void Cascade()
	{
		this.LayoutMdi(MdiLayout.Cascade);
		
	}

	

	//Disable the menu and toolbar items when there is no active child form


	//App closing handler
	public void ClosingMainAppHandler(Object sender,CancelEventArgs e)
	{
		this.Exit();		
	}

	/*
	* The main entry point for the application.
    *
    */
	[STAThread]
	public static void Main(string[] args) 
	{
		Application.Run(new FormMain());
	}


    private void Form1_Load(object sender, EventArgs e)
    {
        this.m_dbConnector = new DBConnector();
        if (!this.m_dbConnector.setConnectionString(String.Format("server={0};uid={1};pwd={2};database={3}", this.m_DBServer, this.m_DBLogin, this.m_DBPassword, this.m_DBName), this.m_DBServer, this.m_DBLogin, this.m_DBPassword, this.m_DBName))
        {
            MessageBox.Show("Íåîáõîäèìî èñïîëüçîâàòü áîëåå íîâóş âåğñèş ïğîãğàììû!", "Âíèìàíèå", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.bCanClose = true;
            this.Close();
        }
    }

    private void menuItemFile_Click(object sender, EventArgs e)
    {

    }

    private void menuItemHelp_Click(object sender, EventArgs e)
    {

    }

    private void menuItem6_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void menuItem1_Click_1(object sender, EventArgs e)
    {
        AboutBox1 formAbout = new AboutBox1();
        formAbout.ShowDialog();
    }



    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }

    private void menuItemOptions_Click(object sender, EventArgs e)
    {
    }

    private void menuItem2_Click(object sender, EventArgs e)
    {
    }

    private void menuItem3_Click(object sender, EventArgs e)
    {
    }

    private void menuItem2_Click_1(object sender, EventArgs e)
    {
        if (formEkskursovody == null)
        {
            formEkskursovody = new FormEkskursovody(this);
            formEkskursovody.Show();
        }
        else
        {
            formEkskursovody.WindowState = FormWindowState.Maximized;
        }
    }

    private void menuItemReports_Click(object sender, EventArgs e)
    {
        if (formReports == null)
        {
            formReports = new FormReports(this);
            formReports.Show();
        }
        else
        {
            formReports.WindowState = FormWindowState.Maximized;
        }
    }

    private void menuItemBilety2_Click(object sender, EventArgs e)
    {
        if (formBilety2 == null)
        {
            formBilety2 = new FormBilety2(this);
            formBilety2.Show();
        }
        else
        {
            formBilety2.WindowState = FormWindowState.Maximized;
        }

    }


    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (bCanClose)
            return;
        if (bCancelClose)
        {
            bCancelClose = false;
            e.Cancel = true;
            return;
        }
        if (DialogResult.Cancel == MessageBox.Show("Çàêğûòü?", "Âíèìàíèå", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
        {
            bCancelClose = true;
            e.Cancel = true;
        }
        else
            e.Cancel = false;
    }

    private void menuItem11_Click(object sender, EventArgs e)
    {
        if (formEkskursii == null)
        {
            formEkskursii = new FormEkskursii(this);
            formEkskursii.Show();
        }
        else
        {
            formEkskursii.WindowState = FormWindowState.Maximized;
        }
        
    }

    private void menuItemJournal_Click(object sender, EventArgs e)
    {
        if (formZhurnal == null)
        {
            formZhurnal = new FormZhurnal(this);
            formZhurnal.Show();
        }
        else
        {
            formZhurnal.WindowState = FormWindowState.Maximized;
        }

    }

    private void menuItemZakazczik_Click(object sender, EventArgs e)
    {
        if (formZakazczik == null)
        {
            formZakazczik = new FormZakazczik(this);
            formZakazczik.Show();
        }
        else
        {
            //formZakazczik.Activate();
            formZakazczik.WindowState = FormWindowState.Maximized;
        }

    }


    private void menuItemBilety_Click(object sender, EventArgs e)
    {
        if (formBilety == null)
        {
            formBilety = new FormBilety(this);
            formBilety.Show();
        }
        else
        {
            formBilety.WindowState = FormWindowState.Maximized;
        }

    }

    private void menuItemSettings_Click(object sender, EventArgs e)
    {
        if (formOptions == null)
        {
            formOptions = new FormOptions(this);
            formOptions.ShowDialog();
        }
    }
    private void menuItemRasselenie_Click(object sender, EventArgs e)
    {
        if (formRasselenie == null)
        {
            formRasselenie = new FormRasselenie(this);
            formRasselenie.Show();
        }
        else
        {
            formRasselenie.WindowState = FormWindowState.Maximized;
        }

    }
    private void menuItemZhitie_Click(object sender, EventArgs e)
    {
        if (formZhitie == null)
        {
            formZhitie = new FormZhitie(this);
            formZhitie.Show();
        }
        else
        {
            formZhitie.WindowState = FormWindowState.Maximized;
        }

    }
    private void menuItemReportsRasselenie_Click(object sender, EventArgs e)
    {
        if (formReportsRasselenie == null)
        {
            formReportsRasselenie = new FormReportsRasselenie(this);
            formReportsRasselenie.Show();
        }
        else
        {
            formReportsRasselenie.WindowState = FormWindowState.Maximized;
        }

    }
    private void menuKvit_Click(object sender, EventArgs e)
    {
        if (formKvit == null)
        {
            formKvit = new FormKvit(this);
            formKvit.Show();
        }
        else
        {
            formKvit.WindowState = FormWindowState.Maximized;
        }

    }
    private void menuItemCities_Click(object sender, EventArgs e)
    {
        if (formCities == null)
        {
            formCities = new FormCities(this);
            formCities.Show();
        }
        else
        {
            formCities.WindowState = FormWindowState.Maximized;
        }

    }

    
}
}
