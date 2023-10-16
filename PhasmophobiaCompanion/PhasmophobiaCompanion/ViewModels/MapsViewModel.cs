using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace PhasmophobiaCompanion.ViewModels
{
    public class MapsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        public string SearchQuery { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICommand SearchCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private ObservableCollection<Map> maps;
        private ObservableCollection<Map> filteredMaps;
        public ObservableCollection<Map> Maps
        {
            get { return filteredMaps; }
            set
            {
                SetProperty(ref filteredMaps, value);
            }
        }
        public void Filter()
        {
            throw new NotImplementedException();
        }

        public void Search(string query)
        {
            throw new NotImplementedException();
        }
    }
}
