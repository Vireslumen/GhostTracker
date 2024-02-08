using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhasmophobiaCompanion.Data;
using PhasmophobiaCompanion.Models;

namespace PhasmophobiaCompanion.Services
{
    public class DataService
    {
        private readonly DatabaseLoader DatabaseLoader;

        /// <summary>
        ///     Код языка, на котором будут отображаться данные в приложении.
        /// </summary>
        private readonly string LanguageCode;

        //Main Page data
        private ObservableCollection<ChallengeMode> ChallengeModes;
        private ObservableCollection<Clue> Clues;

        //Cursed data
        private ObservableCollection<CursedPossession> Curseds;
        private ObservableCollection<Difficulty> Difficulties;
        private EquipmentCommon EquipmentCommon;

        //Equipment data
        private ObservableCollection<Equipment> Equipments;

        /// <summary>
        ///     Путь к папке с кэшированными данными.
        /// </summary>
        public string FolderPath;

        private GhostCommon GhostCommon;
        private ObservableCollection<Ghost> Ghosts;

        //Map data
        private ObservableCollection<Map> Maps;
        private ObservableCollection<OtherInfo> OtherInfos;
        private ObservableCollection<Patch> Patches;
        private ObservableCollection<Quest> Quests;
        private ObservableCollection<string> Tips;

