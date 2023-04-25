using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AIS_GLASS_COMMON;

namespace AIS_GLASS_PC_APP
{
    public partial class frmAccessPassword : Form
    {
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsCancel { get; set; }


        public frmAccessPassword()
        {
            InitializeComponent();
            
        }
        private void Show_ToolTip(Control ctr, string msg)
        {
            toolTip1.SetToolTip(ctr, msg);
            toolTip1.Show(msg, ctr, 2000);

        }
        private void bltOk_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVariable.mAccessUser = "";
                Common common = new Common();
             //   common.GiveAccessToPrint("ACCESSUSER", txtUserName.Text.Trim(), txtpwd.Text.Trim());
                IsCancel = true;
                this.Close();
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            IsCancel = false;
        }

       
    }
}
