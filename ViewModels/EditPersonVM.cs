using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task.Common;
using Task.Interface;
using Task.Models;

namespace Task.ViewModel;

public class EditPersonVM : INotifyPropertyChanged
{

    public Action<IPerson> OnPersonHasEdited;
    public Action OnPersonEditCanceled;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool FirstNameIsReadOnly
    {
        get
        {
            return RoleAccess.FirstName() == AccessMode.ReadOnly;
        }
    }

    public bool LastNameIsReadOnly
    {
        get
        {
            return RoleAccess.LastName() == AccessMode.ReadOnly;
        }
    }

    public bool ThitdNameIsReadOnly
    {
        get
        {
            return RoleAccess.ThirdName() == AccessMode.ReadOnly;
        }
    }

    public bool PassportSeriesIsReadOnly
    {
        get
        {
            return RoleAccess.PassportSeries() == AccessMode.ReadOnly;
        }
    }
    public bool PassportNumberIsReadOnly
    {
        get
        {
            return RoleAccess.PassportNumber() == AccessMode.ReadOnly;
        }
    }

    private IPerson _person;

    public IPerson Person 
    { 
        get 
        {
            return _person;
        }
        set 
        { 
            _person = value;
            OnPropertyChanged("Person");
        } 
    }

    private ICommand _clickOk;
    private ICommand _clickCancel;

    public ICommand ClickOk
    {
        get
        {
            return _clickOk ?? (_clickOk = new CommandHandler(() => FinishEdit(), () => CanExecute));
        }
    }

    public ICommand ClickCancel
    {
        get
        {
            return _clickCancel ?? (_clickCancel = new CommandHandler(() => CancelEdit(), () => CanExecute));
        }
    }

    private void CancelEdit()
    {
        OnPersonEditCanceled?.Invoke();
    }

    private void FinishEdit()
    {
        OnPersonHasEdited?.Invoke(Person);
    }

    public bool CanExecute
    {
        get
        {
            return true;
        }
    }

    public EditPersonVM(){}

    private void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

}