using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MossbauerLab.UnivemMsAggr.GUI.Commands
{
    public class MoveItemCommand : ICommand
    {
        public MoveItemCommand(Action<Int32> handlerAction)
        {
            if (handlerAction == null)
                throw new ArgumentNullException("handlerAction");
            _handlerAction = handlerAction;
        }

        public void Execute(Object parameter)
        {
            Int32 index = Convert.ToInt32(parameter);
            if (index < 0)
                index = 0;
            _handlerAction(index);
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

        private readonly Action<Int32> _handlerAction;
    }

}
