
using SatoLib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AIS_GLASS_COMMON
{
    public class Common : IDisposable
    {

        public string CreatedBy { get; set; }

        public int ModuleId { get; set; }
        public string DbType { get; set; }
        private SqlHelper _SqlHelper = null;
        public DataTable GetGroup()
        {

            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = "SELECT_GROUP";

                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_BIND_COMBO]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetPart()
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = "BIND_PART";

                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_BIND_COMBO]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetSystemDate()
        {

            _SqlHelper = new SqlHelper();
            try
            {

                string sdate = Convert.ToString(_SqlHelper.ExecuteScalar(GlobalVariable.mMainSqlConString, CommandType.Text, "SELECT CONVERT(VARCHAR(10),GETDATE(),103)"));
                return sdate;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetVersion(string sVersionName)
        {

            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[2];

                param[0] = new SqlParameter("@APP_NAME", SqlDbType.VarChar, 100);
                param[0].Value = "PC";
                param[1] = new SqlParameter("@APP_VERSION", SqlDbType.VarChar, 100);
                param[1].Value = sVersionName;

                DataTable dataTable = _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_GET_VERSION]", param).Tables[0];
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void SetModuleChildSectionRights(string sModule, bool updateflag, params Button[] buttons)
        {
            try
            {
                string btnName = string.Empty;
                StringBuilder _SbQry = new StringBuilder();
                _SqlHelper = new SqlHelper();
                SqlParameter[] param = new SqlParameter[2];

                param[0] = new SqlParameter("@GROUP_NAME", SqlDbType.VarChar, 100);
                param[0].Value = GlobalVariable.UserGroup;
                param[1] = new SqlParameter("@MODULE_NAME", SqlDbType.VarChar, 100);
                param[1].Value = sModule;
                DataTable dataTable = _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_SET_CHILDSEC_RIGHTS]", param).Tables[0];
                if (dataTable.Rows.Count > 0)
                {
                    foreach (var item in buttons)
                    {
                        if (item == null) { continue; }
                        btnName = item.Name.Remove(0, 3);
                        if (updateflag)
                        {
                            if (dataTable.Columns["Save"].ColumnName.Contains(btnName))
                            {
                                item.Enabled = (bool)dataTable.Rows[0]["Update"];

                            }
                        }
                        else
                        {
                            item.Enabled = (bool)dataTable.Rows[0]["Save"];

                        }
                        if (dataTable.Columns["Delete"].ColumnName.Contains(btnName))
                        {
                            item.Enabled = (bool)dataTable.Rows[0]["Delete"];
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }
        }




        //public string BoxLablePrint(PL_REPRINTING _PlObj)
        //{
        //    try
        //    {
        //        if (File.Exists(Application.StartupPath + "\\" + GlobalVariable.mBoxPrnFileName))
        //        {
        //            StreamReader sr = new StreamReader(Application.StartupPath + "\\" + GlobalVariable.mBoxPrnFileName);
        //            string PrnFileTemp = sr.ReadToEnd();
        //            sr.Close();

        //            //PrnFileTemp = PrnFileTemp.Replace("{VARPARTNO}", _PlObj.Part_No);
        //            //PrnFileTemp = PrnFileTemp.Replace("{VARPRDDATE}", _PlObj.PRD_Date_Append);
        //            //PrnFileTemp = PrnFileTemp.Replace("{VARLOTNO}", _PlObj.Lot_No_Append);
        //            //PrnFileTemp = PrnFileTemp.Replace("{VARPCKDATE}", _PlObj.PCK_Date_Append.ToString());
        //            //PrnFileTemp = PrnFileTemp.Replace("{VARQTY}", _PlObj.ComputeQty.ToString());
        //            //PrnFileTemp = PrnFileTemp.Replace("{VARBINNO}", _PlObj.Bin_No_Appned);
        //            //PrnFileTemp = PrnFileTemp.Replace("{VARSEALEDDATE}", _PlObj.Sealed_Date);
        //            //PrnFileTemp = PrnFileTemp.Replace("{VARBARLEN}", "DN" + _PlObj.Box_Barcode.Length.ToString().PadLeft(4, '0'));
        //            //PrnFileTemp = PrnFileTemp.Replace("{VARBARCODE}", _PlObj.Box_Barcode);
        //            return PrintBarcode.PrintCommand(PrnFileTemp, GlobalVariable.mPrinterName);
        //        }
        //        else
        //        {
        //            throw new NoNullAllowedException("Prn File " + GlobalVariable.mPalletPrnFileName + " not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void GetFirstAndSecondData(string[] arr, ref string rFirstData, ref string rSecondData)
        {
            StringBuilder stringBuilderFirst = new StringBuilder();
            StringBuilder stringBuilderSecond = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                if (i <= 3)
                {
                    stringBuilderFirst.Append(arr[i].ToString());
                    stringBuilderFirst.Append(",");
                    rFirstData = stringBuilderFirst.ToString();

                }
                else
                {
                    stringBuilderSecond.Append(arr[i].ToString());
                    stringBuilderSecond.Append(",");
                    rSecondData = stringBuilderSecond.ToString();
                }
            }
            rFirstData = rFirstData.TrimEnd(',');
            rSecondData = rSecondData.TrimEnd(',');

        }
        //public string PalletLablePrint(PL_PALLET_LABEL_PRINTING _PlObj)
        //{
        //    try
        //    {
        //        if (File.Exists(Application.StartupPath + "\\" + GlobalVariable.mPalletPrnFileName))
        //        {
        //            StreamReader sr = new StreamReader(Application.StartupPath + "\\" + GlobalVariable.mPalletPrnFileName);
        //            string PrnFileTemp = sr.ReadToEnd();
        //            sr.Close();

        //            PrnFileTemp = PrnFileTemp.Replace("{VARPARTNO}", _PlObj.Part_No);
        //            if (_PlObj.PRD_Date_Append.Split(',').Length > 4)
        //            {
        //                string firstPRDDate = string.Empty;
        //                string secondPRDDate = string.Empty;
        //                GetFirstAndSecondData(_PlObj.PRD_Date_Append.Split(','), ref firstPRDDate, ref secondPRDDate);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARPRDDATE1}", firstPRDDate);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARPRDDATE2}", secondPRDDate);
        //            }
        //            else
        //            {
        //                PrnFileTemp = PrnFileTemp.Replace("{VARPRDDATE1}", _PlObj.PRD_Date_Append);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARPRDDATE2}", "");
        //            }
        //            if (_PlObj.Lot_No_Append.Split(',').Length > 4)
        //            {
        //                string firstLotNo = string.Empty;
        //                string secondLotNo = string.Empty;
        //                GetFirstAndSecondData(_PlObj.Lot_No_Append.Split(','), ref firstLotNo, ref secondLotNo);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARLOTNO1}", firstLotNo);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARLOTNO2}", secondLotNo);
        //            }
        //            else
        //            {
        //                PrnFileTemp = PrnFileTemp.Replace("{VARLOTNO1}", _PlObj.Lot_No_Append);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARLOTNO2}", "");
        //            }
        //            if (_PlObj.PCK_Date_Append.Split(',').Length > 4)
        //            {
        //                string firstPCKDate = string.Empty;
        //                string secondPCKDate = string.Empty;
        //                GetFirstAndSecondData(_PlObj.PCK_Date_Append.Split(','), ref firstPCKDate, ref secondPCKDate);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARPCKDATE1}", firstPCKDate);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARPCKDATE2}", secondPCKDate);
        //            }
        //            else
        //            {
        //                PrnFileTemp = PrnFileTemp.Replace("{VARPCKDATE1}", _PlObj.PCK_Date_Append);
        //                PrnFileTemp = PrnFileTemp.Replace("{VARPCKDATE2}", "");
        //            }

        //            PrnFileTemp = PrnFileTemp.Replace("{VARQTY}", _PlObj.ComputeQty.ToString());
        //            PrnFileTemp = PrnFileTemp.Replace("{VARBARLEN}", "DN" + _PlObj.PalletBarcode.Length.ToString().PadLeft(4, '0'));
        //            PrnFileTemp = PrnFileTemp.Replace("{VARBARCODE}", _PlObj.PalletBarcode);
        //            return PrintBarcode.PrintCommand(PrnFileTemp, GlobalVariable.mPrinterName);
        //        }
        //        else
        //        {
        //            throw new NoNullAllowedException("Prn File " + GlobalVariable.mPalletPrnFileName + " not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }


    }
}
