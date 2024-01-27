using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    public class GhostsViewModel : BaseViewModel, ISearchable, IFilterable
    {

        private readonly DataService _dataService;
        private ObservableCollection<Ghost> ghosts;
        private ObservableCollection<Clue> allClues;

        private ObservableCollection<Ghost> filteredGhosts;
        private string searchQuery;
        private ObservableCollection<Clue> selectedClues;
        public ObservableCollection<Ghost> Ghosts
        {
            get { return filteredGhosts; }
            set
            {
                SetProperty(ref filteredGhosts, value);
            }
        }

        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                SetProperty(ref searchQuery, value);
                SearchGhosts();
            }
        }

        public ObservableCollection<Clue> AllClues
        {
            get { return allClues; }
            set { SetProperty(ref allClues, value); }
        }

        public ObservableCollection<Clue> SelectedClues
        {
            get { return selectedClues; }
            set
            {
                SetProperty(ref selectedClues, value);
            }
        }
        public ICommand SearchCommand { get; set; }
        public GhostsViewModel()
        {
            _dataService = DependencyService.Get<DataService>();
            ghosts = _dataService.GetGhosts();
            allClues = _dataService.GetClues();
            SelectedClues = new ObservableCollection<Clue>();
            Ghosts = new ObservableCollection<Ghost>(ghosts);
            AllClues = new ObservableCollection<Clue>(allClues);
            SearchCommand = new Command<string>(query => Search(query));
        }

        public void Filter()
        {
            var filtered = ghosts.Where(ghost => (!SelectedClues.Any() ||
            SelectedClues.All(selectedClue => ghost.Clues.Any(clue => clue.Title == selectedClue.Title)))).ToList();
            Ghosts = new ObservableCollection<Ghost>(filtered);
        }

        public void Search(string query)
        {
            SearchQuery = query;
        }

        private void SearchGhosts()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Ghosts = new ObservableCollection<Ghost>(ghosts);
            }
            else
            {
                var filtered = ghosts.Where(ghost => ghost.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                Ghosts = new ObservableCollection<Ghost>(filtered);
            }
        }

    }
}
