using AIS_GLASS_COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_GLASS_PL
{
   public class PL_BarcodeGeneration : Common
    {
        public string AISPartNo { get; set; }
        public string FieldValue { get; set; }
        public string FieldName { get; set; }
        public int ValueLength { get; set; }
        public int ValueIndex { get; set; }
    }
}
