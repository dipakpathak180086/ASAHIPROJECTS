using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using AIS_GLASS_BL;
using AIS_GLASS_PL;
using AIS_GLASS_COMMON;

namespace AIS_GLASS_PC_APP
{
    public partial class frmChangePassword : Form
    {
        #region Variables

        BL_USER_MASTER _blObj = null;
        PL_USER_MASTER _plObj = null;

        Common _comObj = null;

        #endregion

        #region Form Methods

        public frmChangePassword()
        {
            try
            {
                InitializeComponent();
                _plObj = new PL_USER_MASTER();
                _blObj = new BL_USER_MASTER();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "ERROR: " + ex.Message;
            }
        }

        private void frmModelMaster_Load(object sender, EventArgs e)
        {
            try
            {
               // this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                //this.FormBorderStyle = FormBorderStyle.None;
                //this.WindowState = FormWindowState.Normal;
               
                lblMessage.Text = "";
                txtUserID.Focus();
            }
            catch (Exception ex)
            {
                 GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,ex.Message, 3);
            }
        }

        #endregion

        #region Button Event
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (string.IsNullOrEmpty(txtUserID.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Enter User ID", 2);
                    txtUserID.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtOldPassword.Text))
                {
                     GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,"Enter Old Password", 2);
                    txtOldPassword.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                     GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,"Enter New Password", 2);
                    txtNewPassword.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtConfirmPassword.Text))
                {
                     GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,"Enter Confirm Password", 2);
                    txtConfirmPassword.Focus();
                    return;
                }

                if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
                {
                     GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,"Password does not match", 2);
                    txtNewPassword.Focus();
                    return;
                }

                _plObj.UserId = txtUserID.Text.Trim();
                _plObj.Password = txtOldPassword.Text.Trim();
                _plObj.NewPassword = txtNewPassword.Text.Trim();
                _plObj.DbType = "UPDATEPASSWORD";
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString().Trim().ToUpper() == "Y")
                    {
                        btnReset_Click(sender, e);
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Password changed successfully!!", 1);
                    }
                    else
                    {
                         GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,dt.Rows[0]["Result"].ToString(), 2);
                    }
                }
                else
                {
                     GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,"No data return form database", 2);
                }
            }
            catch (Exception ex)
            {
                 GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,ex.Message, 3);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtUserID.Text = "";

                lblMessage.Text = "";
                txtOldPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
            }
            catch (Exception ex)
            {
                 GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,ex.Message, 3);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Label Event
        private void lblMessage_DoubleClick(object sender, EventArgs e)
        {
           
        }

        #endregion
    }
}
