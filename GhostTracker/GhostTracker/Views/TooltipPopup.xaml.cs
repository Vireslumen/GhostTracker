﻿using System;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TooltipPopup
    {
        public TooltipPopup(string message)
        {
            try
            {
                InitializeComponent();
                BindingContext = message;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации TooltipPopup.");
            }
        }

        /// <summary>
        ///     Закрытие всплывающей подсказки по нажатию на фон.
        /// </summary>
        protected override bool OnBackgroundClicked()
        {
            PopupNavigation.Instance.PopAsync();
            return false;
        }
    }
}