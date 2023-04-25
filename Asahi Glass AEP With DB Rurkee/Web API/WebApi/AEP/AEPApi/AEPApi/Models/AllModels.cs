using System.Collections.Generic;

namespace AEPApi.Models
{
    public enum EnumDbType { BIND_MODEL, SAVE };

    public class Common
    {
        public string CreatedBy { get; set; }
        public string  DbType { get; set; }
    }
    public class AEP_PRINTING : Common
    {

        public string PartNo { get; set; }
        public int Qty { get; set; }
        public string Barcode { get; set; }
        public string PrinterIp { get; set; }
        public string Customer { get; set; }
        public string PartName { get; set; }
        public string SerialNo { get; set; }
        public string MfgDate { get; set; }

        public string Response { get; set; }
        public string ErrorMessage { get; set; }

    }



}