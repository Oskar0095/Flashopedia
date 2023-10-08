using Flashopedia.Services;
using Flashopedia.Services.Droid;
using Xamarin.Forms;

namespace Flashopedia.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			DependencyService.RegisterSingleton<IStorageHelperClient>(new UwpStorageHelperClient());
			LoadApplication(new Flashopedia.App());
		}
	}
}
