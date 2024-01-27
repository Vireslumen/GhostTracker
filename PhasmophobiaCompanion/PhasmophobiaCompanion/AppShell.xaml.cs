using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.ViewModels;
using PhasmophobiaCompanion.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhasmophobiaCompanion
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            // Номер главной вкладки 2
            //CurrentItem = Items[2];
            // Загрузка данных для других вкладок в фоне
            //var dataService = DependencyService.Get<DataService>();
            //Task.Run(() => dataService.LoadGhostsDataAsync());
        }

    }
}
