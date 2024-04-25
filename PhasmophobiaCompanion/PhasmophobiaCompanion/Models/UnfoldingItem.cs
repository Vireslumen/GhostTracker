using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Serilog;

namespace PhasmophobiaCompanion.Models
{
    public class UnfoldingItem : INotifyPropertyChanged
    {
        private bool isExpanded;
        private string body;
        public bool CanExpand => !string.IsNullOrWhiteSpace(Body);
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
        public string Body
        {
            get => body;
            set
            {
                body = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanExpand));
                OnPropertyChanged(nameof(Icon));
            }
        }
        public string Header { get; set; }
        public string Icon => CanExpand ? IsExpanded ? "icon_collapse.png" : "icon_expand.png" : null;
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