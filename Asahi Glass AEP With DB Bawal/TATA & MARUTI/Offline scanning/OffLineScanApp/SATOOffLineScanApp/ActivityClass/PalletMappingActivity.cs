using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using IOCLAndroidApp;
using IOCLAndroidApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Linq;
using SATOOffLineScanApp.Adapter;

namespace SATOOffLineScanApp
{
    [Activity(Label = "Sato ScanningApp", WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class PalletMappingActivity : AppCompatActivity
    {
        List<ScanCaseModel> _listScanCase;
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        ListView listViewScanCase;
        ArrayAdapter<string> adapter = null;
        List<string> lst = null;
        private clsGlobal clsGLB;
        private clsNetwork oNetwork;
        private MediaPlayer mediaPlayerSound;
        private Vibrator vibrator;
        private List<PickList> _ListPickList = new List<PickList>();
        private Spinner spinnerPickListNo;
        private EditText editItemBarcode;
        private TextView txtMsg, txtScanQty;
        private RecyclerView recycleViewPicking;
        private Button btnReset;
        private RecyclerView.LayoutManager mLayoutManager;
        private DataTable dt = null;
        private DataTable dtBarcode = null;
        private int FromPos = 0;
        private int ToPos = 0;
        public PalletMappingActivity()
        {
            try
            {
                clsGLB = new clsGlobal();
                oNetwork = new clsNetwork();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }

        #region Activity Events
        protected override void OnCreate(Bundle savedInstanceState)
        {

            try
            {
                base.OnCreate(savedInstanceState);
                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.activity_PalletMapping);
                vibrator = this.GetSystemService(VibratorService) as Vibrator;
                ImageView imgBack = FindViewById<ImageView>(Resource.Id.imgBack);
                imgBack.Click += (e, a) =>
                {
                    this.Finish();
                };

                TextView txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);
                txtHeader.Text = clsGlobal.Process_Header_Text;

                txtMsg = FindViewById<TextView>(Resource.Id.txtMsg);
                txtMsg.Text = "";


                spinnerPickListNo = FindViewById<Spinner>(Resource.Id.spinnerPickListNo);
                spinnerPickListNo.ItemSelected += SpinnerPickListNo_ItemSelected;

                editItemBarcode = FindViewById<EditText>(Resource.Id.editItemBarcode);
                editItemBarcode.KeyPress += editItemBarcode_KeyPress;

                txtScanQty = FindViewById<TextView>(Resource.Id.txtScanQty);
                txtScanQty.Text = "Scan Qty: 0";

                btnReset = FindViewById<Button>(Resource.Id.btnReset);
                btnReset.Click += BtnReset_Click;

                listViewScanCase = FindViewById<ListView>(Resource.Id.listViewScanCase);
                _listScanCase = new List<ScanCaseModel>();
                listViewScanCase.Adapter = new CaseItemAdapter(this, _listScanCase);

                recycleViewPicking = FindViewById<RecyclerView>(Resource.Id.recycleViewPicking);
                mLayoutManager = new LinearLayoutManager(this);
                recycleViewPicking.SetLayoutManager(mLayoutManager);
                clsGlobal.ReadBarcode();
                GetPart();
                spinnerPickListNo.RequestFocus();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }


        private void SpinnerPickListNo_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                Spinner spinner = (Spinner)sender;
                string reqData = spinner.GetItemAtPosition(e.Position).ToString();
                if (reqData.ToLower() != "--select--" && spinnerPickListNo.SelectedItemPosition > 0)
                {
                    _listScanCase.Clear();
                    GetBarcode();

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString() == spinnerPickListNo.SelectedItem.ToString().Trim().ToString())
                        {
                            FromPos = Convert.ToInt32(row[2].ToString());
                            ToPos = Convert.ToInt32(row[3].ToString());
                            editItemBarcode.Enabled = true;
                            editItemBarcode.Text = "";
                            editItemBarcode.RequestFocus();
                            return;
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
        public void BindCombo<T>(Spinner cmb, T lst) where T : List<string>
        {
            try
            {
                cmb.Adapter = null;
                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, lst);
                cmb.Adapter = adapter;
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
        private void GetPart()
        {
            try
            {
                _dic.Clear();
                if (File.Exists(Path.Combine(clsGlobal.FilePath, "PartMasters.csv")))
                {
                    dt = clsGlobal.ConvertCsvToDataTable(Path.Combine(clsGlobal.FilePath, "PartMasters.csv"));
                    if (dt.Rows.Count > 0)
                    {
                        lst = new List<string>();
                        lst.Add("--Select--");
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                _dic.Add(dt.Rows[i][0].ToString().Trim().ToString(), dt.Rows[i][1].ToString().Trim().ToString());
                                lst.Add(dt.Rows[i][0].ToString().Trim().ToString());
                            }
                            BindCombo(spinnerPickListNo, lst);
                            spinnerPickListNo.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerPickListNo_ItemSelected);
                        }
                    }

                    else
                    {
                        clsGLB.ShowMessage("Part Not Found", this, MessageTitle.ERROR);
                    }
                }
                else
                {
                    clsGLB.ShowMessage("Part Masters file Not Found", this, MessageTitle.ERROR);
                }
            }
            catch (System.Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        private void GetBarcode()
        {
            try
            {
                string date = DateTime.Now.ToString("ddMMyy");
                string Files = date.ToString();
                if (File.Exists(Path.Combine(clsGlobal.FilePath, Files + clsGlobal.FileName)))
                {
                    dtBarcode = clsGlobal.ConvertCsvToDataTable(Path.Combine(clsGlobal.FilePath, Files + clsGlobal.FileName));
                    if (dtBarcode.Rows.Count > 0)
                    {
                        _listScanCase.Clear();
                        string expression = "[Part No.] = '" + _dic[spinnerPickListNo.SelectedItem.ToString().Trim().ToString()] + "' ";
                        string sortOrder = "[Scanned On] DESC";
                        DataView dv = new DataView(dtBarcode, expression, sortOrder, DataViewRowState.CurrentRows);
                        DataTable new_table = dv.ToTable("NewTableName");
                        if (new_table.Rows.Count > 10)
                        {
                            for (int i = 0; i <= 10; i++)
                            {
                                _listScanCase.Add(new ScanCaseModel(new_table.Rows[i]["Part No."].ToString().Trim().ToString(), new_table.Rows[i]["Barcode"].ToString().Trim().ToString(), "OK"));
                            }
                            listViewScanCase.Adapter = new CaseItemAdapter(this, _listScanCase);
                            txtScanQty.Text = "Scan Qty: " + new_table.Rows.Count;
                        }
                        else
                        {
                            for (int i = 0; i < new_table.Rows.Count; i++)
                            {
                                _listScanCase.Add(new ScanCaseModel(new_table.Rows[i]["Part No."].ToString().Trim().ToString(), new_table.Rows[i]["Barcode"].ToString().Trim().ToString(), "OK"));
                            }
                            listViewScanCase.Adapter = new CaseItemAdapter(this, _listScanCase);
                            txtScanQty.Text = "Scan Qty: " + new_table.Rows.Count;
                        }

                    }
                    else
                    {
                        clsGLB.ShowMessage("Data not Found", this, MessageTitle.ERROR);
                    }
                }
                //else
                //{
                //    clsGLB.ShowMessage(Files + clsGlobal.FileName + " file Not Found", this, MessageTitle.ERROR);
                //}
            }
            catch (System.Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }


        #endregion


        #region Button Events

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                _listScanCase.Clear();
                GetPart();
                spinnerPickListNo.RequestFocus();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        #endregion

        #region EditText Events

        private void BarcodeScan()
        {
            try
            {
                string firstSevenChar = "";
                if (spinnerPickListNo.SelectedItem.ToString().Trim() == "")
                {
                    StartPlayingSound();
                    ShowMessageBox("Please Part No.", this);
                    spinnerPickListNo.RequestFocus();
                    return;
                }
                else if (editItemBarcode.Text.Trim() == "")
                {
                    StartPlayingSound();
                    ShowMessageBox("Please Scan valid Barcode", this);
                    editItemBarcode.Text = "";
                    editItemBarcode.RequestFocus();
                    return;
                }
                else if (editItemBarcode.Text.Trim() == "" || editItemBarcode.Text.Trim().Length > 40 || editItemBarcode.Text.Trim().Length < 11)
                {
                    StartPlayingSound();
                    ShowMessageBox("Please Scan valid Barcode", this);
                    editItemBarcode.Text = string.Empty;
                    editItemBarcode.RequestFocus();
                    return;
                }

                else if ((editItemBarcode.Text.Trim().Substring(FromPos, ToPos)) != (_dic[spinnerPickListNo.SelectedItem.ToString().Trim().ToString()]))
                {
                    StartPlayingSound();
                    ShowMessageBox("Invalid barcode scan against select part", this);
                    editItemBarcode.Text = "";
                    editItemBarcode.RequestFocus();
                    return;
                }

                else if ((clsGlobal.dictionary.ContainsKey(editItemBarcode.Text.Trim())) || clsGlobal.dictionary.ContainsValue(editItemBarcode.Text.Trim()))
                {
                    StartPlayingSound();
                    ShowMessageBox("Barcode already scanned", this);
                    editItemBarcode.Text = string.Empty;
                    editItemBarcode.RequestFocus();
                    return;
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(editItemBarcode.Text.Trim()) && editItemBarcode.Text.Trim().Length >= 7)
                    {
                        firstSevenChar = editItemBarcode.Text.Trim().Substring(FromPos, ToPos);

                        if (firstSevenChar == _dic[spinnerPickListNo.SelectedItem.ToString().Trim().ToString()])
                        {
                            clsGlobal.dictionary.Add(editItemBarcode.Text.Trim(), editItemBarcode.Text.Trim());
                            clsGlobal.WriteFile(clsGlobal.UserId, _dic[spinnerPickListNo.SelectedItem.ToString().Trim().ToString()], editItemBarcode.Text.Trim());

                            MediaScannerConnection.ScanFile(this, new String[] { Path.Combine(clsGlobal.FilePath, DateTime.Now.ToString("ddMMyy") + clsGlobal.FileName) }, null, null);


                            _listScanCase.Add(new ScanCaseModel(_dic[spinnerPickListNo.SelectedItem.ToString().Trim().ToString()], editItemBarcode.Text, "OK"));
                            listViewScanCase.Adapter = new CaseItemAdapter(this, _listScanCase);

                            txtScanQty.Text = "Scan Qty : " + _listScanCase.Count().ToString();
                            txtMsg.Text = "Successfully Scanned";
                            txtMsg.SetTextColor(Android.Graphics.Color.Green);
                            editItemBarcode.Text = string.Empty;
                            editItemBarcode.RequestFocus();
                        }
                        else
                        {
                            StartPlayingSound();
                            ShowMessageBox("Invalid barcode scan against select part", this);
                            editItemBarcode.Text = "";
                            editItemBarcode.RequestFocus();
                            return;
                        }
                    }
                    else
                    {
                        StartPlayingSound();
                        ShowMessageBox("Invalid barcode scan against select part", this);
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                StartPlayingSound();
                clsGLB.ShowMessage(ex.ToString(), this, MessageTitle.ERROR);
            }

        }

        private void editItemBarcode_KeyPress(object sender, View.KeyEventArgs e)
        {
            try
            {
                if (e.Event.Action == KeyEventActions.Down)
                {
                    if (e.KeyCode == Keycode.Enter)
                    {
                        if (IsValidForBarcode())
                        {

                            BarcodeScan();
                        }
                        else
                        {
                            editItemBarcode.Text = "";
                        }
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                StartPlayingSound();
                ShowMessageBox("Error : " + ex.Message, this);
            }
        }

        public override void OnBackPressed()
        {
        }

        #endregion



        #region Methods

        public void ShowConfirmBox(string msg, Activity activity, EventHandler<DialogClickEventArgs> handler)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(activity);
            builder.SetTitle("Message");
            builder.SetMessage(msg);
            builder.SetCancelable(false);
            builder.SetPositiveButton("Yes", handler);
            builder.SetNegativeButton("No", handllerCancelButton);
            builder.Show();
        }
        private void handllerCancelButton(object sender, DialogClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }
        public void ShowMessageBox(string msg, Activity activity)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(activity);
            builder.SetTitle("Message");
            builder.SetMessage(msg);
            builder.SetCancelable(false);
            builder.SetPositiveButton("Ok", handleOkMessage);
            builder.Show();
        }

        private void handleOkMessage(object sender, DialogClickEventArgs e)
        {
            try
            {
                vibrator.Cancel();
                StopPlayingSound();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }
        private void StartPlayingSound()
        {
            try
            {
                //Start Vibration
                long[] pattern = { 0, 200, 0 }; //0 to start now, 200 to vibrate 200 ms, 0 to sleep for 0 ms.
                vibrator.Vibrate(pattern, 0);//

                StopPlayingSound();
                mediaPlayerSound = MediaPlayer.Create(this, Resource.Raw.Beep);
                mediaPlayerSound.Start();
            }
            catch (Exception ex) { throw ex; }
        }
        private void StopPlayingSound()
        {
            try
            {
                if (mediaPlayerSound != null)
                {
                    mediaPlayerSound.Stop();
                    mediaPlayerSound.Release();
                    mediaPlayerSound = null;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void Clear()
        {
            try
            {

                editItemBarcode.Text = "";
                txtMsg.Text = "";
                txtScanQty.Text = "Scan Qty: 0";
                editItemBarcode.Enabled = false;
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        private bool IsValidForBarcode()
        {
            try
            {
                if (spinnerPickListNo.SelectedItemPosition <= 0)
                {
                    StartPlayingSound();
                    ShowMessageBox("Select Part No", this);
                    spinnerPickListNo.RequestFocus();
                    return false;
                }
                if (editItemBarcode.Text.Trim() == "")
                {
                    StartPlayingSound();
                    ShowMessageBox("Scan Bin barcode", this);
                    editItemBarcode.RequestFocus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }


        #endregion
    }
}