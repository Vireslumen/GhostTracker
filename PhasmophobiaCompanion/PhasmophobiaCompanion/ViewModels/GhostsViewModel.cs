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
                new CluesStructure { Name="Ghost Writing", Source="Book_icon.png"},
                new CluesStructure { Name = "Spirit Box", Source = "Radio_icon.png" },
                new CluesStructure { Name = "Freezing Temperatures", Source = "Minus_icon.png" },
                new CluesStructure { Name = "Ghost Orb", Source = "Ghost_orb.png" },
                new CluesStructure { Name = "Ultraviolet", Source = "Ultraviolet.png" },
                new CluesStructure { Name = "EMF Level 5", Source = "EMF.png" },
                new CluesStructure { Name = "D.O.T.S Projector", Source = "DOTS.png" }
            };
            AllClues = new ObservableCollection<CluesStructure>(allClues);
            ghosts = new ObservableCollection<Ghost>()
            {
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        AllClues[0],
                        AllClues[2],
                        AllClues[1]
                    },
                    ImageUrl="Moroi.jpg",
                    Title="Moroi",
                    Description="The Moroi is a type of ghost in Phasmophobia. Its ability to \"curse\" players to lower their Sanity, combined with its increased speed at low sanity levels, can cause it to become the fastest ghost in the game.",
                    Strength= new ObservableCollection<string>
                    {
                        "Hunting speeds depend directly on average team sanity.",
                        "The Moroi's speed is dependent on the average sanity at the beginning of the hunt. The base speed increases by an average of 0.083 m/s for every 5% average sanity below its average hunt threshold of 50%."
                    },
                    Abilities= new ObservableCollection<string>
                    {
                        "Can curse players through the spirit box or the parabolic microphone, making them lose sanity faster.",
                        "The Moroi has an ability to \"curse\" a target through the following ways:\r\nBy successfully obtaining a response from the Spirit Box; this will curse the person who \"caused\" the response, either by asking questions or triggering the failsafe.\r\nBy hearing whispers through the parabolic microphone.\r\nWhen a player is cursed, their passive sanity drain is doubled, being in a lit area will no longer prevent sanity drain, and the light from Firelights will no longer mitigate against sanity loss. Walking outside of the investigation area will still pause sanity drain until they enter again. Consuming Sanity Medication removes the curse for that player. Players that have been cursed before in the contract can be cursed again. Multiple players can be cursed at the same time, but curses do not stack on a single player."
                    },
                    Weaknesses= new ObservableCollection<string>
                    {
                        "Will be disoriented for longer when smudged during a hunt.",
                        "When using an Incense during a hunt, the duration where the Moroi cannot see nor kill players is 7.5 seconds instead of 5."
                    },
                    SanityHunt = new ObservableCollection<string>
                    {
                        "Moroi, like a regular ghost, can start hunting starting at 50% sanity.",
                        "Moroi, like a regular ghost, can start hunting starting at 50% sanity."
                    },
                    Speed = new ObservableCollection<string>
                    {
                        "The Moroi's speed is dependent on the average sanity at the beginning of the hunt.",
                        "The base speed increases by an average of 0.083 m/s for every 5% average sanity below its average hunt threshold of 50%. The list below shows each possible base speed for the Moroi:Average sanity %\tGhost speed\r\n≥ 45%\t1.5 m/s\r\n40% - 45%\t1.583 m/s\r\n35% - 40%\t1.66 m/s\r\n30% - 35%\t1.749 m/s\r\n25% - 30%\t1.832 m/s\r\n20% - 25%\t1.915 m/s\r\n15% - 20%\t1.998 m/s\r\n10% - 15%\t2.081 m/s\r\n5% - 15%\t2.164 m/s\r\n0% - 5%\t2.25 m/s\r\n\r\nThe speed changes during a hunt according to the current average sanity. Unlike most other ghosts with implicit speed bonuses, the Moroi accelerates with continuous line-of-sight of a player. Below 5% average sanity, the maximum speed of a Moroi is 3.71 m/s, almost 25% faster than the Revenant's 3 m/s."
                    },
                    Identification = "The easiest way to identify a Moroi is by paying attention to the ghost's speed:\r\n\r\nListen for the ghost's speed both over multiple hunts and during a given hunt, as average sanity lowers; a ghost that speeds up over the course of multiple hunts as average sanity lowers, or gradually speeds up during wandering when all players are hiding, could be a Moroi. Remember to take into account line-of-sight acceleration, if applicable. Note that the Hantu will increase in speed if the fuse box is left off as the temperature in the map decreases, but it does not speed up through line-of-sight; either keep the fuse box turned on to avoid misidentification, or make sure that the ghost does not have the Hantu's signature freezing breath during a chase with the power off.\r\nAs an alternate strategy, sanity medication can be utilized during a hunt. As the Moroi's roaming speed changes in real time as average sanity changes, a significant increase of it while it's hunting will cause the Moroi to slow down, potentially identifying it in the span of a single hunt, while also potentially bolstering sanity enough to get the team above hunt range afterwards. All players should be hidden (or, most optimally, having been hidden from the start) to avoid the speed loss being confused for a ghost's deceleration from line-of-sight or the Revenant's losing track of players, and to wait a few seconds in order to get a read on its roaming speed before using the medication to note if the Moroi begins to slow down afterwards. Additionally, a marked degree of coordination in multiplayer is required, as multiple uses will be needed to raise average sanity enough to cause the Moroi to lose a definitive amount of speed. This strategy is much more reliable when using Tier III sanity medication in order to have sanity increase more quickly, which will both exacerbate this effect and reduce the odds that the hunt ends before sanity restoration kicks in sufficently.\r\nWhen the sanity monitor is available, one can also identify a Moroi based on its ability: if your sanity has drained much more than normal after a Spirit Box response or a whisper on the parabolic microphone, especially if you've been in fully lit rooms and/or holding a Firelight for extended periods of time, then it is likely a Moroi. Take into account sanity drain from ghost events, if any, as well as if you have used any Cursed possessions throughout the mission."
                },
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        AllClues[0],
                        AllClues[3],
                        AllClues[1]
                    },
                    ImageUrl="Mara.jpg",
                    Title="Mare"
                },
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        AllClues[6],
                        AllClues[3],
                        AllClues[4]
                    },
                    ImageUrl="Banshi.jpg",
                    Title="Banshee"
                },
                new Ghost()
                {
                    Clues=new ObservableCollection<CluesStructure>
                    {
                        AllClues[0],
                        AllClues[4],
                        AllClues[1]
                    },
                    ImageUrl="Polter.jpg",
                    Title="Poltergeist"
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
        private async void OnGhostTapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is Ghost selectedGhost)
            {
                // Создайте экземпляр вашей детальной страницы, передавая выбранный призрак
                var detailPage = new GhostDetailPage(selectedGhost);

                // Используйте навигацию для открытия детальной страницы
                await Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
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
