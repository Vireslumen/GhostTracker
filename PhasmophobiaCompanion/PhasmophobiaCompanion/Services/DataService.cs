using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhasmophobiaCompanion.Data;
using PhasmophobiaCompanion.Models;
using Serilog;

namespace PhasmophobiaCompanion.Services
{
    public class DataService
    {
        /// <summary>
        ///     Код языка, на котором будут отображаться данные в приложении.
        /// </summary>
        public readonly string LanguageCode;

        private readonly DatabaseManager databaseManager;

        /// <summary>
        ///     Путь к папке с кэшированными данными.
        /// </summary>
        public string FolderPath;

        public string SelectedTipLevel;
        private AchievementCommon achievementCommon;
        private ChallengeModeCommon challengeModeCommon;
        private ClueCommon clueCommon;
        private CursedPossessionCommon cursedPossessionCommon;
        private DifficultyCommon difficultyCommon;
        private EquipmentCommon equipmentCommon;
        private GhostCommon ghostCommon;
        private List<Achievement> achievements;
        private List<ChallengeMode> challengeModes;
        private List<Clue> clues;
        private List<CursedPossession> curseds;
        private List<Difficulty> difficulties;
        private List<Equipment> equipments;
        private List<Ghost> ghosts;
        private List<Map> maps;
        private List<OtherInfo> otherInfos;
        private List<Patch> patches;
        private List<Quest> quests;
        private List<Tip> tips;
        private MainPageCommon mainPageCommon;
        private MapCommon mapCommon;
        private QuestCommon questCommon;
        private SettingsCommon settingsCommon;

