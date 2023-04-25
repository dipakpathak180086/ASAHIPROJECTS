
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
using System.Globalization;

namespace AIS_GLASS_PC_APP
{
    public partial class frmBarcodeGeneration : Form
    {

        #region Variables

        private BL_BarcodeGeneration _blObj = null;
        private PL_BarcodeGeneration _plObj = null;
        private Common _comObj = null;
        private string _packType = string.Empty;
        private DataTable dtBindGrid = new DataTable();
        DataTable dtPicklistNo = new DataTable();
        DataTable dtAddUpdateRunTime = new DataTable();
        DataTable dtAdd = new DataTable();
        private bool _IsUpdate = false;
        DataRow dr;
        private int rowIndex = 0;

        #endregion

        #region Form Methods

        public frmBarcodeGeneration()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_BarcodeGeneration();
                _plObj = new PL_BarcodeGeneration();
                dtAdd.Columns.Add("Field_Name");
                dtAdd.Columns.Add("Field_Value");
                dtAdd.Columns.Add("Value_Length");
                dtAdd.Columns.Add("Value_Index");
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void frmRMPicklistGeneration_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                Clear();
                BindGrid();
                GePartNo();
                //GeFields();

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
                GePartNo();
               // GeFields();
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


        private void Clear()
        {
            try
            {
                cmbAISPartNo.Items.Clear();
                cmbAISPartNo.Text = string.Empty;
                cmbAISPartNo.DataSource = null;
                cmbFieldName.Items.Clear();
                cmbFieldName.Text = string.Empty;
                cmbFieldName.DataSource = null;
                txtPartVal.Text = "";
                txtIndex.Text = "";
                txtLength.Text = "";
                lblDateFormat.Visible = false;
                chkDaily.Visible = false;
                chkMonthly.Visible = false;
                chkYearly.Visible = false;
                lblCount.Text = "Rows Count :0";
                cmbAISPartNo.Enabled = true;
                cmbFieldName.Enabled = true;
                txtPartVal.Enabled = true;
                btnAdd.Enabled = true;
                _IsUpdate = false;
                dtAdd.Clear();
                GePartNo();
               // GeFields();
                cmbAISPartNo.Focus();

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private bool ValidateInputs(bool p, string Mode)
        {
             if (cmbAISPartNo.SelectedIndex == -1)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please fill AIS Part No.", 3);
                cmbAISPartNo.Focus();
                return p;
            }

            if (Mode == "ADD")
            {
               if (cmbAISPartNo.SelectedIndex == -1)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please fill AIS Part No.", 3);
                    cmbAISPartNo.Focus();
                    return p;
                }
                else if (cmbFieldName.SelectedIndex == -1)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please Enter Field Name", 3);
                    cmbFieldName.Focus();
                    return p;
                }

                else if (txtPartVal.Text=="")
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please Enter Fields value", 3);
                    txtPartVal.Focus();
                    return p;
                }

