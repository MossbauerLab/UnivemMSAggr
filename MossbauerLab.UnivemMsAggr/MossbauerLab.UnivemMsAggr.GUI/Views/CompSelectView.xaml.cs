using System;
using System.Windows;
using Microsoft.Win32;

namespace MossbauerLab.UnivemMsAggr.GUI.Views
{
    /// <summary>
    /// Interaction logic for CompSelectView.xaml
    /// </summary>
    public partial class CompSelectView
    {
        public CompSelectView()
        {
            InitializeComponent();

            SelectCompFileButton.Click += OnSelectCompFileClick;
            CancelButton.Click += OnCancelButtonClick;
            //AddCompButton.PreviewMouseLeftButtonUp += OnAddButtonMouseUp;
        }

        private void OnSelectCompFileClick(Object sender, RoutedEventArgs args)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select Univem MS components file";
            dialog.RestoreDirectory = true;
            dialog.AddExtension = true;
            dialog.Filter = "Components file (.txt)|*.txt| All files (*.*)| *.*";
            dialog.FilterIndex = 0;
            Boolean? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                CompFileBox.Text = dialog.FileName;
            }

        }

        private void OnCancelButtonClick(Object sender, RoutedEventArgs args)
        {
            Close();
        }

/*        private void OnAddButtonMouseUp(Object sender, RoutedEventArgs args)
        {
            Close();
        }*/
    }
}
