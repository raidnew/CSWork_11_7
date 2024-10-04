using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Interface;

namespace Task.Models
{

    public enum ChangeType { Modify, Create }

    internal class ExtendedPersonInStorage : PersonInStorage
    {
        protected DateTime _timeModified;
        protected List<String> _changedFields;
        protected ChangeType _changeType;
        protected Role _changeRole;

#pragma warning disable CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        virtual public string FirstName { 
            get { return _firstName; } 
            set 
            { 
                _firstName = value;
                Modified("FirstName");
            }
        }
        virtual public string LastName { 
            get { return _lastName; } 
            set 
            { 
                _lastName = value;
                Modified("LastName");
            }
        }
        virtual public string ThirdName {
            get { return _thirdName; } 
            set 
            { 
                _thirdName = value;
                Modified("ThirdName");
            }
        }
        virtual public string PhoneNumber { 
            get { return _phoneNumber; } 
            set 
            { 
                _phoneNumber = value;
                Modified("PhoneNumber");
            }
        }
        virtual public string PassportSeries {
            get { return _passportSeries; } 
            set 
            { 
                _passportSeries = value;
                Modified("PassportSeries");
            }
        }
        virtual public string PassportNumber { 
            get { return _pasportNumber; } 
            set 
            { 
                _pasportNumber = value;
                Modified("PassportNumber");
            }
        }
#pragma warning restore CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения

        public DateTime TimeModified{ get { return _timeModified; } set { _timeModified = value; } }
        public List<String> ChangedFields { get { return _changedFields ?? new List<string>(); } set { _changedFields = value; } }
        public ChangeType ChangeType { get { return _changeType; } set { _changeType = value; } }
        public Role ChangeRole { get { return _changeRole; } set { _changeRole = value; } }

        public ExtendedPersonInStorage() : base() { }

        public ExtendedPersonInStorage(int id) : base(id) { }

        public ExtendedPersonInStorage(int id, string firstName, string lastName, string thirdName) : base(id, firstName, lastName, thirdName) { }

        public ExtendedPersonInStorage(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSeries, string passportNumber) :
            base(id, firstName, lastName, thirdName, phoneNumber, passportSeries, passportNumber)
        { }

        private void Modified(string field)
        {
            TimeModified = DateTime.Now;
            ChangeRole = CurrentRole.Current;
            ChangedFields.Add(field);
        }

        virtual public void Clone(IPerson person)
        {
            if (FirstName != person.FirstName) FirstName = person.FirstName;
            if (LastName != person.LastName) LastName = person.LastName;
            if (ThirdName != person.ThirdName) ThirdName = person.ThirdName;
            if (PhoneNumber != person.PhoneNumber) PhoneNumber = person.PhoneNumber;
            if (PassportSeries != person.PassportSeries) PassportSeries = person.PassportSeries;
            if (PassportNumber != person.PassportNumber) PassportNumber = person.PassportNumber;
        }

    }
}
