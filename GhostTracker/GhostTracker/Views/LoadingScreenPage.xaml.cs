using System;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingScreenPage
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