using Windows.Storage;
using Windows.Storage.AccessCache;

namespace Flashopedia.Services.Droid
{
	public class UwpStorageHelperClient : IStorageHelperClient
	{
		internal UwpStorageHelperClient()
		{
		}

		public string GetAppPersistentDataPath()
		{
			StorageFolder localFolder = ApplicationData.Current.LocalFolder;
			return localFolder.Path;
		}

		public bool TryGetSdCardPath(out string sdCardPath)
		{
			sdCardPath = string.Empty;
			if (RequestAccessToRemovableStorage())
			{
				StorageFolder removableFolder = KnownFolders.RemovableDevices;

				if (removableFolder != null)
				{
					sdCardPath = removableFolder.Path;
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}

		}

		public bool RequestAccessToRemovableStorage()
		{
			bool hasAccess = false;

			StorageFolder removableFolder = KnownFolders.RemovableDevices;

			if (removableFolder != null)
			{
				string token = StorageApplicationPermissions.FutureAccessList.Add(removableFolder);
				hasAccess = !string.IsNullOrEmpty(token);
			}

			return hasAccess;
		}
	}
}