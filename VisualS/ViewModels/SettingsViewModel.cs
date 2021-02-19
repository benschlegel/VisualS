using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using VisualS.Core.Models;
using VisualS.Helpers;
using VisualS.Services;

using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace VisualS.ViewModels
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/pages/settings.md
    public class SettingsViewModel : Observable
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            if(_hasInstanceBeenInitialized)
                            {
                                ElementTheme = param;
                                await ThemeSelectorService.SetThemeAsync(param);
                            }
                        });
                }

                return _switchThemeCommand;
            }
        }

        public SettingsViewModel()
        {
            BarDataStore.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var BarData = (BarDataStore) sender;
            switch (e.PropertyName)
            {
                case nameof(BarDataStore.MaxValue):

                    //Task.Run(async () => await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(BarDataStore.Instance.MaxValue), BarData.MaxValue));
                    //BarDataStore.Instance.MaxValue = BarData.MaxValue;
                    break;
                case nameof(BarDataStore.MaxBars):
                    //BarDataStore.Instance.MaxValue;
                    break;
            }
        }

        private bool _hasInstanceBeenInitialized = false;

        public async Task EnsureInstanceInitializedAsync()
        {
            if (!_hasInstanceBeenInitialized)
            {
                BarDataStore.Instance.MaxBars = await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<int>(nameof(BarDataStore.Instance.MaxBars));
                BarDataStore.Instance.MaxValue = await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<int>(nameof(BarDataStore.Instance.MaxValue));                
                //TODO: Check for nullable

                await InitializeAsync();

                _hasInstanceBeenInitialized = true;
            }
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
