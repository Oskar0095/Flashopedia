using Xamarin.Forms;

namespace Flashopedia.Services
{
	public static class StorageHelper
	{
		private static IStorageHelperClient _client;

		public static IStorageHelperClient Client
		{
			get
			{
				if (_client == null)
				{
					_client = DependencyService.Get<IStorageHelperClient>();
				}
				return _client;
			}
		}

		private static string _persistentDataPath = string.Empty;
		public static string PersistentDataPath
		{
			get
			{
				if (_persistentDataPath == string.Empty)
				{
					_persistentDataPath = Client.GetAppPersistentDataPath();
				}
				return _persistentDataPath;
			}
		}
	}
}
