

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.Id) != null)//If there is such an engineer already
            throw new DalAlreadyExistsException($"An Engineers With Id= {item.Id} Already Exist");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        //Engineer? removeEngineer = Read(id);
        //if (removeEngineer == null)//If it didn't find the ID
        //    throw new Exception($"An Engineers With Id= {id} Does Not Exist");

        //if (DataSource.Tasks.Find(x => x.EngineerId == id) != null)//If there is a task related to this engineer
        //    throw new Exception("This Engineer Cannot Be Deleted");
        //DataSource.Engineers.Remove(removeEngineer);     
        throw new DalDeletionImpossible("You cannot delete engineer");
    }

    public Engineer? Read(int id)
    {
      return DataSource.Engineers.FirstOrDefault(x=> x.Id == id);//Returns the engineer
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }

    public Engineer? Read(Func<Engineer, bool>? filter = null) => DataSource.Engineers.FirstOrDefault(item => filter!(item));


    public void Update(Engineer item)
    {
        Engineer? removeEngineer = Read(item.Id);
        if (removeEngineer == null)//If not find an engineer to update
            throw new DalDoesNotExistException($"An Engineers With Id= {item.Id} Does Not Exist");

        DataSource.Engineers.Remove(removeEngineer);
        DataSource.Engineers.Add(item);
    }
}
