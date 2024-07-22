using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhasmophobiaCompanion.Data;
using PhasmophobiaCompanion.Models;
using Serilog;

namespace PhasmophobiaCompanion.Services
{
    /// <summary>
    ///     Отвечает за добавление и загрузку различных типов данных из и в базу данных PhasmaDB.
    /// </summary>
    public class DatabaseManager
    {
        private readonly PhasmaDB phasmaDbContext;

        public DatabaseManager(PhasmaDB context)
        {
            try
            {
                phasmaDbContext = context;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время определения контекста.");
                throw;
            }
        }

        /// <summary>
        ///     Добавление записи в таблицу патчей в базе данных
        /// </summary>
        /// <param name="patch">Патч</param>
        public async Task AddPatchAsync(Patch patch)
        {
            if (patch == null) return;

            phasmaDbContext.PatchBase.Add(new PatchBase
            {
                Source = patch.Source,
                Title = patch.Title
            });
            await phasmaDbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для достижений - Achievement, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для достижений.</returns>
        public async Task<AchievementCommon> GetAchievementCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var achievementCommonData = await phasmaDbContext.AchievementCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект AchievementCommon.
                return achievementCommonData.Select(a => new AchievementCommon
                {
                    Title = a.Title,
                    Description = a.Description
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для достижений.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список достижений - Achievement на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список достижений.</returns>
        public async Task<List<Achievement>> GetAchievementsAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var achievementData = await phasmaDbContext.AchievementBase
                    .Include(a => a.Translations)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return achievementData
                    .Select(a => new Achievement
                    {
                        ImageFilePath = a.ImageFilePath,
                        Title = a.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = a.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        Tip = a.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Tip
                    }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд достижений.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает название для вкладок, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Названия вкладок.</returns>
        public async Task<AppShellCommon> GetAppShellCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var appShellCommonData = await phasmaDbContext.AppShellCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект AppShellCommon.
                return appShellCommonData.Select(a => new AppShellCommon
                {
                    CursedPossessions = a.CursedPossessions,
                    Equipments = a.Equipments,
                    Ghosts = a.Ghosts,
                    Main = a.Main,
                    Maps = a.Maps
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд названий вкладок.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для квестов - ChallengeMode, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для особых режимов.</returns>
        public async Task<ChallengeModeCommon> GetChallengeModeCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var challengeModeCommonData = await phasmaDbContext.ChallengeModeCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();
                //Преобразование данных в объект ChallengeModeCommon.
                return challengeModeCommonData.Select(c => new ChallengeModeCommon
                {
                    Title = c.Title,
                    Description = c.Description,
                    DiffucltyParams = c.DiffucltyParams,
                    EquipmentProvided = c.EquipmentProvided
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для особых режимов.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список особых режимов - ChallengeMode на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список особых режимов.</returns>
        public async Task<List<ChallengeMode>> GetChallengeModesAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var challengeModeData = await phasmaDbContext.ChallengeModeBase
                    .Include(c => c.Translations)
                    .Include(c => c.EquipmentBase)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return challengeModeData
                    .Select(c => new ChallengeMode
                    {
                        ID = c.ID,
                        Title = c.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = c.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        Parameters = c.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Parameters,
                        EquipmentsID = c.EquipmentBase.Select(e => e.ID).ToList(),
                        MapID = c.MapID
                    }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд особых режимов.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для улик - Clue, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для улик.</returns>
        public async Task<ClueCommon> GetClueCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var clueCommonData = await phasmaDbContext.ClueCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект ClueCommon.
                return clueCommonData.Select(c => new ClueCommon
                {
                    OtherGhosts = c.OtherGhosts,
                    RelatedEquipment = c.RelatedEquipment
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для улик.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список улик - Clue на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список улик.</returns>
        public async Task<List<Clue>> GetCluesAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var cluesData = await phasmaDbContext.ClueBase
                    .Include(c => c.Translations)
                    .Include(c => c.GhostBase)
                    .Include(c => c.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations)
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations)
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations)
                    .Include(c => c.EquipmentBase)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return cluesData.Select(
                    c => new Clue
                    {
                        ID = c.ID,
                        IconFilePath = c.IconFilePath,
                        ImageFilePath = c.ImageFilePath,
                        EquipmentsID = c.EquipmentBase.Select(e => e.ID).ToList(),
                        Title = c.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = c.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        UnfoldingItems = MapUnfoldingItems(c.UnfoldingItemBase, languageCode),
                        ExpandFieldsWithImages = MapExpandFieldWithImages(c.ExpandFieldWithImagesBase, languageCode),
                        GhostsID = c.GhostBase.Select(g => g.ID).ToList()
                    }
                ).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд улик.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для проклятых предметов - CursedPossessionCommon, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для проклятых предметов.</returns>
        public async Task<CursedPossessionCommon> GetCursedPossessionCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var cursedCommonData = await phasmaDbContext.CursedPossessionCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект CursedPossessionCommon.
                return cursedCommonData.Select(c => new CursedPossessionCommon
                {
                    Search = c.Search,
                    CursedsTitle = c.CursedsTitle,
                    EmptyView = c.EmptyView
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для проклятых предметов.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список проклятых предметов - CursedPossession на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список проклятых предметов.</returns>
        public async Task<List<CursedPossession>> GetCursedPossessionsAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var cursedPossessionData = await phasmaDbContext.CursedPossessionBase
                    .Include(c => c.Translations)
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations)
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations)
                    .Include(c => c.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return cursedPossessionData
                    .Select(c => new CursedPossession
                    {
                        ID = c.ID,
                        ImageFilePath = c.ImageFilePath,
                        Title = c.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = c.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        UnfoldingItems = MapUnfoldingItems(c.UnfoldingItemBase, languageCode),
                        ExpandFieldsWithImages = MapExpandFieldWithImages(c.ExpandFieldWithImagesBase, languageCode)
                    }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд проклятых предметов.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список сложностей - Difficulty на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список сложностей.</returns>
        public async Task<List<Difficulty>> GetDifficultiesAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var difficultyData = await phasmaDbContext.DifficultyBase
                    .Include(d => d.Translations)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return difficultyData
                    .Select(d => new Difficulty
                    {
                        ID = d.ID,
                        UnlockLevel = d.UnlockLevel,
                        RewardMultiplier = d.RewardMultiplier,
                        SetupTime = d.SetupTime,
                        SanityConsumption = d.SanityConsumption,
                        ElectricityOn = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.ElectricityOn,
                        SanityMonitorWork = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.SanityMonitorWork,
                        ActivityMonitorWork = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.ActivityMonitorWork,
                        EvidenceAvailable = d.EvidenceAvailable,
                        SanityRestoration = d.SanityRestoration,
                        IsCursedAvailable = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.IsCursedAvailable,
                        Title = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        GhostActivity = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.GhostActivity,
                        GhostHuntTime = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.GhostHuntTime,
                        DoorOpenedCount = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.DoorOpenedCount,
                        DeadCashBack = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.DeadCashBack,
                        ObjectiveBoardPendingAloneAll = d.Translations
                            .FirstOrDefault(t => t.LanguageCode == languageCode)?.ObjectiveBoardPendingAloneAll,
                        HidingSpotBlocked = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.HidingSpotBlocked,
                        ElectricityBlockNotShowedOnMap = d.Translations
                            .FirstOrDefault(t => t.LanguageCode == languageCode)?.ElectricityBlockNotShowedOnMap,
                        HuntExtendByKilling = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.HuntExtendByKilling,
                        FingerPrints = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.FingerPrints,
                        SanityStartAt = d.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.SanityStartAt
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд сложностей.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для проклятых предметов - DifficultyCommon, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для сложностей.</returns>
        public async Task<DifficultyCommon> GetDifficultyCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var difficultyCommonData = await phasmaDbContext.DifficultyCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект DifficultyCommon.
                return difficultyCommonData.Select(d => new DifficultyCommon
                {
                    ActivityMonitorWork = d.ActivityMonitorWork,
                    DeadCashBack = d.DeadCashBack,
                    DifficultiesTitle = d.DifficultiesTitle,
                    DifficultyTitle = d.DifficultyTitle,
                    DoorOpenedCount = d.DoorOpenedCount,
                    ElectricityBlockNotShowedOnMap = d.ElectricityBlockNotShowedOnMap,
                    ElectricityOn = d.ElectricityOn,
                    EvidenceAvailable = d.EvidenceAvailable,
                    FingerPrints = d.FingerPrints,
                    GhostActivity = d.GhostActivity,
                    GhostHuntTime = d.GhostHuntTime,
                    HidingSpotBlocked = d.HidingSpotBlocked,
                    HuntExtendByKilling = d.HuntExtendByKilling,
                    ObjectiveBoardPendingAloneAll = d.ObjectiveBoardPendingAloneAll,
                    RewardMultiplier = d.RewardMultiplier,
                    SanityConsumption = d.SanityConsumption,
                    SanityMonitorWork = d.SanityMonitorWork,
                    SanityRestoration = d.SanityRestoration,
                    SanityStartAt = d.SanityStartAt,
                    SetupTime = d.SetupTime,
                    UnlockLevel = d.UnlockLevel,
                    IsCursedAvailable = d.IsCursedAvailable,
                    DifficultyParams = d.DifficultyParams
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для сложностей.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список снаряжения - Equipment на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список снаряжения.</returns>
        public async Task<List<Equipment>> GetEquipmentAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var equipmentData = await phasmaDbContext.EquipmentBase
                    .Include(e => e.Translations)
                    .Include(e => e.OtherEquipmentStatBase)
                    .Include(e => e.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations)
                    .Include(e => e.ClueBase)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return equipmentData
                    .Select(e => new Equipment
                    {
                        ID = e.ID,
                        UnlockLevel = e.UnlockLevel,
                        Cost = e.Cost,
                        UnlockCost = e.UnlockCost,
                        MaxLimit = e.MaxLimit,
                        ImageFilePath = e.ImageFilePath,
                        Title = e.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = e.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        Tier = e.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Tier,
                        CluesID = e.ClueBase.Select(c => c.ID).ToList(),
                        OtherEquipmentStats = MapOtherEquipmentStat(e.OtherEquipmentStatBase, languageCode),
                        UnfoldingItems = MapUnfoldingItems(e.UnfoldingItemBase, languageCode)
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд снаряжения.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для снаряжения - EquipmentCommon, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для снаряжения.</returns>
        public async Task<EquipmentCommon> GetEquipmentCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var equipmentCommonData = await phasmaDbContext.EquipmentCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект EquipmentCommon.
                return equipmentCommonData.Select(e => new EquipmentCommon
                {
                    EquipmentsTitle = e.EquipmentsTitle,
                    FilterTier = e.FilterTier,
                    FilterUnlock = e.FilterUnlock,
                    MaxLimit = e.MaxLimit,
                    Price = e.Price,
                    PriceUnlock = e.PriceUnlock,
                    Search = e.Search,
                    Tier = e.Tier,
                    Apply = e.Apply,
                    UnlockLevel = e.UnlockLevel,
                    Clear = e.Clear,
                    RelatedClues = e.RelatedClues,
                    OtherTier = e.OtherTier,
                    EmptyView = e.EmptyView
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для снаряжения.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для призраков - GhostCommon, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для призраков.</returns>
        public async Task<GhostCommon> GetGhostCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var ghostCommonData = await phasmaDbContext.GhostCommonTranslations
                    .Where(g => g.LanguageCode == languageCode).ToListAsync();

                // Преобразование данных в объект GhostCommon.
                return ghostCommonData.Select(g => new GhostCommon
                {
                    ApplyTitle = g.ApplyTitle,
                    FilterTitle = g.FilterTitle,
                    MaxSanityHunt = g.MaxSanityHunt,
                    MaxSpeed = g.MaxSpeed,
                    MaxSpeedLoS = g.MaxSpeedLoS,
                    MinSanityHunt = g.MinSanityHunt,
                    MinSpeed = g.MinSpeed,
                    SanityHunt = g.SanityHunt,
                    Search = g.Search,
                    Speed = g.Speed,
                    GhostsTitle = g.GhostsTitle,
                    Min = g.Min,
                    Max = g.Max,
                    GhostTitle = g.GhostTitle,
                    LoS = g.LoS,
                    Clear = g.Clear,
                    EmptyView = g.EmptyView
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названий для призраков.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для страницы определения призрака - GhostGuessQuestion, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для страницы определения призрака.</returns>
        public async Task<GhostGuessQuestionCommon> GetGhostGuessQuestionCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var ghostGuessQuestionCommonData = await phasmaDbContext.GhostGuessQuestionCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект GhostGuessQuestionCommon.
                return ghostGuessQuestionCommonData.Select(g => new GhostGuessQuestionCommon
                {
                    AnswerDontKnow = g.AnswerDontKnow,
                    AnswerNo = g.AnswerNo,
                    AnswerYes = g.AnswerYes,
                    AnswerThinkSo = g.AnswerThinkSo,
                    CheckBoxTitle = g.CheckBoxTitle,
                    GhostListTitle = g.GhostListTitle,
                    MeterSecTitle = g.MeterSecTitle,
                    ChooseAnswer = g.ChooseAnswer,
                    PageTitle = g.PageTitle,
                    SpeedTitle = g.SpeedTitle,
                    TapButtonTitle = g.TapButtonTitle
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названий для страницы определения призрака.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список вопросов для определения призрака - GhostGuessQuestion на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список вопросов для определения призрака.</returns>
        public async Task<List<GhostGuessQuestion>> GetGhostGuessQuestionsAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var ghostGuessQuestionData = await phasmaDbContext.GhostGuessQuestionBase
                    .Include(c => c.Translations)
                    .Include(c => c.GhostBase)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return ghostGuessQuestionData.Select(
                    ggq => new GhostGuessQuestion
                    {
                        ID = ggq.ID,
                        QuestionText = ggq.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.QuestionText,
                        AnswerMeaning = ggq.AnswerMeaning,
                        AnswerNegativeMeaning = ggq.AnswerNegativeMeaning,
                        Answer = 0, // Предполагается, что это значение по умолчанию.
                        GhostsID = ggq.GhostBase.Select(g => g.ID).ToList()
                    }
                ).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд вопросов определения призрака.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список призраков - Ghost на основе кода языка, а также всех улик.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список призраков.</returns>
        public async Task<List<Ghost>> GetGhostsAsync(string languageCode)
        {
            try
            {
                // Загрузка всех данных без фильтрации по языку.
                var ghostData = await phasmaDbContext.GhostBase
                    .Include(g => g.Translations)
                    .Include(g => g.SpeedRangesBase)
                    .Include(g => g.ClueBase)
                    .ThenInclude(c => c.Translations)
                    .Include(g => g.ClueBase)
                    .ThenInclude(c => c.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations)
                    .Include(g => g.ClueBase)
                    .ThenInclude(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations)
                    .Include(g => g.ClueBase)
                    .ThenInclude(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations)
                    .Include(g => g.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations)
                    .ToListAsync();

                // Фильтрация и преобразование данных в список объектов Ghost после загрузки.
                return ghostData
                    .Select(g => new Ghost
                    {
                        ID = g.ID,
                        ImageFilePath = g.ImageFilePath,
                        MinSanityHunt = g.MinSanityHunt,
                        MaxSanityHunt = g.MaxSanityHunt,
                        MinGhostSpeed = g.MinGhostSpeed,
                        MaxGhostSpeed = g.MaxGhostSpeed,
                        MaxGhostSpeedLoS = g.MaxGhostSpeedLoS,
                        Identification = g.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.Identification,
                        Title = g.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = g.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        MaxGhostSpeedClause = g.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.MaxGhostSpeedClause,
                        MaxSanityHuntClause = g.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.MaxSanityHuntClause,
                        MinGhostSpeedClause = g.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.MinGhostSpeedClause,
                        MinSanityHuntClause = g.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.MinSanityHuntClause,
                        MaxGhostSpeedLoSClause = g.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.MaxGhostSpeedLoSClause,
                        UnfoldingItems = MapUnfoldingItems(g.UnfoldingItemBase, languageCode),
                        CluesID = g.ClueBase.Select(c => c.ID).ToList(),
                        SpeedRanges = MapSpeedRange(g.SpeedRangesBase)
                    }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд призраков.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для главной страницы - MainPageCommon, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для главной страницы.</returns>
        public async Task<MainPageCommon> GetMainPageCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var mainPageCommonData = await phasmaDbContext.MainPageCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект MainPageCommon.
                return mainPageCommonData.Select(m => new MainPageCommon
                {
                    Clue = m.Clue,
                    DailyQuest = m.DailyQuest,
                    OtherPages = m.OtherPages,
                    Patches = m.Patches,
                    Search = m.Search,
                    Settings = m.Settings,
                    SpecialMode = m.SpecialMode,
                    Theme = m.Theme,
                    Tip = m.Tip,
                    WeeklyQuest = m.WeeklyQuest,
                    Difficulties = m.Difficulties,
                    PlayerMinSpeedTip = m.PlayerMinSpeedTip,
                    PlayerMaxSpeedTip = m.PlayerMaxSpeedTip,
                    PlayerMaxSpeed = m.PlayerMaxSpeed,
                    PlayerMinSpeed = m.PlayerMinSpeed,
                    PlayerTitle = m.PlayerTitle,
                    Ok = m.Ok,
                    GhostGuess = m.GhostGuess,
                    PatchIsOut = m.PatchIsOut,
                    ReadMore = m.ReadMore
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для главной страницы.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для карт - MapCommon, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для карт.</returns>
        public async Task<MapCommon> GetMapCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var mapCommonData = await phasmaDbContext.MapCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект MapCommon.
                return mapCommonData.Select(m => new MapCommon
                {
                    Search = m.Search,
                    Apply = m.Apply,
                    FilterSize = m.FilterSize,
                    Exits = m.Exits,
                    FilterRoom = m.FilterRoom,
                    Floors = m.Floors,
                    MapSize = m.MapSize,
                    MapsTitle = m.MapsTitle,
                    RoomNumber = m.RoomNumber,
                    UnlockLvl = m.UnlockLvl,
                    HidenSpot = m.HidenSpot,
                    Clear = m.Clear,
                    EmptyView = m.EmptyView
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для карт.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список карт - Map на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список карт.</returns>
        public async Task<List<Map>> GetMapsAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var mapData = await phasmaDbContext.MapBase
                    .Include(m => m.Translations)
                    .Include(m => m.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations)
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations)
                    .Include(m => m.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return mapData
                    .Select(m => new Map
                    {
                        ID = m.ID,
                        RoomCount = m.RoomCount,
                        UnlockLevel = m.UnlockLevel,
                        Exits = m.Exits,
                        Floors = m.Floors,
                        SizeNumeric = m.SizeNumeric,
                        ImageFilePath = m.ImageFilePath,
                        Title = m.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = m.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        Size = m.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Size,
                        HidingSpotCount = m.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)
                            ?.HidingSpotCount,
                        ExpandFieldsWithImages = MapExpandFieldWithImages(m.ExpandFieldWithImagesBase, languageCode),
                        UnfoldingItems = MapUnfoldingItems(m.UnfoldingItemBase, languageCode)
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд карт.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список данных для некатегоризованных страниц - OtherInfo на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список данных для некатегоризованных страниц.</returns>
        public async Task<List<OtherInfo>> GetOtherInfosAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var otherInfoData = await phasmaDbContext.OtherInfoBase
                    .Include(o => o.Translations)
                    .Include(o => o.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations)
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations)
                    .Include(o => o.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return otherInfoData
                    .Select(o => new OtherInfo
                    {
                        ID = o.ID,
                        ImageFilePath = o.ImageFilePath,
                        Title = o.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Description = o.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description,
                        ExpandFieldsWithImages = MapExpandFieldWithImages(o.ExpandFieldWithImagesBase, languageCode),
                        UnfoldingItems = MapUnfoldingItems(o.UnfoldingItemBase, languageCode)
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд некатегоризируемых страниц.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список патчей - Patch.
        /// </summary>
        /// <returns>Список патчей.</returns>
        public async Task<List<Patch>> GetPatchesAsync()
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var patchData = await phasmaDbContext.PatchBase.ToListAsync();

                // Преобразование данных в список объектов Patch.
                return patchData
                    .Select(p => new Patch
                        {
                            ID = p.ID,
                            Source = p.Source,
                            Title = p.Title
                        }
                    ).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд патчей.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для квестов - QuestCommon, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для квестов.</returns>
        public async Task<QuestCommon> GetQuestCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var questCommonData = await phasmaDbContext.QuestCommonTranslations
                    .Where(e => e.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект QuestCommon.
                return questCommonData.Select(q => new QuestCommon
                {
                    Daily = q.Daily,
                    Title = q.Title,
                    Weekly = q.Weekly,
                    Description = q.Description
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для квестов.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список квестов - Quest на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список квестов.</returns>
        public async Task<List<Quest>> GetQuestsAsync(string languageCode)
        {
            try
            {
                // Загрузка данных без учета языкового фильтра.
                var questData = await phasmaDbContext.QuestBase
                    .Include(q => q.Translations)
                    .ToListAsync();

                // Фильтрация и преобразование данных после загрузки.
                return questData
                    .Select(q => new Quest
                    {
                        ID = q.ID,
                        Title = q.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                        Clause = q.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Clause,
                        Reward = q.Reward,
                        Tip = q.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Tip,
                        Type = q.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Type
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд квестов.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает общие данные для страницы настроек - SettingsCommon, на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Общие данные для страницы настроек.</returns>
        public async Task<SettingsCommon> GetSettingsCommonAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var settingsCommonData = await phasmaDbContext.SettingsCommonTranslations
                    .Where(s => s.LanguageCode == languageCode).ToListAsync();

                //Преобразование данных в объект SettingsCommon.
                return settingsCommonData.Select(s => new SettingsCommon
                {
                    AnyLevel = s.AnyLevel,
                    AppLanguage = s.AppLanguage,
                    ErrorReport = s.ErrorReport,
                    TipLevel = s.TipLevel,
                    SelectLanguage = s.SelectLanguage,
                    SelectLevel = s.SelectLevel,
                    SelectedLevel = s.SelectedLevel,
                    SettingsTitle = s.SettingsTitle
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названия для страницы настроек.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список подсказок - Tips на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов</param>
        /// <returns>Список подсказок.</returns>
        public async Task<List<Tip>> GetTipsAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var tipsData = await phasmaDbContext.TipsTranslations.Where(t => t.LanguageCode == languageCode)
                    .ToListAsync();

                // Преобразование данных в список строк.
                return tipsData.Select(t => new Tip {TipValue = t.Tip, Level = t.Level}).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд подсказок.");
                throw;
            }
        }

        /// <summary>
        ///     Преобразует коллекцию объектов ExpandFieldWithImagesBase в List ExpandFieldWithImages, используя
        ///     заданный код языка для выбора подходящих переводов.
        /// </summary>
        /// <param name="expandFieldWithImages">Коллекция базовых объектов ExpandFieldWithImagesBase для преобразования.</param>
        /// <param name="languageCode">Код языка, который используется для выбора соответствующих переводов.</param>
        /// <returns>List объектов ExpandFieldWithImages, содержащих переведенные данные.</returns>
        private static List<ExpandFieldWithImages> MapExpandFieldWithImages(
            IEnumerable<ExpandFieldWithImagesBase> expandFieldWithImages, string languageCode)
        {
            try
            {
                return expandFieldWithImages.Select(e => new ExpandFieldWithImages
                {
                    Title = e.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                    Header = e.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Header,
                    Body = e.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Body,
                    ImageWithDescriptions = MapImageWithDescription(e.ImageWithDescriptionBase, languageCode)
                }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время преобразований коллекции ExpandFieldWithImagesBase.");
                throw;
            }
        }

        /// <summary>
        ///     Преобразует коллекцию объектов ImageWithDescriptionBase в List ImageWithDescription, используя
        ///     заданный код языка для выбора подходящих переводов.
        /// </summary>
        /// <param name="imageWithDescriptions">Коллекция базовых объектов ImageWithDescriptionBase для преобразования.</param>
        /// <param name="languageCode">Код языка, который используется для выбора соответствующих переводов.</param>
        /// <returns>List объектов ImageWithDescription, содержащих переведенные данные.</returns>
        private static List<ImageWithDescription> MapImageWithDescription(
            IEnumerable<ImageWithDescriptionBase> imageWithDescriptions, string languageCode)
        {
            try
            {
                return imageWithDescriptions.Select(i => new ImageWithDescription
                {
                    ImageFilePath = i.ImageFilePath,
                    Description = i.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Description
                }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время преобразований коллекции ImageWithDescription.");
                throw;
            }
        }

        /// <summary>
        ///     Преобразует коллекцию объектов OtherEquipmentStatBase в List OtherEquipmentStat, используя заданный
        ///     код языка для выбора подходящих переводов.
        /// </summary>
        /// <param name="otherEquipmentStats">Коллекция базовых объектов OtherEquipmentStatBase для преобразования.</param>
        /// <param name="languageCode">Код языка, который используется для выбора соответствующих переводов.</param>
        /// <returns>List объектов OtherEquipmentStat, содержащих переведенные данные.</returns>
        private static List<OtherEquipmentStat> MapOtherEquipmentStat(
            IEnumerable<OtherEquipmentStatBase> otherEquipmentStats, string languageCode)
        {
            try
            {
                return new List<OtherEquipmentStat>(otherEquipmentStats
                    .Where(o => o.LanguageCode == languageCode).Select(
                        o => new OtherEquipmentStat
                        {
                            Stat = o.Stat,
                            ShortStat = o.ShortStat
                        }
                    ));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время преобразований коллекции ImageWithDescription.");
                throw;
            }
        }

        /// <summary>
        ///     Преобразует коллекцию объектов SpeedRangesBase в List SpeedRange.
        /// </summary>
        /// <param name="speedranges">Коллекция базовых объектов SpeedRangesBase для преобразования.</param>
        /// <returns>List объектов SpeedRange.</returns>
        private static List<SpeedRange> MapSpeedRange(IEnumerable<SpeedRangesBase> speedranges)
        {
            try
            {
                return new List<SpeedRange>(speedranges.Select(sp =>
                    new SpeedRange
                    {
                        Max = sp.Max,
                        Min = sp.Min
                    }));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время преобразований коллекции SpeedRange.");
                throw;
            }
        }

        /// <summary>
        ///     Преобразует коллекцию объектов UnfoldingItemBase в List UnfoldingItem, используя заданный код языка
        ///     для выбора подходящих переводов.
        /// </summary>
        /// <param name="unfoldingItems">Коллекция базовых объектов UnfoldingItemBase для преобразования.</param>
        /// <param name="languageCode">Код языка, который используется для выбора соответствующих переводов.</param>
        /// <returns>List объектов UnfoldingItemBase, содержащих переведенные данные.</returns>
        private static List<UnfoldingItem> MapUnfoldingItems(
            IEnumerable<UnfoldingItemBase> unfoldingItems, string languageCode)
        {
            try
            {
                return unfoldingItems.Select(u => new UnfoldingItem
                {
                    Title = u.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Title,
                    Header = u.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Header,
                    Body = u.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Body
                }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время преобразований коллекции UnfoldingItemBase.");
                throw;
            }
        }
    }
}