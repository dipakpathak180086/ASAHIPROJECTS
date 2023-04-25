using Android.App;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace IOCLAndroidApp
{
    public enum MessageTitle
    {
        ERROR = 1,
        CONFIRM = 2,
        INFORMATION = 3,
    }

    public class clsGlobal : Activity
    {
        public static string FileFolder = "SatoFiles";
        public static string ServerIpFileName = "ServerIP.txt";
        public static string FileName = "BarcodeData.csv";
        public static string FilePath = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/SATO/";
        internal static string mSockIp = "";
        internal static Int32 mSockPort = 0;
        public static string UserId { get; set; }
        public static string UserName { get; set; }
        public static string UserGroup { get; set; }
        public static string Process_Type { get; set; }
        public static string Process_Header_Text { get; set; }
        public static string Db_Type { get; set; }
        public static string Selected_PartNo = "";
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public static Dictionary<string, string> dictionaryLogin = new Dictionary<string, string>();

        //***************************************
        public void ShowMessage(string msg, Activity activity, MessageTitle MsgTitle)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);
            builder.SetTitle(MsgTitle.ToString());
            builder.SetMessage(msg);
            builder.SetCancelable(false);
            builder.SetPositiveButton("OK", delegate { Finish(); });
            builder.Show();
        }

        public static DataTable ConvertCsvToDataTable(string filePath)
        {
            string[] rows = File.ReadAllLines(filePath);

            DataTable dtData = new DataTable();
            string[] rowValues = null;
            DataRow dr = dtData.NewRow();

            //Creating columns
            if (rows.Length > 0)
            {
                foreach (string columnName in rows[0].Split(','))
                    dtData.Columns.Add(columnName);
            }
            for (int row = 1; row < rows.Length; row++)
            {
                rowValues = rows[row].Split(',');
                dr = dtData.NewRow();
                dr.ItemArray = rowValues;
                dtData.Rows.Add(dr);
            }

            return dtData;
        }

        public static bool ReadBarcode()
        {
           bool breturn = false;
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            string Files = date.ToString();
            try
            {
                if (File.Exists(Path.Combine(FilePath, Files + FileName)))
                {
                    using (StreamReader sr = File.OpenText(Path.Combine(FilePath, Files + FileName)))
                    {
                        dictionary.Clear();
                        string[] lines = File.ReadAllLines(Path.Combine(FilePath, Files + FileName));
                        for (int x = 1; x < lines.Length; x++)
                        {
                            dictionary.Add(lines[x].Split(',')[2].ToString(), lines[x].Split(',')[2].ToString());
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return breturn;
        }

        public static bool ReadUserLogin()
        {
            bool breturn = false;
            try
            {
                if (File.Exists(Path.Combine(FilePath, "LoginUser.csv")))
                {
                    using (StreamReader sr = File.OpenText(Path.Combine(FilePath, "LoginUser.csv")))
                    {
                        dictionaryLogin.Clear();
                        string[] lines = File.ReadAllLines(Path.Combine(FilePath, "LoginUser.csv"));
                        for (int x = 1; x < lines.Length; x++)
                        {
                            dictionaryLogin.Add(lines[x], lines[x]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return breturn;
        }

        public static bool WriteFile(string LoginUser, string PartNo, string Barcode)
        {
            try
            {
                DateTime d = DateTime.Now;
                string date = DateTime.Now.ToString("ddMMyy");
                string Files = date.ToString();

                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }

                if (!File.Exists(Path.Combine(FilePath, Files + FileName)))
                {
                    File.Create(Path.Combine(FilePath, Files + FileName)).Dispose();

                    using (StreamWriter writer1 = new StreamWriter(Path.Combine(FilePath, Files + FileName), append: true))
                    {
                        writer1.WriteLine("Login User" + ',' + "Part No." + ',' + "Barcode" + ',' + "Scanned On" + ',');
                        writer1.WriteLine(LoginUser + ',' + PartNo + ',' + Barcode + ',' + DateTime.Now + ',');
                        writer1.Dispose();
                    }

                }
                else if (File.Exists(Path.Combine(FilePath, Files + FileName)))
                {
                    using (StreamWriter writer1 = new StreamWriter(Path.Combine(FilePath, Files + FileName), append: true))
                    {
                        writer1.WriteLine(LoginUser + ',' + PartNo + ',' + Barcode + ',' + DateTime.Now + ',');
                        writer1.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return true;
        }

    }
}
