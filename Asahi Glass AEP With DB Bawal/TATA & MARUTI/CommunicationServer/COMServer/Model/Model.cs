namespace AISCOMServer
{

    #region User Master
    public class User : Common
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Group { get; set; }
    }

    #endregion



    #region Picking
    public class PL_Pallet : Common
    {
        public string PartNo { get; set; }
        public string WorkOrderNo { get; set; }
        public string PalletNo { get; set; }
        public string ItemBarcode { get; set; }
        public string LineNo { get; set; }

    }
    #endregion

}
