using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapDetailPage : ContentPage
    {
        public MapDetailPage(Map selectMap)
        {
            InitializeComponent();
            BindingContext = selectMap;
        }
        private void OnItemTapped(object sender, EventArgs e)
        {
            if (sender is StackLayout layout && layout.BindingContext is UnfoldingItem unfoldingItem)
            {
                unfoldingItem.IsExpanded = !unfoldingItem.IsExpanded;
            }
        }
    }
}