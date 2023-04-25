using AIS_GLASS_BL;
using AIS_GLASS_COMMON;
using AIS_GLASS_PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIS_GLASS_PC_APP
{
    public partial class frmPartMaster : Form
    {


        #region Variables

        private BL_PART_MASTER _blObj = null;
        private PL_PART_MASTER _plObj = null;
        private bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmPartMaster()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_PART_MASTER();
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
                // this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                btnDelete.Enabled = false;
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
                Clear();
                GetCustomer();
                GetLine();
                BindGrid();

                txtInternalPartNo.Focus();
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
                string LineNo = ""; ;
               
                if (ValidateInput())
                {
                    foreach (var item in checkedListBoxLine.CheckedItems)
                    {
                        LineNo = LineNo + item.ToString() + ",";
                    }
                    LineNo = LineNo.TrimEnd(',');

                    _plObj = new PL_PART_MASTER();
                   
                    _plObj.InternalPartNo = txtInternalPartNo.Text.Trim().ToString();
                    _plObj.InternalPartName = txtInternalPartName.Text.Trim().ToString();
                    _plObj.CustomerPartNo = txtCustomerPartNo.Text.Trim().ToString();
                    _plObj.VendorCode = txtVendorCode.Text.Trim().ToString();
                    _plObj.PackSize = Convert.ToInt32(txtPackSize.Text.Trim().ToString());
                    _plObj.PrintQty = Convert.ToInt32(txtDefaultPrintQty.Text.Trim().ToString());
                    _plObj.Line = LineNo;
                    if (cmbCustomer.SelectedIndex > 0)
                    {
                        _plObj.CustomerCode = cmbCustomer.SelectedItem.ToString();
                    }
                    else
                    {
                        _plObj.CustomerCode = "";
                    }
                    if(chkQAEnable.Checked)
                    {
                        _plObj.IsQAEnable = "Y";
                    }
                    else
                    {
                        _plObj.IsQAEnable = "N";
                    }
                    _plObj.Separator = txtSeparator.Text.Trim().ToString();
                    _plObj.CreatedBy = GlobalVariable.mSatoAppsLoginUser;
                   
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        _plObj.DbType = "INSERT";
                        DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
                        if (dataTable.Rows.Count > 0)
                        {
                            if (dataTable.Rows[0]["RESULT"].ToString() == "Y")
                            {
                                btnReset_Click(sender, e);
                                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Saved successfully!!", 1);
                                frmModelMaster_Load(null, null);
                            }
                            else
                            {
                                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dataTable.Rows[0][0].ToString(), 3);
                            }
                        }
                    }
                    else // if updating data
                    {
                        _plObj.DbType = "UPDATE";
                        DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
                        if (dataTable.Rows.Count > 0)
                        {
                            if (dataTable.Rows[0]["RESULT"].ToString() == "Y")
                            {
                                btnReset_Click(sender, e);
                                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Updated successfully!!", 1);
                            }
                            else
                            {
                                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dataTable.Rows[0][0].ToString(), 3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Part already exist!!", 3);
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
                txtPartSearch.Text = "";
                Clear();
                BindGrid();
                GetLine();
                GetCustomer();
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInternalPartNo.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Internal Part No. can't be blank!!", 3);
                    return;
                }
                if (GlobalVariable.mStoCustomFunction.ConfirmationMsg(GlobalVariable.mSatoApps, "Äre you sure to delete the record !!"))
                {
                    _plObj = new PL_PART_MASTER();
                    _blObj = new BL_PART_MASTER();
                    _plObj.InternalPartNo = txtInternalPartNo.Text.Trim().ToString();
                    _plObj.CustomerPartNo = txtCustomerPartNo.Text.Trim().ToString();
                    _plObj.DbType = "DELETE";
                    DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
                    if (dataTable.Rows.Count > 0)
                    {
                        if (dataTable.Rows[0][0].ToString().StartsWith("Y"))
                        {
                            btnReset_Click(sender, e);
                            GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Deleted successfully!!", 1);
                            frmModelMaster_Load(null, null);
                        }
                        else
                        {
                            GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dataTable.Rows[0][0].ToString(), 3);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE constraint"))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "This is already in use!!!", 3);
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVariable.ExportInCSV(dgv);
            }
            catch (Exception ex)
            {

                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }
        #endregion

        #region Methods

        private void GetCustomer()
        {
            try
            {

                _plObj = new PL_PART_MASTER();
                _plObj.DbType = "GETCUSTOMERCODE";
                cmbCustomer.Items.Clear();
                cmbCustomer.Items.Add("--Select--");
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbCustomer.Items.Add(row["CustomerCode"].ToString());
                    }

                    cmbCustomer.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void GetLine()
        {
            try
            {

                _plObj = new PL_PART_MASTER();
                _plObj.DbType = "GETLINE";
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);

                if (dt.Rows.Count > 0)
                {
                    checkedListBoxLine.Items.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        checkedListBoxLine.Items.Add(row["Line"].ToString());
                    }
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
                txtInternalPartNo.Text = "";
                txtInternalPartName.Text = "";
                txtCustomerPartNo.Text = "";
                txtVendorCode.Text = "";
                txtPackSize.Text = "";
                txtDefaultPrintQty.Text = "1";
                cmbCustomer.SelectedIndex = -1;
                txtSeparator.Text = "";

                foreach (int i in checkedListBoxLine.CheckedIndices)
                {
                    checkedListBoxLine.SetItemCheckState(i, CheckState.Unchecked);
                }
                //checkedListBoxLine.CheckedItems = false;
                chkQAEnable.Checked = true;
                txtInternalPartNo.Enabled = true;
                txtCustomerPartNo.Enabled = true;
                txtInternalPartNo.Focus();
                btnDelete.Enabled = false;
                _IsUpdate = false;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }
        private void BindGrid()
        {
            try
            {
                _plObj = new PL_PART_MASTER();
                _blObj = new BL_PART_MASTER();
                _plObj.DbType = "SELECT";
                
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }

        private void BindlINEmAPPEDDATA(string PartNo)
        {
            try
            {
                _plObj = new PL_PART_MASTER();
                _blObj = new BL_PART_MASTER();
                _plObj.DbType = "GETLINEMAPPING";
                _plObj.InternalPartNo = PartNo;

                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
               
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < checkedListBoxLine.Items.Count; i++)
                        {
                            if (checkedListBoxLine.Items[i].ToString()== row["Line"].ToString())
                            checkedListBoxLine.SetItemChecked(i, true);
                        }
                    }
                    
                }
                else
                {
                    GetLine();
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
                if (txtInternalPartNo.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Internal Part No. can't be blank!!", 3);
                    txtInternalPartNo.Focus();
                    return false;
                }
               
                if (txtCustomerPartNo.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Customer Part No. can't be blank!!", 3);
                    txtCustomerPartNo.Focus();
                    return false;
                }
                
                if (txtVendorCode.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Vendor Code can't be blank!!", 3);
                    txtVendorCode.Focus();
                    return false;
                }
                if (cmbCustomer.SelectedIndex <= 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Customer can't be blank!!", 3);
                    cmbCustomer.Focus();
                    return false;
                }
                if (txtPackSize.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Pack Size can't be blank!!", 3);
                    txtPackSize.Focus();
                    return false;
                }
                if (Convert.ToInt32(txtDefaultPrintQty.Text.Trim())<= 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Default Print Qty can't be 0 !!", 3);
                    txtDefaultPrintQty.Focus();
                    return false;
                }

                if (checkedListBoxLine.CheckedItems.Count<=0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Line can't be blank !!", 3);
                    checkedListBoxLine.Focus();
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
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1)
                {
                    return;
                }
                Clear();
               
                txtInternalPartNo.Text = dgv.Rows[e.RowIndex].Cells["InternalPartNo"].Value.ToString();
                txtInternalPartName.Text = dgv.Rows[e.RowIndex].Cells["InternalPartName"].Value.ToString();
                txtCustomerPartNo.Text = dgv.Rows[e.RowIndex].Cells["CustomerPartNo"].Value.ToString();
                txtVendorCode.Text = dgv.Rows[e.RowIndex].Cells["VendorCode"].Value.ToString();
                txtPackSize.Text = dgv.Rows[e.RowIndex].Cells["PackSize"].Value.ToString();
                if (dgv.Rows[e.RowIndex].Cells["CustomerCode"].Value.ToString() != "")
                {
                    cmbCustomer.SelectedItem = dgv.Rows[e.RowIndex].Cells["CustomerCode"].Value.ToString();
                }
                if (dgv.Rows[e.RowIndex].Cells["IsQAEnable"].Value.ToString()=="N")
                {
                    chkQAEnable.Checked = false;
                }
                else
                {
                    chkQAEnable.Checked = true;
                }
                txtSeparator.Text = dgv.Rows[e.RowIndex].Cells["Separator"].Value.ToString();
                txtDefaultPrintQty.Text = dgv.Rows[e.RowIndex].Cells["PrintQty"].Value.ToString();
                BindlINEmAPPEDDATA(dgv.Rows[e.RowIndex].Cells["InternalPartNo"].Value.ToString());
               


                btnDelete.Enabled = true;
                txtInternalPartNo.Enabled=false;
                txtCustomerPartNo.Enabled = false;
                _IsUpdate = true;
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

        #region TextBox Event

        private void txtPartSearch_TextChanged(object sender, EventArgs e)
        {
            (dgv.DataSource as DataTable).DefaultView.RowFilter = string.Format("InternalPartNo LIKE '%{0}%'", txtPartSearch.Text);
        }


        #endregion

       
        private void txtPackSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            GlobalVariable.allowOnlyNumeric(sender, e);
        }

        private void chkQAEnable_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtDefaultPrintQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            GlobalVariable.allowOnlyNumeric(sender, e);
        }
    }
}
