using Flashopedia.Services;
using Flashopedia;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Flashopedia.Services
{
	public class FlashCardsStore : IDataStore<FlashCardsPack>
	{
		private readonly ObservableCollection<FlashCardsPack> flashCards;
		private static readonly string FlashCardsDirectory = Path.Combine(StorageHelper.PersistentDataPath, "flashCards");

		private static FlashCardsStore instance;
		public static FlashCardsStore Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new FlashCardsStore();
				}
				return instance;
			}
		}

		
		private FlashCardsStore()
		{
			if (!Directory.Exists(FlashCardsDirectory))
				Directory.CreateDirectory(FlashCardsDirectory);

			string[] flashCardsPaths = Directory.GetFiles(FlashCardsDirectory);
			flashCards = new ObservableCollection<FlashCardsPack>();
			foreach (string path in flashCardsPaths)
			{
				try
				{
					byte[] bytes = File.ReadAllBytes(path);
					FlashCardsPack flashCardsPack = FlashCardsPack.Decompress(bytes);
					flashCards.Add(flashCardsPack);
				}
				catch
				{
					// ignore for now
				}
			}
			if (flashCards.Count == 0)
			{
				AddItem(new FlashCardsPack { Title = "Test Title"});
			}
		}

		public bool AddItem(FlashCardsPack flashCardsPack)
		{
			if (flashCardsPack.Id == Guid.Empty)
				flashCardsPack.Id = Guid.NewGuid();
			flashCards.Add(flashCardsPack);

			string path = Path.Combine(FlashCardsDirectory, $"{flashCardsPack.Title}_{flashCardsPack.Id.ToString("N")}");
			File.WriteAllBytes(path, flashCardsPack.Compress());
			return true;
		}

		public bool UpdateItem(FlashCardsPack flashCardsPack)
		{
			int index = -1;
			int i = 0;
			foreach (FlashCardsPack fcp in flashCards)
			{
				if (fcp == flashCardsPack)
				{
					index = i;
					break;
				}
				++i;
			}
			if (index != -1)
			{
				flashCards[index] = flashCardsPack;
				string path = Path.Combine(FlashCardsDirectory, $"{flashCardsPack.Title}_{flashCardsPack.Id.ToString("N")}");
				File.WriteAllBytes(path, flashCardsPack.Compress());
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool DeleteItem(Guid id)
		{
			int index = -1;
			int i = 0;
			foreach (FlashCardsPack fcp in flashCards)
			{
				if (fcp.Id == id)
				{
					index = i;
					break;
				}
				++i;
			}
			if (index != -1)
			{
				string path = Path.Combine(FlashCardsDirectory, $"{flashCards[index].Title}_{flashCards[index].Id.ToString("N")}");
				File.Delete(path);
				flashCards.RemoveAt(index);
				return true;
			}
			else
			{
				return false;
			}
		}

		public FlashCardsPack GetItem(Guid id)
		{
			return flashCards.FirstOrDefault(fcp => fcp.Id == id);
		}

		public ObservableCollection<FlashCardsPack> GetItems()
		{
			return flashCards;
		}
	}
}