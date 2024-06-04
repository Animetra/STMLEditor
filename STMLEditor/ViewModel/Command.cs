using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace STMLEditor.ViewModel
{
    class Command : ICommand
    {
        private Action _action;
        private bool _canExecute = true;
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {

            if (parameter is bool newStatus && newStatus != _canExecute) { _canExecute = newStatus;  CanExecuteChanged?.Invoke(this, new EventArgs()); }

            return _canExecute;
        }

        public void Execute(object? parameter)
        {
            _action.Invoke();
        }

        public Command(Action action)
        {
            _action = action;
        }
    }
}
