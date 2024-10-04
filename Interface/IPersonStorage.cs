using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task.Interface;


public abstract class IPersonStorage: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public virtual ObservableCollection<IPerson> Persons { get; }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public abstract void Init();
    public abstract IPerson? GetPersonByID(int ID);
    public abstract IPerson GetPersonByIndex(int index);
    public abstract void Save();
    public abstract void SavePerson(IPerson person);
    public abstract void RemovePerson(IPerson person);

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}