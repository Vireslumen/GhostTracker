using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GhostTracker.Views;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     Базовый ViewModel класс для страниц.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected bool IsNavigating;
        private string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public async Task NavigateWithLoadingAsync(Page page)
        {
            IsNavigating = true;
            var isPopupShown = false;
            try
            {
                var loadingPopup = new LoadingPopup();
                await PopupNavigation.Instance.PushAsync(loadingPopup);
                isPopupShown = true;
                var existingPages = Application.Current.MainPage.Navigation.NavigationStack;
                while (existingPages.Count > 3)
                {
                    var pageToRemove = existingPages[1];
                    Application.Current.MainPage.Navigation.RemovePage(pageToRemove);
                }

                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на страницу с использованием индикатора загрузки.");
            }
            finally
            {
                if (isPopupShown)
                    try
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Ошибка при попытке закрыть popup загрузки.");
                    }

                IsNavigating = false;
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}