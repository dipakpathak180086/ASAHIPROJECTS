using AIS_GLASS_COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_GLASS_PL
{

    #region Group Master
    public class PL_ACTIVE_PART_MASTER : Common
    {
        public string PartNo { get; set; }

        public bool Active { get; set; }

    }
    #endregion

}
