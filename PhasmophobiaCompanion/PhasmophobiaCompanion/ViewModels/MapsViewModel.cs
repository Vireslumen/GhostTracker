using AutoFixture;
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
    public class MapsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                SetProperty(ref searchQuery, value);
                SearchMaps();
            }
        }

        private void SearchMaps()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Maps = new ObservableCollection<Map>(maps);
            }
            else
            {
                var filtered = maps.Where(maps => maps.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                Maps = new ObservableCollection<Map>(filtered);
            }
        }

        private string searchQuery;
        public ICommand SearchCommand { get; set; }
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

        public MapsViewModel()
        {
            var fixture = new Fixture();
            var items = fixture.CreateMany<Map>(10);
            maps = new ObservableCollection<Map>(items);
            for (int i = 0; i < maps.Count; i++)
            {
                Random rand = new Random();
                int x = rand.Next(3);
                switch (x)
                {
                    case 0:
                        maps[i].ImageUrl = "asylum.jpg";
                        maps[i].Size = "Small";
                        break;
                    case 1:
                        maps[i].ImageUrl = "bframhouse.jpg";
                        maps[i].Size = "Medium";
                        break;
                    case 2:
                        maps[i].ImageUrl = "campwood.jpg";
                        maps[i].Size = "Large";
                        break;


                }
            }
            maps.Add(new Map
            {
                Title = "Campwood",
                Description = "Camp Woodwind is a small map in Phasmophobia. It very much resembles Maple Lodge Campsite, but is much more compact in size, bearing only a handful of tents, a couple of bathrooms, and the central campfire. This map unlocks at player level 13.",
                ImageUrl = "campwood.jpg",
                Size = "Small",
                RoomCount = 10,
                UnlockLvl = 12,
                Floors = 1,
                Exits = 2,
                HidenSpotCount = "2-5",
                ExpandFieldsWithImages = new ObservableCollection<ExpandFieldWithImages>
                {


                    new ExpandFieldWithImages
                    {
                        Title = "Map Structure",
                        Header = "Small map with 1 floor",
                        Body = "...",
                        ImageWithDescriptions = new ObservableCollection<ImagewithDescription>
                {
                    new ImagewithDescription{
                    Description="Full map",
                    ImageSource="asylum.jpg"
                    }
                }
                    },
                   new ExpandFieldWithImages
                    {
                        Title = "Cursed locations",
                        Header = "Cursed items locates far from home",
                        Body = "...",
                        ImageWithDescriptions = new ObservableCollection<ImagewithDescription>
                {
                   new ImagewithDescription{
                    Description="Monkey paw",
                    ImageSource="asylum.jpg"
                    },
                   new ImagewithDescription{
                    Description="Monkey paw2",
                    ImageSource="bfframhouse.jpg"
                    },
                   new ImagewithDescription{
                    Description="Monkey paw3",
                    ImageSource="asylum.jpg"
                    },
                   new ImagewithDescription{
                    Description="Monkey paw4",
                    ImageSource="bfframhouse.jpg"
                    },
                   new ImagewithDescription{
                    Description="Monkey paw5",
                    ImageSource="asylum.jpg"
                    },
                   new ImagewithDescription{
                    Description="Monkey paw6",
                    ImageSource="bfframhouse.jpg"
                    },
                   new ImagewithDescription{
                    Description="Monkey paw7",
                    ImageSource="asylum.jpg"
                    },
                   new ImagewithDescription{
                    Description="Monkey paw8",
                    ImageSource="bfframhouse.jpg"
                    },
                   new ImagewithDescription{
                    Description="Monkey paw9",
                    ImageSource="asylum.jpg"
                    },

                }
                    },
                    new ExpandFieldWithImages
                    {
                        Title = "Hiding spot location",
                        Header = "Map has 2-55 spot location ()(&(",
                        Body = "...",
                        ImageWithDescriptions = new ObservableCollection<ImagewithDescription>
                {
                    new ImagewithDescription
                    {
                        ImageSource="bfframhouse.jpg",
                        Description="Good spot"
                    },
                    new ImagewithDescription
                    {
                        ImageSource="bfframhouse.jpg",
                        Description="Good spot2"
                    },
                    new ImagewithDescription
                    {
                        ImageSource="bfframhouse.jpg",
                        Description="Good spot3"
                    },
                    new ImagewithDescription
                    {
                        ImageSource="bfframhouse.jpg",
                        Description="Good spot4"
                    }

                }
                    },
                    new ExpandFieldWithImages
                    {
                        Title = "Cycling spot location",
                        Header = "There is 2 spot on this map",
                        Body = "...",
                        ImageWithDescriptions = new ObservableCollection<ImagewithDescription>
                {
                    new ImagewithDescription
                    {
                        ImageSource="asylum.jpg",
                        Description = "FIrst spot is pretty good"
                    },
                    new ImagewithDescription
                    {
                        ImageSource="asylum.jpg",
                        Description = "Second spot is pretty good"
                    }
                }
                    },
                },
                UnfoldingItems = new ObservableCollection<UnfoldingItem>
            {
                new UnfoldingItem
                {
                    Title="Tips",
                    Header="There is several tips on this map",
                    Body="First, ypu need to x_x",
                },
                new UnfoldingItem
                {
                    Title="Trivia",
                    Header="avoabas asjdajsd asdasdb asmdajsd",
                    Body="dahsdgjasdjasdasd asdjhasbdjahs djas dhjasdhjasdjas dhajsd hasdhajsd ahsdajsdhajsd ahs djasd has djashdjhasdhasjd hjasdj",
                }
            }
            });
            Maps = new ObservableCollection<Map>(maps);
            allSizes = new ObservableCollection<string>
            {
                "Small",
                "Medium",
                "Large"
            };
            AllSizes = new ObservableCollection<string>(allSizes);
            SelectedSizes = new ObservableCollection<string>();
            SearchCommand = new Command<string>(query => Search(query));
        }

        private double minRoom = 0;
        public double MinRoom
        {
            get { return minRoom; }
            set
            {
                if (minRoom != value)
                {
                    minRoom = value;
                    OnPropertyChanged(nameof(MinRoom));
                }
            }
        }

        private double maxRoom = 100;
        public double MaxRoom
        {
            get { return maxRoom; }
            set
            {
                if (maxRoom != value)
                {
                    maxRoom = value;
                    OnPropertyChanged(nameof(MaxRoom));
                }
            }
        }
        private ObservableCollection<string> allSizes;

        public ObservableCollection<string> AllSizes
        {
            get { return allSizes; }
            set
            {
                SetProperty(ref allSizes, value);
            }
        }
        private ObservableCollection<string> selectedSizes;
        public ObservableCollection<string> SelectedSizes
        {
            get { return selectedSizes; }
            set
            {
                SetProperty(ref selectedSizes, value);
            }
        }
        public void Filter()
        {
            var filteredSize = maps.Where(maps => (!SelectedSizes.Any() || SelectedSizes.Any(selectedSize => maps.Size == selectedSize))).ToList();
            var filteredRoom = filteredSize.Where(maps => ((MinRoom <= maps.RoomCount && MaxRoom >= maps.RoomCount))).ToList();
            Maps = new ObservableCollection<Map>(filteredRoom);
        }

        public void Search(string query)
        {
            SearchQuery = query;
        }
    }
}
