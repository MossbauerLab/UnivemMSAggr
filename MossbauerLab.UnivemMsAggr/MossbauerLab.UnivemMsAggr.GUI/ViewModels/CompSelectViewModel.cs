using System.ComponentModel;
using System.Windows.Input;
using MossbauerLab.UnivemMsAggr.GUI.Annotations;
using MossbauerLab.UnivemMsAggr.GUI.Commands;
using MossbauerLab.UnivemMsAggr.GUI.Models;

namespace MossbauerLab.UnivemMsAggr.GUI.ViewModels
{
    public class CompSelectViewModel : INotifyPropertyChanged
    {

        public ICommand AddCommand
        {
            get { return new AddCompCommand(AddItemAction); }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddItemAction(CompSelectionModel compFile)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
