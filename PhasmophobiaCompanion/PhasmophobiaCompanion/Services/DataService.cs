using Newtonsoft.Json;
using PhasmophobiaCompanion.Data;
using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PhasmophobiaCompanion.Services
{
    public class DataService
    {
        private DatabaseLoader DatabaseLoader;
        private string LanguageCode;
        public string FolderPath;
        public DataService()
        {
            DatabaseLoader = new DatabaseLoader(new PhasmaDB());
            LanguageCode = "EN";
            Clues = new ObservableCollection<Clue>();
            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
        public event Action GhostsDataLoaded;
        public event Action MapsDataLoaded;
        public event Action EquipmentsDataLoaded;
        public event Action CursedsDataLoaded;

        //Map data
        private ObservableCollection<Map> Maps;

        //Equipment data
        private ObservableCollection<Equipment> Equipments;

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
        public ChallengeMode GetChallengeMode(int challengeID)
        {
            return ChallengeModes[challengeID];
        }
        public async Task LoadInitialDataAsync()
        {
            try
            {
                await LoadGhostsDataAsync();
                await LoadCluesAsync();
                await LoadTipsDataAsync();
                await LoadDifficultiesAsync();
                await LoadPatchesAsync();
                await LoadQuestsAsync();
                await LoadOtherInfoAsync();
                await LoadChallengeModeAsync();
                foreach (var ghost in Ghosts)
                {
                    ghost.PopulateAssociatedClues(Clues);
                }
                foreach (var clue in Clues)
                {
                    clue.PopulateAssociatedGhosts(Ghosts);
                }
            }
            catch (Exception ex)
            {
                Console.ReadLine();
            }
        }
        public async Task LoadOtherDataAsync()
        {
            await LoadMapsDataAsync().ConfigureAwait(false);
            await LoadCursedsDataAsync().ConfigureAwait(false);
            await LoadEquipmentsDataAsync().ConfigureAwait(false);
        }
        public bool IsGhostsDataLoaded { get; private set; }
        public bool IsMapsDataLoaded { get; private set; }
        public bool IsEquipmentsDataLoaded { get; private set; }
        public bool IsCursedsDataLoaded { get; private set; }
        public async Task LoadGhostsDataAsync()
        {
            string filePath = Path.Combine(FolderPath, "ghosts_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                Ghosts = JsonConvert.DeserializeObject<ObservableCollection<Ghost>>(cachedData);
            }
            else
            {
                Ghosts = new ObservableCollection<Ghost>(await DatabaseLoader.GetGhostsAsync(LanguageCode));
                string serializedData = JsonConvert.SerializeObject(Ghosts);
                File.WriteAllText(filePath, serializedData);
            }
            IsGhostsDataLoaded = true;
            GhostsDataLoaded?.Invoke();
        }
        public async Task LoadTipsDataAsync()
        {
            string filePath = Path.Combine(FolderPath, "tips_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                Tips = JsonConvert.DeserializeObject<ObservableCollection<string>>(cachedData);
            }
            else
            {
                Tips = new ObservableCollection<string>(await DatabaseLoader.GetTipsAsync(LanguageCode));
                string serializedData = JsonConvert.SerializeObject(Tips);
                File.WriteAllText(filePath, serializedData);
            }
        }
        public async Task LoadDifficultiesAsync()
        {
            string filePath = Path.Combine(FolderPath, "difficulties_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                Difficulties = JsonConvert.DeserializeObject<ObservableCollection<Difficulty>>(cachedData);
            }
            else
            {
                Difficulties = new ObservableCollection<Difficulty>(await DatabaseLoader.GetDifficultiesAsync(LanguageCode));
                string serializedData = JsonConvert.SerializeObject(Difficulties);
                File.WriteAllText(filePath, serializedData);
            }
        }
        public async Task LoadPatchesAsync()
        {
            string filePath = Path.Combine(FolderPath, "patch_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                Patches = JsonConvert.DeserializeObject<ObservableCollection<Patch>>(cachedData);
            }
            else
            {
                Patches = new ObservableCollection<Patch>(await DatabaseLoader.GetPatchesAsync());
                string serializedData = JsonConvert.SerializeObject(Patches);
                File.WriteAllText(filePath, serializedData);
            }
        }
        public async Task LoadQuestsAsync()
        {
            string filePath = Path.Combine(FolderPath, "quest_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                Quests = JsonConvert.DeserializeObject<ObservableCollection<Quest>>(cachedData);
            }
            else
            {
                Quests = new ObservableCollection<Quest>(await DatabaseLoader.GetQuestsAsync(LanguageCode));
                string serializedData = JsonConvert.SerializeObject(Quests);
                File.WriteAllText(filePath, serializedData);
            }
        }
        public async Task LoadOtherInfoAsync()
        {
            string filePath = Path.Combine(FolderPath, "other_info_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                OtherInfos = JsonConvert.DeserializeObject<ObservableCollection<OtherInfo>>(cachedData);
            }
            else
            {
                OtherInfos = new ObservableCollection<OtherInfo>(await DatabaseLoader.GetOtherInfosAsync(LanguageCode));
                string serializedData = JsonConvert.SerializeObject(OtherInfos);
                File.WriteAllText(filePath, serializedData);
            }

        }
        public async Task LoadChallengeModeAsync()
        {
                ChallengeModes = new ObservableCollection<ChallengeMode>(await DatabaseLoader.GetChallengeModesAsync(LanguageCode));
        }

        public async Task LoadCluesAsync()
        {
            Clues=new ObservableCollection<Clue>(await DatabaseLoader.GetCluesAsync(LanguageCode));
        }

        public async Task LoadMapsDataAsync()
        {
            string filePath = Path.Combine(FolderPath, "maps_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                Maps = JsonConvert.DeserializeObject<ObservableCollection<Map>>(cachedData);
            }
            else
            {
                Maps = new ObservableCollection<Map>(await DatabaseLoader.GetMapsAsync(LanguageCode).ConfigureAwait(false));
                string serializedData = JsonConvert.SerializeObject(Maps);
                File.WriteAllText(filePath, serializedData);
            }
            IsMapsDataLoaded = true;
            MapsDataLoaded?.Invoke();
        }
        public async Task LoadEquipmentsDataAsync()
        {
            string filePath = Path.Combine(FolderPath, "equipments_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                Equipments = JsonConvert.DeserializeObject<ObservableCollection<Equipment>>(cachedData);
            }
            else
            {
                Equipments = new ObservableCollection<Equipment>(await DatabaseLoader.GetEquipmentAsync(LanguageCode).ConfigureAwait(false));
                string serializedData = JsonConvert.SerializeObject(Equipments);
                File.WriteAllText(filePath, serializedData);
            }
            IsEquipmentsDataLoaded = true;
            EquipmentsDataLoaded?.Invoke();
        }
        public async Task LoadCursedsDataAsync()
        {
            string filePath = Path.Combine(FolderPath, "curseds_cache.json");
            if (File.Exists(filePath))
            {
                string cachedData = File.ReadAllText(filePath);
                Curseds = JsonConvert.DeserializeObject<ObservableCollection<CursedPossession>>(cachedData);
            }
            else
            {
                Curseds = new ObservableCollection<CursedPossession>(await DatabaseLoader.GetCursedPossessionsAsync(LanguageCode).ConfigureAwait(false));
                string serializedData = JsonConvert.SerializeObject(Curseds);
                File.WriteAllText(filePath, serializedData);
            }
            IsCursedsDataLoaded = true;
            CursedsDataLoaded?.Invoke();
        }

    }
}
