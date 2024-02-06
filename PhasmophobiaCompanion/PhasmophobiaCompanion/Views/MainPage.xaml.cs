using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Services;
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
        MainPageViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            BindingContext = viewModel;
        }
        private void OnOtherPageTapped(object sender, EventArgs e)
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
                var patch = label.BindingContext as Patch;
                if (patch != null && Uri.TryCreate(patch.Source, UriKind.Absolute, out Uri uri))
                {
                    await Launcher.OpenAsync(uri);
                }
            }
        }

        private async void OnMinGhostSpeedTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var minGhostSpeedClause = ghostItem.MinGhostSpeedClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(minGhostSpeedClause));
            }
        }

        private void OnGhostTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var Page = new GhostDetailPage(ghostItem);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }

        private async void OnMaxGhostSpeedTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var maxGhostSpeedClause = ghostItem.MaxGhostSpeedClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxGhostSpeedClause));
            }
        }

        private async void OnMaxGhostSpeedLoSTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var maxGhostSpeedLoSClause = ghostItem.MaxGhostSpeedLoSClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxGhostSpeedLoSClause));
            }
        }

        private async void OnMinSanityHuntTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var minSanityHuntClause = ghostItem.MinSanityHuntClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(minSanityHuntClause));
            }

        }

        private async void OnMaxSanityHuntTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var maxSanityHuntClause = ghostItem.MaxSanityHuntClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxSanityHuntClause));
            }

        }

        private void OnChallengeModeTapped(object sender, EventArgs e)
        {
            var challengeMode = viewModel.ChallengeMode;
            if (viewModel._dataService.IsMapsDataLoaded && viewModel._dataService.IsEquipmentsDataLoaded)
            {
                challengeMode.ChallengeMap = viewModel._dataService.GetMaps().Where(m => m.ID == challengeMode.MapID).FirstOrDefault();
                challengeMode.ChallengeEquipments = new System.Collections.ObjectModel.ObservableCollection<Equipment>
                                                    ( viewModel._dataService.GetEquipments().Where(e => challengeMode.EquipmentsID.Contains(e.ID)).ToList());
                challengeMode.ChallengeDifficulty= viewModel._dataService.GetDifficulties().Where(d => d.ID == challengeMode.DifficultyID).FirstOrDefault();
                var Page = new ChallengeModeDetailPage(challengeMode);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
            // TODO: Сделать, чтобы если Карты не были загружены, какую-нибудь загрузки или что-нибудь в этом духе.

        }

        private void OnClueTapped(object sender, EventArgs e)
        {
            var parentPancake = sender as Xamarin.Forms.PancakeView.PancakeView;
            if(parentPancake?.BindingContext is Clue clueItem)
            {
                var Page=new ClueDetailPage(clueItem);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }

        private void OnDifficultyTapped(object sender, EventArgs e)
        {
            var parentStack = sender as Xamarin.Forms.StackLayout;
            if (parentStack?.BindingContext is Difficulty difficultyItem)
            {
                var Page = new DifficultyDetailPage(difficultyItem);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }
    }
}