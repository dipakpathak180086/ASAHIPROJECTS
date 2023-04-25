using AIS_GLASS_PL;
using AIS_GLASS_DL;
using System.Data;


namespace AIS_GLASS_BL
{
   public class BL_BarcodeGeneration
    {
        public DataTable BL_ExecuteTask(PL_BarcodeGeneration objPl)
        {
            DL_BarcodeGeneration objDl = new DL_BarcodeGeneration();
            return objDl.DL_ExecuteTask(objPl);

        }

       
    }
}