        public DataService()
        {
            try
            {
                NewPatch = false;
                databaseManager = new DatabaseManager(new PhasmaDB());
                var userLanguage = LanguageHelper.GetUserLanguage();
                //Настройка языка приложения
                if (!string.IsNullOrEmpty(userLanguage))
                    LanguageCode = userLanguage;
                else
                    LanguageCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
                //Если в приложении нет такого языка, то язык английский
                if (!LanguageDictionary.LanguageMap.ContainsValue(LanguageCode))
                {
                    LanguageCode = "EN";
                }
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
        public bool NewPatch { get; set; }
        public event Action CursedsDataLoaded;
        public event Action EquipmentsDataLoaded;

        public AchievementCommon GetAchievementCommon()
        {
            try
            {
                return achievementCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих данных для достижений.");
                throw;
            }
        }

        public List<Achievement> GetAchievements()
        {
            try
            {
                return achievements;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения достижений.");
                throw;
            }
        }

        public ChallengeMode GetCurrentChallengeMode()
        {
            try
            {
                var startDate = new DateTime(2023, 6, 26, 0, 0, 0, DateTimeKind.Utc);
                var currentDate = DateTime.UtcNow;
                var difference = currentDate - startDate;
                var totalWeeks = (int)(difference.TotalDays / 7);
                int weeksModulo = totalWeeks % 26;
                return challengeModes[weeksModulo];
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения особого режима.");
                throw;
            }
        }

        public ChallengeModeCommon GetChallengeModeCommon()
        {
            try
            {
                return challengeModeCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих данных для особого режима.");
                throw;
            }
        }

        public List<ChallengeMode> GetChallengeModes()
        {
            try
            {
                return challengeModes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения особых режимов.");
                throw;
            }
        }

        public ClueCommon GetClueCommon()
        {
            try
            {
                return clueCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для улик.");
                throw;
            }
        }

        public List<Clue> GetClues()
        {
            try
            {
                return clues;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения улик.");
                throw;
            }
        }

        public CursedPossessionCommon GetCursedCommon()
        {
            try
            {
                return cursedPossessionCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для проклятых предметов.");
                throw;
            }
        }

        public List<CursedPossession> GetCurseds()
        {
            try
            {
                return curseds;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения проклятых предметов.");
                throw;
            }
        }

        public List<Difficulty> GetDifficulties()
        {
            try
            {
                return difficulties;
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
                return difficultyCommon;
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
                return equipmentCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для снаряжения.");
                throw;
            }
        }

        public List<Equipment> GetEquipments()
        {
            try
            {
                return equipments;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения снаряжения.");
                throw;
            }
        }

        public List<Equipment> GetEquipmentsSameTypeCollection(Equipment equipment)
        {
            try
            {
                return new List<Equipment>(
                    equipments.Where(e => e.Title == equipment.Title && e != equipment));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения списка снаряжения того же типа.");
                throw;
            }
        }

        public GhostCommon GetGhostCommon()
        {
            try
            {
                return ghostCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для призраков.");
                throw;
            }
        }

        public List<Ghost> GetGhosts()
        {
            try
            {
                return ghosts;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения призраков.");
                throw;
            }
        }

        public MainPageCommon GetMainPageCommon()
        {
            try
            {
                return mainPageCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих данных для главной страницы.");
                throw;
            }
        }

        public MapCommon GetMapCommon()
        {
            try
            {
                return mapCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих названий для карт.");
                throw;
            }
        }

        public List<Map> GetMaps()
        {
            try
            {
                return maps;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения карт.");
                throw;
            }
        }

        public List<OtherInfo> GetOtherInfos()
        {
            try
            {
                return otherInfos;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения некатегоризируемых страниц.");
                throw;
            }
        }

        public List<Patch> GetPatches()
        {
            try
            {
                return patches;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения патчей.");
                throw;
            }
        }

        public QuestCommon GetQuestCommon()
        {
            try
            {
                return questCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих данных для квестов.");
                throw;
            }
        }

        public List<Quest> GetQuests()
        {
            try
            {
                return quests;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения квестов.");
                throw;
            }
        }

        public SettingsCommon GetSettingsCommon()
        {
            try
            {
                return settingsCommon;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения общих данных для страницы настроек.");
                throw;
            }
        }

        /// <summary>
        ///     Получение всех уникальных Размеров карт, что есть среди всех элементов карт.
        /// </summary>
        /// <returns>Список Тиров.</returns>
        public List<string> GetSizes()
        {
            var uniqueSize = new HashSet<string>();
            foreach (var map in maps) uniqueSize.Add(map.Size);

            return new List<string>(uniqueSize);
        }

        /// <summary>
        ///     Получение всех уникальных Тиров, что есть среди всех элементов снаряжения.
        /// </summary>
        /// <returns>Список Тиров.</returns>
        public List<string> GetTiers()
        {
            var uniqueTiers = new HashSet<string>();
            foreach (var equip in equipments) uniqueTiers.Add(equip.Tier);

            return new List<string>(uniqueTiers);
        }

        public List<string> GetTipLevels()
        {
            try
            {
                var allTipsLevel = tips.Select(t => t.Level).Distinct().ToList();
                allTipsLevel.Add(settingsCommon.AnyLevel);
                return allTipsLevel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения подсказок.");
                throw;
            }
        }

        public List<Tip> GetTips()
        {
            try
            {
                return tips;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения подсказок.");
                throw;
            }
        }

        public event Action GhostsDataLoaded;

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к достижениям - Achievement из базы данных, а затем
        ///     кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadAchievementCommonAsync()
        {
            try
            {
                achievementCommon = await LoadDataAsync(LanguageCode + "_" + "achievement_common_cache.json",
                    async () => await databaseManager.GetAchievementCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для достижений.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список достижений  - Achievement, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadAchievementsAsync()
        {
            try
            {
                achievements = await LoadDataAsync(
                    LanguageCode + "_" + "achievements_cache.json",
                    async () => new List<Achievement>(await databaseManager.GetAchievementsAsync(LanguageCode))
                );
                //Загрузка текстовых данных для интерфейса, относящихся к достижениям - Achievement
                await LoadAchievementCommonAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки достижений.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список особых режимов  - ChallengeMode, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadChallengeModeAsync()
        {
            try
            {
                challengeModes = await LoadDataAsync(
                    LanguageCode + "_" + "challenge_mode_cache.json",
                    async () => new List<ChallengeMode>(
                        await databaseManager.GetChallengeModesAsync(LanguageCode))
                );
                //Загрузка текстовых данных для интерфейса, относящимся к особым режимам - ChallengeMode
                await LoadChallengeModeCommonAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки особых режимов.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к особым режимам - ChallengeMode из базы данных, а затем
        ///     кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadChallengeModeCommonAsync()
        {
            try
            {
                challengeModeCommon = await LoadDataAsync(LanguageCode + "_" + "challenge_mode_common_cache.json",
                    async () => await databaseManager.GetChallengeModeCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для особых режимов.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к уликам - Clue из базы данных, а
        ///     затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadClueCommonAsync()
        {
            try
            {
                clueCommon = await LoadDataAsync(LanguageCode + "_" + "clue_common_cache.json",
                    async () => await databaseManager.GetClueCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для улик.");
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
                clues = await LoadDataAsync(
                    LanguageCode + "_" + "clues_cache.json",
                    async () => new List<Clue>(await databaseManager.GetCluesAsync(LanguageCode))
                );
                //Загрузка текстовых данных для интерфейса, относящимся к уликам - Clue
                await LoadClueCommonAsync();
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
                cursedPossessionCommon = await LoadDataAsync(LanguageCode + "_" + "cursed_common_cache.json",
                    async () => await databaseManager.GetCursedPossessionCommonAsync(LanguageCode));
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
                curseds = await LoadDataAsync(
                    LanguageCode + "_" + "curseds_cache.json",
                    async () => new List<CursedPossession>((await databaseManager
                        .GetCursedPossessionsAsync(LanguageCode).ConfigureAwait(false)).ToList().OrderBy(c => c.Title))
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
        public async Task<T> LoadDataAsync<T>(string cacheFileName, Func<Task<T>> databaseLoadFunction,
            bool notAwaitWrite = true)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, cacheFileName);
                // Проверка наличия кэша
                if (File.Exists(filePath))
                {
                    var cachedData = await File.ReadAllTextAsync(filePath);
                    return JsonConvert.DeserializeObject<T>(cachedData);
                }

                // Загрузка данных из базы данных и кэширование
                var data = await databaseLoadFunction();
                var serializedData = JsonConvert.SerializeObject(data);
                if (notAwaitWrite)
                    File.WriteAllTextAsync(filePath, serializedData).ContinueWith(task =>
                    {
                        if (task.Exception != null)

                            Log.Error(task.Exception, "Ошибка во время асинхронной записи в файл.");
                    });
                else
                    await File.WriteAllTextAsync(filePath, serializedData);
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
                difficulties = await LoadDataAsync(
                    LanguageCode + "_" + "difficulties_cache.json",
                    async () => new List<Difficulty>(
                        await databaseManager.GetDifficultiesAsync(LanguageCode))
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
                difficultyCommon = await LoadDataAsync(LanguageCode + "_" + "difficulty_common_cache.json",
                    async () => await databaseManager.GetDifficultyCommonAsync(LanguageCode).ConfigureAwait(false));
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
                equipmentCommon = await LoadDataAsync(LanguageCode + "_" + "equipment_common_cache.json",
                    async () => await databaseManager.GetEquipmentCommonAsync(LanguageCode).ConfigureAwait(false));
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
                equipments = await LoadDataAsync(
                    LanguageCode + "_" + "equipments_cache.json",
                    async () => new List<Equipment>((await databaseManager.GetEquipmentAsync(LanguageCode)
                        .ConfigureAwait(false)).OrderBy(e => e.Title))
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
                ghostCommon = await LoadDataAsync(LanguageCode + "_" + "ghost_common_cache.json",
                    async () => await databaseManager.GetGhostCommonAsync(LanguageCode));
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
                ghosts = await LoadDataAsync(
                    LanguageCode + "_" + "ghosts_cache.json",
                    async () => new List<Ghost>(await databaseManager.GetGhostsAsync(LanguageCode))
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
                await LoadAchievementsAsync();
                await LoadTipsDataAsync();
                await LoadDifficultiesAsync();
                await LoadPatchesAsync();
                await LoadQuestsAsync();
                await LoadOtherInfoAsync();
                await LoadChallengeModeAsync();
                await LoadSettingsCommonAsync();
                await LoadMainPageCommonAsync();
                await LoadPatchesSteamAsync();
                //Добавление связи от призраков Ghost к уликам Clue
                //Связи добавляются после кэширования, из-за невозможности кэшировать данные с такими связями
                foreach (var ghost in ghosts) ghost.PopulateAssociatedClues(clues);
                //Добавление связи от улик Clue - к призракам Ghost
                foreach (var clue in clues) clue.PopulateAssociatedGhosts(ghosts);

                SelectedTipLevel = settingsCommon.SelectedLevel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки первоначальных данных.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к главной странице - MainPage из базы данных, а затем
        ///     кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadMainPageCommonAsync()
        {
            try
            {
                mainPageCommon = await LoadDataAsync(LanguageCode + "_" + "main_page_common_cache.json",
                    async () => await databaseManager.GetMainPageCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для главной страницы.");
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
                mapCommon = await LoadDataAsync(LanguageCode + "_" + "map_common_cache.json",
                    async () => await databaseManager.GetMapCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для карт.");
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
                maps = await LoadDataAsync(
                    LanguageCode + "_" + "maps_cache.json",
                    async () => new List<Map>(await databaseManager.GetMapsAsync(LanguageCode)
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
                otherInfos = await LoadDataAsync(
                    LanguageCode + "_" + "other_infos_cache.json",
                    async () => new List<OtherInfo>(
                        await databaseManager.GetOtherInfosAsync(LanguageCode))
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
                patches = await LoadDataAsync(
                    LanguageCode + "_" + "patch_cache.json",
                    async () => new List<Patch>(await databaseManager.GetPatchesAsync())
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки патчей.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает список патчей - Patch из steam.
        /// </summary>
        public async Task LoadPatchesSteamAsync()
        {
            try
            {
                var url = "https://api.steampowered.com/ISteamNews/GetNewsForApp/v2/?appid=739630&count=5";

                using (var httpClient = new HttpClient())
                {
                    var jsonResponse = await httpClient.GetStringAsync(url);
                    var appNews = JsonConvert.DeserializeObject<AppNewsRoot>(jsonResponse);
                    appNews.AppNews.PatchItems.Reverse();
                    var patchWasAdded = false;
                    foreach (var patch in appNews.AppNews.PatchItems)
                    {
                        var patchWasnot = true;
                        foreach (var patchDB in patches)
                            if (patch.Source == patchDB.Source)
                            {
                                patchWasnot = false;
                                break;
                            }

                        if (patchWasnot)
                        {
                            patches.Add(patch);
                            await databaseManager.AddPatchAsync(patch);
                            patchWasAdded = true;
                        }
                    }

                    if (patchWasAdded)
                    {
                        NewPatch = true;
                        var serializedData = JsonConvert.SerializeObject(patches);
                        var filePath = Path.Combine(FolderPath, LanguageCode + "_" + "patch_cache.json");
                        File.WriteAllText(filePath, serializedData);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки патчей из Steam.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к квестам - Quest из базы данных, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadQuestCommonAsync()
        {
            try
            {
                questCommon = await LoadDataAsync(LanguageCode + "_" + "quest_common_cache.json",
                    async () => await databaseManager.GetQuestCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для квестов.");
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
                quests = await LoadDataAsync(
                    LanguageCode + "_" + "quest_cache.json",
                    async () => new List<Quest>(await databaseManager.GetQuestsAsync(LanguageCode))
                );
                //Загрузка текстовых данных для интерфейса, относящимся к квестам - Quest
                await LoadQuestCommonAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки квестов.");
                throw;
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к странице настроек - SettingsPage из базы данных, а затем
        ///     кэширует их, либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadSettingsCommonAsync()
        {
            try
            {
                settingsCommon = await LoadDataAsync(LanguageCode + "_" + "settings_common_cache.json",
                    async () => await databaseManager.GetSettingsCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для особых режимов.");
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
                tips = await LoadDataAsync(
                    LanguageCode + "_" + "tips_cache.json",
                    async () => new List<Tip>(await databaseManager.GetTipsAsync(LanguageCode))
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки подсказок.");
                throw;
            }
        }

        public event Action MapsDataLoaded;

        /// <summary>
        ///     Поиск среди списков, у которых есть Title и подробные страницы.
        /// </summary>
        /// <param name="query">Поисковый запрос.</param>
        /// <returns>Список найденных объектов.</returns>
        public IEnumerable<object> Search(string query)
        {
            try
            {
                var results = new List<object>();
                results.AddRange(clues.Where(item => item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(difficulties.Where(item =>
                    item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(patches.Where(item => item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(
                    otherInfos.Where(item => item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(ghosts.Where(item => item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(
                    equipments.Where(item => item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(maps.Where(item => item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(curseds.Where(item => item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(challengeModes.Where(item =>
                    item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(quests.Where(item =>
                    item.Clause.Contains(query, StringComparison.OrdinalIgnoreCase)));
                results.AddRange(achievements.Where(item =>
                    item.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
                return results;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска по Title среди списков имеющих подробную страницу.");
                throw;
            }
        }
    }
}