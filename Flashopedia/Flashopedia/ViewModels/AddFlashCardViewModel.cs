using Flashopedia.Services;
using Flashopedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Flashopedia.ViewModels
{
    public class AddFlashCardViewModel : ObservableObject
    {
		public ICommand PickFileCommand { get; private set; }
		public ICommand ParseCommand { get; private set; }
		public ICommand AddCommand { get; private set; }

        private enum Parsers { Hyphen }
        public string[] AvailableParsers { get; private set; }

		private int selectedParser;
		public int SelectedParser
        {
            get => selectedParser;
            set
            {
                if (SetProperty(ref selectedParser, value))
                    Preferences.Set("selectedParser", value);
            }
        }

		private enum Formats { PlainText, Word }
		public string[] AvailableFormats { get; private set; }

		private int selectedFormat;
		public int SelectedFormat
		{
			get => selectedFormat;
			set
			{
				if (SetProperty(ref selectedFormat, value))
					Preferences.Set("selectedFormat", value);
			}
		}

		private FileResult pickedFile = null;
		private List<FlashCard> parsedFlashCards = null;

		public AddFlashCardViewModel()
        {
            AvailableParsers = Enum.GetNames(typeof(Parsers));
            AvailableFormats = Enum.GetNames(typeof(Formats));
            SelectedParser = Preferences.Get("selectedParser", 0);
            SelectedFormat = Preferences.Get("selectedFormat", 0);

			PickFileCommand = new AsyncCommand(PickInputFile);
			ParseCommand = new AsyncCommand(Parse);
			AddCommand = new AsyncCommand(AddFlashCardsPack);
        }

		private async Task PickInputFile()
		{
			FileResult fileResult = await FilePicker.PickAsync();
			if (fileResult != null)
			{
				pickedFile = fileResult;
			}
		}

		private async Task Parse()
        {
			Stream stream = await pickedFile.OpenReadAsync();

			string rawText = string.Empty;
			switch ((Formats)SelectedFormat)
			{
				case Formats.PlainText:
					byte[] buffer = new byte[1024];
					using (MemoryStream memoryStream = new MemoryStream())
					{
						int bytesRead;
						while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
						{
							memoryStream.Write(buffer, 0, bytesRead);
						}
						byte[] bytes = memoryStream.ToArray();
						rawText = Encoding.UTF8.GetString(bytes);
					}
					break;
				case Formats.Word:
					rawText = FormatsHelper.RawTextFromDocx(stream);
					break;
			}

			switch ((Parsers)SelectedParser)
			{
				case Parsers.Hyphen:
                    parsedFlashCards = HyphenParser.Parse(rawText, HyphenParser.ParseMode.DefinitionTerm);
					break;
			}
		}

		private async Task AddFlashCardsPack()
		{
			FlashCardsPack flashCardsPack = new FlashCardsPack
			{
				Title = $"Parse test. Parsed cards: {parsedFlashCards.Count}",
				FlashCards = parsedFlashCards
			};
			FlashCardsStore.Instance.AddItem(flashCardsPack);
			await Shell.Current.Navigation.PopAsync();
		}
	}
}
