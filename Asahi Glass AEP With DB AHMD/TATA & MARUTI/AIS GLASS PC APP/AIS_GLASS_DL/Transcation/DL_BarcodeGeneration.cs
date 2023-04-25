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
   public class DL_BarcodeGeneration
    {
        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DL_ExecuteTask(PL_BarcodeGeneration obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[7];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@AISPartNo", SqlDbType.VarChar, 100);
                param[1].Value = obj.AISPartNo;
                param[2] = new SqlParameter("@FieldValue", SqlDbType.VarChar, 50);
                param[2].Value = obj.FieldValue;
                param[3] = new SqlParameter("@FieldName", SqlDbType.VarChar, 100);
                param[3].Value = obj.FieldName;
                param[4] = new SqlParameter("@ValueLength", SqlDbType.Int);
                param[4].Value = obj.ValueLength;
                param[5] = new SqlParameter("@ValueIndex", SqlDbType.Int);
                param[5].Value = obj.ValueIndex;
                param[6] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 100);
                param[6].Value = obj.CreatedBy;

                return _SqlHelper.ExecuteDataset(GlobalVariable.mMainSqlConString, CommandType.StoredProcedure, "[PRC_BarcodeGenerationIndex]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        #endregion   
    }
}
