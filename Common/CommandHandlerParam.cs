using System.Windows.Input;

namespace Task.Common;

public class CommandHandlerParam : ICommand
{
    private Action<object> _action;
    private Func<bool>? _canExecute;

    public CommandHandlerParam(Action<object> action)
    {
        this._action = action;
    }

    public CommandHandlerParam(Action<object> action, Func<bool> canExecute)
    {
        _action = action;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        if (_canExecute == null) return true;
        return _canExecute.Invoke();
    }

    public void Execute(object parameter)
    {
        _action(parameter);
    }
}