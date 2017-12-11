using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using MossbauerLab.UnivemMsAggr.GUI.Annotations;
using MossbauerLab.UnivemMsAggr.GUI.Commands;
using MossbauerLab.UnivemMsAggr.GUI.Models;

namespace MossbauerLab.UnivemMsAggr.GUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            UnivemMsSpectraCompFiles = new List<CompSelectionModel>();
        }

        public ICommand AddCommand
        {
            get { return new AddNewCompCommand(); }
        }

        public ICommand RemoveCommand
        {
            get { return new RemoveCompCommand(); }
        }

        public ICommand MoveItemCommand
        {
            get { return new MoveItemCommand(); }
        }

        public ICommand RunCommand
        {
            get { return new RunProcessingCommand(); }
        }

        public IList<CompSelectionModel> UnivemMsSpectraCompFiles { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
