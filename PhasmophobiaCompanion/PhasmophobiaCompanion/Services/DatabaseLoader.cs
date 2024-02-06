using Microsoft.EntityFrameworkCore;
using PhasmophobiaCompanion.Data;
using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhasmophobiaCompanion.Services
{
    /// <summary>
    /// Отвечает за загрузку различных типов данных из базы данных PhasmaDB.
    /// </summary>
    public class DatabaseLoader
    {
        // Контекст базы данных.
        private readonly PhasmaDB _phasmaDbContext;

        /// <summary>
        /// Инициализирует новый экземпляр класса DatabaseLoader.
        /// </summary>
        /// <param name="context">Контекст базы данных для операций с данными.</param>
        public DatabaseLoader(PhasmaDB context)
        {
            _phasmaDbContext = context;
        }

        /// <summary>
        /// Ассинхронно возвращает список подсказок - Tips на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов</param>
        /// <returns>Список подсказок.</returns>
        public async Task<List<string>> GetTipsAsync(string languageCode)
        {
            var tipsData = await _phasmaDbContext.TipsTranslations.Where(t => t.LanguageCode == languageCode).ToListAsync();
            return tipsData.Select(t => t.Tip).ToList();
        }

        /// <summary>
        /// Асинхронно возвращает список проклятых предметов - CursedPossession на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список проклятых предметов.</returns>
        public async Task<List<CursedPossession>> GetCursedPossessionsAsync(string languageCode)
        {
            // Загрузка данных с учетом перевода и связанных сущностей.
            var cursedPossessionData = await _phasmaDbContext.CursedPossessionBase
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

        /// <summary>
        /// Асинхронно возвращает список сложностей - Difficulty на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список сложностей.</returns>
        public async Task<List<Difficulty>> GetDifficultiesAsync(string languageCode)
        {
            var difficultyData = await _phasmaDbContext.DifficultyBase
                        .Include(d => d.Translations.Where(t => t.LanguageCode == languageCode))
                        .ToListAsync();
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
                      Title = d.Translations.FirstOrDefault()?.Title,
                      Description = d.Translations.FirstOrDefault()?.Description,
                      GhostActivity = d.Translations.FirstOrDefault()?.GhostActivity,
                      GhostHuntTime = d.Translations.FirstOrDefault()?.GhostHuntTime,
                      DoorOpenedCount = d.Translations.FirstOrDefault()?.DoorOpenedCount,
                      DeadCashBack = d.Translations.FirstOrDefault()?.DeadCashBack,
                      ObjectiveBoardPendingAloneAll = d.Translations.FirstOrDefault()?.ObjectiveBoardPendingAloneAll,
                      HidingSpotBlocked = d.Translations.FirstOrDefault()?.HidingSpotBlocked,
                      ElectricityBlockNotShowedOnMap = d.Translations.FirstOrDefault()?.ElectricityBlockNotShowedOnMap,
                      HuntExtendByKilling = d.Translations.FirstOrDefault()?.HuntExtendByKilling,
                      FingerPrints = d.Translations.FirstOrDefault()?.FingerPrints,
                      SanityStartAt = d.Translations.FirstOrDefault()?.SanityStartAt
                  })
                  .ToList();
        }

        /// <summary>
        /// Асинхронно возвращает список снаряжения - Equipment на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список снаряжения.</returns>
        public async Task<List<Equipment>> GetEquipmentAsync(string languageCode)
        {
            var equipmentData = await _phasmaDbContext.EquipmentBase
                        .Include(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                        .Include(e => e.OtherEquipmentStatBase)
                        .Include(e => e.UnfoldingItemBase)
                            .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                        .ToListAsync().ConfigureAwait(false);

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
                      Uses = e.Translations.FirstOrDefault()?.Uses,
                      OtherEquipmentStats = MapOtherEquipmentStat(e.OtherEquipmentStatBase, languageCode),
                      UnfoldingItems = MapUnfoldingItems(e.UnfoldingItemBase, languageCode)
                  })
                  .ToList();
        }

        /// <summary>
        /// Асинхронно возвращает список особых режимов - ChallengeMode на основе языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <param name="equipments">Список всего снаряжения - Equipment.</param>
        /// <param name="maps">Список всех карт - Map.</param>
        /// <param name="difficulties">Список всех сложностей - Difficulty.</param>
        /// <returns>Список особых режимов.</returns>
        public async Task<List<ChallengeMode>> GetChallengeModesAsync(string languageCode)
        {
            var challangeModeData = await _phasmaDbContext.ChallengeModeBase
                            .Include(c => c.Translations.Where(t => t.LanguageCode == languageCode))
                            .Include(c => c.EquipmentBase)
                            .ToListAsync();
            // Супер-ультра костыль
            return challangeModeData
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


        public async Task<List<Clue>> GetCluesAsync(string languageCode)
        {
            var cluesData = await _phasmaDbContext.ClueBase
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


        public async Task<GhostCommon> GetGhostCommonAsync(string languageCode)
        {
            var ghostCommonData = await _phasmaDbContext.GhostCommonTranslations
    .Where(g => g.LanguageCode == languageCode).ToListAsync();
            return ghostCommonData.Select(g => new GhostCommon {
            ApplyTitle=g.ApplyTitle,
            FilterTitle=g.FilterTitle,
            MaxSanityHunt=g.MaxSanityHunt,
            MaxSpeed=g.MaxSpeed,
            MaxSpeedLoS=g.MaxSpeedLoS,
            MinSanityHunt=g.MinSanityHunt,
            MinSpeed=g.MinSpeed,
            SanityHunt=g.SanityHunt,
            Search=g.Search,
            Speed=g.Speed,
            GhostsTitle=g.GhostsTitle,
            Min=g.Min,
            Max=g.Max,
            GhostTitle=g.GhostTitle,
            LoS=g.LoS
            }).FirstOrDefault();
        }

        /// <summary>
        /// Асинхронно возвращает список призраков - Ghost на основе кода языка, а также всех улик.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <param name="clues">Список всех улик, который будет записан методом.</param>
        /// <returns>Список призраков.</returns>
        public async Task<List<Ghost>> GetGhostsAsync(string languageCode)
        {
            var ghostData = await _phasmaDbContext.GhostBase
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

        /// <summary>
        /// Асинхронно возвращает список карт - Map на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список карт.</returns>
        public async Task<List<Map>> GetMapsAsync(string languageCode)
        {
            var mapData = await _phasmaDbContext.MapBase
                            .Include(m => m.Translations.Where(t => t.LanguageCode == languageCode))
                            .Include(m => m.ExpandFieldWithImagesBase)
                                .ThenInclude(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                            .Include(c => c.ExpandFieldWithImagesBase)
                                .ThenInclude(e => e.ImageWithDescriptionBase)
                                    .ThenInclude(i => i.Translations.Where(t => t.LanguageCode == languageCode))
                            .Include(m => m.UnfoldingItemBase)
                                .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                            .ToListAsync().ConfigureAwait(false);
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

        /// <summary>
        /// Асинхронно возвращает список данных для некатегоризованных страниц - OtherInfo на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список данных для некатегоризованных страниц.</returns>
        public async Task<List<OtherInfo>> GetOtherInfosAsync(string languageCode)
        {
            var otherInfoData = await _phasmaDbContext.OtherInfoBase
                            .Include(o => o.Translations.Where(t => t.LanguageCode == languageCode))
                            .Include(o => o.ExpandFieldWithImagesBase)
                                .ThenInclude(e => e.Translations.Where(t => t.LanguageCode == languageCode))
                            .Include(c => c.ExpandFieldWithImagesBase)
                                .ThenInclude(e => e.ImageWithDescriptionBase)
                                    .ThenInclude(i => i.Translations.Where(t => t.LanguageCode == languageCode))
                            .Include(o => o.UnfoldingItemBase)
                                .ThenInclude(u => u.Translations.Where(t => t.LanguageCode == languageCode))
                            .ToListAsync();

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

        /// <summary>
        /// Асинхронно возвращает список патчей - Patch.
        /// </summary>
        /// <returns>Список патчей.</returns>
        public async Task<List<Patch>> GetPatchesAsync()
        {
            var patchData = await _phasmaDbContext.PatchBase.ToListAsync();

            return patchData
                        .Select(p => new Patch
                        {
                            ID = p.ID,
                            Source = p.Source,
                            Title = p.Title
                        }
                        ).ToList();
        }

        /// <summary>
        /// Асинхронно возвращает список квестов - Quest на основе кода языка.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <returns>Список квестов.</returns>
        public async Task<List<Quest>> GetQuestsAsync(string languageCode)
        {
            var questData = await _phasmaDbContext.QuestBase
    .Include(q =>
    q.Translations.Where(t => t.LanguageCode == languageCode))
                                .ToListAsync();

            return questData.Select(
                      q => new Quest
                      {
                          ID = q.ID,
                          Description = q.Translations.FirstOrDefault()?.Description,
                          Clause = q.Translations.FirstOrDefault()?.Clause,
                          Reward = q.Reward
                      }
                      ).ToList();
        }
        private static ObservableCollection<ExpandFieldWithImages> MapExpandFieldWithImages(IEnumerable<ExpandFieldWithImagesBase> expandFieldWithImages, string languageCode)
        {
            return new ObservableCollection<ExpandFieldWithImages>(expandFieldWithImages.Select(e => new ExpandFieldWithImages
            {
                Title = e.Translations.FirstOrDefault()?.Title,
                Header = e.Translations.FirstOrDefault()?.Header,
                Body = e.Translations.FirstOrDefault()?.Body,
                ImageWithDescriptions = MapImageWithDescription(e.ImageWithDescriptionBase, languageCode)
            }));
        }

        private static ObservableCollection<ImageWithDescription> MapImageWithDescription(IEnumerable<ImageWithDescriptionBase> imageWithDescriptions, string languageCode)
        {
            return new ObservableCollection<ImageWithDescription>(imageWithDescriptions.Select(i => new ImageWithDescription
            {
                ImageFilePath = i.ImageFilePath,
                Description = i.Translations.FirstOrDefault()?.Description
            }));
        }

        private static ObservableCollection<OtherEquipmentStat> MapOtherEquipmentStat(IEnumerable<OtherEquipmentStatBase> otherEquipmentStats, string languageCode)
        {
            return new ObservableCollection<OtherEquipmentStat>(otherEquipmentStats.Where(o => o.LanguageCode == languageCode).Select(
                                  o => new OtherEquipmentStat
                                  {
                                      Stat = o.Stat
                                  }
                                  ));
        }

        private static ObservableCollection<UnfoldingItem> MapUnfoldingItems(IEnumerable<UnfoldingItemBase> unfoldingItems, string languageCode)
        {
            return new ObservableCollection<UnfoldingItem>(unfoldingItems.Select(u => new UnfoldingItem
            {
                Title = u.Translations.FirstOrDefault()?.Title,
                Header = u.Translations.FirstOrDefault()?.Header,
                Body = u.Translations.FirstOrDefault()?.Body
            }));
        }
    }
}
