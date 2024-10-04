using System.ComponentModel;
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
            return _clickOk ?? (_clickOk = new CommandHandler(() => FinishEdit(), () => CanExecuteSave));
        }
    }

    public ICommand ClickCancel
    {
        get
        {
            return _clickCancel ?? (_clickCancel = new CommandHandler(() => CancelEdit(), () => true));
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

    public bool CanExecuteSave
    {
        get
        {
            if (Person != null &&
                Person.FirstName.Length > 0 &&
                Person.LastName.Length > 0 &&
                Person.ThirdName.Length > 0 &&
                Person.PhoneNumber.Length > 0 &&
                Person.PassportSeries.Length > 0 &&
                Person.PassportNumber.Length > 0)
            {
                return true;
            }
            return false;
        }
    }

    public EditPersonVM(){}

    private void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

}