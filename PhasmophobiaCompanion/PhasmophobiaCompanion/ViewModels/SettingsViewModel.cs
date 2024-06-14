using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly DataService dataService;
        private List<string> languages;
        private List<string> tipLevels;
        private SettingsCommon settingsCommon;
        private string selectedLanguage;
        private string selectedTipLevel;

        public SettingsViewModel()
        {
            dataService = DependencyService.Get<DataService>();
            tipLevels = dataService.GetTipLevels();
            languages = new List<string>(LanguageDictionary.LanguageMap.Keys);
            selectedLanguage = LanguageDictionary.GetLanguageNameByCode(dataService.LanguageCode);
            settingsCommon = dataService.GetSettingsCommon();
            selectedTipLevel = settingsCommon.SelectedLevel;
            ReportBugCommand = new Command(() => ReportBug());
        }

        public ICommand ReportBugCommand { get; }
        public List<string> Languages
        {
            get => languages;
            set => SetProperty(ref languages, value);
        }
        public List<string> TipLevels
        {
            get => tipLevels;
            set => SetProperty(ref tipLevels, value);
        }
        public SettingsCommon SettingsCommon
        {
            get => settingsCommon;
            set
            {
                settingsCommon = value;
                OnPropertyChanged();
            }
        }
        public string SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;
                    OnPropertyChanged();
                    var selectedLanguageCode = LanguageDictionary.LanguageMap[value];
                    LanguageHelper.SaveUserLanguage(selectedLanguageCode);
                    App.CurrentApp.InitializeAppShellAsync();
                }
            }
        }
        public string SelectedTipLevel
        {
            get => selectedTipLevel;
            set
            {
                if (selectedTipLevel != value)
                {
                    selectedTipLevel = value;
                    SettingsCommon.SelectedLevel = value;
                    dataService.SelectedTipLevel = value;
                    var serializedData = JsonConvert.SerializeObject(SettingsCommon);
                    var filePath = Path.Combine(dataService.FolderPath,
                        dataService.LanguageCode + "_" + "settings_common_cache.json");
                    File.WriteAllText(filePath, serializedData);
                    OnPropertyChanged();
                }
            }
        }

        private void ReportBug()
        {
            //TODO: Добавить логику отправления баг репорта.
        }
    }
}