
using System.Xml.Linq;

namespace BO;

public static class Tools
{
    public static string ToStringProperty(this Object ob)
    {
        var prop = ob.GetType().GetProperties();
        string str = "";
        foreach (var property in prop)
        {
            str += property.Name + ":" + property.GetValue(ob);
        }
        return str;
    }
    //verification Checking
    public static void CheckId(int id)
    {
        if (id <= 0)//If the ID number is negative or 0
            throw new BO.BlPropertyException($"The identity number is invalid");//
    }
    public static void CheckName(string? name)
    {
        if (name == null)//If this is an empty string
            throw new BO.BlPropertyException($"You need to enter a value");//
    }
    public static void CheckCost(double? cost)
    {
        if ( cost < 0)//<=If it is a negative number
            throw new BO.BlPropertyException($"The amount to be paid is invalid");//
    }
    public static void CheckEmail(string? email) 
    {
        if (email == null)//If this is an empty string
            throw new BO.BlPropertyException($"You need to enter an email");//

        if (!email!.Contains("@"))//If this is a correct email address (contain @)
            throw new BO.BlPropertyException($"The email address is invalid");//
    }
}

