namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        int id = Config.NextTaskId;//Brings the running number
        Dependence copyDependence = item with { Id = id };//Replaces the id with the new one
        List<Dependence> list = XMLTools.LoadListFromXMLSerializer<Dependence>("dependence");//טעינה מקובץXML לרשימה ו
        list.Add(copyDependence);//הוספת הפריט לרשימה
        XMLTools.SaveListToXMLSerializer<Dependence>(list, "dependence");
        return id;
    }

    public void Delete(int id)
    {
        Dependence? removeDependence = Read(id);
        if (removeDependence == null)
            throw new DalDoesNotExistException($"A Dependence With Number= {id} Does Not Exist");

        List<Dependence> list = XMLTools.LoadListFromXMLSerializer<Dependence>("dependence");//טעינה מקובץXML לרשימה ו
        list.Remove(removeDependence);//הסרת הפריט לרשימה
        XMLTools.SaveListToXMLSerializer<Dependence>(list, "dependence");

    }

    public Dependence? Read(int id)
    {
        return XMLTools.LoadListFromXMLSerializer<Dependence>("dependence").FirstOrDefault(x => x.Id == id);
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        return XMLTools.LoadListFromXMLSerializer<Dependence>("dependence").FirstOrDefault(item => filter!(item));

    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        List<Dependence> list = XMLTools.LoadListFromXMLSerializer<Dependence>("dependence")!;//

        if (filter != null)
        {
            return from item in list
                   where filter(item)
                   select item;
        }
        return from item in list
               select item;
    }

    public void Update(Dependence item)
    {
        Dependence? dependenceTask = Read(item.Id);
        if (dependenceTask == null) //If not find an dependence to update
            throw new DalDoesNotExistException($"A Dependence With Number= {item.Id} Does Not Exist");
        List<Dependence> list = XMLTools.LoadListFromXMLSerializer<Dependence>("dependence")!;//
        list.Remove(dependenceTask);
        list.Add(item);
        XMLTools.SaveListToXMLSerializer<Dependence>(list, "dependence");
    }
}
