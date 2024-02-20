using System.Windows.Input;

namespace PhasmophobiaCompanion.Interfaces
{
    public interface ISearchable
    {
        ICommand SearchCommand { get; }
        string SearchQuery { get; set; }
        void OnSearchCompleted(string query);
    }
}