        public DataService()
        {
            DatabaseLoader = new DatabaseLoader(new PhasmaDB());
            //TODO: Сделать чтобы код языка выбирался изначально исходя из языка системы пользователя, а затем исходя из настроек
            LanguageCode = "EN";
            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public bool IsCursedsDataLoaded { get; private set; }
        public bool IsEquipmentsDataLoaded { get; private set; }
        public bool IsGhostsDataLoaded { get; private set; }
        public bool IsMapsDataLoaded { get; private set; }
        public event Action GhostsDataLoaded;
        public event Action MapsDataLoaded;
        public event Action EquipmentsDataLoaded;
        public event Action CursedsDataLoaded;

        public ObservableCollection<Ghost> GetGhosts()
        {
            return Ghosts;
        }

        public GhostCommon GetGhostCommon()
        {
            return GhostCommon;
        }

        public EquipmentCommon GetEquipmentCommon()
        {
            return EquipmentCommon;
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

        /// <summary>
        ///     Загрузка первоначальных данных требуемых на главной странице
        /// </summary>
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

                //Добавление связи от призраков Ghost к уликам Clue
                //Связи добавляются после кэширования, из-за невозможности кэшировать данные с такими связями
                foreach (var ghost in Ghosts) ghost.PopulateAssociatedClues(Clues);
                //Добавление связи от улик Clue - к призракам Ghost
                foreach (var clue in Clues) clue.PopulateAssociatedGhosts(Ghosts);
            }
            catch (Exception ex)
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        ///     Загрузка вторичных данных, которые не нужны для главной страницы, загружаются после первоначальных и уже во время
        ///     работы приложения
        /// </summary>
        public async Task LoadOtherDataAsync()
        {
            await LoadMapsDataAsync().ConfigureAwait(false);
            await LoadCursedsDataAsync().ConfigureAwait(false);
            await LoadEquipmentsDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Асинхронно загружает данные из кэша или из базы данных, если кэш отсутствует.
        /// </summary>
        /// <typeparam name="T">Тип данных, который нужно загрузить.</typeparam>
        /// <param name="cacheFileName">Имя файла кэша для проверки наличия и загрузки данных.</param>
        /// <param name="databaseLoadFunction">Функция для загрузки данных из базы данных, если кэш отсутствует.</param>
        /// <returns>Объект типа <typeparamref name="T" />, содержащий загруженные данные.</returns>
        /// <remarks>
        ///     Этот метод сначала пытается загрузить данные из файла кэша. Если файл кэша не найден,
        ///     данные загружаются из базы данных с помощью предоставленной функции и затем кэшируются.
        /// </remarks>
        public async Task<T> LoadDataAsync<T>(string cacheFileName, Func<Task<T>> databaseLoadFunction)
        {
            var filePath = Path.Combine(FolderPath, cacheFileName);
            // Проверка наличия кэша
            if (File.Exists(filePath))
            {
                var cachedData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(cachedData);
            }

            // Загрузка данных из базы данных и кэширование
            var data = await databaseLoadFunction();
            var serializedData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, serializedData);
            return data;
        }

        /// <summary>
        ///     Загружает данные о призраках - Ghost из базы данных, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadGhostsDataAsync()
        {
            Ghosts = await LoadDataAsync(
                "ghosts_cache.json",
                async () => new ObservableCollection<Ghost>(await DatabaseLoader.GetGhostsAsync(LanguageCode))
            );
            //Загрузка текстовых данных для интерфейса, относящимся к призракам - Ghost
            await LoadGhostCommonAsync();
            //Уведомление о том, что данные загружены
            IsGhostsDataLoaded = true;
            GhostsDataLoaded?.Invoke();
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к призракам - Ghost из базы данных, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadGhostCommonAsync()
        {
            GhostCommon = await LoadDataAsync("ghost_common_cache.json",
                async () => await DatabaseLoader.GetGhostCommonAsync(LanguageCode));
        }

        /// <summary>
        ///     Загружает список подсказок - Tip, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadTipsDataAsync()
        {
            Tips = await LoadDataAsync(
                "tips_cache.json",
                async () => new ObservableCollection<string>(await DatabaseLoader.GetTipsAsync(LanguageCode))
            );
        }

        /// <summary>
        ///     Загружает список сложностей - Difficulty, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadDifficultiesAsync()
        {
            Difficulties = await LoadDataAsync(
                "difficulties_cache.json",
                async () => new ObservableCollection<Difficulty>(
                    await DatabaseLoader.GetDifficultiesAsync(LanguageCode))
            );
        }

        /// <summary>
        ///     Загружает список патчей - Patch, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadPatchesAsync()
        {
            Patches = await LoadDataAsync(
                "patch_cache.json",
                async () => new ObservableCollection<Patch>(await DatabaseLoader.GetPatchesAsync())
            );
        }

        /// <summary>
        ///     Загружает список квестов - Quest, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadQuestsAsync()
        {
            Quests = await LoadDataAsync(
                "quest_cache.json",
                async () => new ObservableCollection<Quest>(await DatabaseLoader.GetQuestsAsync(LanguageCode))
            );
        }

        /// <summary>
        ///     Загружает список некатегоризированных страниц  - OtherInfo, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadOtherInfoAsync()
        {
            OtherInfos = await LoadDataAsync(
                "quest_cache.json",
                async () => new ObservableCollection<OtherInfo>(await DatabaseLoader.GetOtherInfosAsync(LanguageCode))
            );
        }

        /// <summary>
        ///     Загружает список особых режимов  - ChallengeMode, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadChallengeModeAsync()
        {
            ChallengeModes = await LoadDataAsync(
                "challenge_mode_cache.json",
                async () => new ObservableCollection<ChallengeMode>(
                    await DatabaseLoader.GetChallengeModesAsync(LanguageCode))
            );
        }

        /// <summary>
        ///     Загружает список улик  - Clue, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadCluesAsync()
        {
            Clues = await LoadDataAsync(
                "clues_cache.json",
                async () => new ObservableCollection<Clue>(await DatabaseLoader.GetCluesAsync(LanguageCode))
            );
        }

        /// <summary>
        ///     Загружает список карт  - Map, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadMapsDataAsync()
        {
            Maps = await LoadDataAsync(
                "maps_cache.json",
                async () => new ObservableCollection<Map>(await DatabaseLoader.GetMapsAsync(LanguageCode)
                    .ConfigureAwait(false))
            );
            // Уведомление о загрузки данных
            IsMapsDataLoaded = true;
            MapsDataLoaded?.Invoke();
        }

        /// <summary>
        ///     Загружает список снаряжения  - Equipment, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadEquipmentsDataAsync()
        {
            Equipments = await LoadDataAsync(
                "equipments_cache.json",
                async () => new ObservableCollection<Equipment>(await DatabaseLoader.GetEquipmentAsync(LanguageCode)
                    .ConfigureAwait(false))
            );

            //Загрузка текстовых данных для интерфейса, относящимся к снаряжению - Equipment
            await LoadEquipmentCommonAsync();
            // Уведомление о загрузки данных
            IsEquipmentsDataLoaded = true;
            EquipmentsDataLoaded?.Invoke();
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к снаряжению - Equipment из базы данных, а затем кэширует
        ///     их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadEquipmentCommonAsync()
        {
            EquipmentCommon = await LoadDataAsync("equipment_common_cache.json",
                async () => await DatabaseLoader.GetEquipmentCommonAsync(LanguageCode).ConfigureAwait(false));
        }

        /// <summary>
        ///     Загружает список проклятых предметов  - CursedPossession, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadCursedsDataAsync()
        {
            Curseds = await LoadDataAsync(
                "curseds_cache.json",
                async () => new ObservableCollection<CursedPossession>(await DatabaseLoader
                    .GetCursedPossessionsAsync(LanguageCode).ConfigureAwait(false))
            );
            // Уведомление о загрузки данных
            IsCursedsDataLoaded = true;
            CursedsDataLoaded?.Invoke();
        }
    }
}