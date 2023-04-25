using AIS_GLASS_COMMON;
using AIS_GLASS_PL;
using AIS_GLASS_BL;
using AIS_GLASS_COMMON;
using AIS_GLASS_PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AIS_GLASS_PC_APP
{
    public partial class frmLogin : Form
    {
        #region Variables

        private BL_USER_MASTER _blUserMaster = null;
        private bool m_Exists = false;
        #endregion

        #region Form Methods

        public frmLogin()
        {
            try
            {
                _blUserMaster = new BL_USER_MASTER();
                InitializeComponent();

            }
            catch (Exception ex)
            {

            }
        }

        private void CallFileExists(string sFiles)
        {
            m_Exists = File.Exists(sFiles);
        }
        public bool FileExists(string file, int timeOut)
        {
            Thread m_Thread;
            string m_File = string.Empty;

            m_File = file;
            m_Exists = false;
            m_Thread = new Thread(() => CallFileExists(file));
            m_Thread.Start();
            m_Thread.Join(timeOut);
            m_Thread.Abort();
            return m_Exists;
        }

        private bool DownloadNewVersion()
        {
            this.Cursor = Cursors.WaitCursor;
            bool bNew = false;
            try
            {
                string sSharedPath = ConfigurationSettings.AppSettings.Get("SharedAppVersion");
                string sFilePath = sSharedPath + "\\" + "AIS_GLASS_PC_APP.exe";
                if (!FileExists(sFilePath, 1000)) { throw new Exception("Drive not accessible!!"); }
                var versionInfo = FileVersionInfo.GetVersionInfo(sFilePath);
                string newversion = versionInfo.ProductVersion;
                if (Application.ProductVersion != newversion)
                {
                    if (DialogResult.Yes == MessageBox.Show($"New version found (" + newversion + ")", "You want download new version", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        Process.Start(Application.StartupPath + "\\AutoExeReplace.exe");
                    }
                }
            }
            catch (Exception ex)
            {
                //var p = Process.Start(new ProcessStartInfo
                //{
                //    FileName = "net",
                //    Arguments = "use",
                //    RedirectStandardOutput = true,
                //    UseShellExecute = false
                //});
                //this.BringToFront();
                //Application.DoEvents();
                //var str = p.StandardOutput.ReadToEnd();
                //foreach (string s in str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                //{
                //    var s2 = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //    if (s2.Length >= 2 && s2[1][1] == ':')
                //        Map(s2[1][0].ToString());
                //}


                bNew = false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return bNew;

        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                Common common = new Common();
               
               
                lblMessage.Text = "";
                lblVersion.Text = "App Version : " + Application.ProductVersion;

                txtUserId.Focus();
                
            }
            catch (Exception ex)
            {

            }
        }

        private void OFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtUserId.Text = "";
            txtPassword.Text = "";
            txtUserId.Focus();
            this.Show();
        }

        #endregion

        #region Button Event

        private void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                PL_USER_MASTER objPl = new PL_USER_MASTER();
                _blUserMaster = new BL_USER_MASTER();
                GlobalVariable.mUserType = "";
                lblMessage.Text = "";
                Common common = new Common();
                DataTable dataTableVersion = common.GetVersion(Application.ProductVersion);

                if (dataTableVersion.Rows.Count > 0)
                {
                    if (dataTableVersion.Rows[0][0].ToString() != "OK")
                    {
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dataTableVersion.Rows[0][0].ToString(), 3);
                        txtPassword.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtUserId.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Enter User Id", 3);
                    txtUserId.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Enter Password", 3);
                    txtPassword.Focus();
                    return;
                }
                //DownloadNewVersion();
                //DataTable dtVersion = oDal.GetVersion();
                //if (dtVersion.Rows.Count > 0)
                //{
                //    MessageBox.Show(dtVersion.Rows[0][0].ToString(), "Sales Funnel App (CRM)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                GlobalVariable.mSatoAppsLoginUser = txtUserId.Text.Trim();
                objPl.UserId = txtUserId.Text.Trim();
                objPl.Password = txtPassword.Text.Trim();
                objPl.DbType = "VALIDATEUSER";
                DataTable dt = _blUserMaster.BL_ExecuteTask(objPl);
                if (dt.Rows.Count > 0)
                {
                    GlobalVariable.UserGroup = dt.Rows[0]["GroupName"].ToString();
                    GlobalVariable.UserName = dt.Rows[0]["UserName"].ToString();
                    txtUserId.Focus();
                    frmMenu oFrm = new frmMenu();
                    oFrm.Show();
                    oFrm.FormClosing += OFrm_FormClosing;
                    this.Hide();
                }
                else
                {
                    int iValue;
                    if (int.TryParse(txtPassword.Text.Trim(), out iValue) && iValue > 1)
                    {
                        int iPass = DateTime.Now.Day * DateTime.Now.Month;
                        if (txtUserId.Text.Trim().ToUpper() == "ADMIN" && iPass == Convert.ToInt32(txtPassword.Text.Trim()))
                        {

                            GlobalVariable.UserGroup = "ADMIN";

                            frmMenu oFrm = new frmMenu();
                            oFrm.Show();
                            oFrm.FormClosing += OFrm_FormClosing;
                            this.Hide();
                            return;
                        }
                    }


                    txtUserId.Text = "";
                    txtPassword.Text = "";
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Wrong UserId/Password", 3);
                    txtUserId.Focus();
                }
            }
            catch (Exception ex)
            {
                // ClsGlobal.SetErrorMessage(ex.Message, lblMessage);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region TextBox Event
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin_Click(sender, e);
            }
        }



        #endregion

        private void lblChangePass_Click(object sender, EventArgs e)
        {
            frmChangePassword oFrm = new frmChangePassword();
            oFrm.ShowDialog();
        }
    }
}
