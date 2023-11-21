
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependenceImplementation : IDependence
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
        return DataSource.Dependences.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Dependence> ReadAll(Func<Dependence, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Dependences
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependences
               select item;
    }

    public Dependence? Read(Func<Dependence, bool>? filter = null) => DataSource.Dependences.FirstOrDefault(item => filter!(item));

    public void Update(Dependence item)
    {
        Dependence? dependenceTask = Read(item.Id);
        if (dependenceTask == null) //If not find an dependence to update
            throw new Exception($"A Dependence With Number= {item.Id} Does Not Exist");

        DataSource.Dependences.Remove(dependenceTask);
        DataSource.Dependences.Add(item);
    }
}
