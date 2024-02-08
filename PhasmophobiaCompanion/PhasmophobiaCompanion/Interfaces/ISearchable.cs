using System.Windows.Input;

namespace PhasmophobiaCompanion.Interfaces
{
    public interface ISearchable
    {
        ICommand SearchCommand { get; set; }
        string SearchQuery { get; set; }
        void Search(string query);
    }
}