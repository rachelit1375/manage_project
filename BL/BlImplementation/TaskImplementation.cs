﻿
using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        BO.Tools.CheckId(item.Id);// validation
        BO.Tools.CheckName(item.Alias);
        var dependencesTask = (from dependence in item.DependenceList
                               select _dal.Dependence.Create(new DO.Dependence(0, dependence.Id, item.Id)));//building the dependencies

        int? idEngineer = null;
        if (item.Engineer != null)
            idEngineer = item.Engineer.Id;

        _dal.Task.Create(new DO.Task(0, item.Description, item.Alias, false, DateTime.Today, item.Start, item.ScheduledDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, idEngineer, (DO.EngineerExperience)item.ComplexityLevel!, null));//maybe true in the milestone
        return item.Id;
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
                              where dependence.DependentOnTask == id && _dal.Task.Read(dependence.DependentTask)!.MileStone
                              select new BO.MilestoneInTask()
                              {
                                  Id = dependence.DependentTask,
                                  Alias = _dal.Task.Read(dependence.DependentTask)!.Alias
                              }).FirstOrDefault();
        return milestoneTask;
    }

    public List<TaskInList> GetDependentTasks(int id)
    {
        var dependences = _dal.Dependence.ReadAll();//Accepts all dependences
        var dependentTasks = (from dependence in dependences
                              where dependence.DependentTask == id
                              select new BO.TaskInList()
                              {
                                  Id = dependence.Id,
                                  Description = _dal.Task.Read(dependence.DependentOnTask)?.Description,
                                  Alias = _dal.Task.Read(dependence.DependentOnTask)?.Alias,
                                  Status = GetStatus(_dal.Task.Read(dependence.DependentOnTask)!)
                              });
        return (List<TaskInList>)dependentTasks;
    }
    public Status GetStatus(DO.Task doTask)
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
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)//Checking whether there is such an engineer
            throw new BO.BlDoesNotExistException($"Task {id} doesn't exists");

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
            ComplexityLevel = (EngineerExperience?)doTask!.ComplexityLevel,
        };
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        var doAllTask = _dal.Task.ReadAll();
        if(filter != null)
        {
            return from doTask in doAllTask
                   where filter(Read(doTask.Id)!)// !  & twice Read...
                   select Read(doTask.Id);
        }
        return from doTask in doAllTask
               select Read(doTask.Id);
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
    }
}