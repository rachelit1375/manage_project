

namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;

internal class TaskImplementation : ITask
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
        //Task? removeTask = Read(id);//brings the task
        //if (removeTask == null) 
        //    throw new Exception($"A Task With Number= {id} Does Not Exist");

        //if (DataSource.Dependences.Find(x => x.DependentOnTask == id) != null) //If a task dependency is found
        //    throw new Exception("The Task Cannot Be Deleted");
        //DataSource.Tasks.Remove(removeTask);

        //Dependence? tempDependence = DataSource.Dependences.Find(x => x.DependentTask == id);
        //if (tempDependence != null)
        //    DataSource.Dependences.Remove(tempDependence);//Deleting the task
        throw new DalDeletionImpossible("You cannot delete task");
    }

    public Task? Read(int id)//Finding the assignment by ID
    {
        return DataSource.Tasks.FirstOrDefault(x => x.Id == id);
    }

    public Task? Read(Func<Task, bool>? filter)
    {
        return DataSource.Tasks.FirstOrDefault(item => filter!(item));
    }

    public IEnumerable<Task> ReadAll(Func<Task,bool>? filter)
    {
        //if(filter !=null)
        //{
            return from item in DataSource.Tasks
                   where filter!(item)
                   select item;
        //}
        //return from item in DataSource.Tasks
        //       select item;
    }

    public void Update(Task item)
    {
        Task? removeTask = Read(item.Id);//If the task number is not found
        if (removeTask == null)
            throw new DalDoesNotExistException($"A Task With Number= {item.Id} Does Not Exist");

        DataSource.Tasks.Remove(removeTask);
        DataSource.Tasks.Add(item);
    }

    public void Reset()
    {
        DataSource.Tasks.Clear();
    }
}
