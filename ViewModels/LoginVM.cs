using System.Windows.Input;
using Task.Common;
using Task.Models;

namespace Task.ViewModel;

public class LoginVM
{

    public static Action<Role>? OnLogin;

    private ICommand? _clickLogin;

    private string _login = "";
    private string _password = "";
    private Role _currentRole;

    public string? Login
    {
        get { return _login; }
        set
        {
            _login = value;
            CommandManager.InvalidateRequerySuggested();
        }
    }
    public string? Password
    {
        get { return _password; }
        set
        {
            _password = value;
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public Role CurrentRole
    { 
        get 
        { 
            return _currentRole; 
        }
        set 
        { 
            _currentRole = value; 
        } 
    }

    public Role[] AllowRoles { get; private set; }

    public LoginVM()
    {
        AllowRoles = new Role[2];
        AllowRoles[0] = Role.Manager;
        AllowRoles[1] = Role.Consultant;
    }

    public ICommand ClickLogin
    {
        get
        {
            return _clickLogin ?? (_clickLogin = new CommandHandler(() => TryLogin(), () => CanExecute));
        }
    }

    public bool CanExecute
    {
        get
        {
            return Login.Length > 0 && Password.Length > 0;
        }
    }

    private void TryLogin()
    {
        OnLogin?.Invoke(CurrentRole);
    }

}
