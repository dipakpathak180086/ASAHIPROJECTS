
using AIS_GLASS_BL;
using AIS_GLASS_COMMON;
using AIS_GLASS_PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AIS_GLASS_PC_APP
{
    public partial class frmReportPalletMapping : Form
    {

        #region Variables

        private BL_REPORT _blObj = null;
        private PL_REPORT _plObj = null;
        private Common _comObj = null;
        private string _packType = string.Empty;
        private DataTable dtBindGrid = new DataTable();
        DataTable dt;
        #endregion

        #region Form Methods

        public frmReportPalletMapping()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_REPORT();
                _plObj = new PL_REPORT();
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void frmReprinting_Load(object sender, EventArgs e)
        {
            try
            {
                //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                dpToDate_CloseUp(null, null);

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }

        #endregion

        #region Button Event
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
       
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {

                Clear();

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _plObj = new PL_REPORT();
                _plObj.DbType = "GETREPORTPALLETMAPPING";
                _plObj.FromDate = dpFromDate.Value.ToString("yyyy-MM-dd");
                _plObj.ToDate = dpToDate.Value.ToString("yyyy-MM-dd");
                if (cmbPallet.SelectedIndex > 0)
                {
                    _plObj.PalletNo = cmbPallet.SelectedItem.ToString();
                }
                else
                {
                    _plObj.PalletNo = "";
                }
                if (cmbPartNo.SelectedIndex > 0)
                {
                    _plObj.PartNo = cmbPartNo.SelectedItem.ToString();
                }
                else
                {
                    _plObj.PartNo = "";
                }
                dtBindGrid = _blObj.BL_ExecuteTask(_plObj);
                if (dtBindGrid.Rows.Count > 0)
                {
                    dgv.DataSource = dtBindGrid.DefaultView;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    lblcount.Text = "Count : " + dtBindGrid.Rows.Count;
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "No Data Found !!!", 2);
                    lblcount.Text = "Count : " + dtBindGrid.Rows.Count;
                    dgv.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            GlobalVariable.ExportInCSV(dgv);
        }
        #endregion

        #region Methods

        private void GetPartNo()
        {
            try
            {

                _plObj = new PL_REPORT();
                _plObj.DbType = "GETPARTNO";
                cmbPartNo.Items.Clear();
                cmbPartNo.Items.Add("--Select--");
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbPartNo.Items.Add(row["AISPartNo"].ToString());
                    }

                    cmbPartNo.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }
        private void GetPallet()
        {
            try
            {
                _plObj = new PL_REPORT();
                _plObj.DbType = "GETPALLETNO";
                cmbPallet.Items.Clear();
                cmbPallet.Items.Add("--Select--");
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbPallet.Items.Add(row["PalletNo"].ToString());
                    }

                    cmbPallet.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }

        }

        private void Clear()
        {
            try
            {
                
                cmbPallet.SelectedIndex = -1;
                cmbPartNo.SelectedIndex = -1;
                lblcount.Text = "Count : 0 ";
                for (int i = dgv.Rows.Count - 1; i >= 0; i--)
                {
                    dgv.Rows.RemoveAt(i);
                }
                dpFromDate.Value = dpFromDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        #endregion

        #region Label Event

        #endregion

        #region DataGridView Events


        #endregion

        #region TextBox Event
        private void dpToDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dpToDate_CloseUp(object sender, EventArgs e)
        {
            Clear();
            GetPallet();
            GetPartNo();

        }
        private void dpFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void dpToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        private void lblcount_Click(object sender, EventArgs e)
        {

        }
    }
}
