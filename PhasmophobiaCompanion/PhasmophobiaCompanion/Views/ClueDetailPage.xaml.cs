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
    public partial class ClueDetailPage : ContentPage
    {
        public ClueDetailPage(Clue clue)
        {
            InitializeComponent();
            BindingContext = clue;
        }
        private void OnItemTapped(object sender, EventArgs e)
        {
            if (sender is StackLayout layout && layout.BindingContext is UnfoldingItem unfoldingItem)
            {
                unfoldingItem.IsExpanded = !unfoldingItem.IsExpanded;
            }
            else if (sender is StackLayout layout2 && layout2.BindingContext is ExpandFieldWithImages expandFieldWithImages)
            {
                expandFieldWithImages.IsExpanded = !expandFieldWithImages.IsExpanded;
            }
            //TODO: Возможно стоит убрать else if вроде должно и в первый заходить из-за наследования
        }
    }
}