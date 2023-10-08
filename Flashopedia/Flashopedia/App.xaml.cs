using Flashopedia.Services;
using Xamarin.Forms;

namespace Flashopedia
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.Register<FlashCardsStore>();
			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
