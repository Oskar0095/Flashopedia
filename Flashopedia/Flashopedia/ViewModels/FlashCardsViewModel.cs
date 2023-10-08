using Flashopedia.Services;
using Flashopedia.Views;
using Flashopedia;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Flashopedia.ViewModels
{
	public class FlashCardsViewModel : ObservableObject
	{
		public ICommand CreateNewFlashCardsCommand { get; private set; }

		public FlashCardsViewModel()
		{
			CreateNewFlashCardsCommand = new AsyncCommand(CreateNewFlashCards);
		}

		public ObservableCollection<FlashCardsPack> FlashCardsPacks
		{
			get => FlashCardsStore.Instance.GetItems();
		}

		private async Task CreateNewFlashCards()
		{
			await Shell.Current.Navigation.PushAsync(new AddFlashCardPage());
		}
	}
}
