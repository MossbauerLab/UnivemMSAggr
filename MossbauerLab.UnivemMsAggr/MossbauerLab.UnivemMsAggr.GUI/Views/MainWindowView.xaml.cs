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

            MoveUpButton.MouseMove += OnItemMoveMouseMove;
            MoveDownButton.MouseMove += OnItemMoveMouseMove;
            MoveUpButton.MouseLeftButtonUp += OnItemMoveClick;
            MoveDownButton.MouseLeftButtonUp += OnItemMoveClick;
        }

        private void OnItemMoveMouseMove(Object sender, RoutedEventArgs args)
        {
            if (CompFilesGrid.SelectedIndex >= 0)
                _selectedDataGridIndex = CompFilesGrid.SelectedIndex;
        }

        private void OnItemMoveClick(Object sender, RoutedEventArgs args)
        {
            if (_selectedDataGridIndex >= 0)
            {
                CompFilesGrid.Focus();
                CompFilesGrid.SelectedItem = CompFilesGrid.Items[_selectedDataGridIndex];
            }
        }

        private Int32 _selectedDataGridIndex;
    }
}