                else if (txtLength.Text == "")
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please Enter fields Length", 3);
                    txtLength.Focus();
                    return p;
                }
                else if (Convert.ToInt32(txtLength.Text) <= 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please Enter fields Length", 3);
                    txtLength.Focus();
                    return p;
                }
                else if (txtIndex.Text=="")
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please Enter fields index", 3);
                    txtIndex.Focus();
                    return p;
                }
                else if (Convert.ToInt32(txtIndex.Text) <0)
                {
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please Enter fields index", 3);
                    txtIndex.Focus();
                    return p;
                }
            }
            return true;
        }

        private void GePartNo()
        {
            try
            {
                _plObj = new PL_BarcodeGeneration();
                _plObj.DbType = "GETPARTNO";
                cmbAISPartNo.Items.Clear();
                cmbAISPartNo.Items.Add("--Select--");
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbAISPartNo.Items.Add(row["InternalPartNo"].ToString());
                    }

                    cmbAISPartNo.SelectedIndex = 0;
                }
                
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void GeFields()
        {
            try
            {
                _plObj = new PL_BarcodeGeneration();
                _plObj.AISPartNo= cmbAISPartNo.SelectedItem.ToString();
                _plObj.DbType = "GETFIELDS";
                cmbFieldName.Items.Clear();
                cmbFieldName.Items.Add("--Select--");
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbFieldName.Items.Add(row["FieldName"].ToString());
                    }

                    cmbFieldName.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }
        private void BindGrid()
        {
            try
            {
                _plObj = new PL_BarcodeGeneration();
                _blObj = new BL_BarcodeGeneration();
                _plObj.DbType = "SELECT";
                dtBindGrid = _blObj.BL_ExecuteTask(_plObj);

                dgv.DataSource = dtBindGrid;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }


        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInputs(false, "ADD") == true)
            {
                 DataTable dt = new DataTable();
                _plObj = new PL_BarcodeGeneration();
                _blObj = new BL_BarcodeGeneration();
                _plObj.AISPartNo = cmbAISPartNo.SelectedItem.ToString();
                _plObj.FieldName = cmbFieldName.SelectedItem.ToString();
                _plObj.ValueIndex = Convert.ToInt32(txtIndex.Text);
                _plObj.DbType = "CHECKDUPINDEX";
                dt = _blObj.BL_ExecuteTask(_plObj);
                if (dt.Rows[0]["RESULT"].ToString() == "N")
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dt.Rows[0]["Msg"].ToString(), 3);
                    ClearBeforAdd();
                    return;
                }
            }

            if (_IsUpdate == false)
            {
                if (ValidateInputs(false, "ADD") == true)
                {

                    DataTable dt = new DataTable();
                    _plObj = new PL_BarcodeGeneration();
                    _blObj = new BL_BarcodeGeneration();
                    _plObj.AISPartNo = cmbAISPartNo.SelectedItem.ToString();
                    _plObj.FieldName = cmbFieldName.SelectedItem.ToString();
                    _plObj.ValueIndex = Convert.ToInt32(txtIndex.Text);
                    _plObj.DbType = "CHECKDUPFIELD";
                    dt = _blObj.BL_ExecuteTask(_plObj);
                    if (dt.Rows[0]["RESULT"].ToString() == "N")
                    {
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dt.Rows[0]["Msg"].ToString(), 3);
                        ClearBeforAdd();
                        return;
                    }

                    dgvShowSaveDetails.Visible = false;
                    dgvShowSaveDetails.Height = 0;
                    dgvAdd.Visible = true;
                    dgvAdd.Height = 236;
                    if (dtAdd.Rows.Count > 0)
                    {
                        DataRow[] foundAuthors = dtAdd.Select("Field_Name = '" + cmbFieldName.SelectedItem.ToString().Trim().ToString() + "' and Field_Value = '" + txtPartVal.Text.ToString() + "' and Value_Index = '" + txtIndex.Text.ToString() + "'  ");
                        DataRow[] Index = dtAdd.Select("Value_Index = '" + txtIndex.Text.ToString().Trim().ToString() + "'");
                        if (Index.Length != 0)
                        {
                            GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Field Index (" + txtIndex.Text.ToString().Trim().ToString() + ") Already Exist", 3);
                            ClearAdd();
                        }
                        else if (foundAuthors.Length != 0)
                        {
                            GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Field Name (" + cmbFieldName.SelectedItem.ToString().Trim().ToString() + ") Already Exist", 3);
                            ClearAdd();
                        }
                        else
                        {
                            dr = dtAdd.NewRow();
                            dr["Field_Name"] = cmbFieldName.SelectedItem.ToString().Trim().ToString();
                            dr["Field_Value"] = txtPartVal.Text.Trim().ToString();
                            dr["Value_Length"] = txtLength.Text;
                            dr["Value_Index"] = txtIndex.Text;
                            dtAdd.Rows.Add(dr);
                            dgvAdd.DataSource = dtAdd;
                            ClearAdd();
                        }
                    }
                    else
                    {
                        dr = dtAdd.NewRow();
                        dr["Field_Name"] = cmbFieldName.SelectedItem.ToString().Trim().ToString();
                        dr["Field_Value"] = txtPartVal.Text.Trim().ToString();
                        dr["Value_Length"] = txtLength.Text;
                        dr["Value_Index"] = txtIndex.Text;
                        dtAdd.Rows.Add(dr);
                        dgvAdd.DataSource = dtAdd;
                        ClearAdd();
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (dtAddUpdateRunTime.Rows.Count > 0 && dgvShowSaveDetails.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    DataTable dtNew = new DataTable();
                    dtNew = (DataTable)dgvShowSaveDetails.DataSource;
                    dt = dtNew.Copy();

                    dtAddUpdateRunTime.DefaultView.RowFilter = "FieldName = '" + cmbFieldName.SelectedItem.ToString().Trim().ToString() + "'";
                    DataTable dtd = (dtAddUpdateRunTime.DefaultView).ToTable();


                    //DataRow[] foundAuthors = dt.Select("FieldName = '" + cmbFieldName.SelectedItem.ToString().Trim().ToString() + "' and FieldValue = '" + txtPartVal.Text.ToString() + "' and ValueIndex = '" + txtIndex.Text.ToString() + "'  ");
                     DataRow[] Index = dt.Select("FieldName = '" + cmbFieldName.SelectedItem.ToString().Trim().ToString() + "' and FieldValue = '" + txtPartVal.Text.ToString() + "' and ValueIndex = '" + txtIndex.Text.ToString() + "'  ");
                    if (Index.Length != 0)
                    {
                        dgvShowSaveDetails.DataSource = dt;
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Field Index (" + txtIndex.Text.ToString().Trim().ToString() + ") Already Exist", 3);
                        ClearAdd();

                        return;
                    }
                    //else if (foundAuthors.Length != 0)
                    //{
                    //    dgvShowSaveDetails.DataSource = dt;
                    //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Field Name (" + cmbFieldName.SelectedItem.ToString().Trim().ToString() + ") Already Exist", 3);
                    //    ClearAdd();
                    //    return;
                    //}

                    // else
                    // {

                    if (dtd.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["FieldName"].ToString() == cmbFieldName.SelectedItem.ToString().Trim().ToString())
                            {
                                row.SetField("ValueLength", txtLength.Text);
                                row.SetField("ValueIndex", txtIndex.Text);
                                row.SetField("FieldValue", txtPartVal.Text);
                            }
                        }
                        dgvShowSaveDetails.DataSource = dt;
                        ClearAdd();
                    }
                    else
                    {
                        DataRow row;
                        row = dt.NewRow();
                        row["AIS_PartNo"] = cmbAISPartNo.SelectedItem.ToString().Trim().ToString();
                        dr["Field_Name"] = cmbFieldName.SelectedItem.ToString().Trim().ToString();
                        dr["FieldValue"] = txtPartVal.Text.Trim().ToString();
                        dr["ValueLength"] = txtLength.Text;
                        dr["ValueIndex"] = txtIndex.Text;
                        dt.Rows.Add(row);
                        dgvShowSaveDetails.DataSource = dt;
                        dgvShowSaveDetails.Update();
                        dgvShowSaveDetails.Refresh();
                        ClearAdd();
                    }
                }
            }
            // }
        }

        private void ClearAdd()
        {
            cmbAISPartNo.Enabled = false;
            cmbFieldName.SelectedIndex = -1;
            txtPartVal.Text = "";
            btnAdd.Enabled = false;
            txtIndex.Text = "";
            txtLength.Text ="";
            cmbFieldName.Focus();
            lblCount.Text = "Rows Count : " + dgvAdd.Rows.Count;
        }
        private void ClearBeforAdd()
        {
            cmbFieldName.SelectedIndex = -1;
            txtPartVal.Text = "";
            btnAdd.Enabled = false;
            txtIndex.Text = "";
            txtLength.Text = "";
            cmbFieldName.Focus();
        }

        private void cmbPartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAISPartNo.SelectedIndex>0)
            {
                GeFields();
                cmbFieldName.Focus();
            }
        }

     

        private void dgvAdd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvAdd.Columns["Delete"].Index && e.RowIndex >= 0) //make sure button index here
            {
                DataGridViewRow row = dgvAdd.Rows[e.RowIndex];
                string ss = row.Cells["Field_Value"].Value.ToString();

                for (int i = dtAdd.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtAdd.Rows[i];
                    if (dr["Field_Value"].ToString() == ss)
                        dr.Delete();
                }
                dtAdd.AcceptChanges();
                dgvAdd.DataSource = dtAdd;
                ClearAdd();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dataTable = new DataTable();
                DialogResult MessResult = MessageBox.Show("Do You Really Want To Save.?", "Save Confirmation", MessageBoxButtons.YesNo);
                if (MessResult == DialogResult.No)
                { return; }
                if (_IsUpdate == false)
                {
                    if (dgvAdd.Rows.Count > 0)
                    {
                        
                        if (cmbAISPartNo.SelectedIndex<=0)
                        { return; }

                        for (int i = 0; i < dgvAdd.Rows.Count; i++)
                        {
                            _plObj = new PL_BarcodeGeneration();
                            _plObj.CreatedBy = GlobalVariable.mSatoAppsLoginUser;
                            _plObj.AISPartNo =cmbAISPartNo.SelectedItem.ToString();
                            _plObj.FieldValue = dgvAdd.Rows[i].Cells["Field_Value"].Value.ToString();
                            _plObj.FieldName = dgvAdd.Rows[i].Cells["Field_Name"].Value.ToString();
                            _plObj.ValueLength = Convert.ToInt32( dgvAdd.Rows[i].Cells["Value_Length"].Value.ToString());
                            _plObj.ValueIndex = Convert.ToInt32(dgvAdd.Rows[i].Cells["Value_Index"].Value.ToString());
                            _plObj.DbType = "INSERT";
                            dataTable = _blObj.BL_ExecuteTask(_plObj);

                        }
                    }
                    else
                    {
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "At least one row grid!!!", 2);
                        return;
                    }
                }
                else
                {
                    if (dgvShowSaveDetails.Rows.Count > 0)
                    {
                        if (cmbAISPartNo.SelectedIndex <= 0)
                        { return; }

                        for (int i = 0; i < dgvShowSaveDetails.Rows.Count; i++)
                        {
                            _plObj = new PL_BarcodeGeneration();
                            _plObj.CreatedBy = GlobalVariable.mSatoAppsLoginUser;
                            _plObj.AISPartNo = dgvShowSaveDetails.Rows[i].Cells["AIS_PartNo"].Value.ToString();
                            _plObj.FieldValue = dgvShowSaveDetails.Rows[i].Cells["FieldValue"].Value.ToString();
                            _plObj.FieldName = dgvShowSaveDetails.Rows[i].Cells["FieldName"].Value.ToString();
                            _plObj.ValueLength = Convert.ToInt32(dgvShowSaveDetails.Rows[i].Cells["ValueLength"].Value.ToString());
                            _plObj.ValueIndex = Convert.ToInt32(dgvShowSaveDetails.Rows[i].Cells["ValueIndex"].Value.ToString());
                            _plObj.DbType = "UPDATE";
                            dataTable = _blObj.BL_ExecuteTask(_plObj);

                        }
                    }
                    else
                    {
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "At least one row grid!!!", 2);
                        return;
                    }
                }
                if (dataTable.Rows[0]["RESULT"].ToString() == "Y")
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Save successfully!!", 1);
                    dgvAdd.Visible = false;
                    dgvShowSaveDetails.Visible = true;
                    dgvShowSaveDetails.Height = 236;

                   
                   _plObj.DbType = "GETINDEXDETAILS";
                    DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                    dgvShowSaveDetails.DataSource = dt;
                    lblCount.Text = "Rows Count : " + dgvShowSaveDetails.Rows.Count;
                    dgvAdd.Height = 0;
                    Clear();
                    frmRMPicklistGeneration_Load(null, null);
                   
                }
            }

            catch (Exception ex)
            {

                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void dgvShowPickDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1)
                {
                    return;
                }
                Clear();
                cmbAISPartNo.SelectedItem = dgvShowSaveDetails.Rows[e.RowIndex].Cells["AIS_PartNo"].Value.ToString();
                cmbFieldName.SelectedItem = dgvShowSaveDetails.Rows[e.RowIndex].Cells["FieldName"].Value.ToString();
                txtPartVal.Text = dgvShowSaveDetails.Rows[e.RowIndex].Cells["FieldValue"].Value.ToString();
                txtLength.Text = dgvShowSaveDetails.Rows[e.RowIndex].Cells["ValueLength"].Value.ToString();
                txtIndex.Text = dgvShowSaveDetails.Rows[e.RowIndex].Cells["ValueIndex"].Value.ToString();

                _IsUpdate = true;
                cmbAISPartNo.Enabled = false;
                //cmbFieldName.Enabled = false;
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void lblHeader_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    dgvAdd.Visible = false;
                    dgvShowSaveDetails.Visible = true;
                    dgvShowSaveDetails.Height = 236;
                    dgvAdd.Height = 0;
                    _plObj = new PL_BarcodeGeneration();
                    _blObj = new BL_BarcodeGeneration();
                    _plObj.AISPartNo = dgv.Rows[e.RowIndex].Cells["AISPartNo"].Value.ToString();
                    _plObj.DbType = "GETINDEXDETAILS";
                    dtAddUpdateRunTime = _blObj.BL_ExecuteTask(_plObj);
                    dgvShowSaveDetails.DataSource = dtAddUpdateRunTime;
                    lblCount.Text = "Rows Count : " + dgvShowSaveDetails.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void dgvShowSaveDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvShowSaveDetails.Columns["DeletePart"].Index && e.RowIndex >= 0) //make sure button index here
            {
                DialogResult MessResult = MessageBox.Show("Do You Really Want To Delete.?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (MessResult == DialogResult.No)
                { return; }

                DataTable dt = new DataTable();
                DataGridViewRow row = dgvShowSaveDetails.Rows[e.RowIndex];
                _plObj = new PL_BarcodeGeneration();
                _blObj = new BL_BarcodeGeneration();
                _plObj.AISPartNo = row.Cells["AIS_PartNo"].Value.ToString();
                _plObj.FieldValue = row.Cells["FieldValue"].Value.ToString();
                _plObj.FieldName = row.Cells["FieldName"].Value.ToString();
                
                _plObj.DbType = "DELETE";
                dt = _blObj.BL_ExecuteTask(_plObj);

                if (dt.Rows[0]["RESULT"].ToString() == "Y")
                {
                    _plObj.AISPartNo = row.Cells["AIS_PartNo"].Value.ToString();
                    _plObj.DbType = "GETINDEXDETAILS";
                    dt = _blObj.BL_ExecuteTask(_plObj);
                    dgvShowSaveDetails.DataSource = dt;
                    BindGrid();
                    dtAdd.Clear();
                    dtAdd.AcceptChanges();
                    Clear();
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dt.Rows[0]["Msg"].ToString(), 2);
                    return;
                }
            }
        }

        private void cmbFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAISPartNo.SelectedIndex > 0)
            {
                if (cmbFieldName.SelectedIndex > 0)
                {
                    DataTable dt = new DataTable();
                    _plObj = new PL_BarcodeGeneration();
                    _blObj = new BL_BarcodeGeneration();
                    _plObj.AISPartNo = cmbAISPartNo.SelectedItem.ToString();
                    _plObj.DbType = cmbFieldName.SelectedItem.ToString();
                    if (cmbFieldName.SelectedItem.ToString() == "CustomerPart" || cmbFieldName.SelectedItem.ToString() == "VendorCode")
                    {
                        dt = _blObj.BL_ExecuteTask(_plObj);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        lblDateFormat.Visible = false;
                        chkDaily.Visible = false;
                        chkMonthly.Visible = false;
                        chkYearly.Visible = false;
                        txtPartVal.Text = dt.Rows[0][0].ToString();
                        txtLength.Text = txtPartVal.Text.Length.ToString();
                        txtLength.Focus();
                        btnAdd.Enabled = true;

                    }
                    else
                    {
                        if (cmbFieldName.SelectedItem.ToString() == "MfgFormat")
                        {
                            lblDateFormat.Visible = true;
                            chkDaily.Visible = true;
                            chkMonthly.Visible = true;
                            chkYearly.Visible = true;
                            chkMonthly.Checked = true;
                            txtPartVal.Text = "MMyy";//DateTime.Now.ToString("MMyy", CultureInfo.InvariantCulture);
                            txtLength.Text = txtPartVal.Text.Length.ToString();
                            txtPartVal.Focus();
                            btnAdd.Enabled = true;
                        }
                        //else
                        //{
                        //    lblDateFormat.Visible = false;
                        //    chkDaily.Visible = false;
                        //    chkMonthly.Visible = false;
                        //    chkYearly.Visible = false;
                        //}
                        //if (cmbFieldName.SelectedItem.ToString() == "MfgFormat")
                        //{
                        //    txtPartVal.Text = "MMyy";//DateTime.Now.ToString("MMyy", CultureInfo.InvariantCulture);
                        //    txtPartVal.Focus();
                        //    btnAdd.Enabled = true;
                        //}
                        //else
                        //{
                        //    txtPartVal.Text = "";
                        //    txtPartVal.Focus();
                        //    btnAdd.Enabled = true;
                        //}
                       else if (cmbFieldName.SelectedItem.ToString() == "SerialNo")
                        {
                            txtPartVal.Text = "SerialNo";//DateTime.Now.ToString("MMyy", CultureInfo.InvariantCulture);
                            txtPartVal.Enabled = false;
                            txtLength.Text = "";//txtPartVal.Text.Length.ToString();//txtPartVal.Text.Length();
                            txtPartVal.Focus();
                            btnAdd.Enabled = true;
                        }
                       else if (cmbFieldName.SelectedItem.ToString() == "Shift")
                        {
                            txtPartVal.Text = "Shift";//DateTime.Now.ToString("MMyy", CultureInfo.InvariantCulture);
                            txtPartVal.Enabled = false;
                            txtLength.Text = "1";
                            txtPartVal.Focus();
                            btnAdd.Enabled = true;
                        }
                        else
                        {
                            txtLength.Text = "";
                            txtPartVal.Text = "";
                            txtPartVal.Enabled = true;
                            txtPartVal.Focus();
                            btnAdd.Enabled = true;
                        }

                        //if (cmbFieldName.SelectedItem.ToString() == "Shift")
                        //{
                        //    txtPartVal.Text = "Shift";//DateTime.Now.ToString("MMyy", CultureInfo.InvariantCulture);
                        //    txtPartVal.Enabled = false;
                        //    txtPartVal.Focus();
                        //    btnAdd.Enabled = true;
                        //}
                        //else
                        //{
                        //    txtPartVal.Text = "";
                        //    txtPartVal.Enabled = true;
                        //    txtPartVal.Focus();
                        //    btnAdd.Enabled = true;
                        //}
                    }

                }
            }
            else
            {
                cmbAISPartNo.Focus();
                return;
            }
        }

        private void txtPartVal_Leave(object sender, EventArgs e)
        {
            if (txtPartVal.Text.Length > 0)
            {
                //if (txtPartVal.Text== "Shift")
                //{
                //    txtLength.Text = "1";

                //}
                //else
                //{
                    txtLength.Text = txtPartVal.Text.Length.ToString();
                //}
            }
            else
            {
                txtLength.Text = "";
            }

        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            GlobalVariable.allowOnlyNumeric(sender, e);
        }

        private void txtIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            GlobalVariable.allowOnlyNumeric(sender, e);
        }

        private void chkDaily_Click(object sender, EventArgs e)
        {
            if (chkDaily.Checked)
            {
                txtPartVal.Text = "ddMMyy";//DateTime.Now.ToString("ddMMyy", CultureInfo.InvariantCulture);
                txtLength.Text = txtPartVal.Text.Length.ToString();
                chkMonthly.Checked = false;
                chkYearly.Checked = false;
            }
            else
            {
                txtPartVal.Text = "";
            }
        }

        private void chkMonthly_Click(object sender, EventArgs e)
        {
            if (chkMonthly.Checked)
            {
                txtPartVal.Text = "MMyy";//DateTime.Now.ToString("MMyy", CultureInfo.InvariantCulture);
                txtLength.Text = txtPartVal.Text.Length.ToString();
                chkDaily.Checked = false;
                chkYearly.Checked = false;
            }
            else
            {
                txtPartVal.Text = "";
            }
        }

        private void chkYearly_Click(object sender, EventArgs e)
        {
            if (chkYearly.Checked)
            {
                txtPartVal.Text = "yy";//DateTime.Now.ToString("yy", CultureInfo.InvariantCulture);
                txtLength.Text = txtPartVal.Text.Length.ToString();
                chkDaily.Checked = false;
                chkMonthly.Checked = false;
            }
            else
            {
                txtPartVal.Text = "";
            }

        }
    }
}
