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
        public event Action EquipmentsDataLoaded;
        public event Action CursedsDataLoaded;

        //Ghost data
        // Уже загружается на главной странице

        //Map data
        private ObservableCollection<Map> Maps;
        //private ObservableCollection<string> AllSizes;
        //private double minRoom = 0;
        //private double maxRoom = 0;

        //Equipment data
        private ObservableCollection<Equipment> Equipments;
        //private ObservableCollection<string> AllTiers;
        //private double minUnlockLevel = 0;
        //private double maxUnlockLevel = 100;

        //Cursed data
        private ObservableCollection<CursedPossession> Curseds;

        //Main Page data
        private ObservableCollection<ChallengeMode> ChallengeModes;
        private ObservableCollection<Quest> Quests;
        private ObservableCollection<string> Tips;
        private ObservableCollection<Difficulty> Difficulties;
        private ObservableCollection<Patch> Patches;
        private ObservableCollection<OtherInfo> OtherInfos;
        private ObservableCollection<Ghost> Ghosts;
        private ObservableCollection<Clue> Clues;

        public ObservableCollection<Ghost> GetGhosts()
        {
            return Ghosts;
        }
        public ObservableCollection<Clue> GetClues()
        {
            return Clues;
        }
        public ObservableCollection<Quest> GetQuests()
        {
            return Quests;
        }
        public ObservableCollection<string> GetTips()
        {
            return Tips;
        }
        public ObservableCollection<Difficulty> GetDifficulties()
        {
            return Difficulties;
        }
        public ObservableCollection<Patch> GetPatches()
        {
            return Patches;
        }
        public ObservableCollection<Map> GetMaps()
        {
            return Maps;
        }
        public ObservableCollection<Equipment> GetEquipments()
        {
            return Equipments;
        }
        public ObservableCollection<OtherInfo> GetOtherInfos()
        {
            return OtherInfos;
        }
        public ObservableCollection<CursedPossession> GetCurseds()
        {
            return Curseds;
        }
        public async Task LoadInitialDataAsync()
        {
            await LoadGhostsDataAsync();
            await LoadTipsDataAsync();
            await LoadDifficultiesAsync();
            await LoadPatchesAsync();
            await LoadQuestsAsync();
            await LoadOtherInfoAsync();
            await LoadMapsDataAsync();
            await LoadEquipmentsDataAsync();
            await LoadChallengeModeAsync();
        }
        public async Task LoadOtherDataAsync()
        {
            await LoadCursedsDataAsync();
        }
        public bool IsGhostsDataLoaded { get; private set; }
        public bool IsMapsDataLoaded { get; private set; }
        public bool IsEquipmentsDataLoaded { get; private set; }
        public bool IsCursedsDataLoaded { get; private set; }
        public async Task LoadGhostsDataAsync()
        {
            Ghosts = new ObservableCollection<Ghost>(await DatabaseLoader.GetGhostsAsync(LanguageCode, Clues));
            IsGhostsDataLoaded = true;
            GhostsDataLoaded?.Invoke();
        }
        public async Task LoadTipsDataAsync()
        {
            Tips = new ObservableCollection<string>(await DatabaseLoader.GetTipsAsync(LanguageCode));
        }
        public async Task LoadDifficultiesAsync()
        {
            Difficulties = new ObservableCollection<Difficulty>(await DatabaseLoader.GetDifficultiesAsync(LanguageCode));
        }
        public async Task LoadPatchesAsync()
        {
            Patches = new ObservableCollection<Patch>(await DatabaseLoader.GetPatchesAsync());
        }
        public async Task LoadQuestsAsync()
        {
            Quests = new ObservableCollection<Quest>(await DatabaseLoader.GetQuestsAsync(LanguageCode));
        }
        public async Task LoadOtherInfoAsync()
        {
            OtherInfos = new ObservableCollection<OtherInfo>(await DatabaseLoader.GetOtherInfosAsync(LanguageCode));
        }
        public async Task LoadChallengeModeAsync()
        {
            try
            {

                ChallengeModes = new ObservableCollection<ChallengeMode>(await DatabaseLoader.GetChallengeModesAsync(LanguageCode, Equipments, Maps, Difficulties));
            }
            catch (Exception ex)
            {
                Console.ReadLine();
            }
        }
        public async Task LoadMapsDataAsync()
        {
            Maps = new ObservableCollection<Map>(await DatabaseLoader.GetMapsAsync(LanguageCode));
            IsMapsDataLoaded = true;
            MapsDataLoaded?.Invoke();
        }
        public async Task LoadEquipmentsDataAsync()
        {
            Equipments = new ObservableCollection<Equipment>(await DatabaseLoader.GetEquipmentAsync(LanguageCode));
            IsEquipmentsDataLoaded = true;
            EquipmentsDataLoaded?.Invoke();

        }
        public async Task LoadCursedsDataAsync()
        {
            Curseds = new ObservableCollection<CursedPossession>(await DatabaseLoader.GetCursedPossessionsAsync(LanguageCode));
            IsCursedsDataLoaded = true;
            CursedsDataLoaded?.Invoke();
        }

    }
}
