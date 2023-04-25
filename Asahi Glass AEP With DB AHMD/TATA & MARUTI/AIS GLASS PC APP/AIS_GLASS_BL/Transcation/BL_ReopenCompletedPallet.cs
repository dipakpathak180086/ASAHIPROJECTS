using AIS_GLASS_PL;
using AIS_GLASS_DL;
using System.Data;

namespace AIS_GLASS_BL
{
   public class BL_ReopenCompletedPallet
    {
        public DataTable BL_ExecuteTask(PL_ReopenCompletedPallet objPl)
        {
            DL_ReopenCompletedPallet objDl = new DL_ReopenCompletedPallet();
            return objDl.DL_ExecuteTask(objPl);
        }
    }
}
