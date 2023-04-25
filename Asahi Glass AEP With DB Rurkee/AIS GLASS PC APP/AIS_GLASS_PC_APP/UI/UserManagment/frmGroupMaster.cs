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
    public partial class frmGroupMaster : Form
    {

        #region Variables

        BL_GROUP_MASTER _blObj = null;
        PL_GROUP_MASTER _plObj = null;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmGroupMaster()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_GROUP_MASTER();
                _plObj = new PL_GROUP_MASTER();
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
                this.WindowState = FormWindowState.Maximized;

                btnDelete.Enabled = false;
                GetGroup();
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
                txtGroupName.Focus();
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
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
                    _plObj = new PL_GROUP_MASTER();
                    _blObj = new BL_GROUP_MASTER();
                    _plObj.CreatedBy = GlobalVariable.mSatoAppsLoginUser;
                    _plObj.GroupName = txtGroupName.Text.Trim();

                    //If saving data
                    if (_IsUpdate == false)
                    {
                        _plObj.DbType = "INSERT";
                        _blObj.BL_ExecuteTask(_plObj);
                        foreach (DataGridViewRow row in dgvGroupRights.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells["HasRight"].Value) == true)
                            {
                                _plObj.DbType = "INSERT_GROUP_RIGHT";
                                _plObj.ModuleId = Convert.ToInt32(row.Cells["ModuleId"].Value.ToString());
                                _plObj.HasRight = true;
                                 _plObj.Add = row.Cells["Add"].Value == null  || row.Cells["Add"].Value.ToString() == "" ? false : Convert.ToBoolean(row.Cells["Add"].Value) == true ? true : false;
                                _plObj.Update = row.Cells["Update"].Value == null || row.Cells["Update"].Value.ToString()=="" ? false : Convert.ToBoolean(row.Cells["Update"].Value) == true ? true : false; 
                                _plObj.Delete = row.Cells["Delete"].Value == null || row.Cells["Delete"].Value.ToString() == "" ? false : Convert.ToBoolean(row.Cells["Delete"].Value) == true ? true : false;
                                _blObj.BL_ExecuteTask(_plObj);
                            }
                        }
                        btnReset_Click(sender, e);
                        GetGroup();
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Saved successfully!!", 1);
                    }
                    else // if updating data
                    {
                        _plObj.DbType = "DELETE_GROUP_RIGHT";
                        _blObj.BL_ExecuteTask(_plObj);
                        foreach (DataGridViewRow row in dgvGroupRights.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells["HasRight"].Value) == true)
                            {
                                _plObj.DbType = "INSERT_GROUP_RIGHT";
                                _plObj.ModuleId = Convert.ToInt32(row.Cells["ModuleId"].Value.ToString());
                                _plObj.HasRight = true;
                                _plObj.Add = row.Cells["Add"].Value == null  || row.Cells["Add"].Value.ToString() == "" ? false : Convert.ToBoolean(row.Cells["Add"].Value) == true ? true : false;
                                _plObj.Update = row.Cells["Update"].Value == null || row.Cells["Update"].Value.ToString()=="" ? false : Convert.ToBoolean(row.Cells["Update"].Value) == true ? true : false; 
                                _plObj.Delete = row.Cells["Delete"].Value == null || row.Cells["Delete"].Value.ToString() == "" ? false : Convert.ToBoolean(row.Cells["Delete"].Value) == true ? true : false;
                                _blObj.BL_ExecuteTask(_plObj);
                               // _plObj.HasRight = true;
                            }
                        }
                        btnReset_Click(sender, e);
                        GetGroup();
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Updated successfully!!", 1);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Group Name already exist!!", 3);
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtGroupName.Text = "";
                btnDelete.Enabled = false;
                txtGroupName.Enabled = true;
                _IsUpdate = false;
                chkAll.Checked = false;
                chkAll_CheckedChanged(sender, e);
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGroupName.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please select Group", 3);
                    return;
                }
                if (GlobalVariable.mStoCustomFunction.ConfirmationMsg(GlobalVariable.mSatoApps, "Äre you sure to delete the record !!"))
                {
                    _plObj = new PL_GROUP_MASTER();
                    _blObj = new BL_GROUP_MASTER();
                    _plObj.GroupName = txtGroupName.Text.Trim();
                    _plObj.DbType = "DELETE";
                    DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                    if (dt.Rows[0][0].ToString().ToUpper() == "SUCCESS")
                    {
                        btnReset_Click(sender, e);
                        GetGroup();
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Deleted successfully!!", 3);
                    }
                    else
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dt.Rows[0][0].ToString(), 3);
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

        #endregion

        #region Methods

        private void GetGroup()
        {
            try
            {
                _plObj = new PL_GROUP_MASTER();
                _blObj = new BL_GROUP_MASTER();
                _plObj.DbType = "SELECT";
                DataSet ds = _blObj.BL_ExecuteTaskAsDataset(_plObj);
                dgvGroupMaster.DataSource = ds.Tables[0];

                dgvGroupRights.DataSource = ds.Tables[1];

                lblCount.Text = "Rows Count : " + dgvGroupMaster.Rows.Count;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }


        private bool ValidateInput()
        {
            try
            {

                if (txtGroupName.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Group Name can't be blank!!", 3);
                    txtGroupName.Focus();
                    txtGroupName.SelectAll();
                    return false;
                }
                //Atleast one Module right
                bool IsChecked = false;
                foreach (DataGridViewRow row in dgvGroupRights.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["HasRight"].Value) == true)
                    {
                        IsChecked = true;
                        break;
                    }
                }
                if (IsChecked == false)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Select Atleast one module right!!", 3);
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Label Event


        #endregion

        #region CheckBox Event
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvGroupRights.Rows)
                {
                    row.Cells["HasRight"].Value = chkAll.Checked;
                    row.Cells["Add"].Value = chkAll.Checked;
                    row.Cells["Update"].Value = chkAll.Checked;
                    row.Cells["Delete"].Value = chkAll.Checked;
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        #endregion

        #region DataGridView Events
        private string getString(object o)

        {

            if (o == DBNull.Value) return null;

            return (string)o;

        }
        private void dgvGroupMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1)
                {
                    return;
                }
                chkAll.Checked = false;
                chkAll_CheckedChanged(sender, e);
                txtGroupName.Text = dgvGroupMaster.Rows[e.RowIndex].Cells["GroupName"].Value.ToString();
                btnDelete.Enabled = true;
                txtGroupName.Enabled = false;
                _IsUpdate = true;
                _plObj = new PL_GROUP_MASTER();
                _blObj = new BL_GROUP_MASTER();
                _plObj.GroupName = txtGroupName.Text.Trim();
                _plObj.DbType = "SELECTBYID";
                DataSet ds = _blObj.BL_ExecuteTaskAsDataset(_plObj);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (DataGridViewRow rowGrid in dgvGroupRights.Rows)
                    {
                        if (rowGrid.Cells["ModuleId"].Value.ToString() == row["ModuleId"].ToString())
                        {
                            rowGrid.Cells["HasRight"].Value = true;
                            //rowGrid.Cells["Add"].Value = true;
                            //rowGrid.Cells["Update"].Value = true;
                            //rowGrid.Cells["Delete"].Value = true;
                            rowGrid.Cells["Add"].Value = row["Add"].ToString() == "" ? Convert.ToBoolean(false) : Convert.ToBoolean(row["Add"]);
                            rowGrid.Cells["Update"].Value = row["Update"].ToString() == "" ? Convert.ToBoolean(false) : Convert.ToBoolean(row["Update"]);
                            rowGrid.Cells["Delete"].Value = row["Delete"].ToString() == "" ? Convert.ToBoolean(false) : Convert.ToBoolean(row["Delete"]);
                            break;
                        }
                    }
                }
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        #endregion

        
    }
}
