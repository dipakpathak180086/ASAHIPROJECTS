using AIS_GLASS_BL;
using AIS_GLASS_COMMON;
using AIS_GLASS_PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AIS_GLASS_PC_APP
{
    public partial class frmActivePartMaster : Form
    {
        #region Variables

        private BL_ACTIVE_PART_MASTER _blObj = null;
        private PL_ACTIVE_PART_MASTER _plObj = null;
        private bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmActivePartMaster()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_ACTIVE_PART_MASTER();
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void frmModelMaster_Load(object sender, EventArgs e)
        {
            try
            {
                //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;


                GetPartNo();
                cbPartNo.Focus();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    _plObj = new PL_ACTIVE_PART_MASTER();
                    _plObj.PartNo = cbPartNo.Text.Trim();
                    _plObj.Active = chkQAEnable.Checked;
                    _plObj.CreatedBy = GlobalVariable.mSatoAppsLoginUser;
                    //If saving data
                    _plObj.DbType = "UPDATE";
                    DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
                    if (dataTable.Rows[0]["RESULT"].ToString() == "Y")
                    {
                        btnReset_Click(sender, e);
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Update successfully!!", 1);
                        frmModelMaster_Load(null, null);
                    }
                    else
                    {
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dataTable.Rows[0][0].ToString(), 3);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "UserId already exist!!", 3);
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                if (cbPartNo.SelectedIndex > 0)
                {
                    cbPartNo.SelectedIndex = 0;
                }
                _IsUpdate = false;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }

        private void GetPartNo()
        {
            try
            {
                _blObj = new BL_ACTIVE_PART_MASTER();
                _plObj = new PL_ACTIVE_PART_MASTER();
                _plObj.DbType = "BIND_PART";
                cbPartNo.Items.Clear();
                cbPartNo.Items.Add("--Select--");
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cbPartNo.Items.Add(row["PartNo"].ToString());
                    }

                    cbPartNo.SelectedIndex = 0;
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "User group data not found", 3);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }
        private void GetPartNoStatus()
        {
            try
            {
                _blObj = new BL_ACTIVE_PART_MASTER();
                _plObj = new PL_ACTIVE_PART_MASTER();
                _plObj.DbType = "GET_STATUS";
                _plObj.PartNo = cbPartNo.SelectedItem.ToString();
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                if (dt.Rows.Count > 0)
                {
                    chkQAEnable.Checked = Convert.ToBoolean(dt.Rows[0]["Active"]);
                }
               
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }




        private bool ValidateInput()
        {
            try
            {


                if (cbPartNo.SelectedIndex <= 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please select Part!!", 3);
                    cbPartNo.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Label Event

        #endregion

        #region DataGridView Events


        #endregion

        #region TextBox Event









        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbPartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPartNo.SelectedIndex > 0)
            {
                GetPartNoStatus();
            }
        }
    }
}
