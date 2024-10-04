using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Interface;

namespace Task.Models
{
    public class PersonInStorage : PersonBase
    {

        public int ID { get { return _id; } set { _id = value; } }

        public PersonInStorage() : base() { }

        public PersonInStorage(int id) : base(id) { }

        public PersonInStorage(int id, string firstName, string lastName, string thirdName) : base(id, firstName, lastName, thirdName) { }

        public PersonInStorage(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSeries, string passportNumber) :
            base(id, firstName, lastName, thirdName, phoneNumber, passportSeries, passportNumber)
        { }

        virtual public void Clone(IPerson person)
        {
            _firstName = person.FirstName;
            _lastName = person.LastName;
            _thirdName = person.ThirdName;
            _phoneNumber = person.PhoneNumber;
            _passportSeries = person.PassportSeries;
            _pasportNumber = person.PassportNumber;
        }

    }
}
