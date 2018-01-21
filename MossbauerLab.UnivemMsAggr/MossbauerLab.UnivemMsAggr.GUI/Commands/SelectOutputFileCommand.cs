using System;
using System.Windows.Input;
using Microsoft.Win32;

namespace MossbauerLab.UnivemMsAggr.GUI.Commands
{
    public class SelectOutputFileCommand : ICommand
    {
        public SelectOutputFileCommand(Action<String> handlerAction)
        {
            if (handlerAction == null)
                throw new ArgumentNullException("handlerAction");
            _handlerAction = handlerAction;
        }

        public void Execute(Object parameter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Select MS Word file to save processing result";
            dialog.RestoreDirectory = true;
            dialog.AddExtension = true;
            dialog.Filter = "MS Word 2007-2016 (.docx)|*.docx";
            dialog.FilterIndex = 0;
            Boolean? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
                _handlerAction(dialog.FileName);
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        private readonly Action<String> _handlerAction;
    }
}
