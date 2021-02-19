using System;
using System.ComponentModel;
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


        private int _maxValue = 200;

        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                if(value > 0)
                {
                    if (value != _maxValue)
                    {
                        Task.Run(async () => await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(MaxValue), value));
                    }

                    Set(ref _maxValue, value);
                }
            }
        }

        private int _maxBars = 20;

        public int MaxBars
        {
            get { return _maxBars; }
            set {
                if (value > 0)
                {
                    if (value != _maxBars)
                    {
                        Task.Run(async () => await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(MaxBars), value));
                    }

                    Set(ref _maxBars, value);
                }
            }
        }

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

        }

        private bool _hasInstanceBeenInitialized = false;

        public async Task EnsureInstanceInitializedAsync()
        {
            if (!_hasInstanceBeenInitialized)
            {

                MaxBars = await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<int>(nameof(MaxBars));
                MaxValue = await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<int>(nameof(MaxValue));
                if (MaxBars < 1)
                {
                    MaxBars = _maxBars;
                }
                if(MaxValue < 1)
                {
                    MaxValue = _maxValue;
                }
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
