using System;

using VisualS.ViewModels;

using Windows.UI.Xaml.Controls;

namespace VisualS.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
