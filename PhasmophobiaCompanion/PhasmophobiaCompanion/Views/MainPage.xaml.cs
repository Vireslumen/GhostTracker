using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private  void OnOtherPageTapped(object sender, EventArgs e)
        {
            if (sender is BindableObject bindable && bindable.BindingContext is OtherInfo otherInfo)
            {
                var Page = new OtherInfoPage(otherInfo);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }
        private async void OnPatchTapped(object sender, EventArgs e)
        {
            var label = sender as Label;
            if (label != null)
            {
                var patch = label.BindingContext as Patches;
                if (patch != null && Uri.TryCreate(patch.Source, UriKind.Absolute, out Uri uri))
                {
                    await Launcher.OpenAsync(uri);
                }
            }
        }
    }
}