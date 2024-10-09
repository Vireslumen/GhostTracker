using System;
using System.ComponentModel;
using GhostTracker.ViewModels;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class FilterEquipmentPage
    {
        public FilterEquipmentPage(EquipmentsViewModel viewModel)
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации FilterEquipmentPage.");
            }
        }
    }
}