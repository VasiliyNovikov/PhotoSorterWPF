using System;
using System.Windows.Input;

namespace PhotoSorterWPF.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action action) => _action = action;

        bool ICommand.CanExecute(object parameter) => true;

        public void Execute(object parameter) => _action();
    }

    public class DelegateCommand<T> : ICommand<T>
    {
        private readonly Action<T> _action;

        public DelegateCommand(Action<T> action) => _action = action;

        public void Execute(T parameter) => _action(parameter);
    }
}
