using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
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
                FilterGhosts();
            }
        }
        public ICommand SearchCommand { get; private set; }
        public GhostsViewModel()
        {
            ghosts = new ObservableCollection<Ghost>()
            {
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        new CluesStructure { Name="Book", Source="Book_icon.png"},
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
            SearchCommand = new Command<string>(query=>Search(query));
        }
        public List<IListItem> Filter(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public void Search(string query)
        {
            SearchQuery = query;
        }

        private void FilterGhosts()
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
