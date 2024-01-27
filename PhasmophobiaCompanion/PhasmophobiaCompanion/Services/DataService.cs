using PhasmophobiaCompanion.Data;
using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PhasmophobiaCompanion.Services
{
    public class DataService
    {
        private DatabaseLoader DatabaseLoader;
        private string LanguageCode;
        public DataService()
        {
            DatabaseLoader = new DatabaseLoader(new PhasmaDB());
            LanguageCode = "EN";
            Clues = new ObservableCollection<Clue>();
        }
        public event Action GhostsDataLoaded;
        public event Action MapsDataLoaded;
        public event Action EquipmentDataLoaded;
        public event Action CursedDataLoaded;

        //Ghost data
        // Уже загружается на главной странице

        //Map data
        private ObservableCollection<Map> Maps;
        private ObservableCollection<string> AllSizes;
        //private double minRoom = 0;
        //private double maxRoom = 0;

        //Equipment data
        private ObservableCollection<Equipment> Equipments;
        private ObservableCollection<string> AllTiers;
        //private double minUnlockLevel = 0;
        //private double maxUnlockLevel = 100;

        //Cursed data
        private ObservableCollection<CursedPossession> Curseds;

        //Main Page data
        private SpecialMode specialMode;
        private ObservableCollection<string> Tips;
        private ObservableCollection<Difficulty> Difficulties;
        private ObservableCollection<Patch> Patches;
        private ObservableCollection<string> DailyQuest;
        private ObservableCollection<string> WeeklyQuest;
        private ObservableCollection<OtherInfo> OtherInfos;
        private ObservableCollection<Ghost> Ghosts;
        private ObservableCollection<Clue> Clues;


        public async Task LoadInitialDataAsync()
        {
            await LoadGhostsDataAsync();
        }

        public bool IsGhostsDataLoaded { get; private set; }
        public bool IsMapsDataLoaded { get; private set; }
        public bool IsEquipmentsDataLoaded { get; private set; }
        public bool IsCursedsDataLoaded { get; private set; }

        public async Task LoadGhostsDataAsync()
        {
            Ghosts= new ObservableCollection<Ghost>(await DatabaseLoader.GetGhostsAsync(LanguageCode,Clues));
            IsGhostsDataLoaded = true;
        }
        public async Task LoadMapsDataAsync()
        {
            // Загрузка данных
            IsMapsDataLoaded = true;
        }
        public async Task LoadEquipmentsDataAsync()
        {
            // Загрузка данных
            IsEquipmentsDataLoaded = true;
        }
        public async Task LoadCursedsDataAsync()
        {
            // Загрузка данных
            IsCursedsDataLoaded = true;
        }

        public ObservableCollection<Ghost> GetGhosts()
        {
            return Ghosts;
        }
        public ObservableCollection<Clue> GetClues()
        {
            return Clues;
        }
    }
}
