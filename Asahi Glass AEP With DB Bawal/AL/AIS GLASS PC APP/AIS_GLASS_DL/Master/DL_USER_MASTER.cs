using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIS_GLASS_COMMON;
using AIS_GLASS_PL;

using SatoLib;


namespace AIS_GLASS_DL
{
    public class DL_USER_MASTER 
    {

        
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_USER_MASTER obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@Type", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@UserID", SqlDbType.VarChar, 50);
                param[1].Value = obj.UserId;
                param[2] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                param[2].Value = obj.Name;
                param[3] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
                param[3].Value = obj.Password;
                param[4] = new SqlParameter("@Group", SqlDbType.VarChar, 50);
                param[4].Value = obj.Group;
                param[5] = new SqlParameter("@NewPassword", SqlDbType.VarChar, 50);
                param[5].Value = obj.NewPassword;
                param[6] = new SqlParameter("@EmailId", SqlDbType.VarChar, 50);
                param[6].Value = obj.EmailId;
                param[7] = new SqlParameter("@EmpCode", SqlDbType.VarChar, 100);
                param[7].Value = obj.EmpCode;
                param[8] = new SqlParameter("@EmpDesignation", SqlDbType.VarChar, 50);
                param[8].Value = obj.Designation;
                param[9] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
                param[9].Value = obj.CreatedBy;
                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_UserMaster]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion      
    }
}
