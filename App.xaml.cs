using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using Task.Interface;
using Task.Models;
using Task.ViewModel;

namespace Task;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    //private Dictionary<class, Window> Windows;

    private WndLogin _wndLogin;
    private WndPersonsList _wndPersonsList;
    private IPersonStorage _personStorage;
    public App()
    {
        _wndLogin = new WndLogin();
        _wndPersonsList = new WndPersonsList();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        ShowLoginWindow();
        //OnLogin(Role.Manager);
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        _personStorage?.Save();
    }

    private void ShowLoginWindow()
    {
        _wndLogin.Show();
        LoginVM.OnLogin += OnLogin;
    }

    private void OnLogin(Role role)
    {
        CurrentRole.Current = role;
        LoginVM.OnLogin -= OnLogin;
        _personStorage = new ExtendedJsonPersonStorage<ExtendedPersonInStorage>();
        if (_wndLogin != null && _wndLogin.IsActive)
            _wndLogin.Close();
        ShowPersonList();
        _personStorage.Init();
    }
    private void ShowPersonList()
    {
        _wndPersonsList.Show();
        (_wndPersonsList.DataContext as PersonsListVM).SetModel(_personStorage);
    }


}