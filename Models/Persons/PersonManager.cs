using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Common;

namespace Task.Models;

public class PersonManager : PersonBase
{
    public PersonManager() : base() { }

    public PersonManager(int id) : base(id) { }

    public PersonManager(int id, string firstName, string lastName, string thirdName) : base(id, firstName, lastName, thirdName) { }

    public PersonManager(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSeries, string passportNumber) :
        base(id, firstName, lastName, thirdName, phoneNumber, passportSeries, passportNumber)
    { }

    ~PersonManager() { }
}
