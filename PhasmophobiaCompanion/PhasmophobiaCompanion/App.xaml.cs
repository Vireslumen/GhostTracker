using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // Регистрация DataService
            DependencyService.Register<DataService>();

            // Устанановка загрузочной страницы, как начальной
            MainPage = new LoadingScreenPage();


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
                int i = 0;
                i++;
            }
        }

        protected override async void OnStart()
        {
            // Инициализация и загрузка начальных данных 
            await InitializeAppShellAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
