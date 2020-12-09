using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using System.Threading.Tasks;
using JhipsterXamarin;

namespace JhipsterXamarin.Droid
{
    [Activity(Label = "Jhipster", MainLauncher = true, NoHistory = true, Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : MvxFormsSplashScreenActivity<MvxFormsAndroidSetup<App, FormsApp>, App, FormsApp>
    {
        public SplashScreenActivity()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return Task.CompletedTask;
        }

    }
}