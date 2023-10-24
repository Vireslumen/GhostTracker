using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
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
	public partial class CursedsPage : ContentPage
	{
		public CursedsPage ()
		{
			InitializeComponent ();
            CursedViewModel viewModel = new CursedViewModel();
            BindingContext = viewModel;
        }
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            if (BindingContext is ViewModels.CursedViewModel viewModel)
            {
                viewModel.SearchCommand.Execute(null);
            }
        }

        private void OnCursedTapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is CursedPossession selectedCursed)
            {
                // Создайте экземпляр вашей детальной страницы, передавая выбранный призрак
                var detailPage = new CursedDetailPage(selectedCursed);

                // Используйте навигацию для открытия детальной страницы
                Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
        }
    }
}