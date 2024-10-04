using Task.Interface;

namespace Task.Models;

public abstract class PersonBase : IPerson
{
    protected int _id = -1;
    protected string _firstName = "";
    protected string _lastName = "";
    protected string _thirdName = "";
    protected string _phoneNumber = "";
    protected string _passportSeries = "";
    protected string _pasportNumber = "";

    virtual public int ID { get { return _id; } set { } }
    virtual public string FirstName { get { return _firstName; } set { _firstName = value; } }
    virtual public string LastName { get { return _lastName; } set { _lastName = value; } }
    virtual public string ThirdName { get { return _thirdName; } set { _thirdName = value; } }
    virtual public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
    virtual public string PassportSeries { get { return _passportSeries; } set { _passportSeries = value; } }
    virtual public string PassportNumber { get { return _pasportNumber; } set { _pasportNumber = value; } }

    public PersonBase() {}

    public PersonBase(int id)
    {
        _id = id;
    }

    public PersonBase(int id, string firstName, string lastName, string thirdName) : this(id)
    {
        FirstName = firstName ?? "";
        LastName = lastName ?? "";
        ThirdName = thirdName ?? "";
    }

    public PersonBase(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSeries, string passportNumber) : this(id, firstName, lastName, thirdName)
    {
        PhoneNumber = phoneNumber ?? "";
        PassportSeries = passportSeries ?? "";
        PassportNumber = passportNumber ?? "";
    }

    ~PersonBase() { }
}
