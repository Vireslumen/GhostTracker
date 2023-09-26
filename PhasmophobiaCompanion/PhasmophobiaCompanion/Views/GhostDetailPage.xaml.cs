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
    public partial class GhostDetailPage : ContentPage
    {
        private List<StackLayout> expandingStack;
        public GhostDetailPage(Ghost selectedGhost)
        {
            InitializeComponent();
            BindingContext = selectedGhost;
            expandingStack = new List<StackLayout> { StrengthStack, AbilityStack, WeaknessesStack, SanityHuntStack, SpeedStack };

            foreach (var stack in expandingStack)
            {
                stack.Children[0].GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() =>
                    {
                        stack.Children[1].IsVisible = !stack.Children[1].IsVisible;
                        (((stack.Children[0] as Frame).Content as StackLayout).Children[1] as Image).Source = stack.Children[1].IsVisible ? "collapse_icon.png" : "expand_icon.png";
                    })
                });
            }
        }

        private void OnImageTapped(object sender, EventArgs e)
        {

        }
    }
}