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
    public class PickingAdapter : RecyclerView.Adapter
    {
        public List<PickList> lstItem;
        public event EventHandler<int> ItemClick;
        Context context;
        public PickingAdapter(List<PickList> itemDetails, Context cont)
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
                PickingHolder vh = holder as PickingHolder;
                vh.txtPartNo.Text = lstItem[position].PartNo;
                vh.txtBarcode.Text = lstItem[position].Barcode.ToString();
              //  vh.txtScanQty.Text = lstItem[position].ScanQty.ToString();
                //Change Background color
                //if (lstItem[position].ScanQty >= lstItem[position].Qty)
                //{
                //    vh.txtPartNo.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                //    vh.txtQty.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                //    vh.txtScanQty.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                //}
                //else
                //{
                    vh.txtPartNo.SetBackgroundResource(Resource.Drawable.BorderStyle);
                    vh.txtBarcode.SetBackgroundResource(Resource.Drawable.BorderStyle);
                   // vh.txtScanQty.SetBackgroundResource(Resource.Drawable.BorderStyle);
                //}
            }
            catch (System.Exception ex) { Toast.MakeText(context, ex.Message, ToastLength.Long).Show(); }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            PickingHolder vh = null;
            try
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_Picking, parent, false);
                vh = new PickingHolder(itemView, OnClick);

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

    public class PickingHolder : RecyclerView.ViewHolder
    {
        public TextView txtPartNo;
        public TextView txtBarcode;
        //public TextView txtScanQty;
        public ImageButton imgbtnViewLoc;
        public PickingHolder(View itemview, Action<int> listener) : base(itemview)
        {
            try
            {
                txtPartNo = itemview.FindViewById<TextView>(Resource.Id.txtPartNo);
                txtBarcode = itemview.FindViewById<TextView>(Resource.Id.txtBarcode);
                //txtScanQty = itemview.FindViewById<TextView>(Resource.Id.txtScanQty);
               // imgbtnViewLoc = itemview.FindViewById<ImageButton>(Resource.Id.imgbtnViewLoc);
               // imgbtnViewLoc.Click += (sender, e) => listener(base.Position);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}