
using BlApi;
using BO;


namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer item)
    {
        //verification Checking
        Tools.CheckId(item.Id);
        Tools.CheckName(item.Name);
        Tools.CheckCost(item.Cost);
        Tools.CheckEmail(item.Email);

        DO.Engineer doEngineer = new DO.Engineer(item.Id, item.Name!, item.Email!, (DO.EngineerExperience)item.Level!, item.Cost, item.Active);//Creating a new DAL object
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);//Saving the engineer
            return idEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        //    try
        //    {
        //        _dal.Engineer.Delete(id);
        //    }
        //    catch (DO.DalDoesNotExistException exception)
        //    {
        //        throw new BO.BlDoesNotExistException($"An object of type Engineer with ID {id} does not exist", exception);
        //    }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)//Checking whether there is such an engineer
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} doesn't exists");
        var tasks = _dal.Task.ReadAll();//Accepts all tasks
        var foundTask = (from task in tasks
                         where task.EngineerId == id && task.Start is not null && task.Complete is null
                         select new { task.Id, task.Alias }).FirstOrDefault();//Goes through all tasks and looks for the current engineer's current task
        BO.TaskInEngineer? taskInEngineer = null;
        if (foundTask != null) //If found -creates a TaskInEngineer object
            taskInEngineer = new BO.TaskInEngineer() { Id = foundTask.Id, Alias = foundTask.Alias };
        return new BO.Engineer()//  return a new obj of BO.Engineer
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience?)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = taskInEngineer
        };
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        var doAllEngineer = _dal.Engineer.ReadAll();// takes all the engineerd from the dal
        var boAllEngineers = from doEngineer in doAllEngineer// Converts all engineers to BO objects
                             let engineerTask = _dal.Task.ReadAll(task => task.EngineerId == doEngineer.Id).FirstOrDefault()
        select new BO.Engineer() 
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience?)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = engineerTask == null? null : new TaskInEngineer()
            {
                Id = engineerTask.Id,
                Alias = engineerTask.Alias
            }
        };

        if (filter == null)
            return boAllEngineers;
        return boAllEngineers.Where(filter);//If there is a filter - runs a wherer query
    }

    public void Update(BO.Engineer item)
    {
        //verification Checking
        Tools.CheckId(item.Id);
        Tools.CheckName(item.Name);
        Tools.CheckCost(item.Cost);
        Tools.CheckEmail(item.Email);
        DO.Engineer doEngineer = new DO.Engineer(item.Id, item.Name!, item.Email!, (DO.EngineerExperience?)item.Level, item.Cost, item.Active);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={item.Id} doesn't exists", ex);
        }
    }
}
