
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        int id = DataSource.Config.NextDependenceId;//The running number
        Dependence copyDependence = item with { Id = id };//Replaces the id with the new one
        DataSource.Dependences.Add(copyDependence);
        return id;
    }

    public void Delete(int id)//Deletion of dependencies
    {
        Dependence? removeDependence = Read(id);
        if (removeDependence == null)
            throw new Exception($"A Dependence With Number= {id} Does Not Exist");

        DataSource.Dependences.Remove(removeDependence);
    }

    public Dependence? Read(int id)//Display of dependencies is requested
    {
        return DataSource.Dependences.Find(x => x.Id == id);
    }

    public List<Dependence> ReadAll()//Showing all the dependencies that exist
    {
        return new List<Dependence>(DataSource.Dependences);
    }

    public void Update(Dependence item)
    {
        Dependence? dependenceTask = Read(item.Id);
        if (dependenceTask == null) //If not find an dependence to update
            throw new Exception($"A Dependence With Number= {item.Id} Does Not Exist");

        DataSource.Dependences.Remove(dependenceTask);
        DataSource.Dependences.Add(item);
    }
}
