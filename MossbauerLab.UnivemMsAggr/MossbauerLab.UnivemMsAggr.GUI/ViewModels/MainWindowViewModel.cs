using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MossbauerLab.UnivemMsAggr.GUI.Commands;
using MossbauerLab.UnivemMsAggr.GUI.Models;

namespace MossbauerLab.UnivemMsAggr.GUI.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            UnivemMsSpectraCompFiles = new List<CompSelectionModel>();
        }

        public ICommand AddCommand
        {
            get { return new AddNewCompCommand(); }
        }

        public IList<CompSelectionModel> UnivemMsSpectraCompFiles { get; set; }
    }
}
