using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MossbauerLab.UnivemMsAggr.GUI.Commands
{
    public class RunProcessingCommand : ICommand
    {
        public void Execute(Object parameter)
        {
            throw new NotImplementedException();
        }

        public Boolean CanExecute(Object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
