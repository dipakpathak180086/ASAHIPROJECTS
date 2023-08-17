using AIS_GLASS_COMMON;
using AIS_GLASS_PL;
using SatoLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_GLASS_DL
{
    public class DL_BarcodePrinting
    {
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_BarcodePrinting obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[6];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@PART_NO", SqlDbType.VarChar, 100);
                param[1].Value = obj.AISPartNo;
                param[2] = new SqlParameter("@QTY", SqlDbType.VarChar, 50);
                param[2].Value = obj.Qty;
                param[3] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 100);
                param[3].Value = obj.CreatedBy;
                param[4] = new SqlParameter("@PRINTER_IP", SqlDbType.VarChar, 100);
                param[4].Value = obj.PrinterIp;
                param[5] = new SqlParameter("@CUSTOMER", SqlDbType.VarChar);
                param[5].Value = obj.Customer;

                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_PC_PRINTNG]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
