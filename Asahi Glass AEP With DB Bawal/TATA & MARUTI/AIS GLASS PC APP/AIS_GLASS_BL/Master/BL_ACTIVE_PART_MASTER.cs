

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIS_GLASS_PL;
using AIS_GLASS_DL;
using System.Data;

namespace AIS_GLASS_BL
{
  public  class BL_ACTIVE_PART_MASTER
    {
        public DataTable BL_ExecuteTask(PL_ACTIVE_PART_MASTER objPl)
        {
            DL_ACTIVE_PART_MASTER objDl = new DL_ACTIVE_PART_MASTER();
            return objDl.DL_ExecuteTask(objPl);
        }
    }
}
