using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using MossbauerLab.UnivemMsAggr.GUI.ViewModels;
using MossbauerLab.UnivemMsAggr.GUI.Views.Activators;
using MossbauerLab.UnivemMsAggr.GUI.Theme;

namespace MossbauerLab.UnivemMsAggr.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            ThemesControl.ItemsSource = GetThemes();
            DataContext = new MainWindowViewModel();
            AddButton.Click += OnAddItemClick;
        }

        private void OnAddItemClick(Object sender, RoutedEventArgs args)
        {
            ViewActivator.Activate<CompSelectView>();
        }

        private IEnumerable<Theme.Theme> GetThemes()
        {
            return new[] {
                new Theme.Theme("Default theme", new Uri(GetThemeUri("DefaultTheme.xaml"), UriKind.Relative)),
                new Theme.Theme("Dark theme", new Uri(GetThemeUri("DarkTheme.xaml"), UriKind.Relative))
            };
        }

        private String GetThemeUri(String filename)
        {
            return String.Concat("/MossbauerLab.UnivemMsAggr.GUI;component/Themes/", filename);
        }
    }
}
