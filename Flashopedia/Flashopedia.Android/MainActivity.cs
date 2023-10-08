using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Flashopedia.Services;
using Flashopedia.Services.Droid;
using Xamarin.Forms;

namespace Flashopedia.Droid
{
	[Activity(Label = "FlashCrads", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			DependencyService.RegisterSingleton<IStorageHelperClient>(new AndroidStorageHelperClient(this));
			Window.SetStatusBarColor(new Android.Graphics.Color(18, 18, 18));
			LoadApplication(new App());
		}
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}