using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PhasmophobiaCompanion.Interfaces
{
    public interface ISearchable
    {
        void Search(string query);
        string SearchQuery { get; set; }
        ICommand SearchCommand { get; set; }
    }
}
