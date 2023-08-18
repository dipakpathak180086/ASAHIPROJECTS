
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
    public partial class frmBarcodePrinting : Form
    {

        #region Variables

        private BL_BarcodePrinting _blObj = null;
        private PL_BarcodePrinting _plObj = null;
        private Common _comObj = null;
        private string _packType = string.Empty;
        private DataTable dtBindGrid = new DataTable();
        DataTable dt;
        #endregion

        #region Form Methods

        public frmBarcodePrinting()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_BarcodePrinting();
                _plObj = new PL_BarcodePrinting();
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
                GetLine();

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
               
                if (cmbPrinterLine.SelectedIndex <= 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Select Line", 2);
                    cmbPrinterLine.Focus();
                    return;
                }
                if (cmbPartNo.SelectedIndex <= 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Select Part", 2);
                    cmbPartNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtEnterQty.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Enter Print Qty", 2);
                    txtEnterQty.Focus();
                    return;
                }
                if (Convert.ToInt32(txtEnterQty.Text) == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Enter Print Qty can't be Zero", 2);
                    txtEnterQty.Focus();
                    return;
                }
                _plObj = new PL_BarcodePrinting();
                _plObj.DbType = "SAVE";
                _plObj.PrinterLine = cmbPrinterLine.Text.Trim();
                _plObj.AISPartNo = cmbPartNo.Text.Trim();
                _plObj.CustomerPartNo = lblCustomerPartNo.Text.Trim();
                _plObj.PrinterIp = GlobalVariable.mPrinterName = lblPrinterIP.Text.Trim();
                _plObj.Customer = "MARUTI";
                _plObj.Qty = 1;
                _plObj.CreatedBy = GlobalVariable.UserName;
                Cursor.Current = Cursors.WaitCursor;
                SatoPrinter printer = new SatoPrinter();
                //;
                //if (!printer.GetprinterStatus().Contains("OK_"))
                //{
                //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Printer is offline!!!", 3);
                //    Cursor.Current = Cursors.Default;
                //    return;
                //}
                int iCounter = 0;
                DataTable dtBind = new DataTable();
                dtBind.Columns.Add("Barcode");
                for (int i = 1; i <= Convert.ToInt32(txtEnterQty.Text.Trim()); i++)
                {
                    dtBindGrid = _blObj.BL_ExecuteTask(_plObj);
                    if (dtBindGrid.Rows.Count > 0)
                    {
                        //DataTable dtFinal = dtBindGrid.AsDataView().ToTable(true, "Barcode");
                        _plObj.Barcode = dtBindGrid.Rows[0]["BARCODE"].ToString();
                        _plObj.SrNo = dtBindGrid.Rows[0]["SERIAL_NO"].ToString();

                        printer.PrintPartBarcode(_plObj);
                        DataRow dr = dtBind.NewRow();
                        dr[0] = _plObj.Barcode;
                        dtBind.Rows.Add(dr);
                       
                      
                    }
                    iCounter = iCounter + 1;
                    lblPrintCounter.Text = iCounter + "/" + txtEnterQty.Text.Trim();
                    Application.DoEvents();
                }
                dgv.DataSource = dtBind.DefaultView;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
                Cursor.Current = Cursors.Default;
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

        private void GetLine()
        {
            try
            {
                _plObj = new PL_BarcodePrinting();
                _plObj.DbType = "BIND_LINE";
              
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);

                if (dt.Rows.Count > 0)
                {
                    GlobalVariable.BindCombo(cmbPrinterLine, dt);
                }

            }
            catch (Exception ex)
            {

                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void GetPartNo()
        {
            try
            {

                _plObj = new PL_BarcodePrinting();
                _plObj.DbType = "BIND_MODEL";
                _plObj.PrinterIp = lblPrinterIP.Text.Trim();
                _plObj.Customer = "MARUTI";
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);

                if (dt.Rows.Count > 0)
                {
                    GlobalVariable.BindCombo(cmbPartNo, dt);
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

                if (cmbPrinterLine.SelectedIndex > 0) { cmbPrinterLine.SelectedIndex = 0; }
                if (cmbPartNo.SelectedIndex > 0) { cmbPartNo.SelectedIndex = 0; }
                lblPrinterIP.Text=lblCustomerPartNo.Text = "XXXXXXXXXXXXXXX";
                lblPrintCounter.Text = "0/0";
                for (int i = dgv.Rows.Count - 1; i >= 0; i--)
                {
                    dgv.Rows.RemoveAt(i);
                }
                txtEnterQty.Text = "";
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

        private void txtEnterQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            GlobalVariable.allowOnlyNumeric(sender, e);
        }


        #endregion

        private void cmbPrinterLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPrinterLine.SelectedIndex > 0)
            {
                try
                {
                    lblPrinterIP.Text = cmbPrinterLine.SelectedValue.ToString();
                   
                    GetPartNo();
                }  
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        private void cmbPartNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPartNo.SelectedIndex > 0)
            {
                try
                {
                    lblCustomerPartNo.Text = cmbPartNo.SelectedValue.ToString();
                    txtEnterQty.Focus();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        private void txtEnterQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEnterQty.Text))
            {
                if (Convert.ToInt32(txtEnterQty.Text) > 0)
                {
                    lblPrintCounter.Text = "0/" + txtEnterQty.Text;
                }
            }
        }
    }
}
