using AIS_GLASS_PL;
using AIS_GLASS_DL;
using System.Data;

namespace AIS_GLASS_BL
{
    public class BL_REPORT
    {
        public DataTable BL_ExecuteTask(PL_REPORT objPl)
        {
            DL_REPORT objDl = new DL_REPORT();
            return objDl.DL_ExecuteTask(objPl);
        }
    }
}
