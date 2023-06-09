﻿using SatoLib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AISCOMServer
{
    public class clsProcess
    {
        private clsMsgRule oRule = new clsMsgRule();
        private StringBuilder sb = null;
        private SqlHelper _SqlHelper = new SqlHelper();

        #region PALLET MAPPING
        public string PALLETMAPPING_ExecuteTask(PL_Pallet obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@PartNo", SqlDbType.VarChar, 100);
                param[1].Value = obj.PartNo;
                param[2] = new SqlParameter("@WorkOrderNo", SqlDbType.VarChar, 100);
                param[2].Value = obj.WorkOrderNo;
                param[3] = new SqlParameter("@PalletNo", SqlDbType.VarChar, 100);
                param[3].Value = obj.PalletNo;
                param[4] = new SqlParameter("@Barcode", SqlDbType.VarChar, 100);
                param[4].Value = obj.ItemBarcode;
                param[5] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 100);
                param[5].Value = obj.CreatedBy;

                DataTable dt = _SqlHelper.ExecuteDataset(Program.mMainSqlConString, CommandType.StoredProcedure, "[PRC_Pallet_Mapping]", param).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (obj.DbType == "BIND_PARTNO" || obj.DbType == "BIND_VIEW" || obj.DbType == "BIND_TOTAL_AND_SCAN_QTY" || obj.DbType == "VALIDATEPALLET")
                    {
                        oRule.sResponse = clsMsgRule.sValid + "~" + DtToString(dt);
                    }
                    else if (dt.Rows[0]["Result"].ToString() == "Y")
                    {
                        oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["MSG"].ToString();

                    }
                    else if (dt.Rows[0]["Result"].ToString() == "PALLETCOMPLETED")
                    {
                        oRule.sResponse = "PALLETCOMPLETED" + "~" + dt.Rows[0]["MSG"].ToString();

                    }
                    else
                    {
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["Result"].ToString();
                    }
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~No Data Found!!!";
                }
            }
            catch (Exception ex)
            {
                oRule.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString();
            }
            finally
            {

            }
            return oRule.sResponse;
        }
        #endregion

        #region PALLET MAPPING
        public string STATUS_ExecuteTask(PL_Pallet obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@Barcode", SqlDbType.VarChar, 100);
                param[1].Value = obj.ItemBarcode;
                
                DataTable dt = _SqlHelper.ExecuteDataset(Program.mMainSqlConString, CommandType.StoredProcedure, "[PRS_BarcodeStatus]", param).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "Barcode not exists in database")
                    {
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        oRule.sResponse = clsMsgRule.sValid + "~" + DtToString(dt);
                    }
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~No Data Found!!!";
                }
            }
            catch (Exception ex)
            {
                oRule.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString();
            }
            finally
            {

            }
            return oRule.sResponse;
        }
        #endregion

        #region CommonMethod
        public string DtToString(DataTable dt)
        {
            string sRow = string.Empty;
            string sDTString = string.Empty;
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sCol = string.Empty;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sCol = sCol + dt.Rows[i][j].ToString() + "$";
                    }
                    sRow = sRow + sCol.Substring(0, sCol.Length - 1) + "#";
                }
                sDTString = sRow.Substring(0, sRow.Length - 1);
            }
            return sDTString;
        }
        #endregion
    }
}
