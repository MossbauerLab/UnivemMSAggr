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
            //UnivemMsSpectraCompFiles = new ObservableCollection<CompSelectionModel>();
            GlobalDefs.ViewModelsMediator.AddParticipant(GlobalDefs.MainWindowdViewModelId, this);
            StubInit();
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
            OnPropertyChanged("UnivemMsSpectraCompFiles");
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

        private void StubInit()
        {
            UnivemMsSpectraCompFiles.Add(new CompSelectionModel("NA", "na_11s2_comp.txt"));
            UnivemMsSpectraCompFiles.Add(new CompSelectionModel("NB", "nb_11s1_comp.txt"));
            UnivemMsSpectraCompFiles.Add(new CompSelectionModel("NC", "nc_11s2_comp.txt"));
            UnivemMsSpectraCompFiles.Add(new CompSelectionModel("ND", "nd_11s3_comp.txt"));
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
            get { return new RunProcessingCommand(); }
        }

        public ObservableCollection<CompSelectionModel> UnivemMsSpectraCompFiles
        {
            get { return _univemMsSpectraCompFiles; }
            set { _univemMsSpectraCompFiles = value; OnPropertyChanged("UnivemMsSpectraCompFiles");}
        }
        public CompSelectionModel SelectedModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CompSelectionModel> _univemMsSpectraCompFiles  = new ObservableCollection<CompSelectionModel>();
    }
}
