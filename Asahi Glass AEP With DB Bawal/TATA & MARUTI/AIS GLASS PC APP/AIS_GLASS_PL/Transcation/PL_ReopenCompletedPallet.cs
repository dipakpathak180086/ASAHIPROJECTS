using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIS_GLASS_COMMON;

namespace AIS_GLASS_PL
{
   public class PL_ReopenCompletedPallet : Common
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RPT_Type { get; set; }
        public string PalletNo { get; set; }
        public string PartNo { get; set; }
    }
}
