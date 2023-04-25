
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

    public class DL_PART_MASTER
    {
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_PART_MASTER obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[12];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("InternalPartNo", SqlDbType.VarChar, 50);
                param[1].Value = obj.InternalPartNo;
                param[2] = new SqlParameter("@InternalPartName", SqlDbType.VarChar, 50);
                param[2].Value = obj.InternalPartName;
                param[3] = new SqlParameter("@CustomerPartNo", SqlDbType.VarChar, 50);
                param[3].Value = obj.CustomerPartNo;
                param[4] = new SqlParameter("@VendorCode", SqlDbType.VarChar, 50);
                param[4].Value = obj.VendorCode;
                param[5] = new SqlParameter("@PackSize", SqlDbType.Int);
                param[5].Value = obj.PackSize;
                param[6] = new SqlParameter("@CustomerCode", SqlDbType.VarChar, 50);
                param[6].Value = obj.CustomerCode;
                param[7] = new SqlParameter("@IsQAEnable", SqlDbType.VarChar, 1);
                param[7].Value = obj.IsQAEnable;
                param[8] = new SqlParameter("@Separator", SqlDbType.VarChar, 1);
                param[8].Value = obj.Separator;
                param[9] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
                param[9].Value = obj.CreatedBy;
                param[10] = new SqlParameter("@PrintQty", SqlDbType.Int);
                param[10].Value = obj.PrintQty;
                param[11] = new SqlParameter("@Line", SqlDbType.VarChar,500);
                param[11].Value = obj.Line;
                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_PartMaster]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion      

    }
}
