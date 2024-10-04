using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой раскрывающийся элемент с текстом.
    /// </summary>
    public class UnfoldingItem : INotifyPropertyChanged
    {
        private bool isExpanded;
        private string body;
        public virtual bool CanExpand => !string.IsNullOrWhiteSpace(Body);
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
        public string Icon => GetThemedIcon();
        public string Title { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string GetThemedIcon()
        {
            var themePrefix = Application.Current.UserAppTheme == OSAppTheme.Dark ||
                              (Application.Current.UserAppTheme == OSAppTheme.Unspecified &&
                               Application.Current.RequestedTheme == OSAppTheme.Dark)
                ? "dark_"
                : "";
            return CanExpand ? IsExpanded ? $"{themePrefix}icon_collapse.png" : $"{themePrefix}icon_expand.png" : null;
        }
    }
}