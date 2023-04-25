
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
    public partial class frmReopenCompletedPallet : Form
    {

        #region Variables

        private BL_ReopenCompletedPallet _blObj = null;
        private PL_ReopenCompletedPallet _plObj = null;
        private Common _comObj = null;
        private string _packType = string.Empty;
        private DataTable dtBindGrid = new DataTable();
        DataTable dt;
        #endregion

        #region Form Methods

        public frmReopenCompletedPallet()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_ReopenCompletedPallet();
                _plObj = new PL_ReopenCompletedPallet();
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
                
                cmbPartNo.Focus();

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
                GetPartNo();
                GetPallet();
                cmbPartNo.Focus();

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
                _plObj = new PL_ReopenCompletedPallet();
                _plObj.DbType = "REOPENCOMPLETEDPALLETNO";

                if (cmbPartNo.SelectedIndex > 0)
                {
                    _plObj.PartNo = cmbPartNo.SelectedItem.ToString();
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please select Part No. !!!", 2);
                    return;
                }
                if (cmbPallet.SelectedIndex > 0)
                {
                    _plObj.PalletNo = cmbPallet.SelectedItem.ToString();
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please select Pallet No. !!!", 2);
                    return;
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
                _plObj = new PL_ReopenCompletedPallet();
                _plObj.DbType = "GETPARTNO";
                _plObj.FromDate = dpFromDate.Value.ToString("yyyy-MM-dd");
                _plObj.ToDate = dpToDate.Value.ToString("yyyy-MM-dd");
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
                _plObj = new PL_ReopenCompletedPallet();
                _plObj.DbType = "GETPALLETNO";
                _plObj.FromDate = dpFromDate.Value.ToString("yyyy-MM-dd");
                _plObj.ToDate = dpToDate.Value.ToString("yyyy-MM-dd");
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
                cmbPartNo.Focus();
                lblcount.Text = "Count : 0 ";
                dgv.DataSource = null;
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

        private void btnReopen_Click(object sender, EventArgs e)
        {
            try
            {
                _plObj = new PL_ReopenCompletedPallet();
                _plObj.DbType = "UPDATE";
                _plObj.FromDate = dpFromDate.Value.ToString("yyyy-MM-dd");
                _plObj.ToDate = dpToDate.Value.ToString("yyyy-MM-dd");

                if (cmbPartNo.SelectedIndex > 0)
                {
                    _plObj.PartNo = cmbPartNo.SelectedItem.ToString();
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please select Part No. !!!", 2);
                    return;
                }
                if (cmbPallet.SelectedIndex > 0)
                {
                    _plObj.PalletNo = cmbPallet.SelectedItem.ToString();
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please select Pallet No. !!!", 2);
                    return;
                }
                if (dgv.RowCount > 0)
                {
                    DialogResult MessResult = MessageBox.Show("Do You Really Want To Reopen ?", "Reopen Confirmation", MessageBoxButtons.YesNo);
                    if (MessResult == DialogResult.No)
                    { return; }

                    dtBindGrid = _blObj.BL_ExecuteTask(_plObj);
                    if (dtBindGrid.Rows[0]["RESULT"].ToString() == "Y")
                    {
                        btnReset_Click(sender, e);
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Updated successfully!!", 1);
                    }
                    else
                    {
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dtBindGrid.Rows[0][0].ToString(), 3);
                    }
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please Search Data first !!!", 2);

                }

                

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void btnGetPartPallet_Click(object sender, EventArgs e)
        {
            GetPartNo();
            GetPallet();
        }
    }
}
