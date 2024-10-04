using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Task.Interface;
using Task.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Task.Models;

public class JsonPersonsStorage<T> : IPersonStorage, INotifyPropertyChanged
{
    public bool IsReady { get; private set; }

    protected readonly List<IPerson> _persons;
    protected int _lastPersonId = 0;

    private FileStorage _fileStorage;

    public JsonPersonsStorage()
    {
        IsReady = false;
        _persons = new List<IPerson>();
    }

    public override ObservableCollection<IPerson> Persons 
    { 
        get 
        { 
            return new ObservableCollection<IPerson>(_persons); 
        } 
    }

    public override void Init()
    {
        _fileStorage = new FileStorage("jsonstorage.json");
        _fileStorage.OnFileLoaded += OnDataLoad;
        _fileStorage.LoadFile();
    }

    public override void SavePerson(IPerson person)
    {
        if (person.ID >= 0)
            ModifyPerson(person);
        else if (RoleAccess.AddAvailable())
            CreateNewPerson(person);
        OnPropertyChanged(nameof(Persons));
    }

    public override void RemovePerson(IPerson person)
    {
        _persons.Remove(person);
        OnPropertyChanged(nameof(Persons));
    }

    public override IPerson? GetPersonByID(int ID)
    {
        return _persons.Single(person => person.ID == ID);
    }

    public override IPerson GetPersonByIndex(int index)
    {
        return CreatePerson(_persons[index]);
    }

    public override void Save() 
    {
        List<string> serializedPersons = new List<string>();
        foreach (IPerson person in _persons)
        {
            serializedPersons.Add(Serialize((T)person));
        }
        _fileStorage.SaveFile(serializedPersons);
    }

    virtual protected void ModifyPerson(IPerson person)
    {
        PersonInStorage foundPerson = GetPersonByID(person.ID) as PersonInStorage;
        if (foundPerson == null) return;
        foundPerson.Clone(person);
    }

    virtual protected void CreateNewPerson(IPerson personData)
    {
        PersonInStorage newPerson = new PersonInStorage(_lastPersonId++,
            personData.FirstName,
            personData.LastName,
            personData.ThirdName,
            personData.PhoneNumber,
            personData.PassportSeries,
            personData.PassportNumber);
        _persons.Add(newPerson);
    }

    virtual protected IPerson CreatePerson(IPerson person)
    {
        IPerson retPerson = null;
        switch (CurrentRole.Current)
        {
            case Role.Manager:
                retPerson = new PersonManager(person.ID, person.FirstName, person.LastName, person.ThirdName, person.PhoneNumber, person.PassportSeries, person.PassportNumber);
                break;
            case Role.Consultant:
            default:
                retPerson = new PersonConsultant(person.ID, person.FirstName, person.LastName, person.ThirdName, person.PhoneNumber, person.PassportSeries, person.PassportNumber);
                break;
        }
        return retPerson;
    }

    virtual protected string Serialize(T person)
    {
        return JsonSerializer.Serialize(person);
    }

    virtual protected T? Deserialize(string jsonString)
    {
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    private void InitLastID()
    {
        if (_persons != null && _persons.Count > 0)
            _lastPersonId = _persons.Last().ID + 1;
    }

    private void OnDataLoad(List<string> serializedPersons)
    {
        foreach (string jsonPerson in serializedPersons)
        {
            IPerson person = (IPerson)Deserialize(jsonPerson);
            if(person != null) _persons.Add(person);
        }
        InitLastID();
        IsReady = true;
        OnPropertyChanged(nameof(Persons));
    }
}