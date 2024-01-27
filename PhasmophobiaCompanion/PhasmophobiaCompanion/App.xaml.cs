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

            // Инициализация и загрузка начальных данных 
            InitializeAppShellAsync();
        }
        private async Task InitializeAppShellAsync()
        {
            try
            {

                // Получение экземпляра DataService
                var dataService = DependencyService.Get<DataService>();

                // Загрузка данных
                await dataService.LoadInitialDataAsync();

                // После загрузки данных, загрузка начальной страницы
                MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                int i = 0;
                i++;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
