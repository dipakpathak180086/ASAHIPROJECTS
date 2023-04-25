using Android.App;
using System;

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
        public static string FilePath = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        internal static string mSockIp = "";
        internal static Int32 mSockPort = 0;
        public static string UserId { get; set; }
        public static string UserName { get; set; }
        public static string UserGroup { get; set; }
        public static string Process_Type { get; set; }
        public static string Process_Header_Text { get; set; }
        public static string Db_Type { get; set; }
        public static string Selected_PartNo = "";

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
    }
}
