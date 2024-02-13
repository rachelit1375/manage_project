namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer copyEngineer = item;//Replaces the id with the new one
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer");//טעינה מקובץXML לרשימה ו
        list.Add(copyEngineer);//הוספת הפריט לרשימה
        XMLTools.SaveListToXMLSerializer<Engineer>(list, "engineer");
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible("You cannot delete engineer");
    }

    public Engineer? Read(int id)
    {
        return XMLTools.LoadListFromXMLSerializer<Engineer>("engineer").FirstOrDefault(x => x.Id == id);//Returns the requested engineer

    }

    public Engineer? Read(Func<Engineer, bool>? filter)
    {
        return XMLTools.LoadListFromXMLSerializer<Engineer>("engineer").FirstOrDefault(item => filter!(item)); //Returns the requested engineer if the filter conditions are met

    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer")!;//List of engineers

        if (filter != null)
        {
            return from item in list
               where filter!(item)
               select item;
        }
        return from item in list
               select item;
    }

    public void Update(Engineer item)
    {
        Engineer? removeEngineer = Read(item.Id);
        if (removeEngineer == null)//If the task number is not found
            throw new DalDoesNotExistException($"A Task With Number= {item.Id} Does Not Exist");
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer")!;
        list.Remove(removeEngineer);//Deleted from the list
        list.Add(item);//adds to the list
        XMLTools.SaveListToXMLSerializer<Engineer>(list, "engineer");
    }

    public void Reset()
    {
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>("engineer")!;
        list.Clear();
        XMLTools.SaveListToXMLSerializer<Engineer>(list, "engineer");
    }
}
