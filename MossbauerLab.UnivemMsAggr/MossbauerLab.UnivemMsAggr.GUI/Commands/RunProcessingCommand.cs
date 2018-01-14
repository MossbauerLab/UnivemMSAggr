using System;
using System.Windows.Input;

namespace MossbauerLab.UnivemMsAggr.GUI.Commands
{
    public class RunProcessingCommand : ICommand
    {
        public RunProcessingCommand(Action handlerAction)
        {
            if (handlerAction == null)
                throw new ArgumentNullException("handlerAction");
            _handlerAction = handlerAction;
        }

        public void Execute(Object parameter)
        {
            _handlerAction();
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        private Action _handlerAction;
    }
}
