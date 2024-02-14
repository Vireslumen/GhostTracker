using System;
using System.IO;
using System.Threading.Tasks;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            try
            {
                var logFilePath = Path.Combine("/storage/emulated/0/Download/", "logs", "log-.txt");
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                Log.Logger = logger;
                // Регистрация DataService
                DependencyService.Register<DataService>();
                // Устанановка загрузочной страницы, как начальной
                MainPage = new LoadingScreenPage();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации приложения.");
                throw;
            }
        }

        private async Task InitializeAppShellAsync()
        {
            try
            {
                // Получение экземпляра DataService
                var dataService = DependencyService.Get<DataService>();
                // Загрузка данных
                await Task.Run(async () => { dataService.LoadInitialDataAsync(); });
                Task.Run(async () => { dataService.LoadOtherDataAsync(); });
                //await Task.Delay(50000);
                // После загрузки данных, загрузка начальной страницы
                MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации приложения.");
                throw;
            }
        }

        protected override async void OnStart()
        {
            try
            {
                // Инициализация и загрузка начальных данных 
                await InitializeAppShellAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации приложения.");
            }
        }
    }
}