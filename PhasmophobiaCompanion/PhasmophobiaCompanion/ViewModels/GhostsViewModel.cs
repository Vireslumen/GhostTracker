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
            Ghosts = new ObservableCollection<Ghost>()
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
