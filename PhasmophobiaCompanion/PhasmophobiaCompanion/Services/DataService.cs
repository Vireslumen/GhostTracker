using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhasmophobiaCompanion.Data;
using PhasmophobiaCompanion.Models;
using Serilog;

namespace PhasmophobiaCompanion.Services
{
    public class DataService
    {
        private readonly DatabaseLoader DatabaseLoader;

        /// <summary>
        ///     Код языка, на котором будут отображаться данные в приложении.
        /// </summary>
        private readonly string LanguageCode;

        /// <summary>
        ///     Путь к папке с кэшированными данными.
        /// </summary>
        public string FolderPath;

        private CursedPossessionCommon CursedPossessionCommon;
        private DifficultyCommon DifficultyCommon;
        private EquipmentCommon EquipmentCommon;
        private GhostCommon GhostCommon;
        private MapCommon MapCommon;
        private MainPageCommon MainPageCommon;
        private ObservableCollection<ChallengeMode> ChallengeModes;
        private ObservableCollection<Clue> Clues;
        private ObservableCollection<CursedPossession> Curseds;
        private ObservableCollection<Difficulty> Difficulties;
        private ObservableCollection<Equipment> Equipments;
        private ObservableCollection<Ghost> Ghosts;
        private ObservableCollection<Map> Maps;
        private ObservableCollection<OtherInfo> OtherInfos;
        private ObservableCollection<Patch> Patches;
        private ObservableCollection<Quest> Quests;
        private ObservableCollection<string> Tips;

        public DataService()
        {
            try
            {
                DatabaseLoader = new DatabaseLoader(new PhasmaDB());
                //TODO: Сделать чтобы код языка выбирался изначально исходя из языка системы пользователя, а затем исходя из настроек
                LanguageCode = "EN";
                FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки конструктора DataService.");
                throw;
            }
        }

        public bool IsCursedsDataLoaded { get; private set; }
        public bool IsEquipmentsDataLoaded { get; private set; }
        public bool IsGhostsDataLoaded { get; private set; }
        public bool IsMapsDataLoaded { get; private set; }
        public event Action CursedsDataLoaded;
        public event Action EquipmentsDataLoaded;

        public ChallengeMode GetChallengeMode(int challengeID)
        {
            try
            {
                return ChallengeModes[challengeID];
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения особого режима.");
                throw;
            }
        }

        public ObservableCollection<Clue> GetClues()
        {
            try
            {
                return Clues;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения улик.");
                throw;
            }
        }

        public MainPageCommon GetMainPageCommon()
        {
            try
            {
                return MainPageCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих данных для главной страницы.");
                throw;
            }
        }

        public CursedPossessionCommon GetCursedCommon()
        {
            try
            {
                return CursedPossessionCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для проклятых предметов.");
                throw;
            }
        }

        public ObservableCollection<CursedPossession> GetCurseds()
        {
            try
            {
                return Curseds;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения проклятых предметов.");
                throw;
            }
        }

        public ObservableCollection<Difficulty> GetDifficulties()
        {
            try
            {
                return Difficulties;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения сложностей.");
                throw;
            }
        }

        public DifficultyCommon GetDifficultyCommon()
        {
            try
            {
                return DifficultyCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих данных для сложностей.");
                throw;
            }
        }

        public EquipmentCommon GetEquipmentCommon()
        {
            try
            {
                return EquipmentCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для снаряжения.");
                throw;
            }
        }

        public ObservableCollection<Equipment> GetEquipments()
        {
            try
            {
                return Equipments;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения снаряжения.");
                throw;
            }
        }

        public GhostCommon GetGhostCommon()
        {
            try
            {
                return GhostCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для призраков.");
                throw;
            }
        }

        public ObservableCollection<Ghost> GetGhosts()
        {
            try
            {
                return Ghosts;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения призраков.");
                throw;
            }
        }

        public MapCommon GetMapCommon()
        {
            try
            {
                return MapCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для карт.");
                throw;
            }
        }

        public ObservableCollection<Map> GetMaps()
        {
            try
            {
                return Maps;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения карт.");
                throw;
            }
        }

        public ObservableCollection<OtherInfo> GetOtherInfos()
        {
            try
            {
                return OtherInfos;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения некатегоризируемых страниц.");
                throw;
            }
        }

        public ObservableCollection<Patch> GetPatches()
        {
            try
            {
                return Patches;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения патчей.");
                throw;
            }
        }

        public ObservableCollection<Quest> GetQuests()
        {
            try
            {
                return Quests;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения квестов.");
                throw;
            }
        }

        public ObservableCollection<string> GetTips()
        {
            try
            {
                return Tips;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения подсказок.");
                throw;
            }
        }

        public event Action GhostsDataLoaded;

