using System;
using PhasmophobiaCompanion.Models;
using Serilog;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной некатегоризованной страницы.
    /// </summary>
    public class OtherViewModel : BaseViewModel
    {
        private OtherInfo otherInfo;

        public OtherViewModel(OtherInfo otherInfo)
        {
            try
            {
                OtherInfo = otherInfo;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации OtherViewModel.");
                throw;
            }
        }

        public OtherInfo OtherInfo
        {
            get => otherInfo;
            set
            {
                otherInfo = value;
                OnPropertyChanged();
            }
        }
    }
}