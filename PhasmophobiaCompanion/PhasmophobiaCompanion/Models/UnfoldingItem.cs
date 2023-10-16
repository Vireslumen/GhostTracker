using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhasmophobiaCompanion.Models
{
    public class UnfoldingItem : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Icon
        {
            get
            {
                return IsExpanded ? "collapse_icon.png" : "expand_icon.png";
            }
        }


        bool isExpanded;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}