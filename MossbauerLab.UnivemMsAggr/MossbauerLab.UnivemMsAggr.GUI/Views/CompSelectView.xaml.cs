using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        }

        private void OnSelectCompFileClick(Object sender, RoutedEventArgs args)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            Boolean? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                CompFileBox.Text = dialog.FileName;
            }

        }
    }
}
