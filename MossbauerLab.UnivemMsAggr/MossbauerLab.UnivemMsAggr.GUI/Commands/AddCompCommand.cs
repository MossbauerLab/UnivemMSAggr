
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
            try
            {
                Object[] fields = parameter as Object[];
                if (fields == null || fields.Length != 2)
                    return;
                CompSelectionModel compFile = new CompSelectionModel((fields[0] as String) ?? "S#1" , fields[1] as String);
                _handlerAction(compFile);
            }
            catch (Exception) { }
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void HandleCanExecuteChanged(Object sender, EventArgs args)
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
                handler(sender, args);
        }

        private readonly Action<CompSelectionModel> _handlerAction;
    }
}
