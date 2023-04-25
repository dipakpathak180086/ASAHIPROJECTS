using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IOCLAndroidApp.Models
{

   



    #region  Picking
    public class PickList
    {
        public string PartNo { get; set; }
        public string Barcode { get; set; }
       // public Decimal ScanQty { get; set; }
        public bool IsCustomerKanbanEnable { get; set; }
    }

    #endregion

    #region  Picking
    public class BarcodeStatus
    {
        public string PartNo { get; set; }
        public string Barcode { get; set; }
        public string Status { get; set; }
        public bool IsCustomerKanbanEnable { get; set; }
    }

    #endregion

    #region Picking Location
    public class ItemLocation
    {
        public string LocationCode { get; set; }
        public string ItemBarcode { get; set; }
        public Decimal ScanQty { get; set; }
    }

    #endregion
}