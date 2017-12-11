using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MossbauerLab.UnivemMsAggr.GUI.Commands
{
    public class RemoveCompCommand : ICommand
    {
        public void Execute(Object parameter)
        {
            throw new NotImplementedException();
        }

        public Boolean CanExecute(Object parameter)
        {
            //throw new NotImplementedException();
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
