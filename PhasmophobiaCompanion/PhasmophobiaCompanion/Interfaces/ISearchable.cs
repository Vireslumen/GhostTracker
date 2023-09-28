using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Interfaces
{
    public interface ISearchable
    {
        void Search(string query);
        string SearchQuery { get; set; }
    }
}
