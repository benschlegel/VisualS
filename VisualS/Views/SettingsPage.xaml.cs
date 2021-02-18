using System;
using VisualS.Core.Helpers;
using VisualS.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VisualS.Views
{
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; } = Singleton<SettingsViewModel>.Instance;

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.EnsureInstanceInitializedAsync();
        }
    }
}
