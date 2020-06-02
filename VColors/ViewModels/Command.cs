using System;
using System.Windows.Input;

namespace VColors
{
    public class Command : ICommand
    {
        private readonly Action _executeAction;

        public Command(Action executeAction)
        {
            _executeAction = executeAction;
        }

        public void Execute() => _executeAction();

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _executeAction();
        }

        public event EventHandler CanExecuteChanged;

    }
}
