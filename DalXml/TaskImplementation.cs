namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = Config.NextTaskId;//Brings the running  number
        Task copyTask = item with { Id = id };//Replaces the id with the new one
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>("task");//טעינה מקובץXML לרשימה ו
        list.Add(copyTask);//הוספת הפריט לרשימה
        XMLTools.SaveListToXMLSerializer<Task>(list, "task");
        return id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible("You cannot delete task");
    }

    public Task? Read(int id)
    {//Returns the requested task
        return XMLTools.LoadListFromXMLSerializer<Task>("task").FirstOrDefault(x => x.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {//Returns the requested task after filtering
        return XMLTools.LoadListFromXMLSerializer<Task>("task").FirstOrDefault(item => filter!(item)); ;
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter)
    {
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>("task")!;// The task list

        if (filter != null)
        {
            return from item in list
                   where filter!(item)
                   select item;
        }
        return from item in list
               select item;
    }

    public void Update(Task item)
    {

        Task? removeTask = Read(item.Id);
        if (removeTask == null)//If the task number is not found
            throw new DalDoesNotExistException($"A Task With Number= {item.Id} Does Not Exist");
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>("task")!;// The task list
        list.Remove(removeTask);//Deletes the task before the update
        list.Add(item);//Adds the updated task
        XMLTools.SaveListToXMLSerializer<Task>(list, "task");//Saves the updated list
    }

    public void Reset()
    {
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>("task")!;//The task list
        list.Clear();//Deletes everything
        XMLTools.SaveListToXMLSerializer<Task>(list, "task");//Saves the Clear list

        XElement xConfig = XMLTools.LoadListFromXMLElement("data-config");
        xConfig.Element("NextTaskId")!.SetValue(1);//Initializes the running number
        XMLTools.SaveListToXMLElement(xConfig, "data-config");
    }
}
