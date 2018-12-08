using System;
using System.Diagnostics;
using System.Windows.Input;

namespace PropertyChangedCommandBehaviourTutorial
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            TextChangedCommand = new DelegateCommand(v => Debug.WriteLine(v));
        }

        public ICommand TextChangedCommand { get; }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action<object> action;

        public DelegateCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}