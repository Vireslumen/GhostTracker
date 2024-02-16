using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Контекст базы данных для приложений, наследующий от DbContext.
    /// </summary>
    public class PhasmaDB : DbContext
    {        
        //Определения DbSet для различных сущностей.
        public DbSet<ChallengeModeBase> ChallengeModeBase { get; set; }
        public DbSet<ClueBase> ClueBase { get; set; }
        public DbSet<CursedPossessionBase> CursedPossessionBase { get; set; }
        public DbSet<CursedPossessionCommonTranslations> CursedPossessionCommonTranslations { get; set; }
        public DbSet<DifficultyBase> DifficultyBase { get; set; }
        public DbSet<DifficultyCommonTranslations> DifficultyCommonTranslations { get; set; }
        public DbSet<EquipmentBase> EquipmentBase { get; set; }
        public DbSet<EquipmentCommonTranslations> EquipmentCommonTranslations { get; set; }
        public DbSet<GhostBase> GhostBase { get; set; }
        public DbSet<GhostCommonTranslations> GhostCommonTranslations { get; set; }
        public DbSet<MapBase> MapBase { get; set; }
        public DbSet<MapCommonTranslations> MapCommonTranslations { get; set; }
        public DbSet<OtherInfoBase> OtherInfoBase { get; set; }
        public DbSet<PatchBase> PatchBase { get; set; }
        public DbSet<QuestBase> QuestBase { get; set; }
        public DbSet<TipsTranslations> TipsTranslations { get; set; }

        private void ConfigureChallengeModeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChallengeModeBase>()
                .HasMany(c => c.EquipmentBase)
                .WithMany(c => c.ChallengeModeBase)
                .UsingEntity(j => j.ToTable("ChallengeModeToEquipmentLink"));
        }

        private void ConfigureClueEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClueBase>()
                .HasMany(c => c.UnfoldingItemBase)
                .WithMany(u => u.ClueBase)
                .UsingEntity(j => j.ToTable("ClueToUnfoldingItemLink"));

            modelBuilder.Entity<ClueBase>()
                .HasMany(c => c.ExpandFieldWithImagesBase)
                .WithMany(e => e.ClueBase)
                .UsingEntity(j => j.ToTable("ClueToExpandFieldWithImagesLink"));
        }

        private void ConfigureCursedPossessionEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CursedPossessionBase>()
                .HasMany(c => c.ExpandFieldWithImagesBase)
                .WithMany(e => e.CursedPossessionBase)
                .UsingEntity(j => j.ToTable("CursedPossessionToExpandFieldWithImagesLink"));

            modelBuilder.Entity<CursedPossessionBase>()
                .HasMany(c => c.UnfoldingItemBase)
                .WithMany(u => u.CursedPossessionBase)
                .UsingEntity(j => j.ToTable("CursedPossessionToUnfoldingItemLink"));
        }

        private void ConfigureEquipmentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentBase>()
                .HasMany(e => e.UnfoldingItemBase)
                .WithMany(u => u.EquipmentBase)
                .UsingEntity(j => j.ToTable("EquipmentToUnfoldingItemLink"));
        }

        private void ConfigureGhostEnitity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GhostBase>()
                .HasMany(g => g.ClueBase)
                .WithMany(c => c.GhostBase)
                .UsingEntity(j => j.ToTable("GhostToClueLink"));

            modelBuilder.Entity<GhostBase>()
                .HasMany(g => g.UnfoldingItemBase)
                .WithMany(u => u.GhostBase)
                .UsingEntity(j => j.ToTable("GhostToUnfoldingItemLink"));
        }

        private void ConfigureImageWithDescriptionEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageWithDescriptionBase>()
                .HasOne(iwd => iwd.ExpandFieldWithImagesBase)
                .WithMany(efwi => efwi.ImageWithDescriptionBase)
                .HasForeignKey(iwd => iwd.ExpandFieldWithImagesBaseID);
        }

        private void ConfigureMapEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapBase>()
                .HasMany(m => m.UnfoldingItemBase)
                .WithMany(u => u.MapBase)
                .UsingEntity(j => j.ToTable("MapToUnfoldingItemsLink"));

            modelBuilder.Entity<MapBase>()
                .HasMany(m => m.ExpandFieldWithImagesBase)
                .WithMany(e => e.MapBase)
                .UsingEntity(j => j.ToTable("MapToExpandFieldWithImagesLink"));
        }

        private void ConfigureOtherInfoEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OtherInfoBase>()
                .HasMany(o => o.UnfoldingItemBase)
                .WithMany(u => u.OtherInfoBase)
                .UsingEntity(j => j.ToTable("OtherInfoToUnfoldingItemLink"));

            modelBuilder.Entity<OtherInfoBase>()
                .HasMany(o => o.ExpandFieldWithImagesBase)
                .WithMany(e => e.OtherInfoBase)
                .UsingEntity(j => j.ToTable("OtherInfoToExpandFieldWithImagesLink"));
        }

        /// <summary>
        ///     Конфигурация подключения к базе данных.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = "phasmaDATADB.db"; // Путь к файлу базы данных по умолчанию

            // Проверка, выполняется ли код на Android
            if (Device.RuntimePlatform == Device.Android)
            {
                // Получение пути к папке для хранения базы данных на устройстве
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                dbPath = Path.Combine(folderPath, dbPath);
            }

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        /// <summary>
        ///     Конфигурация моделей с помощью Fluent API.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureGhostEnitity(modelBuilder);
            ConfigureClueEntity(modelBuilder);
            ConfigureImageWithDescriptionEntity(modelBuilder);
            ConfigureCursedPossessionEntity(modelBuilder);
            ConfigureEquipmentEntity(modelBuilder);
            ConfigureMapEntity(modelBuilder);
            ConfigureOtherInfoEntity(modelBuilder);
            ConfigureChallengeModeEntity(modelBuilder);
        }
    }
}