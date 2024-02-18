
using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        BO.Tools.CheckName(item.Alias);


        int? idEngineer = null;
        if (item.Engineer != null)
            idEngineer = item.Engineer.Id;

        int newID = _dal.Task.Create(new DO.Task(0, item.Description, item.Alias, false, DateTime.Today, item.Start, item.ScheduledDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, idEngineer, (DO.EngineerExperience)item.ComplexityLevel!, null));//maybe true in the milestone
        if (item.DependenceList != null)
        {
            foreach (var singleDependence in item.DependenceList)//building the dependencies
            {
                    _dal.Dependence.Create(new Dependence(0, singleDependence.Id, newID));
            }
        }

        return newID;
    }

    public void Delete(int id)
    {
        //try
        //{
        //    _dal.Task.Delete(id);
        //}
        //catch (DO.DalDeletionImpossible ex)
        //{
        //    throw new BO.BlDeletionImpossible($"task {id} has dependencies, cannot be deleted",ex);
        //}
        //catch (DO.DalDoesNotExistException ex)
        //{
        //    throw new BlDoesNotExistException($"task id {id} doesn't exist", ex);
        //}
        throw new BO.BlDeletionImpossible($"task {id} has dependencies, cannot be deleted");
    }

    public MilestoneInTask? GetMilestone(int id)
    {
        var allDependences = _dal.Dependence.ReadAll();
        MilestoneInTask? milestoneTask = (from dependence in allDependences
                                          let taskDepend = _dal.Task.Read(dependence.DependentTask)
                                          where dependence.DependentOnTask == id && taskDepend!.MileStone
                                          select new BO.MilestoneInTask()
                                          {
                                              Id = dependence.DependentTask,
                                              Alias = taskDepend!.Alias
                                          }).FirstOrDefault();//Goes through all dependencies and checks if this is a milestone task of this task, if so - creates a milestone
        return milestoneTask;
    }

    public List<TaskInList> GetDependentTasks(int id)
    {
        var dependences = _dal.Dependence.ReadAll();//Accepts all dependences
        var dependentTasks = (from dependence in dependences
                              where dependence.DependentTask == id
                              let taskDependOn = _dal.Task.Read(dependence.DependentOnTask)
                              select new BO.TaskInList()
                              {
                                  Id = dependence.Id,
                                  Description = taskDependOn?.Description,
                                  Alias = taskDependOn?.Alias,
                                  Status = GetStatus(taskDependOn)
                              });//Goes through the dependencies and checks if this is a task that depends on it, if so builds a taskinlist
        return dependentTasks.ToList<TaskInList>();
    }
    public static Status GetStatus(DO.Task doTask)//This operation calculates the status of the task
    {
        if (doTask.Complete != null && doTask.Complete <= DateTime.Now)
            return Status.Complete;

        if (doTask.Start == null || doTask.ScheduledDate == null)
            return Status.Unscheduled;

        if (doTask.Start > DateTime.Now)
            return Status.Scheduled;

        if (DateTime.Now <= doTask.Deadline)
            return Status.OnTrack;

        return Status.InJeopardy;
    }
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Task {id} doesn't exists");
        BO.EngineerInTask? engineerInTask = null;

        if (doTask?.EngineerId != null)
            engineerInTask = new BO.EngineerInTask() { Id = (int)doTask.EngineerId, Name = _dal.Engineer.Read((int)doTask.EngineerId)?.Name };//building of the engineer

        return new BO.Task()//return a new obj of BO.Task
        {
            Id = id,
            Description = doTask?.Description,
            Alias = doTask?.Alias,
            MileStone = GetMilestone(id),
            StatusTask = GetStatus(doTask!),
            DependenceList = GetDependentTasks(id),
            CreateAt = doTask!.CreateAt,
            Start = doTask.Start,
            ScheduledDate = doTask?.ScheduledDate,
            Deadline = doTask!.Deadline,
            Complete = doTask.Complete,
            Deliverables = doTask?.Deliverables,
            Remarks = doTask?.Remarks,
            Engineer = engineerInTask,
            ComplexityLevel = (BO.EngineerExperience?)doTask!.ComplexityLevel,
        };
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        var doAllTask = _dal.Task.ReadAll();
        var a = from doTask in doAllTask
                let taskEngineer = _dal.Engineer.ReadAll(engineer => doTask.EngineerId == engineer.Id).FirstOrDefault()
                select new BO.Task()//return a new obj of BO.Task
                {
                    Id = doTask.Id,
                    Description = doTask?.Description,
                    Alias = doTask?.Alias,
                    MileStone = GetMilestone(doTask.Id),
                    StatusTask = GetStatus(doTask!),
                    DependenceList = GetDependentTasks(doTask.Id),
                    CreateAt = doTask!.CreateAt,
                    Start = doTask.Start,
                    ScheduledDate = doTask?.ScheduledDate,
                    Deadline = doTask!.Deadline,
                    Complete = doTask.Complete,
                    Deliverables = doTask?.Deliverables,
                    Remarks = doTask?.Remarks,
                    Engineer = taskEngineer == null ? null : new EngineerInTask()
                    {
                        Id = taskEngineer.Id,
                        Name = taskEngineer?.Name
                    },
                    ComplexityLevel = (BO.EngineerExperience?)doTask!.ComplexityLevel,
                };

        if (filter == null)
            return a;
        return a.Where(filter);
    }

    public void Update(BO.Task item)
    {
        //verification Checking
        Tools.CheckId(item.Id);
        Tools.CheckName(item.Alias);
        DO.Task doTask = new DO.Task(item.Id, item.Description, item.Alias, false, item.CreateAt!, item.Start, item.ScheduledDate, item.Deadline!, item.Complete, item.Deliverables, item.Remarks, item.Engineer?.Id, (DO.EngineerExperience?)item.ComplexityLevel, new TimeSpan());
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistException($"Task {item.Id} doesn't exists", ex);
        }

        if (item.DependenceList != null)
        {
            foreach (var singleDependence in item.DependenceList)
            {
                bool isExist = false;
                foreach (var dependence in _dal.Dependence.ReadAll())
                {
                    if (dependence!.Id == singleDependence.Id)
                        isExist = true;
                }
                if (!isExist)
                    _dal.Dependence.Create(new Dependence(0, singleDependence.Id, item.Id));
            }
        }
    }
}
