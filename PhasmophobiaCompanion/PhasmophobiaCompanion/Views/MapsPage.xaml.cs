using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapsPage : ContentPage
	{
		public MapsPage ()
		{
			InitializeComponent ();
			MapsViewModel viewModel = new MapsViewModel ();
			BindingContext = viewModel;
        }
        private async void FilterTapped(object sender, EventArgs e)
        {
            if (BindingContext is MapsViewModel viewModel)
            {
                var filterPage = new MapsFilterPage(viewModel);
                await PopupNavigation.Instance.PushAsync(filterPage);
            }
        }
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            if (BindingContext is ViewModels.MapsViewModel viewModel)
            {
                viewModel.SearchCommand.Execute(null);
            }
        }
        private void OnMapTapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is Map selectMap)
            {
                // Создайте экземпляр вашей детальной страницы, передавая выбранный призрак
                var detailPage = new MapDetailPage(selectMap);

                // Используйте навигацию для открытия детальной страницы
                Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
        }
    }
}