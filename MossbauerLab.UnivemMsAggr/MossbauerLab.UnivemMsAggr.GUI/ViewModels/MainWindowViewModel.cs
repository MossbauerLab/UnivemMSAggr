﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MossbauerLab.UnivemMsAggr.Core.Data;
using MossbauerLab.UnivemMsAggr.Core.Export;
using MossbauerLab.UnivemMsAggr.Core.UnivemMs.FilesProcessor;
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
            OutputFile = String.Format(@"{0}\report.docx", Environment.GetEnvironmentVariable("SystemDrive"));
            _spectrumFitsCount = 0;
            _exportService.SpectrumFitProcessed += OnSpectrumProcessed;
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

        private void SelectOutputAction(String output)
        {
            OutputFile = output;
            OnPropertyChanged("OutputFile");
        }

        private void RunAction()
        {
            ProgressValue = 0;
            _spectrumFitsCount = 0;
            Task.Factory.StartNew(() =>
            {
                _currentlyProcessingFile = Path.GetFileName(UnivemMsSpectraCompFiles[0].SpectrumComponentFile);
                OnPropertyChanged("CurrentrlyProccessingFile");
                _exportService.Export(OutputFile, UnivemMsSpectraCompFiles.Select(item =>
                {
                    SpectrumFit fit = CompProcessor.Process(item.SpectrumComponentFile);
                    fit.SampleName = item.SampleName;
                    return fit;
                }).ToList());
            });
        }

        private void OnSpectrumProcessed(Object sender, ProcessedSpectrumFitEventArgs args)
        {
            _spectrumFitsCount++;
            Task.Factory.StartNew(() =>
            {
                _currentlyProcessingFile = Path.GetFileName(UnivemMsSpectraCompFiles[_spectrumFitsCount].SpectrumComponentFile);
                OnPropertyChanged("CurrentrlyProccessingFile");
            });
            Task.Factory.StartNew(() =>
            {
                
                
                
                _progressValue = Decimal.Round(100.0m*((Decimal) _spectrumFitsCount/UnivemMsSpectraCompFiles.Count));
                OnPropertyChanged("ProgressValue");
            });
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

        public SelectOutputFileCommand OutputFileCommand
        {
            get { return new SelectOutputFileCommand(SelectOutputAction);}
        }

        public String OutputFile { get; set; }

        public Decimal ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; }
        }

        public String CurrentrlyProccessingFile
        {
            get { return _currentlyProcessingFile; }
            set { _currentlyProcessingFile = value; }
        }

        public static ObservableCollection<CompSelectionModel> UnivemMsSpectraCompFiles
        {
            get { return _univemMsSpectraCompFiles; }
            set { _univemMsSpectraCompFiles = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private static ObservableCollection<CompSelectionModel> _univemMsSpectraCompFiles  = new ObservableCollection<CompSelectionModel>();

        private readonly ISpectrumFitExport _exportService = new SpectrumFitToMsWord();
        private String _currentlyProcessingFile;
        private Int32 _spectrumFitsCount;
        private Decimal _progressValue;
    }
}
