using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using MossbauerLab.UnivemMsAggr.GUI.Annotations;
using MossbauerLab.UnivemMsAggr.GUI.Commands;
using MossbauerLab.UnivemMsAggr.GUI.Models;
using MossbauerLab.UnivemMsAggr.GUI.Views.Activators;

namespace MossbauerLab.UnivemMsAggr.GUI.ViewModels
{
    public class CompSelectViewModel : INotifyPropertyChanged
    {
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddItemAction(CompSelectionModel compFile, Window window)
        {
            GlobalDefs.ViewModelsMediator.Send(compFile, GlobalDefs.MainWindowdViewModelId);
            ViewActivator.Deactivate(window);
        }

        public ICommand AddCommand
        {
            get { return new AddCompCommand(AddItemAction); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
