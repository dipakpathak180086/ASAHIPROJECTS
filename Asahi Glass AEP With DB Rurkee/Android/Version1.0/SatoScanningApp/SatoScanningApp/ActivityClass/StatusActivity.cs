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
using AISScanningApp;

namespace SatoScanningApp
{
    [Activity(Label = "Sato ScanningApp", WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class StatusActivity : AppCompatActivity
    {
        private clsGlobal clsGLB;
        private clsNetwork oNetwork;
        private MediaPlayer mediaPlayerSound;
        private Vibrator vibrator;
        private List<BarcodeStatus> _ListPickList = new List<BarcodeStatus>();
        private EditText editItemBarcode;
        private TextView txtMsg;
        private Button btnReset;
        private RecyclerView recycleViewPicking;
        private FGUnloadingAdapter StatusAdapter;
        private RecyclerView.LayoutManager mLayoutManager;

        public StatusActivity()
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
                SetContentView(Resource.Layout.activity_Status);
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

                editItemBarcode = FindViewById<EditText>(Resource.Id.editItemBarcode);
                editItemBarcode.KeyPress += editItemBarcode_KeyPress;

                btnReset = FindViewById<Button>(Resource.Id.btnReset);
                btnReset.Click += BtnReset_Click;

                recycleViewPicking = FindViewById<RecyclerView>(Resource.Id.recycleViewPicking);
                mLayoutManager = new LinearLayoutManager(this);
                recycleViewPicking.SetLayoutManager(mLayoutManager);

            }
            catch (Exception ex)
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

                            GetPartDataAsync();
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

        #endregion


        #region Methods

        private string[] SendPalletDataToServer()
        {
            try
            {
                string _MESSAGE = "BARCODESTATUS~" + clsGlobal.Db_Type + "~" + editItemBarcode.Text.Trim() + "~" + clsGlobal.UserId + "}";
                string[] _RESPONSE = oNetwork.fnSendReceiveData(_MESSAGE).Split('~');
                return _RESPONSE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void BindRecycleView()
        {
            try
            {
                StatusAdapter = new FGUnloadingAdapter(_ListPickList, this);
                recycleViewPicking.SetAdapter(StatusAdapter);
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }
        private async Task<string> GetPartDataAsync()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
            try
            {
                _ListPickList.Clear();
                clsGlobal.Db_Type = "VALIDATEBARCODE";
                string[] _RESPONSE = await Task.Run(() => SendPalletDataToServer());

                progressDialog.Hide();

                switch (_RESPONSE[0])
                {
                    case "VALID":
                        string[] ArrRow = _RESPONSE[1].Split("#");
                        for (int i = 0; i < ArrRow.Length; i++)
                        {
                            string[] ArrCol = ArrRow[i].Split("$");
                            _ListPickList.Add(new BarcodeStatus
                            {
                                PartNo = ArrCol[0],
                                Barcode = ArrCol[1],
                                Status = ArrCol[2]
                            });
                        }
                        BindRecycleView();
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        break;

                    case "INVALID":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        break;

                    case "ERROR":
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        break;

                    case "NO_CONNECTION":
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        break;

                    default:
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        editItemBarcode.Text = "";
                        editItemBarcode.RequestFocus();
                        break;
                }
                //Refresh the list
                //StatusAdapter.NotifyDataSetChanged();
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
                _ListPickList.Clear();
                StatusAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        private bool IsValidForItemBarcode()
        {
            try
            {
               
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