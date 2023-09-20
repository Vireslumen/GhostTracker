using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostPage : ContentPage
    {
        public GhostPage()
        {
            InitializeComponent();
            GhostsViewModel viewModel = new GhostsViewModel();
            BindingContext = viewModel;
        }
        private async void FilterTapped(object sender, EventArgs e)
        {
            if (BindingContext is GhostsViewModel viewModel)
            {
                var filterPage = new FilterPage(viewModel);
                await PopupNavigation.Instance.PushAsync(filterPage);
            }
        }
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            if(BindingContext is ViewModels.GhostsViewModel viewModel)
            {
                viewModel.SearchCommand.Execute(null);
            }
        }
    }
}