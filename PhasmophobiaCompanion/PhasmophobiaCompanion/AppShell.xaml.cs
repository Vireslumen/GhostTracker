using PhasmophobiaCompanion.ViewModels;
using PhasmophobiaCompanion.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PhasmophobiaCompanion
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Shell.SetNavBarIsVisible(this, false);
        }

    }
}
