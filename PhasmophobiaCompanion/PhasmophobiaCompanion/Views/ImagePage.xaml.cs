using System;
using PhasmophobiaCompanion.Models;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage : ContentPage
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

        private async void OnCloseRequested()
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

        protected override void OnDisappearing()
        {
            try
            {
                base.OnDisappearing();
                if (Content is InteractiveImageView interactiveImageView)
                {
                    interactiveImageView.TransparencyChanged -= OnTransparencyChanged;
                    interactiveImageView.CloseRequested -= OnCloseRequested;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка в OnDisappearing ImagePage.");
                throw;
            }
        }

        private void OnTransparencyChanged(float opacity)
        {
            try
            {
                BackgroundColor = Color.FromRgba(0, 0, 0, opacity);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при изменении прозрачности фона страницы ImagePage.");
                throw;
            }
        }
    }
}