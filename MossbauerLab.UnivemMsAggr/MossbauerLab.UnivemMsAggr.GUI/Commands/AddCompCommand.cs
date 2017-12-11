
using System;
using System.Windows.Input;
using MossbauerLab.UnivemMsAggr.GUI.Models;

namespace MossbauerLab.UnivemMsAggr.GUI.Commands
{
    public class AddCompCommand : ICommand
    {
        public AddCompCommand(Action<CompSelectionModel> handlerAction)
        {
            if (handlerAction == null)
                throw new ArgumentNullException("handlerAction");
            _handlerAction = handlerAction;
        }

        public void Execute(Object parameter)
        {
            CompSelectionModel compFile = parameter as CompSelectionModel;
            if (compFile != null)
                _handlerAction(compFile);
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        /*{
            add
            {
                CommandManager.RequerySuggested += value; 
            }

            remove
            {
                CommandManager.RequerySuggested -= value; 
            }
        }*/

        private Action<CompSelectionModel> _handlerAction;
    }
}
