
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
}
