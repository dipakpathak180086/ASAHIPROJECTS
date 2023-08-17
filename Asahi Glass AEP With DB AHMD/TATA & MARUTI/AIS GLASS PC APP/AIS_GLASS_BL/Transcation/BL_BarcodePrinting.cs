using AIS_GLASS_PL;
using AIS_GLASS_DL;
using System.Data;


namespace AIS_GLASS_BL
{
   public class BL_BarcodePrinting
    {
        public DataTable BL_ExecuteTask(PL_BarcodePrinting objPl)
        {
            DL_BarcodePrinting objDl = new DL_BarcodePrinting();
            return objDl.DL_ExecuteTask(objPl);

        }

       
    }
}
