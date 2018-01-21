using System;
using System.Windows;
using MossbauerLab.UnivemMsAggr.GUI.ViewModels;
using MossbauerLab.UnivemMsAggr.GUI.Views.Activators;

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
            
            DataContext = new MainWindowViewModel();
            AddButton.Click += OnAddItemClick;
        }

        private void OnAddItemClick(Object sender, RoutedEventArgs args)
        {
            ViewActivator.Activate<CompSelectView>();
        }
    }
}
