using PhasmophobiaCompanion.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}