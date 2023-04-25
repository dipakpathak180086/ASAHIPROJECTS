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
using AISScanningApp.Adapter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISScanningApp
{
    [Activity(Label = "Sato ScanningApp", WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class PalletMappingActivity : AppCompatActivity
    {
        private clsGlobal clsGLB;
        private clsNetwork oNetwork;
        private MediaPlayer mediaPlayerSound;
        private Vibrator vibrator;
        private List<PickList> _ListPickList = new List<PickList>();
        private decimal _ScanQty = 0, _TotalQty = 0;
        private Spinner spinnerPickListNo;
        private EditText editItemBarcode, editPallet, editWorkOrderNo;
        private TextView txtMsg, txtTotalQty, txtScanQty, txtPartName;
        private Button btnReset, btnComplete;
        private RecyclerView recycleViewPicking;
        private PickingAdapter pickingAdapter;
        private RecyclerView.LayoutManager mLayoutManager;

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

                txtPartName = FindViewById<TextView>(Resource.Id.txtPartName);
                txtPartName.Text = "";

                spinnerPickListNo = FindViewById<Spinner>(Resource.Id.spinnerPickListNo);
                spinnerPickListNo.ItemSelected += SpinnerPickListNo_ItemSelected;

                editItemBarcode = FindViewById<EditText>(Resource.Id.editItemBarcode);
                editItemBarcode.KeyPress += editItemBarcode_KeyPress;

                editPallet = FindViewById<EditText>(Resource.Id.editPallet);
                editPallet.KeyPress += editPallet_KeyPress;

                editWorkOrderNo = FindViewById<EditText>(Resource.Id.editWorkOrderNo);
                editWorkOrderNo.KeyPress += editWorkOrderNo_KeyPress;


                txtTotalQty = FindViewById<TextView>(Resource.Id.txtTotalQty);
               // txtTotalQty.Text = "Pack Size: 0";

                txtScanQty = FindViewById<TextView>(Resource.Id.txtScanQty);
               // txtScanQty.Text = "Scan Qty: 0";

                btnComplete = FindViewById<Button>(Resource.Id.btnComplete);
                btnComplete.Click += BtnComplete_Click;

                btnReset = FindViewById<Button>(Resource.Id.btnReset);
                btnReset.Click += BtnReset_Click;

                recycleViewPicking = FindViewById<RecyclerView>(Resource.Id.recycleViewPicking);
                mLayoutManager = new LinearLayoutManager(this);
                recycleViewPicking.SetLayoutManager(mLayoutManager);

                BindRecycleView();

                editWorkOrderNo.Visibility = ViewStates.Gone;
                editPallet.Visibility = ViewStates.Gone;
                btnComplete.Visibility = ViewStates.Gone;
                txtTotalQty.Visibility = ViewStates.Gone;
                txtScanQty.Visibility = ViewStates.Gone;
                GetPartNoAsync();
                spinnerPickListNo.RequestFocus();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }



        #endregion

        #region Spinner Events
        private void SpinnerPickListNo_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                if (spinnerPickListNo.SelectedItemPosition > 0)
                {
                    Clear();
                    GetPartDataAsync();
                    editWorkOrderNo.Text = "";
                    editWorkOrderNo.RequestFocus();
                }
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }
        #endregion

        #region Button Events
        private void BtnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                ShowConfirmBox("Are you sure you want to complete this Pallet?", this, CompletePickingBtnClick);
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetPartNoAsync();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        #endregion

        #region EditText Events

        private void editItemBarcode_KeyPress(object sender, View.KeyEventArgs e)
        {
            try
            {
                if (e.Event.Action == KeyEventActions.Down)
                {
                    if (e.KeyCode == Keycode.Enter)
                    {
                        if (IsValidForItemBarcode())
                        {

                            SaveMappedDataAsync();
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

        private void editPallet_KeyPress(object sender, View.KeyEventArgs e)
        {
            try
            {
                if (e.Event.Action == KeyEventActions.Down)
                {
                    if (e.KeyCode == Keycode.Enter)
                    {
                        if (IsValidForLocation())
                        {
                            ValidatePalletAsync();
                        }
                        else
                        {
                            editPallet.Enabled = true;
                            editPallet.Text = "";
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

        private void editWorkOrderNo_KeyPress(object sender, View.KeyEventArgs e)
        {
            try
            {
                if (e.Event.Action == KeyEventActions.Down)
                {
                    if (e.KeyCode == Keycode.Enter)
                    {
                        if (IsValidForWorkOrder())
                        {
                            editWorkOrderNo.Enabled = false;
                            editPallet.Text = "";
                            editPallet.RequestFocus();
                        }
                        else
                        {
                            editWorkOrderNo.Enabled = true;
                            editWorkOrderNo.Text = "";
                            editWorkOrderNo.RequestFocus();
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

        #region RecycleView

        private void BindRecycleView()
        {
            try
            {
                pickingAdapter = new PickingAdapter(_ListPickList, this);
                pickingAdapter.ItemClick += PickingAdapter_ItemClick;
                recycleViewPicking.SetAdapter(pickingAdapter);
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        private void PickingAdapter_ItemClick(object sender, int e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        #endregion

        #region Methods
        private void CompletePickingBtnClick(object sender, DialogClickEventArgs e)
        {
            if (IsValidForLocation())
            {
                CompletePicklistNo();
            }
            else
            {
                editPallet.Enabled = true;
                editPallet.Text = "";
            }
            

        }

        private async Task<string> CompletePicklistNo()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
            try
            {
                clsGlobal.Db_Type = "PALLET_COMP";
                string[] _RESPONSE = await Task.Run(() => SendPalletDataToServer());

                progressDialog.Hide();
                List<string> _ListPickListNo = new List<string>();
                switch (_RESPONSE[0])
                {
                    case "PALLETCOMPLETED":
                        txtMsg.Text = _RESPONSE[1].Trim();
                        editPallet.Text = "";
                        editItemBarcode.Text = "";
                        txtMsg.Text = "";
                        txtTotalQty.Text = "Pack Size: 0";
                        txtScanQty.Text = "Scan Qty: 0";
                        txtPartName.Text = "";
                        editPallet.Enabled = true;
                        editWorkOrderNo.Enabled = true;
                        editWorkOrderNo.Text = "";
                        _ScanQty = 0;
                        _TotalQty = 0;
                        editWorkOrderNo.RequestFocus();
                        GetPartNoAfterComplete();
                        BindTotalAndScanQtyAfterComplete();
                        GetPartDataAfterScanComplete();
                        ShowMessageBox(_RESPONSE[1].Trim().ToString(), this);
                        break;

                    case "INVALID":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "ERROR":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;
                }
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
                progressDialog.Hide();
            }
            finally
            {
                progressDialog.Hide();
            }
            return "";
        }
        private async Task<string> GetPartNoAsync()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
            try
            {
                clsGlobal.Db_Type = "BIND_PARTNO";
                string[] _RESPONSE = await Task.Run(() => SendPalletDataToServer());

                progressDialog.Hide();
                List<string> _ListPickListNo = new List<string>();
                switch (_RESPONSE[0])
                {
                    case "VALID":
                        _ListPickListNo.Add("--Select--");
                        string[] ArrRow = _RESPONSE[1].Split("#");
                        for (int i = 0; i < ArrRow.Length; i++)
                        {
                            string[] ArrCol = ArrRow[i].Split("$");

                            for (int J = 0; J < ArrCol.Length; J++)
                            {

                                _ListPickListNo.Add(ArrCol[J]);
                            }
                        }
                        ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.Spiner, _ListPickListNo);
                        spinnerPickListNo.Adapter = arrayAdapter;
                        spinnerPickListNo.SetSelection(0);
                        spinnerPickListNo.RequestFocus();
                        break;

                    case "INVALID":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "ERROR":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;
                }
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
                progressDialog.Hide();
            }
            finally
            {
                progressDialog.Hide();
            }
            return "";
        }

        private void GetPartNoAfterComplete()
        {
            try
            {
                clsGlobal.Db_Type = "BIND_PARTNO";
                string[] _RESPONSE =SendPalletDataToServer();
                List<string> _ListPickListNo = new List<string>();
                switch (_RESPONSE[0])
                {
                    case "VALID":
                        _ListPickListNo.Add("--Select--");
                        string[] ArrRow = _RESPONSE[1].Split("#");
                        for (int i = 0; i < ArrRow.Length; i++)
                        {
                            string[] ArrCol = ArrRow[i].Split("$");

                            for (int J = 0; J < ArrCol.Length; J++)
                            {

                                _ListPickListNo.Add(ArrCol[J]);
                            }
                        }
                        ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.Spiner, _ListPickListNo);
                        spinnerPickListNo.Adapter = arrayAdapter;
                        spinnerPickListNo.SetSelection(0);
                        spinnerPickListNo.RequestFocus();
                        break;

                    case "INVALID":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "ERROR":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;
                }
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
                
            }
            
        }

        private string[] SendPalletDataToServer()
        {
            try
            {
                string _MESSAGE = "PALLETMAPPING~" + clsGlobal.Db_Type + "~" + spinnerPickListNo.SelectedItem + "~" + editWorkOrderNo.Text.Trim() + "~" + editPallet.Text.Trim() + "~" + editItemBarcode.Text.Trim()+ "~" + clsGlobal.UserId+"}";
                string[] _RESPONSE = oNetwork.fnSendReceiveData(_MESSAGE).Split('~');
                return _RESPONSE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void BindTotalAndScanQty()
        {
            try
            {
                clsGlobal.Db_Type = "BIND_TOTAL_AND_SCAN_QTY";
                string[] _RESPONSE = SendPalletDataToServer();
                switch (_RESPONSE[0])
                {
                    case "VALID":
                        string[] ArrRow = _RESPONSE[1].Split("#");
                        string[] ArrCol = ArrRow[0].Split("$");
                        _TotalQty = Convert.ToDecimal(ArrCol[0]);
                        _ScanQty = Convert.ToDecimal(ArrCol[1]);
                        txtTotalQty.Text = "Pack Size: " + _TotalQty.ToString();
                        txtScanQty.Text = "Scan Qty: " + _ScanQty.ToString();
                        txtPartName.Text= "Part Name: "+Convert.ToString(ArrCol[2]);
                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindTotalAndScanQtyAfterComplete()
        {
            try
            {
                _TotalQty = 0;
                _ScanQty = 0;
                txtTotalQty.Text = "Pack Size: " + "0";
                txtScanQty.Text = "Scan Qty: " + "0";
                txtPartName.Text = "Part Name: " + "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> GetPartDataAsync()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
            try
            {
                _ListPickList.Clear();
                clsGlobal.Db_Type = "BIND_VIEW";
                string[] _RESPONSE = await Task.Run(() => SendPalletDataToServer());

                progressDialog.Hide();

                switch (_RESPONSE[0])
                {
                    case "VALID":
                        string[] ArrRow = _RESPONSE[1].Split("#");
                        for (int i = 0; i < ArrRow.Length; i++)
                        {
                            string[] ArrCol = ArrRow[i].Split("$");
                            _ListPickList.Add(new PickList
                            {
                                PartNo = ArrCol[0],
                                Barcode = ArrCol[1],
                               // ScanQty = decimal.Parse(ArrCol[2])
                            });
                        }
                        //BindTotalAndScanQty();
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        break;

                    case "INVALID":
                        editWorkOrderNo.Text = "";
                        editWorkOrderNo.RequestFocus();
                        break;

                    case "ERROR":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;
                }
                //Refresh the list
                pickingAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
                progressDialog.Hide();
            }
            finally
            {
                progressDialog.Hide();
            }
            return "";
        }

        private void GetPartDataAfterScanAsync()
        {
            try
            {
                _ListPickList.Clear();
                clsGlobal.Db_Type = "BIND_VIEW";
                string[] _RESPONSE = SendPalletDataToServer();
                switch (_RESPONSE[0])
                {
                    case "VALID":
                        string[] ArrRow = _RESPONSE[1].Split("#");
                        for (int i = 0; i < ArrRow.Length; i++)
                        {
                            string[] ArrCol = ArrRow[i].Split("$");
                            _ListPickList.Add(new PickList
                            {
                                PartNo = ArrCol[0],
                                Barcode = ArrCol[1],
                                //ScanQty = decimal.Parse(ArrCol[2])
                            });
                        }
                        break;

                    case "INVALID":
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        break;

                    case "ERROR":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;
                }
                //Refresh the list
                pickingAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }

        }

        private void GetPartDataAfterScanComplete()
        {
            try
            {
                //_ListPickList.Clear();
                //_ListPickList.Add(new PickList
                //{
                //    PartNo = "",
                //    Qty = 0,
                //    ScanQty = 0
                //});
                pickingAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }

        }

        private async void SaveMappedDataAsync()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
            try
            {
                clsGlobal.Db_Type = "SAVE";
                txtMsg.Text = "";
                string[] _RESPONSE = await Task.Run(() => SendPalletDataToServer());

                progressDialog.Hide();

                switch (_RESPONSE[0])
                {
                    case "VALID":
                        txtMsg.Text = _RESPONSE[1].Trim();
                       // BindTotalAndScanQty();
                        GetPartDataAfterScanAsync();
                        //txtScanQty.Text = "Scan Qty : " + _ScanQty.ToString();
                        pickingAdapter.NotifyDataSetChanged();
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        break;
                    //case "PALLETCOMPLETED":
                    //    txtMsg.Text = _RESPONSE[1].Trim();
                    //    editPallet.Text = "";
                    //    editItemBarcode.Text = "";
                    //    txtMsg.Text = "";
                    //    txtTotalQty.Text = "Pack Size: 0";
                    //    txtScanQty.Text = "Scan Qty: 0";
                    //    txtPartName.Text = "";
                    //    editPallet.Enabled = true;
                    //    editWorkOrderNo.Enabled = true;
                    //    editWorkOrderNo.Text = "";
                    //    _ScanQty = 0;
                    //    _TotalQty = 0;
                    //    editWorkOrderNo.RequestFocus();
                    //    GetPartNoAfterComplete();
                    //    BindTotalAndScanQtyAfterComplete();
                    //    GetPartDataAfterScanComplete();
                        
                    //    txtMsg.Text = _RESPONSE[1].Trim().ToString();
                    //    break;

                    case "INVALID":

                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "ERROR":

                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":

                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:

                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;

                }
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
                progressDialog.Hide();
            }
            finally
            {
                progressDialog.Hide();
            }
        }

        private async void ValidatePalletAsync()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
            try
            {
                clsGlobal.Db_Type = "VALIDATEPALLET";
                txtMsg.Text = "";
                string[] _RESPONSE = await Task.Run(() => SendPalletDataToServer());

                progressDialog.Hide();

                switch (_RESPONSE[0])
                {
                    case "VALID":
                        //txtMsg.Text = _RESPONSE[1].Trim();
                        if (_RESPONSE[1].Replace("$", "").Trim() == "EXISTS")
                        {
                            editPallet.Enabled = false;
                            editItemBarcode.Text = "";
                            editItemBarcode.RequestFocus();
                        }
                        else if (_RESPONSE[1].Replace("$", "").Trim() == "NOTEXISTS")
                        {
                            editPallet.Enabled = false;
                            editItemBarcode.Text = "";
                            editItemBarcode.RequestFocus();
                        }
                        else
                        {
                            editPallet.Text = "";
                            editPallet.RequestFocus();
                            StartPlayingSound();
                            ShowMessageBox(_RESPONSE[1].Replace("$", ""), this);
                        }
                        break;

                    case "INVALID":

                        editPallet.Text = "";
                        editPallet.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "ERROR":

                        editPallet.Text = "";
                        editPallet.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":

                        editPallet.Text = "";
                        editPallet.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:

                        editPallet.Text = "";
                        editPallet.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;

                }
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
                progressDialog.Hide();
            }
            finally
            {
                progressDialog.Hide();
            }
        }

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
                editPallet.Text = "";
                editItemBarcode.Text = "";
                txtMsg.Text = "";
                txtTotalQty.Text = "Pack Size: 0";
                txtScanQty.Text = "Scan Qty: 0";
                txtPartName.Text = "";
                editPallet.Enabled = true;
                editWorkOrderNo.Enabled = true;
                editWorkOrderNo.Text = "";
                editItemBarcode.RequestFocus();
                _ScanQty = 0;
                _TotalQty = 0;
                _ListPickList.Clear();

                pickingAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        private bool IsValidForLocation()
        {
            try
            {
                if (editPallet.Text.Trim() == "")
                {
                    StartPlayingSound();
                    ShowMessageBox("Enter Pallet", this);
                    editPallet.Text = "";
                    editPallet.RequestFocus();
                    return false;
                }
                if (spinnerPickListNo.SelectedItemPosition <= 0)
                {
                    StartPlayingSound();
                    ShowMessageBox("Select Part No", this);
                    spinnerPickListNo.RequestFocus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        private bool IsValidForWorkOrder()
        {
            try
            {
                if (editWorkOrderNo.Text.Trim() == "")
                {
                    StartPlayingSound();
                    ShowMessageBox("Enter Work order", this);
                    editWorkOrderNo.Text = "";
                    editWorkOrderNo.RequestFocus();
                    return false;
                }
                if (spinnerPickListNo.SelectedItemPosition <= 0)
                {
                    StartPlayingSound();
                    ShowMessageBox("Select Part No", this);
                    spinnerPickListNo.RequestFocus();
                    return false;
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }



        private bool IsValidForItemBarcode()
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
                if (_ScanQty > 0 && _TotalQty > 0 && _ScanQty >= _TotalQty)
                {
                    StartPlayingSound();
                    BtnReset_Click(null, null);
                    ShowMessageBox("Part completed", this);
                    return false;
                }

                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}