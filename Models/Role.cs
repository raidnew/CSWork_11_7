namespace Task.Models;
public enum Role { Manager, Consultant }
public enum AccessMode { NotAvailable, ReadOnly, ReadWrite }

public class CurrentRole
{
    public static Role Current { get; set; }
}

public class RoleAccess
{
    public static bool AddAvailable()
    {
        return CurrentRole.Current == Role.Manager;
    }

    public static AccessMode FirstName()
    {
        switch (CurrentRole.Current)
        {
            case Role.Manager:
                return AccessMode.ReadWrite;
            case Role.Consultant:
            default:
                return AccessMode.ReadOnly;
        }
    }
    public static AccessMode LastName()
    {
        switch (CurrentRole.Current)
        {
            case Role.Manager:
                return AccessMode.ReadWrite;
            case Role.Consultant:
            default:
                return AccessMode.ReadOnly;
        }
    }
    public static AccessMode ThirdName()
    {
        switch (CurrentRole.Current)
        {
            case Role.Manager:
                return AccessMode.ReadWrite;
            case Role.Consultant:
            default:
                return AccessMode.ReadOnly;
        }
    }

    public static AccessMode PhoneNumber()
    {
        return AccessMode.ReadWrite;
    }
    public static AccessMode PassportSeries()
    {
        switch (CurrentRole.Current)
        {
            case Role.Manager:
                return AccessMode.ReadWrite;
            case Role.Consultant:
            default:
                return AccessMode.ReadOnly;
        }
    }
    public static AccessMode PassportNumber()
    {
        switch (CurrentRole.Current)
        {
            case Role.Manager:
                return AccessMode.ReadWrite;
            case Role.Consultant:
            default:
                return AccessMode.ReadOnly;
        }
    }

}