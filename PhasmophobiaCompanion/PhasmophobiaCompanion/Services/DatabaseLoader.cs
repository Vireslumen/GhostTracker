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
                        .ToListAsync();

            // Преобразование данных в список объектов CursedPossession.
            return cursedPossessionData
                  .Select(c => new CursedPossession
                  {
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
                      UnlockLevel = d.UnlockLevel,
                      RewardMultiplier = d.RewardMultiplier,
                      SetupTime = d.SetupTime,
                      SanityConsumption = d.SanityConsumption,
                      ElectricityOn = d.ElectricityOn,
                      SanityMonitorWork = d.SanityMonitorWork,
                      ActivityMonitorWork = d.ActivityMonitorWork,
                      EvidenceAvailable = d.EvidenceAvailable,
                      Title = d.Translations.FirstOrDefault()?.Title,
                      Description = d.Translations.FirstOrDefault()?.Description,
                      GhostActivity = d.Translations.FirstOrDefault()?.GhostActivity,
                      GhostHuntTime = d.Translations.FirstOrDefault()?.GhostHuntTime,
                      SanityRestoration = d.Translations.FirstOrDefault()?.SanityRestoration,
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
                        .ToListAsync();

            return equipmentData
                  .Select(e => new Equipment
                  {
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
        /// Асинхронно возвращает список призраков - Ghost на основе кода языка, а также всех улик.
        /// </summary>
        /// <param name="languageCode">Код языка для получения переводов.</param>
        /// <param name="clues">Список всех улик, который будет записан методом.</param>
        /// <returns>Список призраков.</returns>
        public async Task<List<Ghost>> GetGhostsAsync(string languageCode, ObservableCollection<Clue> clues)
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

            List<Ghost> ghosts = new List<Ghost>();
            foreach (var g in ghostData)
            {
                ghosts.Add(new Ghost
                {
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
                    UnfoldingItems = MapUnfoldingItems(g.UnfoldingItemBase, languageCode)
                });
                ghosts[ghosts.Count - 1].Clues = MapClues(g.ClueBase, languageCode, clues, ghosts[ghosts.Count - 1]);
            }
            return ghosts;
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
                            .ToListAsync();
            return mapData
                  .Select(m => new Map
                  {
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
                          Description = q.Translations.FirstOrDefault()?.Description,
                          Clause = q.Translations.FirstOrDefault()?.Clause,
                          Reward = q.Reward
                      }
                      ).ToList();
        }

        private static ObservableCollection<Clue> MapClues(IEnumerable<ClueBase> ghostClueBase, string languageCode, ObservableCollection<Clue> allClue, Ghost ghost)
        {
            ObservableCollection<Clue> ghostClue = new ObservableCollection<Clue>();

            foreach (var c in ghostClueBase)
            {
                bool wasnot = false;
                foreach (var CL in allClue)
                {
                    if (c.Translations.FirstOrDefault()?.Title == CL.Title)
                    {
                        wasnot = true;
                        CL.Ghosts.Add(ghost);
                        ghostClue.Add(CL);
                        break;

                    }
                }
                if (!wasnot)
                {
                    allClue.Add(new Clue
                    {
                        Ghosts = new ObservableCollection<Ghost>(),
                        IconFilePath = c.IconFilePath,
                        ImageFilePath = c.ImageFilePath,
                        Title = c.Translations.FirstOrDefault()?.Title,
                        Description = c.Translations.FirstOrDefault()?.Description,
                        UnfoldingItems = MapUnfoldingItems(c.UnfoldingItemBase, languageCode),
                        ExpandFieldsWithImages = MapExpandFieldWithImages(c.ExpandFieldWithImagesBase, languageCode)
                    });
                    allClue[allClue.Count - 1].Ghosts.Add(ghost);
                    ghostClue.Add(allClue[allClue.Count - 1]);
                }

            }
            return ghostClue;
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
