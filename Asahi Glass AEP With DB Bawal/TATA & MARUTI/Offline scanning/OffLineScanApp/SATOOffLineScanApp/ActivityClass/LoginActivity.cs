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
using System.Collections.Generic;
using Android.Media;
using IOCLAndroidApp.Models;
using System.Threading.Tasks;
using System.Data;
namespace SATOOffLineScanApp
{
    [Activity(Label = "Sato ScanningApp", MainLauncher = true, WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : AppCompatActivity
    {
        clsGlobal clsGLB;
        clsNetwork oNetwork;
        string _AppVersion;
        private DataTable dt = null;
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

                Context context = this.ApplicationContext;
                _AppVersion = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
                TextView txtVersion = FindViewById<TextView>(Resource.Id.txtVersion);
                txtVersion.Text = "App Version : " + _AppVersion;

                editUserId = FindViewById<EditText>(Resource.Id.editUserId);
                editPassword = FindViewById<EditText>(Resource.Id.editPassword);
                editUserId.Text = "Admin";
                editPassword.Text = "1";
                vibrator = this.GetSystemService(VibratorService) as Vibrator;
                clsGlobal.ReadUserLogin();
                GetLoginUser();
                editUserId.RequestFocus();

               // GetVersionAsync();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }
        private void GetLoginUser()
        {
            try
            {
                if (File.Exists(Path.Combine(clsGlobal.FilePath, "LoginUser.csv")))
                {
                    dt = clsGlobal.ConvertCsvToDataTable(Path.Combine(clsGlobal.FilePath, "LoginUser.csv"));
                    if (dt.Rows.Count > 0)
                    {
                        editUserId.RequestFocus();

                    }

                    else
                    {
                        clsGLB.ShowMessage("Part Not Found", this, MessageTitle.ERROR);
                    }
                }
                else
                {
                    clsGLB.ShowMessage("Login User file Not Found", this, MessageTitle.ERROR);
                }
            }
            catch (System.Exception ex)
            {
                clsGLB.ShowMessage(ex.ToString(), this, MessageTitle.ERROR);
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
                GetLoginUser();
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

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["UserID"].ToString().ToUpper() == editUserId.Text.ToUpper() && row["Password"].ToString().ToUpper() == editPassword.Text.ToUpper())
                        {
                            OpenActivity(typeof(MainActivity));
                            clsGlobal.UserId = editUserId.Text;
                            return;
                        }
                        else
                        {
                            editUserId.Text = "Admin";
                            editPassword.Text = "1";
                            editUserId.RequestFocus();
                            StartPlayingSound();
                            ShowMessageBox("Invalid User Id and Password", this);
                           
                            return;
                        }
                    }
                }
                else
                {
                    editUserId.Text = "Admin";
                    editPassword.Text = "1";
                    editUserId.RequestFocus();
                    StartPlayingSound();
                    ShowMessageBox("LoginUser.csv File not found", this);
                    return;

                }
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }


        #endregion

        #region Methods

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


    }
}