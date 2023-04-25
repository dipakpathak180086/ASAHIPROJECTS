using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using AEPApi.Models;
using System.Data.SqlClient;

namespace AEPApi.Dal
{
    public class DataAccess
    {
        StringBuilder _SbQry;

        #region MODEL WISE AEP PRINTING

        public DataTable DL_EXECUTE(AEP_PRINTING opl)
        {
            DataTable DT = new DataTable();
            try
            {
                Datautility DU = new Datautility();
                SqlParameter[] parma = {
                                        new SqlParameter("@TYPE",opl.DbType),
                                          new SqlParameter("@PART_NO",opl.PartNo),
                                              new SqlParameter("@QTY",opl.Qty ),
                                                new SqlParameter("@CREATED_BY",opl.CreatedBy),
                                                new SqlParameter("@PRINTER_IP",opl.PrinterIp),
                                                new SqlParameter("@CUSTOMER",opl.Customer)

                                   };

                DT = DU.GetDataUsingProcedure("[PRC_AEP_PRINTNG]", parma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return DT;
        }

        #endregion

   
    }
}
