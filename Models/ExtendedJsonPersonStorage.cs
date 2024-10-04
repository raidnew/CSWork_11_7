using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Task.Interface;

namespace Task.Models
{
    internal class ExtendedJsonPersonStorage<T> : JsonPersonsStorage<T>, INotifyPropertyChanged
    {
        public ExtendedJsonPersonStorage() : base() {}

        override protected void ModifyPerson(IPerson person)
        {
            ExtendedPersonInStorage? foundPerson = GetPersonByID(person.ID) as ExtendedPersonInStorage;
            if (foundPerson == null) return;
            foundPerson.Clone(person);
            foundPerson.ChangeType = ChangeType.Modify;
        }

        override protected void CreateNewPerson(IPerson personData)
        {
            ExtendedPersonInStorage newPerson = new ExtendedPersonInStorage(_lastPersonId++,
                personData.FirstName,
                personData.LastName,
                personData.ThirdName,
                personData.PhoneNumber,
                personData.PassportSeries,
                personData.PassportNumber);
            newPerson.ChangeType = ChangeType.Create;
            _persons.Add(newPerson);
        }

        override protected IPerson CreatePerson(IPerson person)
        {
            IPerson? retPerson = null;
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

        override protected string Serialize(T person)
        {
            return JsonSerializer.Serialize(person);
        }

        override protected T? Deserialize(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }

    }
}
