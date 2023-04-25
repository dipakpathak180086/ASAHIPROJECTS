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
    public class FGUnloadingAdapter : RecyclerView.Adapter
    {
        public List<BarcodeStatus> lstItem;
        public event EventHandler<int> ItemClick;
        Context context;
        public FGUnloadingAdapter(List<BarcodeStatus> itemDetails, Context cont)
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
                FgUnloadingHolder vh = holder as FgUnloadingHolder;
                vh.txtPartNo.Text = lstItem[position].PartNo;
                vh.TXTBARCODE.Text = lstItem[position].Barcode.ToString();
                vh.TXTSTATUS.Text = lstItem[position].Status.ToString();
               
                vh.txtPartNo.SetBackgroundResource(Resource.Drawable.BorderStyle);
                vh.TXTBARCODE.SetBackgroundResource(Resource.Drawable.BorderStyle);
                vh.TXTSTATUS.SetBackgroundResource(Resource.Drawable.BorderStyle);
               
            }
            catch (System.Exception ex) { Toast.MakeText(context, ex.Message, ToastLength.Long).Show(); }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            FgUnloadingHolder vh = null;
            try
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_Pick_Status, parent, false);
                vh = new FgUnloadingHolder(itemView, OnClick);

            }
            catch (Exception ex) { Toast.MakeText(context, ex.Message, ToastLength.Long).Show(); }
            return vh;
        }
        private void OnClick(int obj)
        {
            try
            {
                if (ItemClick != null)
                    ItemClick(this, obj);
            }
            catch (Exception ex) { Toast.MakeText(context, ex.Message, ToastLength.Long).Show(); }
        }
    }

    public class FgUnloadingHolder : RecyclerView.ViewHolder
    {
        public TextView txtPartNo;
        public TextView TXTBARCODE;
        public TextView TXTSTATUS;
        public FgUnloadingHolder(View itemview, Action<int> listener) : base(itemview)
        {
            try
            {
                txtPartNo = itemview.FindViewById<TextView>(Resource.Id.txtPartNo);
                TXTBARCODE = itemview.FindViewById<TextView>(Resource.Id.txtItemBarcode);
                TXTSTATUS = itemview.FindViewById<TextView>(Resource.Id.txtStatus);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}