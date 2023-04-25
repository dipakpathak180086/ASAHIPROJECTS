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
   public class DL_REPORT
    {
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_REPORT obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[7];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@PartNo", SqlDbType.VarChar, 100);
                param[1].Value = obj.PartNo;
                param[2] = new SqlParameter("@PalletNo", SqlDbType.VarChar, 100);
                param[2].Value = obj.PalletNo;
                param[3] = new SqlParameter("@FROM_DATE", SqlDbType.VarChar, 50);
                param[3].Value = obj.FromDate;
                param[4] = new SqlParameter("@TO_DATE", SqlDbType.VarChar, 50);
                param[4].Value = obj.ToDate;
                param[5] = new SqlParameter("@RPT_Type", SqlDbType.VarChar, 50);
                param[5].Value = obj.RPT_Type;
                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_REPORT]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion   
    }
}
