using System;
using System.Collections.Generic;
using PhasmophobiaCompanion.ViewModels;
using PhasmophobiaCompanion.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace PhasmophobiaCompanion.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EquipmentPage : ContentPage
	{
		public EquipmentPage ()
		{
			InitializeComponent ();
			EquipmentsViewModel viewModel = new EquipmentsViewModel();
			BindingContext = viewModel;
		}
        private async void FilterTapped(object sender, EventArgs e)
        {
            //if (BindingContext is GhostsViewModel viewModel)
            //{
            //    var filterPage = new FilterPage(viewModel);
            //    await PopupNavigation.Instance.PushAsync(filterPage);
            //}
        }
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            if (BindingContext is ViewModels.EquipmentsViewModel viewModel)
            {
                viewModel.SearchCommand.Execute(null);
            }
        }

        private void OnEquipmentTapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is Ghost selectedEquipment)
            {
                // Создайте экземпляр вашей детальной страницы, передавая выбранный призрак
                //var detailPage = new EquipmentDetailPage(selectedEquipment);

                // Используйте навигацию для открытия детальной страницы
                //await Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
        }
    }
}