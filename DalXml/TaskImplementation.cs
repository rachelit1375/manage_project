namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = Config.NextTaskId;//Brings the running number
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
    {
        return XMLTools.LoadListFromXMLSerializer<Task>("task").FirstOrDefault(x => x.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        return XMLTools.LoadListFromXMLSerializer<Task>("task").FirstOrDefault(item => filter!(item)); ;
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>("task")!;

        if (filter != null)
        {
            return from item in list
                   where filter(item)
                   select item;
        }
        return from item in list
               select item;
    }

    public void Update(Task item)
    {

        Task? removeTask = Read(item.Id);//If the task number is not found
        if (removeTask == null)
            throw new DalDoesNotExistException($"A Task With Number= {item.Id} Does Not Exist");
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>("task")!;
        list.Remove(removeTask);
        list.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(list, "task");
    }
}
