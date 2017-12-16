using System;
using System.Windows;
using MossbauerLab.UnivemMsAggr.GUI.ViewModels;

namespace MossbauerLab.UnivemMsAggr.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView
    {
        public MainWindowView()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            MoveUpButton.Click += OnItemMoveClick;
            MoveDownButton.Click += OnItemMoveClick;
        }

        private void OnItemMoveClick(Object sender, RoutedEventArgs args)
        {
            CompFilesGrid.Focus();
            //CompFilesGrid.SelectedIndex = 1;
        }
    }
}
