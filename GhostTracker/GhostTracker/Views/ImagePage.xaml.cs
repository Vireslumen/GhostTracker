using System;
using GhostTracker.Models;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage
    {
        public ImagePage(ImageWithDescription imageData)
        {
            try
            {
                InitializeComponent();
                BindingContext = imageData;

                if (Content is InteractiveImageView interactiveImageView)
                {
                    interactiveImageView.TransparencyChanged += OnTransparencyChanged;
                    interactiveImageView.CloseRequested += OnCloseRequested;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка инициализации ImagePage.");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (!(Content is InteractiveImageView interactiveImageView)) return;
            interactiveImageView.TransparencyChanged -= OnTransparencyChanged;
            interactiveImageView.CloseRequested -= OnCloseRequested;
        }

        private static async void OnCloseRequested()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при закрытии страницы ImagePage.");
            }
        }

        private void OnTransparencyChanged(float opacity)
        {
            BackgroundColor = Color.FromRgba(0, 0, 0, opacity);
        }
    }
}