using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MossbauerLab.UnivemMsAggr.GUI.Annotations;
using MossbauerLab.UnivemMsAggr.GUI.Commands;
using MossbauerLab.UnivemMsAggr.GUI.Models;
using MossbauerLab.UnivemMsAggr.GUI.Utils;

namespace MossbauerLab.UnivemMsAggr.GUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IMessageRecipient<CompSelectionModel>
    {
        public MainWindowViewModel()
        {
            GlobalDefs.ViewModelsMediator.AddParticipant(GlobalDefs.MainWindowdViewModelId, this);
        }

        public void TransferMessage(CompSelectionModel message)
        {
            if(message != null)
                AddItemAction(message);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddItemAction(CompSelectionModel compFile)
        {
            UnivemMsSpectraCompFiles.Add(compFile);
        }

        private void RemoveItemAction(Int32 index)
        {
            if (index >= 0 && index < UnivemMsSpectraCompFiles.Count)
                UnivemMsSpectraCompFiles.RemoveAt(index);
        }

        // todo: umv: reduce code (repeating)
        private void MoveItemUpAction(Int32 index)
        {
            if (index > 0 && index < UnivemMsSpectraCompFiles.Count)
            {
                CompSelectionModel item = UnivemMsSpectraCompFiles[index];
                UnivemMsSpectraCompFiles.RemoveAt(index);
                UnivemMsSpectraCompFiles.Insert(index - 1, item);
            }
        }

        private void MoveItemDownAction(Int32 index)
        {
            if (index >= 0 && index < UnivemMsSpectraCompFiles.Count - 1)
            {
                CompSelectionModel item = UnivemMsSpectraCompFiles[index];
                UnivemMsSpectraCompFiles.RemoveAt(index);
                UnivemMsSpectraCompFiles.Insert(index + 1, item);
            }
        }

        private void RunAction()
        {
            // todo : execute here, notify UI via Binding 
        }

        public ICommand RemoveCommand
        {
            get { return new RemoveCompCommand(RemoveItemAction); }
        }

        public ICommand MoveItemUpCommand
        {
            get { return new MoveItemCommand(MoveItemUpAction); }
        }

        public ICommand MoveItemDownCommand
        {
            get { return new MoveItemCommand(MoveItemDownAction); }
        }

        public ICommand RunCommand
        {
            get { return new RunProcessingCommand(RunAction); }
        }

        public static ObservableCollection<CompSelectionModel> UnivemMsSpectraCompFiles
        {
            get { return _univemMsSpectraCompFiles; }
            set { _univemMsSpectraCompFiles = value; }
        }
        
        //public CompSelectionModel SelectedModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private static ObservableCollection<CompSelectionModel> _univemMsSpectraCompFiles  = new ObservableCollection<CompSelectionModel>();
    }
}
