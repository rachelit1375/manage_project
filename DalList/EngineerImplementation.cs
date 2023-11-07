

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.Id) != null)
            throw new Exception($"An Engineers With Id= {item.Id} Already Exist");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? removeEngineer = Read(id);
        if (removeEngineer == null)
            throw new Exception($"An Engineers With Id= {id} Does Not Exist");

        if (DataSource.Tasks.Find(x => x.EngineerId == id) != null)
            throw new Exception("This Engineer Cannot Be Deleted");
        DataSource.Engineers.Remove(removeEngineer);
    }

    public Engineer? Read(int id)
    {
      return DataSource.Engineers.Find(x=> x.Id == id);
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? removeEngineer = Read(item.Id);
        if (removeEngineer == null)
            throw new Exception($"An Engineers With Id= {item.Id} Does Not Exist");

        DataSource.Engineers.Remove(removeEngineer);
        DataSource.Engineers.Add(item);
    }
}
