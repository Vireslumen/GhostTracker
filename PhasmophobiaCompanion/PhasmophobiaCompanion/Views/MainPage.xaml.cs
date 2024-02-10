using System;
using System.Collections.ObjectModel;
using System.Linq;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            BindingContext = viewModel;
        }

        /// <summary>
        ///     Переход на подробную страницу особого режима по нажатию на него.
        /// </summary>
        private void OnChallengeModeTapped(object sender, EventArgs e)
        {
            var challengeMode = viewModel.ChallengeMode;
            if (viewModel._dataService.IsMapsDataLoaded && viewModel._dataService.IsEquipmentsDataLoaded)
            {
                challengeMode.ChallengeMap = viewModel._dataService.GetMaps().Where(m => m.ID == challengeMode.MapID)
                    .FirstOrDefault();
                challengeMode.ChallengeEquipments = new ObservableCollection<Equipment>
                (viewModel._dataService.GetEquipments().Where(e => challengeMode.EquipmentsID.Contains(e.ID))
                    .ToList());
                challengeMode.ChallengeDifficulty = viewModel._dataService.GetDifficulties()
                    .Where(d => d.ID == challengeMode.DifficultyID).FirstOrDefault();
                var Page = new ChallengeModeDetailPage(challengeMode);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
            // TODO: Сделать, чтобы если Карты не были загружены, какую-нибудь загрузки или что-нибудь в этом духе.
        }

        /// <summary>
        ///     Переход на подробную страницу улики по нажатию на неё.
        /// </summary>
        private void OnClueTapped(object sender, EventArgs e)
        {
            var parentPancake = sender as PancakeView;
            if (parentPancake?.BindingContext is Clue clueItem)
            {
                var Page = new ClueDetailPage(clueItem);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }

        /// <summary>
        ///     Переход на подробную страницу сложности по нажатию на неё.
        /// </summary>
        private void OnDifficultyTapped(object sender, EventArgs e)
        {
            var parentStack = sender as StackLayout;
            if (parentStack?.BindingContext is Difficulty difficultyItem)
            {
                var Page = new DifficultyDetailPage(difficultyItem);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }

        /// <summary>
        ///     Переход на подробную страницу призрака по нажатию на него.
        /// </summary>
        private void OnGhostTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var Page = new GhostDetailPage(ghostItem);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по максимальной скорости призрака с учётом LoS на экране.
        /// </summary>
        private async void OnMaxGhostSpeedLoSTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var maxGhostSpeedLoSClause = ghostItem.MaxGhostSpeedLoSClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxGhostSpeedLoSClause));
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по максимальной скорости призрака на экране.
        /// </summary>
        private async void OnMaxGhostSpeedTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var maxGhostSpeedClause = ghostItem.MaxGhostSpeedClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxGhostSpeedClause));
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимума минимального порога рассудка для начала охоты.
        /// </summary>
        private async void OnMaxSanityHeaderTapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(viewModel.GhostCommon.MaxSanityHunt));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по максимуму минимального порога рассудка для начала охоты на экране.
        /// </summary>
        private async void OnMaxSanityHuntTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var maxSanityHuntClause = ghostItem.MaxSanityHuntClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxSanityHuntClause));
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимальной скорости призрака на экране.
        /// </summary>
        private async void OnMaxSpeedHeaderTapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(viewModel.GhostCommon.MaxSpeed));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимальной скорости призрака с учётом LoS на экране.
        /// </summary>
        private async void OnMaxSpeedLoSHeaderTapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(viewModel.GhostCommon.MaxSpeedLoS));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по минимальной скорости призрака на экране.
        /// </summary>
        private async void OnMinGhostSpeedTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var minGhostSpeedClause = ghostItem.MinGhostSpeedClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(minGhostSpeedClause));
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку минимума минимального порога рассудка для начала охоты.
        /// </summary>
        private async void OnMinSanityHeaderTapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(viewModel.GhostCommon.MinSanityHunt));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по минимуму минимального порога рассудка для начала охоты на экране.
        /// </summary>
        private async void OnMinSanityHuntTapped(object sender, EventArgs e)
        {
            var parentLabel = sender as Label;
            if (parentLabel?.BindingContext is Ghost ghostItem)
            {
                var minSanityHuntClause = ghostItem.MinSanityHuntClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(minSanityHuntClause));
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку минимальной скорости призрака на экране.
        /// </summary>
        private async void OnMinSpeedHeaderTapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(viewModel.GhostCommon.MinSpeed));
        }

        /// <summary>
        ///     Переход на подробную некатегоризованную страницу по нажатию на неё.
        /// </summary>
        private void OnOtherPageTapped(object sender, EventArgs e)
        {
            if (sender is BindableObject bindable && bindable.BindingContext is OtherInfo otherInfo)
            {
                var Page = new OtherInfoPage(otherInfo);
                Application.Current.MainPage.Navigation.PushAsync(Page);
            }
        }

        /// <summary>
        ///     Переход на страницу в браузере патча.
        /// </summary>
        private async void OnPatchTapped(object sender, EventArgs e)
        {
            var label = sender as Label;
            if (label != null)
            {
                var patch = label.BindingContext as Patch;
                if (patch != null && Uri.TryCreate(patch.Source, UriKind.Absolute, out var uri))
                    await Launcher.OpenAsync(uri);
            }
        }
    }
}