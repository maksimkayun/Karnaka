using Karnaka.Data.Models;

namespace Karnaka.Data.Helpers;

public class CustomDataLocation : Location
{
    public string GetFullName
    {
        get
        {
            return $"{Island}, {City}, {Name}";
        }
    }

    public static string GetPartName(string fullName, int i)
    {
        return fullName.Split(", ")[i];
    }
}