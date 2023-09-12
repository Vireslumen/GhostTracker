using PhasmophobiaCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.ViewModels
{
    public class GhostsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        public List<IListItem> ghosts;
        public List<IListItem> Ghosts
        {
            get { return ghosts; }
            set { ghosts = value;
                OnPropertyChanged();
            }
        }
        public List<IListItem> Filter(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public void Search(string query)
        {
            throw new NotImplementedException();
        }
    }
}
