using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIS_GLASS_COMMON;

namespace AIS_GLASS_PC_APP
{
    static class AppsGetUp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Dipak pathak
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DirectoryInfo _dir = new DirectoryInfo(Application.StartupPath + "\\" + "SatoAppResource");
            if (_dir.Exists == false)
            {
                _dir.Create();
            }
            _dir = new DirectoryInfo(Application.StartupPath + "\\" + "Log");
            if (_dir.Exists == false)
            {
                _dir.Create();
            }
            PopulateSystemSetting();
            SatoLib.SatoLogger _obj = new SatoLib.SatoLogger();
            _obj.ChangeInterval = SatoLib.SatoLogger.ChangeIntervals.ciHourly;
            _obj.EnableLogFiles = true;
            _obj.LogDays = 30;
            _obj.LogFilesExt = "log";
            _obj.LogFilesPath = Application.StartupPath;
            _obj.LogFilesPrefix = "SATO_AIS";
            _obj.StartLogging();
            _obj.LogMessage(SatoLib.EventNotice.EventTypes.evtInfo, "SatoAppsInitialize" + "  ::  Main", "Initializing Application.......");
            GlobalVariable.AppLog = _obj;
            _obj = null;
            GlobalVariable.mMainSqlConString = "Server=" + GlobalVariable.mSatoDbServer + "; Database=" + GlobalVariable.mSatoDb + ";Uid=" + GlobalVariable.mSatoDbUser + "; pwd=" + GlobalVariable.mSatoDbPassword + "; pooling=true";
            if (ConnectToDatabase() == false)
            {
                MessageBox.Show("Error in connecting to Database-Server" + "\n" + "Contact To Sato Support Engineer/Restart Application/Network Problem", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GlobalVariable.AppLog.StopLogging();
                Application.Exit();
            }
            else
            {
                Application.Run(new frmLogin());
            }
          
        }
        static bool ConnectToDatabase()
        {
            try
            {
                SqlConnection _sCon = new SqlConnection();
                _sCon.ConnectionString = GlobalVariable.mMainSqlConString;
                _sCon.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void PopulateSystemSetting()
        {
            FileInfo _fi = new FileInfo(Application.StartupPath + "\\System.ini");
            if (!_fi.Exists) { MessageBox.Show("System File Not Found !!!!", GlobalVariable.mSatoApps, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); return; }
            StreamReader _sr = default(StreamReader);
            string strLine = null;
            try
            {
                if (_fi.Exists == true)
                {
                    _fi = null;
                    string[] _strArr = null;
                    _sr = new StreamReader(Application.StartupPath + "\\System.ini");
                    while (!_sr.EndOfStream)
                    {
                        strLine = _sr.ReadLine();
                        _strArr = strLine.Split('=');
                        switch (_strArr[0])
                        {

                            case "SQL_DB_SERVER":
                                GlobalVariable.mSatoDbServer = _strArr[1].ToString().Trim();
                                break;
                            case "SQL_DB":
                                GlobalVariable.mSatoDb = _strArr[1].ToString().Trim();
                                break;
                            case "SQL_DB_USER":
                                GlobalVariable.mSatoDbUser = _strArr[1].ToString().Trim();
                                break;
                            case "SQL_DB_PASSWORD":
                                GlobalVariable.mSatoDbPassword = _strArr[1].ToString().Trim();
                                break;

                            case "PRINTER_NAME":
                                GlobalVariable.mPrinterName = _strArr[1].ToString().Trim();
                                break;
                            case "PortName":
                                GlobalVariable.PortName = _strArr[1].ToString().Trim();
                                break;
                            case "BaudRate":
                                GlobalVariable.BaudRate = _strArr[1].ToString().Trim();
                                break;
                            case "Parity":
                                GlobalVariable.Parity = _strArr[1].ToString().Trim();
                                break;
                            case "StopBits":
                                GlobalVariable.StopBits = _strArr[1].ToString().Trim();
                                break;
                            case "DataBits":
                                GlobalVariable.DataBits = _strArr[1].ToString().Trim();
                                break;
                            case "Handshake":
                                GlobalVariable.Handshake = _strArr[1].ToString().Trim();
                                break;
                        }
                    }
                    _sr.Close();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
