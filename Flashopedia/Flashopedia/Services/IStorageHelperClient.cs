namespace Flashopedia.Services
{
	public interface IStorageHelperClient
	{
		 bool TryGetSdCardPath(out string sdCardPath);

		 string GetAppPersistentDataPath();
	}
}
