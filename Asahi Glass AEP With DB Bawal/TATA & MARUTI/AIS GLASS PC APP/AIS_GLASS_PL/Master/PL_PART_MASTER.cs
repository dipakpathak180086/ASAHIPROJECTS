using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIS_GLASS_COMMON;


namespace AIS_GLASS_PL
{
    public class PL_PART_MASTER : Common
    {
        public string InternalPartNo { get; set; }
        public string InternalPartName { get; set; }
        public string CustomerPartNo { get; set; }
        public string VendorCode { get; set; }
        public int PackSize { get; set; }
        public string CustomerCode { get; set; }
        public string IsQAEnable { get; set; }
        public string Separator { get; set; }
        public int PrintQty { get; set; }
        public string Line { get; set; }
    }
}
