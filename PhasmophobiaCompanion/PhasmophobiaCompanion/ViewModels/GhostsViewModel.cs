using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.ViewModels
{
    public class GhostsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        public ObservableCollection<Ghost> ghosts;
        public ObservableCollection<Ghost> Ghosts
        {
            get { return ghosts; }
            set
            {
                ghosts = value;
            }
        }
        public GhostsViewModel()
        {
            Ghosts = new ObservableCollection<Ghost>();
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
