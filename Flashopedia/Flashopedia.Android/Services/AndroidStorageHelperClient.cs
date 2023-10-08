using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using System.IO;

namespace Flashopedia.Services.Droid
{
	public class AndroidStorageHelperClient : IStorageHelperClient
	{
		private readonly Activity activity;

		internal AndroidStorageHelperClient()
		{
		}

		public AndroidStorageHelperClient(Activity activity)
		{
			this.activity = activity;
		}

		public string GetAppPersistentDataPath()
		{
			string externalStorageDirectory = Environment.ExternalStorageDirectory.Path;
			string packageName = Application.Context.PackageName;
			string persistentDataPath = Path.Combine(externalStorageDirectory, "Android", "data", packageName, "files");
			try
			{
				if (!Directory.Exists(persistentDataPath))
					Directory.CreateDirectory(persistentDataPath);
			}
			catch (System.IO.IOException)
			{
				// ignore exception
			}

			return persistentDataPath;
		}

		//public bool RequestReadWritePermissions()
		//{
		//	const int REQUEST_CODE = 1243;
		//	string[] permissions = { Manifest.Permission.WriteExternalStorage, Manifest.Permission.WriteExternalStorage };

		//	if (ContextCompat.CheckSelfPermission(Application.Context, permissions[0]) == Permission.Granted &&
		//		ContextCompat.CheckSelfPermission(Application.Context, permissions[1]) == Permission.Granted)
		//	{
		//		return true;
		//	}
		//	else
		//	{
		//		ActivityCompat.RequestPermissions(activity, permissions, REQUEST_CODE);
		//		return false;
		//	}
		//}

		public bool TryGetSdCardPath(out string sdCardPath)
		{
			Java.IO.File[] externalStoragePaths = Application.Context.GetExternalFilesDirs(null);

			foreach (Java.IO.File file in externalStoragePaths)
			{
				if (!file.AbsolutePath.StartsWith("/storage/emulated/0"))
				{
					string[] directories = file.Path.Split('/');
					sdCardPath = '/' + directories[1] + '/' + directories[2];
					return true;
				}
			}
			sdCardPath = string.Empty;
			return false;
		}
	}
}