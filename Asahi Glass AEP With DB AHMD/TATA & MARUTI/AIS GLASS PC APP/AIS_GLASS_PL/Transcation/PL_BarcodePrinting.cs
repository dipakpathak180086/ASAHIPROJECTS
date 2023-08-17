using AIS_GLASS_COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_GLASS_PL
{
   public class PL_BarcodePrinting : Common
    {
        public string PrinterLine { get; set; }
        public string AISPartNo { get; set; }
        public string CustomerPartNo { get; set; }
        public int Qty { get; set; }
        public string Customer { get; set; }
        public string PrinterIp { get; set; }
        public string Barcode { get; set; }
        public string SrNo { get; set; }
    }
}
