using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AISCOMServer
{
    static class Program
    {
        public static string PrinterName = "";
        public static string MachiningPrinterIP = "";
        public static string MachiningPrinterPort="";
        public static string FinalPackingPrinterIP = "";
        public static string FinalPackingPrinterPort = "";
        public static string MachiningPrnName = "MACHINING.prn";
        public static string TrolleyBox = "TROLLEYBOX.prn";
        public static string FinalPackingPrnName = "FINALPACKING.prn";
        public static string mMainSqlConString = "";
        public static string mSatoDbServer = "";
        public static string mSatoDb = "";
        public static string mSatoDbUser = "";
        public static string mSatoDbPassword = "";
        public static string mSatoApps = "SatoApps";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DirectoryInfo _dir = new DirectoryInfo(Application.StartupPath + "\\" + "SatoAppResource");
            
            _dir = new DirectoryInfo(Application.StartupPath + "\\" + "Log");
            if (_dir.Exists == false)
            {
                _dir.Create();
            }
            PopulateSystemSetting();
            Program.mMainSqlConString = "Server=" + Program.mSatoDbServer + "; Database=" + Program.mSatoDb + ";Uid=" + Program.mSatoDbUser + "; pwd=" + Program.mSatoDbPassword + "; pooling=true";
            bool CreatedOn;
            var mutex = new System.Threading.Mutex(true, "SatoCOMServer", out CreatedOn);
            if (!CreatedOn)
            {
                MessageBox.Show("Comm Server already running", "SatoCOMServer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            { Application.Run(new frmServer()); }
        }
        static bool ConnectToDatabase()
        {
            try
            {
                SqlConnection _sCon = new SqlConnection();
                _sCon.ConnectionString = Program.mMainSqlConString;
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
            FileInfo _fi = new FileInfo(Application.StartupPath + "\\DBSettings.txt");
            if (!_fi.Exists) { MessageBox.Show("System File Not Found !!!!", Program.mSatoApps, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); return; }
            StreamReader _sr = default(StreamReader);
            try
            {
                if (_fi.Exists == true)
                {
                    _fi = null;
                    _sr = new StreamReader(Application.StartupPath + "\\DBSettings.txt");
                    Program.mSatoDbServer = _sr.ReadLine();
                    Program.mSatoDb = _sr.ReadLine();
                    Program.mSatoDbUser = _sr.ReadLine();
                    Program.mSatoDbPassword = _sr.ReadLine();
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
