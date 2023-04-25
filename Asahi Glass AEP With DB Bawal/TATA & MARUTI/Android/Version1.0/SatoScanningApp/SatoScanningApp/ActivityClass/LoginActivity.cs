using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content.PM;
using Android.Views;
using IOCLAndroidApp;
using Android.Content;
using System;
using System.IO;
using AISScanningApp.ActivityClass;
using System.Collections.Generic;
using Android.Media;
using IOCLAndroidApp.Models;
using System.Threading.Tasks;

namespace AISScanningApp
{
    [Activity(Label = "Sato ScanningApp", MainLauncher = true, WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : AppCompatActivity
    {
        clsGlobal clsGLB;
        clsNetwork oNetwork;
        string _AppVersion;

        Button btnUpdateApp;
        EditText editUserId, editPassword;

        MediaPlayer mediaPlayerSound;
        Vibrator vibrator;

        public LoginActivity()
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
                SetContentView(Resource.Layout.activity_login);

                ImageView imgBack= FindViewById<ImageView>(Resource.Id.imgBack);
                imgBack.Visibility = ViewStates.Invisible;

                TextView txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);
                txtHeader.Text = "LOGIN";

                Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
                btnLogin.Click += BtnLogin_Click;

             

                Button btnServerSetting = FindViewById<Button>(Resource.Id.btnServerSetting);
                btnServerSetting.Click += BtnServerSetting_Click;

                Context context = this.ApplicationContext;
                _AppVersion = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
                TextView txtVersion = FindViewById<TextView>(Resource.Id.txtVersion);
                txtVersion.Text = "App Version : " + _AppVersion;

                editUserId = FindViewById<EditText>(Resource.Id.editUserId);
                editPassword = FindViewById<EditText>(Resource.Id.editPassword);

                vibrator = this.GetSystemService(VibratorService) as Vibrator;

                if (ReadSettingFile() == false)
                    OpenActivity(typeof(SettingActivity));


                editUserId.RequestFocus();

               // GetVersionAsync();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        public override void OnBackPressed()
        {
            ShowConfirmBox("Do you want to exit", this);
        }

        #endregion

        #region Button Events

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrEmpty(editUserId.Text.Trim()))
                {
                    Toast.MakeText(this, "Input user id", ToastLength.Long).Show();
                    editUserId.RequestFocus();
                    return;
                }
                if (string.IsNullOrEmpty(editPassword.Text.Trim()))
                {
                    Toast.MakeText(this, "Input password", ToastLength.Long).Show();
                    editPassword.RequestFocus();
                    return;
                }
                ValidateUserAsync();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        private void BtnServerSetting_Click(object sender, EventArgs e)
        {
            try
            {
                OpenActivity(typeof(SettingActivity));
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        #endregion

        #region Methods

        private async void ValidateUserAsync()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
            try
            {

                string[] _RESPONSE = await Task.Run(() => SendDataToServer());

                progressDialog.Hide();

                switch (_RESPONSE[0])
                {
                    case "VALID":
                        clsGlobal.UserId = _RESPONSE[1];
                        clsGlobal.UserName = _RESPONSE[2];
                        clsGlobal.UserGroup = _RESPONSE[3];
                        editUserId.Text = "";
                        editPassword.Text = "";
                        OpenActivity(typeof(MainActivity));
                        break;

                    case "INVALID":
                        editUserId.Text = "";
                        editPassword.Text = "";
                        editUserId.RequestFocus();
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
            finally
            {
                progressDialog.Hide();
            }
        }

        private string[] SendDataToServer()
        {
            try
            {
                string _MESSAGE = "V_USER~" + editUserId.Text.Trim() + "~" + editPassword.Text.Trim() + "}";
                string[] _RESPONSE = oNetwork.fnSendReceiveData(_MESSAGE).Split('~');

                return _RESPONSE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ReadSettingFile()
        {
            StreamReader sr = null;
            try
            {
                string folderPath = Path.Combine(clsGlobal.FilePath, clsGlobal.FileFolder);
                string filename = Path.Combine(folderPath, clsGlobal.ServerIpFileName);

                if (File.Exists(filename))
                {
                    sr = new StreamReader(filename);
                    clsGlobal.mSockIp = sr.ReadLine();
                    clsGlobal.mSockPort = Convert.ToInt32(sr.ReadLine());

                    sr.Close();
                    sr.Dispose();
                    sr = null;

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                    sr = null;
                }
            }
        }


        public void ShowConfirmBox(string msg, Activity activity)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(activity);
            builder.SetTitle("Message");
            builder.SetMessage(msg);
            builder.SetCancelable(false);
            builder.SetPositiveButton("Yes", handllerOkButton);
            builder.SetNegativeButton("No", handllerCancelButton);
            builder.Show();
        }
        void handllerOkButton(object sender, DialogClickEventArgs e)
        {
            this.FinishAffinity();
        }
        void handllerCancelButton(object sender, DialogClickEventArgs e)
        {

        }
        //This MessageBox Will Be Used As User Click Ok To Stop The Sound
        public void ShowMessageBox(string msg, Activity activity)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(activity);
            builder.SetTitle("Message");
            builder.SetMessage(msg);
            builder.SetCancelable(false);
            builder.SetPositiveButton("Ok", handleOkMessage);
            // builder.SetNegativeButton("No", handllerCancelButton);
            builder.Show();
        }

        void handleOkMessage(object sender, DialogClickEventArgs e)
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

        public void OpenActivity(Type t)
        {
            try
            {
                Intent MenuIntent = new Intent(this, t);
                StartActivity(MenuIntent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region App New Version Update

        private void BtnUpdateApp_Click(object sender, EventArgs e)
        {
            try
            {
                DownloadAppAsync();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }
        private async void GetVersionAsync()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
            try
            {
                string[] _RESPONSE = await Task.Run(() => GetVersionFromServer());

                progressDialog.Hide();

                switch (_RESPONSE[0])
                {
                    case "VALID":
                        if (_AppVersion.Trim() != _RESPONSE[1].Trim())
                        {
                            btnUpdateApp.Visibility = ViewStates.Visible;
                            clsGLB.ShowMessage("New version of the app is available,please click on Update App button to download new app", this, MessageTitle.INFORMATION);
                        }
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
            finally
            {
                progressDialog.Hide();
            }
        }

        private string[] GetVersionFromServer()
        {
            try
            {
                string _MESSAGE = "GET_APP_VERSION~" + "}";
                string[] _RESPONSE = oNetwork.fnSendReceiveData(_MESSAGE).Split('~');

                return _RESPONSE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async void DownloadAppAsync()
        {
            var progressDialog = ProgressDialog.Show(this, "", "Please wait, downloading new app, it may take some time...", true);
            try
            {

                string[] _RESPONSE = await Task.Run(() => GetAppFromServer());

                progressDialog.Hide();

                switch (_RESPONSE[0])
                {
                    case "VALID":
                        if (_RESPONSE.Length <= 2) { Toast.MakeText(this, "App could not be downloaded try again", ToastLength.Long).Show(); break; }
                        string ExeString = _RESPONSE[1];
                        string ExeName = _RESPONSE[2];
                        byte[] byt = Convert.FromBase64String(ExeString);

                        string folderPath = Path.Combine(clsGlobal.FilePath, clsGlobal.FileFolder);
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        string filename = Path.Combine(folderPath, ExeName);

                        File.WriteAllBytes(filename, byt);

                        if (File.Exists(filename))
                        {
                            Toast.MakeText(this, "App Downloaded successfully", ToastLength.Long).Show();

                            Intent intent = new Intent(Intent.ActionView);
                            intent.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(filename)), "application/vnd.android.package-archive");

                            intent.SetFlags(ActivityFlags.NewTask);
                            StartActivity(intent);
                        }
                        else
                        {
                            Toast.MakeText(this, "Download app again", ToastLength.Long).Show();
                        }

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
            finally
            {
                progressDialog.Hide();
            }
        }

        private string[] GetAppFromServer()
        {
            try
            {
                string _MESSAGE = "GET_NEWEXE_DEVICE~" + "}";
                string[] _RESPONSE = oNetwork.GetSetTcpNewApp(_MESSAGE).Split('~');

                return _RESPONSE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}