using AISCOMServer.Classes;
using SILCommServer;
using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace AISCOMServer
{
    public partial class frmServer : Form
    {
        #region local variables

        ContextMenu cMenu;
        NotifyIcon m_notifyicon;
        SILSocketServer oServer;
        bool isStart = false;
        public static string strCnn;
        static string strConfig = Application.StartupPath + "\\dbSetting.ini";
        static string strPort = Application.StartupPath + "\\Port.txt";
        static string IsRunning = Application.StartupPath + "\\IsRunning.txt";



        #endregion

        #region Constructor

        public frmServer()
        {
            try
            {
                InitializeComponent();
                defaultDisplay();
                disConnectUI();
                //clsMain.SetLogger();
                lblVersion.Text = "Version: " + Application.ProductVersion.Trim();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion

        #region local methods

        /// <summary>
        /// Open Socket to communicate Client MC.
        /// </summary>
        private void connectServer()
        {
            //oServer = new BCILSocketServer(5150, 50);
            oServer = new SILSocketServer(clsMsgRule.sPort, 50);
            oServer.EOMChar = "}";
            oServer.SessionTimeOut = 5000;
            oServer.OnClientConnect += new SILSocketServer.NewClientHandler(oServer_OnClientConnect);
            LogFile oLog = oServer.ActiveLog;
            oLog.EnableLogFiles = true;
            oLog.ChangeInterval = LogFile.ChangeIntervals.ciDaily;
            oLog.LogLevel = EventNotice.EventTypes.evtAll;
            oLog.LogFilesPrefix = "SATO";
            string strLogPath = Application.StartupPath + "/Log";
            DirectoryInfo oDir = new DirectoryInfo(strLogPath);
            if (!oDir.Exists)
            {
                oDir.Create();
            }
            oLog.LogFilesPath = strLogPath;
            oLog.LogDays = 15;

            oServer.StartService();
            connectUI();
        }
        private void connectUI()
        {
            picConnect.BringToFront();
            picDisconnect.SendToBack();
            picViewConnection.Image = Properties.Resources.com_server;
            lblConnect.ForeColor = Color.Green;
            cmdConnect.Enabled = false;
            cmdDisconnect.Enabled = true;
            isStart = true;
        }
        /// <summary>
        /// To stop Socket Communication Service.
        /// </summary>
        private void disConnectServer()
        {
            //clsSetting.sCon = string.Empty;
            //clsSetting.sCon1 = string.Empty;
            oServer.StopService();
            oServer = null;
            disConnectUI();
        }
        private void disConnectUI()
        {
            picConnect.SendToBack();
            picDisconnect.BringToFront();
            lblConnect.Text = "Server Disconnected.";
            picViewConnection.Image = Properties.Resources.com_server_disconnect;
            lblConnect.ForeColor = Color.Red;
            cmdDisconnect.Enabled = false;
            cmdConnect.Enabled = true;
            isStart = false;
        }
        private void FillComboBox(ComboBox cbo, DataTable dt, bool isSelect)
        {
            if (isSelect)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            cbo.DisplayMember = dt.Columns[0].ToString();
            cbo.ValueMember = dt.Columns[1].ToString();
            cbo.DataSource = dt;
        }
        private DataTable getDBServer()
        {
            DataTable dtDbServer = new DataTable();
            dtDbServer.Columns.Add("Display");
            dtDbServer.Columns.Add("Value");
            DataTable dtResults = SqlDataSourceEnumerator.Instance.GetDataSources();

            string strInstance;
            foreach (DataRow dr in dtResults.Rows)
            {
                if (dr["InstanceName"].ToString() != string.Empty)
                {
                    strInstance = "\\" + dr["InstanceName"].ToString();
                }
                else
                {
                    strInstance = string.Empty;
                }

                DataRow drRow = dtDbServer.NewRow();
                drRow["Display"] = dr["ServerName"].ToString() + strInstance;
                drRow["Value"] = dr["ServerName"].ToString() + strInstance;
                dtDbServer.Rows.Add(drRow);
            }

            return dtDbServer;
        }
        private DataTable getDBSchema(string strSource, string strUser, string strPwd)
        {
            try
            {
                DataTable dtSchema = new DataTable();
                dtSchema.Columns.Add("Display");
                dtSchema.Columns.Add("Value");
                string strCon = "Data Source=" + strSource + ";";
                strCon = strCon + " User ID=" + strUser + "; Password=" + strPwd + ";";

                SqlConnection oCon = new SqlConnection(strCon);
                oCon.Open();
                DataTable dtResults = oCon.GetSchema("Databases"); ;
                oCon.Close();
                foreach (DataRow dr in dtResults.Rows)
                {
                    DataRow drRow = dtSchema.NewRow();
                    drRow["Display"] = dr["database_name"].ToString();
                    drRow["Value"] = dr["database_name"].ToString();
                    dtSchema.Rows.Add(drRow);
                }
                return dtSchema;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        private void defaultDisplay()
        {
            pnlClients.SetBounds(39, 313, 10, 20);
            pnlLog.SetBounds(10, 313, 10, 20);
            pnlDBSeting.SetBounds(23, 313, 10, 20);
            pnlClients.Visible = false;
            pnlDBSeting.Visible = false;
            pnlLog.Visible = false;
        }
        private void setDBSettingPnl()
        {
            cmbServer.Text = string.Empty;
            cmbSchema.Text = string.Empty;
            txtUserID.Text = string.Empty;
            txtPwd.Text = string.Empty;
        }

        delegate void delegateAddClient(string data, string strFlag);

        delegate void delegateRemoveClient(string data);

        public void AddClient(string data, string strFlag)
        {
            if (strFlag == "LOG")
            {
                if (lvLog.InvokeRequired)
                {
                    lvLog.Invoke(new delegateAddClient(AddClient), data, strFlag);
                }
                else
                {
                    string[] dd = data.Split(';');
                    lvLog.Items.Add(dd[0]);
                    lvLog.Items[lvLog.Items.Count - 1].SubItems.Add(dd[1]);
                    lvLog.Items[lvLog.Items.Count - 1].SubItems.Add(System.DateTime.Now.ToString());
                }
            }
            else if (strFlag == "CLIENT")
            {
                if (lvClient.InvokeRequired)
                {
                    lvClient.Invoke(new delegateAddClient(AddClient), data, strFlag);
                }
                else
                {
                    string[] dd = data.Split(';');
                    lvClient.Items.Add(dd[0]);
                    lvClient.Items[lvClient.Items.Count - 1].SubItems.Add(System.DateTime.Now.ToString());
                }
            }
        }

        public void removeClient(string data)
        {
            if (lvClient.InvokeRequired)
            {
                lvClient.Invoke(new delegateRemoveClient(removeClient), data);
            }
            else
            {
                for (int i = 0; i < lvClient.Items.Count; i++)
                {
                    if (lvClient.Items[i].Text.Trim() == data)
                    {
                        lvClient.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Form

        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                oServer.StopService();
            }
            catch (Exception ex)
            {
            }
            Application.Exit();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(strPort))
                {
                    StreamReader oSrPort = new StreamReader(strPort);
                    while (!oSrPort.EndOfStream)
                    { clsMsgRule.sPort = Convert.ToInt32(oSrPort.ReadLine()); }
                }

                connectServer();

                lblConnect.Text = "Server Connected : Port : " + clsMsgRule.sPort;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                disConnectServer();
            }
            catch (Exception ex)
            {

            }
        }

        private void oServer_OnClientConnect(ClientHandler RemoteClient)
        {
            try
            {
                RemoteClient.OnDataArrival += new ClientHandler.DataArrivalHandler(RemoteClient_OnDataArrival);
            }
            catch (Exception ex)
            {

            }
        }

        private void RemoteClient_OnDataArrival(ClientHandler RemoteClient)
        {
            string Response = "";
            clsSecurity objSecurity = null;
            clsProcess objProc = null;
            DataTable dt = new DataTable();
            try
            {
                AddClient(RemoteClient.ClientIP, "CLIENT");
                AddClient(RemoteClient.ClientIP + ";" + RemoteClient.Message, "LOG");
                //   string data = "RM_QC_VALIDATE_BARCODE~lkj~";
                string[] Data = RemoteClient.Message.Split('~');//data.Split('~'); //
                try
                {
                    switch (Data[0])
                    {
                        case "GET_APP_VERSION":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetAppVersion();
                            objSecurity = null;
                            break;

                        case "GET_NEWEXE_DESKTOP":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetNewExeDesktop();
                            objSecurity = null;
                            break;

                        case "GET_NEWEXE_DEVICE":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetNewExeDevice();
                            objSecurity = null;
                            break;

                        case "V_USER":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.ManageUser(new User { UserId = Data[1], Password = Data[2] });
                            objSecurity = null;
                            break;

                        case "GET_USER_RIGHT":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetUserRights(Data[1]);
                            objSecurity = null;
                            break;



                        #region Printing Verified
                        case "PRINTING_VERIFIED":
                            objProc = new clsProcess();
                            Response = objProc.PrintingVerified_ExecuteTask(new PL_PrirtingVerified
                            {
                                DbType = Data[1],
                                PartNo = Data[2],
                                ItemBarcode = Data[3],
                                CreatedBy = Data[4],
                            });
                            objProc = null;
                            break;
                        case "MANUALLY_VERIFIED":
                            objProc = new clsProcess();
                            Response = objProc.PrintingVerified_ExecuteTask(new PL_PrirtingVerified
                            {
                                DbType = Data[1],
                                PartNo = Data[2],
                                Remarks = Data[3],
                                CreatedBy = Data[4],
                            });
                            objProc = null;
                            break;
                        #endregion



                        default:
                            break;
                    }
                }

                catch (Exception ex)
                { Response = "ERROR" + "~" + ex.Message; }
                AddClient(RemoteClient.ClientIP + ";RESPONSE~", "LOG");
                removeClient(RemoteClient.ClientIP);
                RemoteClient.Response = Response;
            }
            catch (Exception ex1)
            { Response = "ERROR" + "~" + ex1.Message; }
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            try
            {
                cMenu = new System.Windows.Forms.ContextMenu();
                cMenu.MenuItems.Add(0, new MenuItem("Show", new EventHandler(this.Show_Click)));
                cMenu.MenuItems.Add(1, new MenuItem("Close", new EventHandler(this.cmdExit_Click)));
                m_notifyicon = new NotifyIcon();
                m_notifyicon.Text = "Right click for context menu";
                m_notifyicon.Visible = true;
                m_notifyicon.Icon = new Icon(Application.StartupPath + "\\Network.ico");
                m_notifyicon.ContextMenu = cMenu;
                ReadPrinter();
                //Connect button auto click
                cmdConnect_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ReadPrinter()
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\PrinterSetting.txt"))
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\PrinterSetting.txt");
                    Program.MachiningPrinterIP = sr.ReadLine().Split('=')[1].Trim();
                    Program.MachiningPrinterPort = sr.ReadLine().Split('=')[1].Trim();
                    Program.FinalPackingPrinterIP = sr.ReadLine().Split('=')[1].Trim();
                    Program.FinalPackingPrinterPort = sr.ReadLine().Split('=')[1].Trim();
                    Program.PrinterName = sr.ReadLine().Split('=')[1].Trim();
                    sr.Close();
                }
                else
                    MessageBox.Show("Printer setting file not found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Show_Click(Object sender, EventArgs e)
        {
            this.Show();
        }

        private void cmdHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region DBSetting Panel

        private void cmdDbServer_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                setDBSettingPnl();
                pnlDBSeting.Visible = true;
                pnlDBSeting.SetBounds(0, 0, 382, 341);
                pnlDBSeting.Dock = DockStyle.Fill;
                // pnlImage.SendToBack();
                pnlDBSeting.BringToFront();
                cmdConnect.Enabled = false;
                cmdDisconnect.Enabled = false;

                if (File.Exists(strConfig))
                {
                    using (StreamReader oSr = new StreamReader(strConfig))
                    {
                        //while (!oSr.EndOfStream)
                        //{
                        txtConString.Text = oSr.ReadLine();
                        txtPrinterName.Text = oSr.ReadLine();
                        oSr.Close();
                    }
                    // }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbServer_Enter(object sender, EventArgs e)
        {
            try
            {
                if (cmbServer.Items.Count == 0)
                {
                    FillComboBox(cmbServer, getDBServer(), true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sever Not Found!");
            }
        }

        private void cmbSchema_Enter(object sender, EventArgs e)
        {
            try
            {
                if (cmbSchema.Items.Count == 0)
                {
                    FillComboBox(cmbSchema, getDBSchema(cmbServer.Text.ToString(), txtUserID.Text.Trim(), txtPwd.Text.Trim()), true);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdTestCon_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbServer.Text.Trim() != string.Empty)
                {
                    string strCon = "Data Source=" + cmbServer.Text.Trim() + ";Initial Catalog=" + cmbSchema.SelectedValue.ToString() + ";";
                    strCon = strCon + " User ID=" + txtUserID.Text.Trim() + ";";
                    txtConString.Text = strCon + "Password = ******;";
                    strCon = strCon + "Password = " + txtPwd.Text.Trim() + ";";
                    SqlConnection oCon = new SqlConnection(strCon);
                    oCon.Open();
                    oCon.Close();
                    MessageBox.Show("Test Connection Sucessfully!");
                }
            }
            catch (SqlException oSql)
            {
                MessageBox.Show(oSql.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strCon = "Data Source=" + cmbServer.Text.Trim() + ";Initial Catalog=" + cmbSchema.SelectedValue.ToString() + ";";
                strCon = strCon + " User ID=" + txtUserID.Text.Trim() + ";";
                txtConString.Text = strCon + "Password = ******;";
                strCnn = strCon + "Password = " + txtPwd.Text.Trim() + ";";
                SqlConnection oCon = new SqlConnection(strCnn);
                oCon.Open();
                oCon.Close();
                FileInfo oFile = new FileInfo(strConfig);
                if (txtConString.Text == "")
                {
                    MessageBox.Show("Please configure database setting.");
                }
                else
                {
                    using (StreamWriter oSw = new StreamWriter(strConfig))
                    {
                        //if (!oFile.Exists)
                        //{ oSw = new StreamWriter(strConfig); }
                        //else
                        //{ oSw = new StreamWriter(strConfig1); }
                        oSw.WriteLine(strCnn);
                        oSw.WriteLine(txtPrinterName.Text.Trim());
                        oSw.Close();
                        setDBSettingPnl();
                        MessageBox.Show("Operation Successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlImage.BringToFront();

                if (isStart)
                {
                    cmdDisconnect.Enabled = true;
                }
                else
                {
                    cmdConnect.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region View Clients Panel

        private void cmdClients_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlClients.Visible = true;
                pnlClients.SetBounds(0, 0, 382, 341);
                pnlClients.Dock = DockStyle.Fill;
                // pnlImage.SendToBack();
                pnlClients.BringToFront();
                cmdConnect.Enabled = false;
                cmdDisconnect.Enabled = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdClientBack_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlImage.BringToFront();

                if (isStart)
                {
                    cmdDisconnect.Enabled = true;
                }
                else
                {
                    cmdConnect.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region View Logs Panel

        private void cmdLog_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlLog.Visible = true;
                pnlLog.SetBounds(0, 0, 382, 341);
                pnlLog.BringToFront();
                pnlLog.Dock = DockStyle.Fill;

                cmdBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                // pnlImage.SendToBack();
                pnlLog.BringToFront();
                cmdConnect.Enabled = false;
                cmdDisconnect.Enabled = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdLogBack_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlImage.BringToFront();
                if (isStart)
                    cmdDisconnect.Enabled = true;
                else
                    cmdConnect.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

    }
}
