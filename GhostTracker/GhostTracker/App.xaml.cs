using System;
using System.Threading.Tasks;
using GhostTracker.Services;
using GhostTracker.Views;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LoggerConfigurationManager.InitializeLogger();
            CurrentApp = this;

            // Глобальная обработка исключений для .NET
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Log.Error((Exception) args.ExceptionObject, "Необработанное исключение");
            };

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                Log.Error(args.Exception, "Необработанное исключение в задаче");
                args.SetObserved(); // Предотвращение завершения приложения
            };

            try
            {
                // Регистрация DataService
                DependencyService.Register<DataService>();
                // Установка загрузочной страницы, как начальной
                MainPage = new LoadingScreenPage();
                RequestedThemeChanged += (sender, args) =>
                {
                    // Уведомляем всех подписчиков о смене темы
                    ThemeChanged?.Invoke();
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации приложения.");
            }
        }

        public static App CurrentApp { get; private set; }

        public async Task InitializeAppShellAsync()
        {
            try
            {
                // Получение экземпляра DataService
                var dataService = DependencyService.Get<DataService>();
                dataService.ReinitializeLanguage();
                // Загрузка данных
                await Task.Run(async () => { await dataService.LoadInitialDataAsync(); });
                Task.Run(async () => { dataService.LoadOtherDataAsync(); });
                // После загрузки данных, загрузка начальной страницы
                MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации приложения.");
            }
        }

        protected override async void OnStart()
        {
            // Инициализация и загрузка начальных данных 
            await InitializeAppShellAsync();
        }

        public static event Action ThemeChanged;
    }
}