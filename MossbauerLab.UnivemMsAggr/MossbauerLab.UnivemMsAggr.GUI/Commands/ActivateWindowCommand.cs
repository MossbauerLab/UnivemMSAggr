using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MossbauerLab.UnivemMsAggr.GUI.Views;
using MossbauerLab.UnivemMsAggr.GUI.Views.Activators;

namespace MossbauerLab.UnivemMsAggr.GUI.Commands
{
    public class ActivateWindowCommand : ICommand
    {
        public void Execute(Object parameter)
        {
            Object[] fields = parameter as Object[];
            if (fields == null || fields.Length < 1)
                return;
            Boolean activate = Convert.ToBoolean(fields[0]);
            if (!activate)
            {
                Window window = fields[1] as Window;
                if(window != null)
                    ViewActivator.Deactivate(window);
                return;
            }
            ViewActivator.Activate<CompSelectView>();  // todo: make common algorythm
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
