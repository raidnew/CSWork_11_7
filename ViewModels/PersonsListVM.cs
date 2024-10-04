using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task.Common;
using Task.Interface;
using Task.Models;

namespace Task.ViewModel;

public class PersonsListVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private ICommand? _addPerson;
    private ICommand? _editPerson;
    private ICommand? _removePerson;
    private ICommand? _sortPersons;
    private IPersonStorage _model;
    private WndEditPerson wndEditPerson;
    private ObservableCollection<IPerson> _persons;
    private string? _currentSortField;
    private bool _currentSortDirection = true; //true asc, false desc

    private int _selectedIndex;
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set
        {
            _selectedIndex = value;
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public ObservableCollection<IPerson>? Persons
    {
        get { return _persons; }
        set
        {
            _persons = value;
            OnPropertyChanged(nameof(Persons));
        }
    }

    public PersonsListVM() 
    {
        Persons = new ObservableCollection<IPerson>();
    }

    public void SetModel(IPersonStorage model)
    {
        _model = model;
        _model.PropertyChanged += OnModelChanged;
    }

    private void OnModelChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Persons))
            Persons = _model.Persons;

        //SortPersons();
    }

    public IPerson SelectedPerson { get; set; }

    public ICommand ClickEditPerson
    {
        get
        {
            return _editPerson ?? (_editPerson = new CommandHandler(() => EditPerson(), () => CanEditPerson));
        }
    }
    public ICommand ClickAddPerson
    {
        get
        {
            return _addPerson ?? (_addPerson = new CommandHandler(() => AddPerson(), () => CanAddPerson));
        }
    }

    public ICommand ClickRemovePerson
    {
        get
        {
            return _removePerson ?? (_removePerson = new CommandHandler(() => RemovePerson(), () => CanRemovePerson));
        }
    }
    
    public ICommand ClickSortPersons
    {
        get
        {
            return _sortPersons ?? (_sortPersons = new CommandHandlerParam((data) => SortAction(data)));
        }
    }

    public void SortAction(object field)
    {
        IOrderedEnumerable<IPerson> hasSortedList = null;

        if (field == _currentSortField)
            _currentSortDirection = !_currentSortDirection;

        _currentSortField = (string)field;

        switch (field){
            case "FirstName":
                if (_currentSortDirection)
                    hasSortedList = Persons.OrderBy(person => person.FirstName);
                else
                    hasSortedList = Persons.OrderByDescending(person => person.FirstName);
                break;
            case "LastName":
                if (_currentSortDirection)
                    hasSortedList = Persons.OrderBy(person => person.LastName);
                else
                    hasSortedList = Persons.OrderByDescending(person => person.LastName);
                Persons.OrderBy(person => person.LastName);
                break;
            case "ThirdName":
                if (_currentSortDirection)
                    hasSortedList = Persons.OrderBy(person => person.ThirdName);
                else
                    hasSortedList = Persons.OrderByDescending(person => person.ThirdName);
                break;
        }

        if (hasSortedList != null)
            Persons = new ObservableCollection<IPerson>(hasSortedList);
    }

    private bool CanEditPerson
    {
        get
        {
            return SelectedIndex >= 0 && SelectedIndex <= _persons?.Count;
        }
    }

    private bool CanRemovePerson
    {
        get
        {
            return SelectedIndex >= 0 && SelectedIndex <= _persons?.Count;
        }
    }

    private bool CanAddPerson
    {
        get
        {
            return RoleAccess.AddAvailable();
        }
    }

    private void EditPerson()
    {
        ShowPersonEditWnd(_persons[SelectedIndex]);
    }

    private void AddPerson()
    {
        ShowPersonEditWnd(new ExtendedPersonInStorage());
    }

    private void RemovePerson()
    {
        _model.RemovePerson(_persons[SelectedIndex]);
    }

    private void ShowPersonEditWnd(IPerson person)
    {
        wndEditPerson = new WndEditPerson();
        (wndEditPerson.DataContext as EditPersonVM).Person = person;
        (wndEditPerson.DataContext as EditPersonVM).OnPersonHasEdited += OnPersonChange;
        (wndEditPerson.DataContext as EditPersonVM).OnPersonEditCanceled += OnPersonEditCancel;
        wndEditPerson.Show();
    }

    private void OnPersonEditCancel()
    {
        wndEditPerson.Close();
    }

    private void OnPersonChange(IPerson person)
    {
        _model.SavePerson(person);
        wndEditPerson.Close();
    }

    private void OnPropertyChanged(string prop)
    {
        if(PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

}
