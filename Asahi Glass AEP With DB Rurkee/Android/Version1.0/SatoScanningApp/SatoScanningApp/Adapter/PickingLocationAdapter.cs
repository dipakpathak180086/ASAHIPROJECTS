using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using IOCLAndroidApp.Models;
using Java.Lang;
using Exception = System.Exception;

namespace AISScanningApp.Adapter
{
    public class PickingLocationAdapter : RecyclerView.Adapter
    {
        public List<ItemLocation> lstItem;
        Context context;
        public PickingLocationAdapter(List<ItemLocation> itemDetails, Context cont)
        {
            lstItem = itemDetails;
            context = cont;
        }
        public override int ItemCount
        {
            get { return lstItem.Count; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            try
            {
                PickingLocationHolder vh = holder as PickingLocationHolder;
                vh.txtLocationCode.Text = lstItem[position].LocationCode;
                vh.txtItemBarcode.Text = lstItem[position].ItemBarcode;
                vh.txtQty.Text = lstItem[position].ScanQty.ToString();
            }
            catch (System.Exception ex) { Toast.MakeText(context, ex.Message, ToastLength.Long).Show(); }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            PickingLocationHolder vh = null;
            try
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_Pick_location, parent, false);
                vh = new PickingLocationHolder(itemView);
            }
            catch (Exception ex) { Toast.MakeText(context, ex.Message, ToastLength.Long).Show(); }
            return vh;
        }
    }

    public class PickingLocationHolder : RecyclerView.ViewHolder
    {
        public TextView txtLocationCode;
        public TextView txtItemBarcode;
        public TextView txtQty;
        public PickingLocationHolder(View itemview) : base(itemview)
        {
            try
            {
                txtLocationCode = itemview.FindViewById<TextView>(Resource.Id.txtLocationCode);
                txtItemBarcode = itemview.FindViewById<TextView>(Resource.Id.txtItemBarcode);
                txtQty = itemview.FindViewById<TextView>(Resource.Id.txtQty);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}