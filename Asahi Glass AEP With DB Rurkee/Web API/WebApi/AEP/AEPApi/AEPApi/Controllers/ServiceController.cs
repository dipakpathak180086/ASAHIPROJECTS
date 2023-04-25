using AEPApi.Dal;
using AEPApi.Models;
using System;
using System.Data;
using System.Web.Http;

namespace AEPApi.Controllers
{
    public class ServiceController : ApiController
    {
        [HttpGet]
        public string GetTestValue()
        {
            return "Working";
        }



        #region AEP Barcode Printing Line

        /*
      * This action method will return the back no details
      */
        public string AddDoubleQuotes(string value)
        {
            return "\"" + value + "\"";
        }
        /// <summary>
        ///We have Bind the All Model Based on Customer and Lines 
        /// </summary>
        /// <param name="PrinterIp"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/service/GetModel/{PrinterIp}/{Customer}")]
        public AEP_PRINTING GetModelData(string PrinterIp,string Customer)
        {
            AEP_PRINTING _objAEP_PRINTING = new AEP_PRINTING();
            DataAccess dataAccess = new DataAccess();
            try
            {
                DataTable dtPart = dataAccess.DL_EXECUTE(new AEP_PRINTING { DbType = "BIND_MODEL",PrinterIp= PrinterIp, Customer = Customer });
                if (dtPart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPart.Rows.Count; i++)
                    {
                        _objAEP_PRINTING.PartNo += dtPart.Rows[i]["InternalPartNo"].ToString()+",";
                    }
                    _objAEP_PRINTING.PartNo = _objAEP_PRINTING.PartNo.TrimEnd(',');

                    _objAEP_PRINTING.Response = "Y";
                    _objAEP_PRINTING.ErrorMessage = "";

                }
                else
                {
                    _objAEP_PRINTING.Response = "N";
                    _objAEP_PRINTING.ErrorMessage = "Model  details not found ";
                }
            }
            catch (Exception ex)
            {
                _objAEP_PRINTING.Response = "N";
                _objAEP_PRINTING.ErrorMessage = "Error : " + ex.Message;
            }
            return _objAEP_PRINTING;
        }
        [HttpGet]
        [Route("api/service/GetQty/{PrinterIp}/{Customer}/{ModelName}")]
        public AEP_PRINTING GetQtyData(string PrinterIp, string Customer,string ModelName)
        {
            AEP_PRINTING _objAEP_PRINTING = new AEP_PRINTING();
            DataAccess dataAccess = new DataAccess();
            try
            {
                DataTable dtPart = dataAccess.DL_EXECUTE(new AEP_PRINTING { DbType = "GET_QTY", PrinterIp = PrinterIp, Customer = Customer,PartNo=ModelName });
                if (dtPart.Rows.Count > 0)
                {
                    _objAEP_PRINTING.Qty =Convert.ToInt32( dtPart.Rows[0]["PrintQty"].ToString());
                    _objAEP_PRINTING.Response = "Y";
                    _objAEP_PRINTING.ErrorMessage = "";

                }
                else
                {
                    _objAEP_PRINTING.Response = "N";
                    _objAEP_PRINTING.ErrorMessage = "Model  details not found ";
                }
            }
            catch (Exception ex)
            {
                _objAEP_PRINTING.Response = "N";
                _objAEP_PRINTING.ErrorMessage = "Error : " + ex.Message;
            }
            return _objAEP_PRINTING;
        }
        /*
        
        * Model parameter shoule be in the last otherwise AEP is having some problem
        */

        [HttpGet]
        [Route("api/service/Save/{ModelNo}/{EnterQty}/{CreatedBy}")]
        public AEP_PRINTING Save(string ModelNo,string EnterQty,string CreatedBy)
        {
            AEP_PRINTING _objAEP_PRINTING = new AEP_PRINTING();
            DataAccess dataAccess = new DataAccess();
            try
            {
                _objAEP_PRINTING.DbType = "SAVE";
                _objAEP_PRINTING.PartNo = ModelNo;
                _objAEP_PRINTING.Qty = Convert.ToInt32( EnterQty);
                _objAEP_PRINTING.CreatedBy = CreatedBy;
                DataTable dt = dataAccess.DL_EXECUTE(_objAEP_PRINTING);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["RESULT"].ToString() == "Y")
                    {
                        _objAEP_PRINTING.Barcode = dt.Rows[0]["BARCODE"].ToString();
                        if (dt.Columns.Count > 2)
                        {
                            _objAEP_PRINTING.PartName = dt.Rows[0]["PART_NAME"].ToString();
                            _objAEP_PRINTING.SerialNo = dt.Rows[0]["SERIAL_NO"].ToString();
                            _objAEP_PRINTING.MfgDate = dt.Rows[0]["MFG_DATE"].ToString();
                        }
                        _objAEP_PRINTING.Response = "Y";
                        _objAEP_PRINTING.ErrorMessage = "";
                    }
                    else
                    {
                        _objAEP_PRINTING.Response = "N";
                        _objAEP_PRINTING.ErrorMessage = "Data Not Saved";
                    }
                }
                else
                {
                    _objAEP_PRINTING.Response = "N";
                    _objAEP_PRINTING.ErrorMessage = "Could not be saved, details not returned";
                }
            }
            catch (Exception ex)
            {
                _objAEP_PRINTING.Response = "N";
                _objAEP_PRINTING.ErrorMessage = "Error : " + ex.Message;
            }
            return _objAEP_PRINTING;
        }


        #endregion
    }
}
