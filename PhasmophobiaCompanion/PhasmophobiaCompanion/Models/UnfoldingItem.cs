using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Serilog;

namespace PhasmophobiaCompanion.Models
{
    public class UnfoldingItem : INotifyPropertyChanged
    {
        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Icon));
            }
        }
        public string Body { get; set; }
        public string Header { get; set; }
        public string Icon => IsExpanded ? "collapse_icon.png" : "expand_icon.png";
        public string Title { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во смены значения UnfoldingItem.");
                throw;
            }
        }
    }
}