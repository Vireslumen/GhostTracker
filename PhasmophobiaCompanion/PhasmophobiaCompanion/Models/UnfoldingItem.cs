using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhasmophobiaCompanion.Models
{
    public class UnfoldingItem : INotifyPropertyChanged
    {
        private bool isExpanded;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Body { get; set; }
        public string Header { get; set; }

        public string Icon
        {
            get
            {
                return IsExpanded ? "collapse_icon.png" : "expand_icon.png";
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Icon));
            }
        }

        public string Title { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}