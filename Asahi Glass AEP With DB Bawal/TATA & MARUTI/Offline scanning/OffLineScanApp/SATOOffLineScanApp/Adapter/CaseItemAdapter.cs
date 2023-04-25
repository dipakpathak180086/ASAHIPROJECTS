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
using IOCLAndroidApp.Models;

namespace SATOOffLineScanApp.Adapter
{
    public class CaseItemAdapter : BaseAdapter
    {
        private Context context;
        private List<ScanCaseModel> data;
        LayoutInflater inflater = null;

        public CaseItemAdapter(Context context, List<ScanCaseModel> models)
        {
            this.context = context;
            this.data = models;
            inflater = (LayoutInflater)context.
                    GetSystemService(Context.LayoutInflaterService);
        }

        public override int Count
        {
            get
            { return data.Count; }
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return data[position].ToString();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public class ViewHolder
        {

            public TextView PartNo;
            public TextView Barcode;
            public TextView Status;

            public ViewHolder(View v)
            {
                PartNo = (TextView)v.FindViewById(Resource.Id.listPart);
                Barcode = (TextView)v.FindViewById(Resource.Id.listBarcode);
                Status = (TextView)v.FindViewById(Resource.Id.listStatus);
            }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder;

            View view = inflater.Inflate(Resource.Layout.view_case_item, parent, false);

            holder = new ViewHolder(view);
            holder.PartNo.Text = data[position]._PartNo;
            holder.Barcode.Text = data[position]._Barcode;
            holder.Status.Text = data[position]._Status;
            return view;
        }
    }
}