using System;
using System.Diagnostics;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingScreenPage : ContentPage
    {
        public LoadingScreenPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации LoadingScreenPage.");
            }
        }
    }
}