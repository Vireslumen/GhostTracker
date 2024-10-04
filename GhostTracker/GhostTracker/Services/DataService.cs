using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GhostTracker.Data;
using GhostTracker.Models;
using Newtonsoft.Json;
using Serilog;

namespace GhostTracker.Services
{
    public class DataService
    {
        private readonly DatabaseManager databaseManager;

        /// <summary>
        ///     Путь к папке с кэшированными данными.
        /// </summary>
        public string FolderPath;

        /// <summary>
        ///     Код языка, на котором будут отображаться данные в приложении.
        /// </summary>
        public string LanguageCode;

        public string SelectedTipLevel;
        private AchievementCommon achievementCommon;
        private AppShellCommon appShellCommon;

        /// <summary>
        ///     Активно ли открытие окна фидбэка при тряске девайса.
        /// </summary>
        private bool shakeActive;

        private ChallengeModeCommon challengeModeCommon;
        private ClueCommon clueCommon;
        private CursedPossessionCommon cursedPossessionCommon;
        private DifficultyCommon difficultyCommon;
        private EquipmentCommon equipmentCommon;
        private FeedbackCommon feedbackCommon;
        private GhostCommon ghostCommon;
        private GhostGuessQuestionCommon ghostGuessQuestionCommon;
        private List<Achievement> achievements;
        private List<ChallengeMode> challengeModes;
        private List<Clue> clues;
        private List<CursedPossession> curseds;
        private List<Difficulty> difficulties;
        private List<Equipment> equipments;
        private List<Ghost> ghosts;
        private List<GhostGuessQuestion> ghostGuessQuestions;
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
                databaseManager = new DatabaseManager(new GhostTrackerDb());
                var userLanguage = LanguageHelper.GetUserLanguage();
                shakeActive = ShakeHelper.GetShakeActive();
                //Настройка языка приложения
                LanguageCode = !string.IsNullOrEmpty(userLanguage)
                    ? userLanguage
                    : CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
                //Если в приложении нет такого языка, то язык английский
                if (!LanguageDictionary.LanguageMap.ContainsValue(LanguageCode)) LanguageCode = "EN";
                FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки конструктора DataService.");
            }
        }

        public bool IsCursedsDataLoaded { get; private set; }
        public bool IsEquipmentsDataLoaded { get; private set; }
        public bool IsGhostsDataLoaded { get; private set; }
        public bool IsMapsDataLoaded { get; private set; }
        public bool NewPatch { get; set; }
        public event Action CursedsDataLoaded;
        public event Action EquipmentsDataLoaded;
        public event Action GhostsDataLoaded;
        public event Action MapsDataLoaded;

        public AchievementCommon GetAchievementCommon()
        {
            return achievementCommon;
        }

        public List<Achievement> GetAchievements()
        {
            return achievements;
        }

        public AppShellCommon GetAppShellCommon()
        {
            return appShellCommon;
        }

        public ChallengeModeCommon GetChallengeModeCommon()
        {
            return challengeModeCommon;
        }

        public List<ChallengeMode> GetChallengeModes()
        {
            return challengeModes;
        }

        public ClueCommon GetClueCommon()
        {
            return clueCommon;
        }

        public List<Clue> GetClues()
        {
            return clues;
        }

        public ChallengeMode GetCurrentChallengeMode()
        {
            try
            {
                var startDate = new DateTime(2023, 6, 26, 0, 0, 0, DateTimeKind.Utc);
                var currentDate = DateTime.UtcNow;
                var difference = currentDate - startDate;
                var totalWeeks = (int) (difference.TotalDays / 7);
                var weeksModulo = totalWeeks % 26;
                return challengeModes[weeksModulo];
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения особого режима.");
                return challengeModes[0];
            }
        }

        public CursedPossessionCommon GetCursedCommon()
        {
            return cursedPossessionCommon;
        }

        public List<CursedPossession> GetCurseds()
        {
            return curseds;
        }

        public List<Difficulty> GetDifficulties()
        {
            return difficulties;
        }

        public DifficultyCommon GetDifficultyCommon()
        {
            return difficultyCommon;
        }

        public EquipmentCommon GetEquipmentCommon()
        {
            return equipmentCommon;
        }

        public List<Equipment> GetEquipments()
        {
            return equipments;
        }

        public List<Equipment> GetEquipmentsSameTypeCollection(Equipment equipment)
        {
            return new List<Equipment>(
                equipments.Where(e => e.Title == equipment.Title && e != equipment));
        }

        public FeedbackCommon GetFeedbackCommon()
        {
            return feedbackCommon;
        }

        public GhostCommon GetGhostCommon()
        {
            return ghostCommon;
        }

        public GhostGuessQuestionCommon GetGhostGuessQuestionCommon()
        {
            return ghostGuessQuestionCommon;
        }

        public List<GhostGuessQuestion> GetGhostGuessQuestions()
        {
            return ghostGuessQuestions;
        }

        public List<Ghost> GetGhosts()
        {
            return ghosts;
        }

        public MainPageCommon GetMainPageCommon()
        {
            return mainPageCommon;
        }

        public MapCommon GetMapCommon()
        {
            return mapCommon;
        }

        public List<Map> GetMaps()
        {
            return maps;
        }

        public List<OtherInfo> GetOtherInfos()
        {
            return otherInfos;
        }

        public List<Patch> GetPatches()
        {
            return patches;
        }

        public QuestCommon GetQuestCommon()
        {
            return questCommon;
        }

        public List<Quest> GetQuests()
        {
            return quests;
        }

        public SettingsCommon GetSettingsCommon()
        {
            return settingsCommon;
        }

        public bool GetShakeActive()
        {
            return shakeActive;
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
            var allTipsLevel = tips.Select(t => t.Level).Distinct().ToList();
            allTipsLevel.Add(settingsCommon.AnyLevel);
            return allTipsLevel;
        }

        public List<Tip> GetTips()
        {
            return tips;
        }

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
            }
        }

        /// <summary>
        ///     Загружает названия вкладок базы данных, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadAppShellCommonAsync()
        {
            try
            {
                appShellCommon = await LoadDataAsync(LanguageCode + "_" + "app_shell_common_cache.json",
                    async () => await databaseManager.GetAppShellCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки названий вкладок.");
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
            }
        }

        /// <summary>
        ///     Асинхронно загружает данные из кэша или из базы данных, если кэш отсутствует.
        /// </summary>
        /// <typeparam name="T">Тип данных, который нужно загрузить.</typeparam>
        /// <param name="cacheFileName">Имя файла кэша для проверки наличия и загрузки данных.</param>
        /// <param name="databaseLoadFunction">Функция для загрузки данных из базы данных, если кэш отсутствует.</param>
        /// <param name="notAwaitWrite">Не нужно ли ждать завершение записи в файл.</param>
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
                    _ = File.WriteAllTextAsync(filePath, serializedData).ContinueWith(task =>
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
                return default;
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
            }
        }

        /// <summary>
        ///     Загружает текстовых данных для страницы фидбэка, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadFeedbackCommonAsync()
        {
            try
            {
                feedbackCommon = await LoadDataAsync(LanguageCode + "_" + "feedback_common_cache.json",
                    async () => await databaseManager.GetFeedbackCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки текстовых данных для страницы фидбэка.");
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
            }
        }

        /// <summary>
        ///     Загружает текстовые данные для интерфейса, относящиеся к определению призраков - GhostGuessQuestion из базы данных,
        ///     а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadGhostGuessQuestionCommonAsync()
        {
            try
            {
                ghostGuessQuestionCommon = await LoadDataAsync(
                    LanguageCode + "_" + "ghost_guess_question_common_cache.json",
                    async () => await databaseManager.GetGhostGuessQuestionCommonAsync(LanguageCode));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки общих названий для определения призраков.");
            }
        }

        /// <summary>
        ///     Загружает список вопросов для определения призрака  - GhostGuessQuestion, а затем кэширует их,
        ///     либо загружает данные из кэша, в зависимости от наличия кэша.
        /// </summary>
        public async Task LoadGhostGuessQuestionsAsync()
        {
            try
            {
                ghostGuessQuestions = await LoadDataAsync(
                    LanguageCode + "_" + "ghost_guess_question_cache.json",
                    async () => new List<GhostGuessQuestion>(
                        await databaseManager.GetGhostGuessQuestionsAsync(LanguageCode))
                );
                //Загрузка текстовых данных для интерфейса, относящимся к GhostGuessQuestion
                await LoadGhostGuessQuestionCommonAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки вопросов для определения призрака.");
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
            }
        }

        /// <summary>
        ///     Загрузка первоначальных данных требуемых на главной странице
        /// </summary>
        public async Task LoadInitialDataAsync()
        {
            await LoadAppShellCommonAsync();
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
            await LoadGhostGuessQuestionsAsync();
            await LoadFeedbackCommonAsync();
            //Добавление связи от призраков Ghost к уликам Clue
            //Связи добавляются после кэширования, из-за невозможности кэшировать данные с такими связями
            foreach (var ghost in ghosts) ghost.PopulateAssociatedClues(clues);
            //Добавление связи от улик Clue - к призракам Ghost
            foreach (var clue in clues) clue.PopulateAssociatedGhosts(ghosts);
            //Добавление связи от GhostGuessQuestion - к призракам Ghost
            foreach (var ghostGuessQuestion in ghostGuessQuestions)
                ghostGuessQuestion.PopulateAssociatedGhosts(ghosts);
            SelectedTipLevel = settingsCommon.SelectedLevel;
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
            }
        }

        /// <summary>
        ///     Загружает список патчей - Patch из steam.
        /// </summary>
        public async Task LoadPatchesSteamAsync()
        {
            try
            {
                const string url =
                    "https://api.steampowered.com/ISteamNews/GetNewsForApp/v2/?appid=739630&count=5&feeds=steam_community_announcements";

                using var httpClient = new HttpClient();
                var jsonResponse = await httpClient.GetStringAsync(url);
                var appNews = JsonConvert.DeserializeObject<AppNewsRoot>(jsonResponse);
                appNews.AppNews.PatchItems.Reverse();
                var patchWasAdded = false;
                foreach (var patch in appNews.AppNews.PatchItems)
                {
                    var isPatchNew = patches.All(patchDb => patch.Source != patchDb.Source);

                    if (!isPatchNew) continue;
                    patches.Add(patch);
                    await databaseManager.AddPatchAsync(patch);
                    patchWasAdded = true;
                }

                if (patchWasAdded)
                {
                    NewPatch = true;
                    var serializedData = JsonConvert.SerializeObject(patches);
                    var filePath = Path.Combine(FolderPath, LanguageCode + "_" + "patch_cache.json");
                    await File.WriteAllTextAsync(filePath, serializedData);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки новостей из стима.");
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
            }
        }

        public void ReinitializeLanguage()
        {
            var userLanguage = LanguageHelper.GetUserLanguage();
            //Настройка языка приложения
            LanguageCode = !string.IsNullOrEmpty(userLanguage)
                ? userLanguage
                : CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
            //Если в приложении нет такого языка, то язык английский
            if (!LanguageDictionary.LanguageMap.ContainsValue(LanguageCode)) LanguageCode = "EN";
        }

        /// <summary>
        ///     Поиск среди списков, у которых есть Title и подробные страницы.
        /// </summary>
        /// <param name="query">Поисковый запрос.</param>
        /// <returns>Список найденных объектов.</returns>
        public IEnumerable<object> Search(string query)
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

        public void SetShakeActive(bool shake)
        {
            shakeActive = shake;
            ShakeHelper.SaveShakeActive(shakeActive);
        }
    }
}