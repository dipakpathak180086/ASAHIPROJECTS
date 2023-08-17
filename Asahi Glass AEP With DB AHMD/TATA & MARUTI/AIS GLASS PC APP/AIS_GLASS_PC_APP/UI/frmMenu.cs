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
using AIS_GLASS_COMMON;
using AIS_GLASS_BL;
using AIS_GLASS_PL;
using AIS_GLASS_PL;
using AIS_GLASS_BL;

namespace AIS_GLASS_PC_APP
{
    public partial class frmMenu : Form
    {
        #region Variables




        #endregion

        #region Form Methods

        public frmMenu()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
        }
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                cp.ClassStyle |= 0x08;
                return cp;
            }
        }
        private void frmModelMaster_Load(object sender, EventArgs e)
        {
            try
            {
               // this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                //this.Bounds = Screen.PrimaryScreen.Bounds;
                //this.TopMost = true;
                SetMenuRight();
                lblWelcome.Text = "Hi! " + GlobalVariable.mSatoAppsLoginUser;
                Left = Top = 0;
                Width = Screen.PrimaryScreen.WorkingArea.Width;
                Height = Screen.PrimaryScreen.WorkingArea.Height;
                //AutoLogOut timer
                tbReport.SelectedIndex = 1;
                timerAutoLogOut.Enabled = true;
                //Reoiling Counter Timer
                
                timerReOiling.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void OFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        #endregion

        #region Button Event

        private void picLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Menu Click Events
        private void picChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword oFrm = new frmChangePassword();
            oFrm.ShowDialog(); 

        }
        private void picUserMaster_Click(object sender, EventArgs e)
        {
            frmUserMaster frm = new frmUserMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
        private void picGroupMaster_Click(object sender, EventArgs e)
        {
            frmGroupMaster frm = new frmGroupMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

       
      
        private void picPartMaster_Click(object sender, EventArgs e)
        {
            frmPartMaster frm = new frmPartMaster();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();

        }
       
        #endregion

        #region Method

        private void SetMenuRight()
        {
            try
            {
                DataTable dt;
                PL_GROUP_MASTER _plObj = new PL_GROUP_MASTER();
                BL_GROUP_MASTER _blObj = new BL_GROUP_MASTER();
                _plObj.DbType = "GET_USER_RIGHTS";
                _plObj.GroupName = GlobalVariable.UserGroup;
                if (GlobalVariable.UserGroup == "ADMIN")
                {
                    dt = _blObj.BL_ExecuteTask(_plObj);
                }
                else
                    dt = _blObj.BL_ExecuteTask(_plObj);

                foreach (DataRow row in dt.Rows)
                {
                    switch (row["ModuleId"].ToString())
                    {
                        case "101":
                            picGroupMaster.Enabled = true;
                            lblGroupMaster.Enabled = true;
                            break;
                        case "102":
                            picUserMaster.Enabled = true;
                            lblUserMaster.Enabled = true;
                            break;
                        case "103":
                            picPartMaster.Enabled = true;
                            lblPartMaster.Enabled = true;
                            break;
                        case "104":
                            picReportLabelPrinting.Enabled = true;
                            lblReportLabelPrinting.Enabled = true;
                            break;

                        case "105":
                            picReportPalletMapping.Enabled = true;
                            lblReportPalletMapping.Enabled = true;
                            break;

                        case "106":
                            picBarcodeGeneration.Enabled = true;
                            lblBarcodeGeneration.Enabled = true;
                            break;

                        case "107":
                            picReopenCompletedPallet.Enabled = true;
                            lblReopenCompletedPallet.Enabled = true;
                            break;
                        case "201":
                            picBulkPrinting.Enabled = true;
                            lblBulkPrinting.Enabled = true;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        #endregion

        #region Timer Event

        private void timerReOiling_Tick(object sender, EventArgs e)
        {

        }



        #endregion


        private void lblRollReturnFromFillingMachine_Click(object sender, EventArgs e)
        {

        }

        private void picReportLabelPrinting_Click(object sender, EventArgs e)
        {
            frmReportLabelPrinting frm = new frmReportLabelPrinting();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picReportPalletMapping_Click(object sender, EventArgs e)
        {
            frmReportPalletMapping frm = new frmReportPalletMapping();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picBarcodeGeneration_Click(object sender, EventArgs e)
        {
            frmBarcodeGeneration frm = new frmBarcodeGeneration();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picReopenCompletedPallet_Click(object sender, EventArgs e)
        {
            frmReopenCompletedPallet frm = new frmReopenCompletedPallet();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }

        private void picBulkPrinting_Click(object sender, EventArgs e)
        {
            frmBarcodePrinting frm = new frmBarcodePrinting();
            frm.Show();
            frm.FormClosing += OFrm_FormClosing;
            this.Hide();
        }
    }
}
