

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;//Brings the running number
        Task copyTask = item with { Id = id };//Replaces the id with the new one
        DataSource.Tasks.Add(copyTask);
        return id;
    }

    public void Delete(int id)
    {
        Task? removeTask = Read(id);
        if (removeTask == null) 
            throw new Exception($"A Task With Number= {id} Does Not Exist");

        if (DataSource.Dependences.Find(x => x.DependentOnTask == id) != null) 
            throw new Exception("The Task Cannot Be Deleted");
        DataSource.Tasks.Remove(removeTask);

        Dependence? tempDependence = DataSource.Dependences.Find(x => x.DependentTask == id);
        if (tempDependence != null)
            DataSource.Dependences.Remove(tempDependence);
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(x => x.Id == id);
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task? removeTask = Read(item.Id);
        if (removeTask == null)
            throw new Exception($"A Task With Number= {item.Id} Does Not Exist");

        DataSource.Tasks.Remove(removeTask);
        DataSource.Tasks.Add(item);
    }
}
