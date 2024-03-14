using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhasmophobiaCompanion.Data;
using PhasmophobiaCompanion.Models;
using Serilog;

namespace PhasmophobiaCompanion.Services
{
    /// <summary>
    ///     Отвечает за загрузку различных типов данных из базы данных PhasmaDB.
    /// </summary>
    public class DatabaseLoader
    {
        private readonly PhasmaDB phasmaDbContext;

        public DatabaseLoader(PhasmaDB context)
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var challengeModeData = await phasmaDbContext.ChallengeModeBase
                    .Include(c => c.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.EquipmentBase)
                    .ToListAsync();

                // Преобразование данных в список объектов ChallengeMode.
                return challengeModeData
                    .Select(c => new ChallengeMode
                        {
                            ID = c.ID,
                            Title = c.Translations.FirstOrDefault()?.Title,
                            Description = c.Translations.FirstOrDefault()?.Description,
                            EquipmentsID = c.EquipmentBase.Select(e => e.ID).ToList(),
                            MapID = c.MapID,
                            DifficultyID = c.DifficultyID
                        }
                    ).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд особых режимов.");
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var cluesData = await phasmaDbContext.ClueBase
                    .Include(c => c.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.GhostBase)
                    .Include(c => c.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations.Where(t => t.LanguageCode == languageCode))
                    .ToListAsync();

