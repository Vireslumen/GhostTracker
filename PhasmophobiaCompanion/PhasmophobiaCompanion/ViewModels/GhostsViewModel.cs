using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
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
        private ObservableCollection<Ghost> ghosts;
        private ObservableCollection<Ghost> filteredGhosts;
        private string searchQuery;
        private ObservableCollection<CluesStructure> allClues;
        private ObservableCollection<CluesStructure> selectedClues;
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

        public ObservableCollection<CluesStructure> AllClues
        {
            get { return allClues; }
            set { SetProperty(ref allClues, value); }
        }

        public ObservableCollection<CluesStructure> SelectedClues
        {
            get { return selectedClues; }
            set
            {
                SetProperty(ref selectedClues, value);
            }
        }
        public ICommand SearchCommand { get; private set; }
        public GhostsViewModel()
        {
            allClues = new ObservableCollection<CluesStructure>
            {
                new CluesStructure { Name="Book", Source="Book_icon.png"},
                new CluesStructure { Name = "Radio", Source = "Radio_icon.png" },
                new CluesStructure { Name = "Minus", Source = "Minus_icon.png" }
            };
            AllClues = new ObservableCollection<CluesStructure>(allClues);
            ghosts = new ObservableCollection<Ghost>()
            {
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        new CluesStructure { Name="Radio", Source="Radio_icon.png"},
                        new CluesStructure { Name = "Radio", Source = "Radio_icon.png" },
                        new CluesStructure { Name = "Minus", Source = "Minus_icon.png" }
                    },
                    ImageUrl="Moroi.jpg",
                    Title="Moroi"
                },
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        new CluesStructure { Name="Radio", Source="Radio_icon.png"},
                        new CluesStructure { Name = "Book", Source = "Book_icon.png" },
                        new CluesStructure { Name = "Minus", Source = "Minus_icon.png" }
                    },
                    ImageUrl="Mara.jpg",
                    Title="Mara"
                },
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        new CluesStructure { Name="Minus", Source="Minus_icon.png"},
                        new CluesStructure { Name = "Radio", Source = "Radio_icon.png" },
                        new CluesStructure { Name = "Book", Source = "Book_icon.png" }
                    },
                    ImageUrl="Banshi.jpg",
                    Title="Banshi"
                },
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        new CluesStructure { Name="Book", Source="Book_icon.png"},
                        new CluesStructure { Name = "Minus", Source = "Minus_icon.png" },
                        new CluesStructure { Name = "Radio", Source = "Radio_icon.png" },
                        new CluesStructure { Name = "Minus", Source = "Minus_icon.png" }
                    },
                    ImageUrl="Polter.jpg",
                    Title="Polter"
                },
            };
            Ghosts = new ObservableCollection<Ghost>(ghosts);
            SelectedClues = new ObservableCollection<CluesStructure>();
            SearchCommand = new Command<string>(query => Search(query));
        }

        public void Filter()
        {
            var filtered = ghosts.Where(ghost => (!SelectedClues.Any() ||
            SelectedClues.All(selectedClue => ghost.Clues.Any(clue => clue.Name == selectedClue.Name)))).ToList();
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