        /// <summary>
        ///     Загружает список особых режимов  - ChallengeMode, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadChallengeModeAsync()
        {
            try
            {
                ChallengeModes = await LoadDataAsync(
                    "challenge_mode_cache.json",
                    async () => new ObservableCollection<ChallengeMode>(
                        await DatabaseLoader.GetChallengeModesAsync(LanguageCode))
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки особых режимов.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список улик  - Clue, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadCluesAsync()
        {
            try
            {
                Clues = await LoadDataAsync(
                    "clues_cache.json",
                    async () => new ObservableCollection<Clue>(await DatabaseLoader.GetCluesAsync(LanguageCode))
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки улик.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к проклятым предметам - CursedPossession из базы данных, а
        ///     затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadCursedCommonAsync()
        {
            try
            {
                CursedPossessionCommon = await LoadDataAsync("cursed_common_cache.json",
                    async () => await DatabaseLoader.GetCursedPossessionCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для проклятых предметов.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список проклятых предметов  - CursedPossession, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadCursedsDataAsync()
        {
            try
            {
                Curseds = await LoadDataAsync(
                    "curseds_cache.json",
                    async () => new ObservableCollection<CursedPossession>(await DatabaseLoader
                        .GetCursedPossessionsAsync(LanguageCode).ConfigureAwait(false))
                );
                //Загрузка текстовых данных для интерфейса, относящимся к проклятым предметам - CursedPossession
                await LoadCursedCommonAsync();
                // Уведомление о загрузки данных
                IsCursedsDataLoaded = true;
                CursedsDataLoaded?.Invoke();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки проклятых предметов.");
                throw;
            }
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
            try
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
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки данных или из базы данных или из кэша.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список сложностей - Difficulty, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadDifficultiesAsync()
        {
            try
            {
                Difficulties = await LoadDataAsync(
                    "difficulties_cache.json",
                    async () => new ObservableCollection<Difficulty>(
                        await DatabaseLoader.GetDifficultiesAsync(LanguageCode))
                );
                //Загрузка текстовых данных для интерфейса, относящимся к сложностям - Difficulty
                await LoadDifficultyCommonAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки сложностей.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к сложностям - Difficulty из базы данных, а затем кэширует
        ///     их, либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadDifficultyCommonAsync()
        {
            try
            {
                DifficultyCommon = await LoadDataAsync("difficulty_common_cache.json",
                    async () => await DatabaseLoader.GetDifficultyCommonAsync(LanguageCode).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для сложностей.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к снаряжению - Equipment из базы данных, а затем кэширует
        ///     их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadEquipmentCommonAsync()
        {
            try
            {
                EquipmentCommon = await LoadDataAsync("equipment_common_cache.json",
                    async () => await DatabaseLoader.GetEquipmentCommonAsync(LanguageCode).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для снаряжения.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список снаряжения  - Equipment, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadEquipmentsDataAsync()
        {
            try
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
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки снаряжения.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к призракам - Ghost из базы данных, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadGhostCommonAsync()
        {
            try
            {
                GhostCommon = await LoadDataAsync("ghost_common_cache.json",
                    async () => await DatabaseLoader.GetGhostCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для призраков.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает данные о призраках - Ghost из базы данных, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadGhostsDataAsync()
        {
            try
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
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки призраков.");
                throw;
            }
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
                await LoadMainPageCommonAsync();
                //Добавление связи от призраков Ghost к уликам Clue
                //Связи добавляются после кэширования, из-за невозможности кэшировать данные с такими связями
                foreach (var ghost in Ghosts) ghost.PopulateAssociatedClues(Clues);
                //Добавление связи от улик Clue - к призракам Ghost
                foreach (var clue in Clues) clue.PopulateAssociatedGhosts(Ghosts);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки первоначальных данных.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к картам - Map из базы данных, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadMapCommonAsync()
        {
            try
            {
                MapCommon = await LoadDataAsync("map_common_cache.json",
                    async () => await DatabaseLoader.GetMapCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для карт.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к главной странице - MainPage из базы данных, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadMainPageCommonAsync()
        {
            try
            {
                MainPageCommon = await LoadDataAsync("main_page_common_cache.json",
                    async () => await DatabaseLoader.GetMainPageCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для главной страницы.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список карт  - Map, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadMapsDataAsync()
        {
            try
            {
                Maps = await LoadDataAsync(
                    "maps_cache.json",
                    async () => new ObservableCollection<Map>(await DatabaseLoader.GetMapsAsync(LanguageCode)
                        .ConfigureAwait(false))
                );
                //Загрузка текстовых данных для интерфейса, относящимся к картам - Map
                await LoadMapCommonAsync();
                // Уведомление о загрузки данных
                IsMapsDataLoaded = true;
                MapsDataLoaded?.Invoke();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки карт.");
                throw;
            }
        }

        /// <summary>
        ///     Загрузка вторичных данных, которые не нужны для главной страницы, загружаются после первоначальных и уже во время
        ///     работы приложения
        /// </summary>
        public async Task LoadOtherDataAsync()
        {
            try
            {
                await LoadMapsDataAsync().ConfigureAwait(false);
                await LoadCursedsDataAsync().ConfigureAwait(false);
                await LoadEquipmentsDataAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки вторичных данных.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список некатегоризированных страниц  - OtherInfo, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadOtherInfoAsync()
        {
            try
            {
                OtherInfos = await LoadDataAsync(
                    "otherinfos_cache.json",
                    async () => new ObservableCollection<OtherInfo>(
                        await DatabaseLoader.GetOtherInfosAsync(LanguageCode))
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки некатегоризируемых страниц.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список патчей - Patch, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadPatchesAsync()
        {
            try
            {
                Patches = await LoadDataAsync(
                    "patch_cache.json",
                    async () => new ObservableCollection<Patch>(await DatabaseLoader.GetPatchesAsync())
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки патчей.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список квестов - Quest, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadQuestsAsync()
        {
            try
            {
                Quests = await LoadDataAsync(
                    "quest_cache.json",
                    async () => new ObservableCollection<Quest>(await DatabaseLoader.GetQuestsAsync(LanguageCode))
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки квестов.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список подсказок - Tip, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadTipsDataAsync()
        {
            try
            {
                Tips = await LoadDataAsync(
                    "tips_cache.json",
                    async () => new ObservableCollection<string>(await DatabaseLoader.GetTipsAsync(LanguageCode))
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки подсказок.");
                throw;
            }
        }

        public event Action MapsDataLoaded;
    }
}