                // Преобразование данных в список объектов Clue.
                return cluesData.Select(
                    c => new Clue
                    {
                        ID = c.ID,
                        IconFilePath = c.IconFilePath,
                        ImageFilePath = c.ImageFilePath,
                        Title = c.Translations.FirstOrDefault()?.Title,
                        Description = c.Translations.FirstOrDefault()?.Description,
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
                return cursedCommonData.Select(m => new CursedPossessionCommon
                {
                    Search = m.Search,
                    CursedsTitle = m.CursedsTitle
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var cursedPossessionData = await phasmaDbContext.CursedPossessionBase
                    .Include(c => c.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                    .ToListAsync().ConfigureAwait(false);

                // Преобразование данных в список объектов CursedPossession.
                return cursedPossessionData
                    .Select(c => new CursedPossession
                    {
                        ID = c.ID,
                        ImageFilePath = c.ImageFilePath,
                        Title = c.Translations.FirstOrDefault()?.Title,
                        Description = c.Translations.FirstOrDefault()?.Description,
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var difficultyData = await phasmaDbContext.DifficultyBase
                    .Include(d => d.Translations.Where(t => t.LanguageCode == languageCode))
                    .ToListAsync();


                // Преобразование данных в список объектов Difficulty.
                return difficultyData
                    .Select(d => new Difficulty
                    {
                        ID = d.ID,
                        UnlockLevel = d.UnlockLevel,
                        RewardMultiplier = d.RewardMultiplier,
                        SetupTime = d.SetupTime,
                        SanityConsumption = d.SanityConsumption,
                        ElectricityOn = d.ElectricityOn,
                        SanityMonitorWork = d.SanityMonitorWork,
                        ActivityMonitorWork = d.ActivityMonitorWork,
                        EvidenceAvailable = d.EvidenceAvailable,
                        SanityRestoration = d.SanityRestoration,
                        IsCursedAvailable = d.IsCursedAvailable,
                        Title = d.Translations.FirstOrDefault()?.Title,
                        Description = d.Translations.FirstOrDefault()?.Description,
                        GhostActivity = d.Translations.FirstOrDefault()?.GhostActivity,
                        GhostHuntTime = d.Translations.FirstOrDefault()?.GhostHuntTime,
                        DoorOpenedCount = d.Translations.FirstOrDefault()?.DoorOpenedCount,
                        DeadCashBack = d.Translations.FirstOrDefault()?.DeadCashBack,
                        ObjectiveBoardPendingAloneAll = d.Translations.FirstOrDefault()?.ObjectiveBoardPendingAloneAll,
                        HidingSpotBlocked = d.Translations.FirstOrDefault()?.HidingSpotBlocked,
                        ElectricityBlockNotShowedOnMap =
                            d.Translations.FirstOrDefault()?.ElectricityBlockNotShowedOnMap,
                        HuntExtendByKilling = d.Translations.FirstOrDefault()?.HuntExtendByKilling,
                        FingerPrints = d.Translations.FirstOrDefault()?.FingerPrints,
                        SanityStartAt = d.Translations.FirstOrDefault()?.SanityStartAt
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
                    IsCursedAvailable = d.IsCursedAvailable
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var equipmentData = await phasmaDbContext.EquipmentBase
                    .Include(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(e => e.OtherEquipmentStatBase)
                    .Include(e => e.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                    .ToListAsync().ConfigureAwait(false);

                // Преобразование данных в список объектов Equipment.
                return equipmentData
                    .Select(e => new Equipment
                    {
                        ID = e.ID,
                        UnlockLevel = e.UnlockLevel,
                        Cost = e.Cost,
                        UnlockCost = e.UnlockCost,
                        MaxLimit = e.MaxLimit,
                        ImageFilePath = e.ImageFilePath,
                        Title = e.Translations.FirstOrDefault()?.Title,
                        Description = e.Translations.FirstOrDefault()?.Description,
                        Tier = e.Translations.FirstOrDefault()?.Tier,
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
                    OtherTier = e.OtherTier
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
                    Clear = g.Clear
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд общих названий для призраков.");
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var ghostData = await phasmaDbContext.GhostBase
                    .Include(g => g.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(g => g.ClueBase)
                    .ThenInclude(c => c.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(g => g.ClueBase)
                    .ThenInclude(c => c.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(g => g.ClueBase)
                    .ThenInclude(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(g => g.ClueBase)
                    .ThenInclude(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(g => g.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                    .ToListAsync();

                // Преобразование данных в список объектов Ghost.
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
                        Identification = g.Translations.FirstOrDefault()?.Identification,
                        Title = g.Translations.FirstOrDefault()?.Title,
                        Description = g.Translations.FirstOrDefault()?.Description,
                        MaxGhostSpeedClause = g.Translations.FirstOrDefault()?.MaxGhostSpeedClause,
                        MaxSanityHuntClause = g.Translations.FirstOrDefault()?.MaxSanityHuntClause,
                        MinGhostSpeedClause = g.Translations.FirstOrDefault()?.MinGhostSpeedClause,
                        MinSanityHuntClause = g.Translations.FirstOrDefault()?.MinSanityHuntClause,
                        MaxGhostSpeedLoSClause = g.Translations.FirstOrDefault()?.MaxGhostSpeedLoSClause,
                        UnfoldingItems = MapUnfoldingItems(g.UnfoldingItemBase, languageCode),
                        CluesID = g.ClueBase.Select(c => c.ID).ToList()
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
                    PlayerTitle = m.PlayerTitle
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
                    Clear = m.Clear
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var mapData = await phasmaDbContext.MapBase
                    .Include(m => m.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(m => m.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(m => m.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                    .ToListAsync().ConfigureAwait(false);

                // Преобразование данных в список объектов Map.
                return mapData
                    .Select(m => new Map
                    {
                        ID = m.ID,
                        RoomCount = m.RoomCount,
                        UnlockLevel = m.UnlockLevel,
                        Exits = m.Exits,
                        Floors = m.Floors,
                        ImageFilePath = m.ImageFilePath,
                        Title = m.Translations.FirstOrDefault()?.Title,
                        Description = m.Translations.FirstOrDefault()?.Description,
                        Size = m.Translations.FirstOrDefault()?.Size,
                        HidingSpotCount = m.Translations.FirstOrDefault()?.HidingSpotCount,
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var otherInfoData = await phasmaDbContext.OtherInfoBase
                    .Include(o => o.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(o => o.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(c => c.ExpandFieldWithImagesBase)
                    .ThenInclude(e => e.ImageWithDescriptionBase)
                    .ThenInclude(i => i.Translations.Where(t => t.LanguageCode == languageCode))
                    .Include(o => o.UnfoldingItemBase)
                    .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                    .ToListAsync();

                // Преобразование данных в список объектов OtherInfo.
                return otherInfoData
                    .Select(o => new OtherInfo
                    {
                        ID = o.ID,
                        ImageFilePath = o.ImageFilePath,
                        Title = o.Translations.FirstOrDefault()?.Title,
                        Description = o.Translations.FirstOrDefault()?.Description,
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
                // Загрузка данных с учетом перевода и связанных сущностей.
                var questData = await phasmaDbContext.QuestBase
                    .Include(q =>
                        q.Translations.Where(t => t.LanguageCode == languageCode))
                    .ToListAsync();
                // Преобразование данных в список объектов Quest.
                return questData.Select(
                    q => new Quest
                    {
                        ID = q.ID,
                        Title = q.Translations.FirstOrDefault()?.Title,
                        Clause = q.Translations.FirstOrDefault()?.Clause,
                        Reward = q.Reward,
                        Tip = q.Translations.FirstOrDefault()?.Tip,
                        Type = q.Translations.FirstOrDefault()?.Type
                    }
                ).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд квестов.");
                throw;
            }
        }

        /// <summary>
        ///     Асинхронно возвращает список подсказок - Tips на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов</param>
        /// <returns>Список подсказок.</returns>
        public async Task<List<string>> GetTipsAsync(string languageCode)
        {
            try
            {
                // Загрузка данных с учетом перевода и связанных сущностей.
                var tipsData = await phasmaDbContext.TipsTranslations.Where(t => t.LanguageCode == languageCode)
                    .ToListAsync();

                // Преобразование данных в список строк.
                return tipsData.Select(t => t.Tip).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки из бд подсказок.");
                throw;
            }
        }

        /// <summary>
        ///     Преобразует коллекцию объектов ExpandFieldWithImagesBase в ObservableCollection ExpandFieldWithImages, используя
        ///     заданный код языка для выбора подходящих переводов.
        /// </summary>
        /// <param name="expandFieldWithImages">Коллекция базовых объектов ExpandFieldWithImagesBase для преобразования.</param>
        /// <param name="languageCode">Код языка, который используется для выбора соответствующих переводов.</param>
        /// <returns>ObservableCollection объектов ExpandFieldWithImages, содержащих переведенные данные.</returns>
        private static ObservableCollection<ExpandFieldWithImages> MapExpandFieldWithImages(
            IEnumerable<ExpandFieldWithImagesBase> expandFieldWithImages, string languageCode)
        {
            try
            {
                return new ObservableCollection<ExpandFieldWithImages>(expandFieldWithImages.Select(e =>
                    new ExpandFieldWithImages
                    {
                        Title = e.Translations.FirstOrDefault()?.Title,
                        Header = e.Translations.FirstOrDefault()?.Header,
                        Body = e.Translations.FirstOrDefault()?.Body,
                        ImageWithDescriptions = MapImageWithDescription(e.ImageWithDescriptionBase, languageCode)
                    }));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время преобразований коллекции ExpandFieldWithImagesBase.");
                throw;
            }
        }

        /// <summary>
        ///     Преобразует коллекцию объектов ImageWithDescriptionBase в ObservableCollection ImageWithDescription, используя
        ///     заданный код языка для выбора подходящих переводов.
        /// </summary>
        /// <param name="imageWithDescriptions">Коллекция базовых объектов ImageWithDescriptionBase для преобразования.</param>
        /// <param name="languageCode">Код языка, который используется для выбора соответствующих переводов.</param>
        /// <returns>ObservableCollection объектов ImageWithDescription, содержащих переведенные данные.</returns>
        private static ObservableCollection<ImageWithDescription> MapImageWithDescription(
            IEnumerable<ImageWithDescriptionBase> imageWithDescriptions, string languageCode)
        {
            try
            {
                return new ObservableCollection<ImageWithDescription>(imageWithDescriptions.Select(i =>
                    new ImageWithDescription
                    {
                        ImageFilePath = i.ImageFilePath,
                        Description = i.Translations.FirstOrDefault()?.Description
                    }));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время преобразований коллекции ImageWithDescription.");
                throw;
            }
        }

        /// <summary>
        ///     Преобразует коллекцию объектов OtherEquipmentStatBase в ObservableCollection OtherEquipmentStat, используя заданный
        ///     код языка для выбора подходящих переводов.
        /// </summary>
        /// <param name="otherEquipmentStats">Коллекция базовых объектов OtherEquipmentStatBase для преобразования.</param>
        /// <param name="languageCode">Код языка, который используется для выбора соответствующих переводов.</param>
        /// <returns>ObservableCollection объектов OtherEquipmentStat, содержащих переведенные данные.</returns>
        private static ObservableCollection<OtherEquipmentStat> MapOtherEquipmentStat(
            IEnumerable<OtherEquipmentStatBase> otherEquipmentStats, string languageCode)
        {
            try
            {
                return new ObservableCollection<OtherEquipmentStat>(otherEquipmentStats
                    .Where(o => o.LanguageCode == languageCode).Select(
                        o => new OtherEquipmentStat
                        {
                            Stat = o.Stat
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
        ///     Преобразует коллекцию объектов UnfoldingItemBase в ObservableCollection UnfoldingItem, используя заданный код языка
        ///     для выбора подходящих переводов.
        /// </summary>
        /// <param name="unfoldingItems">Коллекция базовых объектов UnfoldingItemBase для преобразования.</param>
        /// <param name="languageCode">Код языка, который используется для выбора соответствующих переводов.</param>
        /// <returns>ObservableCollection объектов UnfoldingItemBase, содержащих переведенные данные.</returns>
        private static ObservableCollection<UnfoldingItem> MapUnfoldingItems(
            IEnumerable<UnfoldingItemBase> unfoldingItems, string languageCode)
        {
            try
            {
                return new ObservableCollection<UnfoldingItem>(unfoldingItems.Select(u => new UnfoldingItem
                {
                    Title = u.Translations.FirstOrDefault()?.Title,
                    Header = u.Translations.FirstOrDefault()?.Header,
                    Body = u.Translations.FirstOrDefault()?.Body
                }));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время преобразований коллекции UnfoldingItemBase.");
                throw;
            }
        }
    }
}