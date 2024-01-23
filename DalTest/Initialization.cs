
namespace DalTest;
using DalApi;
using DO;

/// <summary>
/// 
/// </summary>
public static class Initialization
{
    // private static ITask? s_dalTask = null;
    // private static IDependence? s_dalDependence = null;
    //private static IEngineer? s_dalEngineer = null;
    private static IDal? s_dal;
    private static readonly Random s_rand = new();
    //public static void Do(IDal dal) //stage 2
    public static void Do()//stage 4
    {
        // s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //  s_dalDependence = dalDependence ?? throw new NullReferenceException("DAL can not be null!");
        // s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");

        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        s_dal = DalApi.Factory.Get; //stage 4      
        s_dal.Task.Reset();
        s_dal.Engineer.Reset();
        s_dal.Dependence.Reset();
        CreateEngineers();
        CreateTasks();
        CreateDependences();
    }
    private static void CreateEngineers()//Initializes a list of engineers
    {

        (string name, string email)[] engineerNames =
        {//An array of engineers
           ("Racheli Toledano", "racheli@gmail.com"),
           ("Osnat Shachor", "osnaty@gmail.com"),
           ("Dani Levi", "Dani@gmail.com"),
           ("Eli Amar", "Eli@gmail.com"),
           ("Yair Cohen","Yair@gmail.com"),
           ("Ariela Levin","Ariel@gmail.com"),
           ("Dina Klein", "Dina@gmail.com"),
           ("Zundel Baruch", "Zundel@gmail.com")
        };
        EngineerExperience level;
        int id;
        foreach (var engineerName in engineerNames)
        {
            do
                id = s_rand.Next(200000000, 400000000);
            while (s_dal!.Engineer.Read(id) != null);//As long as he didn't find a new ID
            level = (EngineerExperience)s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());//Engineer level
            Engineer engineer = new(id, engineerName.name, engineerName.email, level, null, false);
            s_dal!.Engineer.Create(engineer);
        }
    }
    private static void CreateTasks()//Initializes a list of tasks
    {
        (string taskAlias, string description)[] tasks =
          {//array
           ("Budget", "manage the budget"),
           ("Permits","obtain building permits"),
           ("Supervision","Supervise the construction"),
           ("Flooring", "floor the apartment"),
           ("Planning", "plan the form of construction"),
           ("Kitchen", "design the kitchen"),
           ("Bedroom", "design the bedroom"),
           ("Living-room", "design the living room")
        };

        var allEngineers = s_dal?.Engineer.ReadAll().ToList();
        int id;

        foreach (var task in tasks)
        {
            do
            {
                id = s_rand.Next(1000, 10000);
            }
            while (s_dal!.Task.Read(id) != null);//As long as he didn't find a new ID

            DateTime createDate = DateTime.Now;
            DateTime startDate = createDate.AddDays(s_rand.Next(0, 11));
            DateTime ScheduledDate = createDate.AddDays(s_rand.Next(11, 61));
            DateTime complete = createDate.AddDays(s_rand.Next(11, 61));
            DateTime deadLineDate = complete.AddDays(s_rand.Next(0, 20));
            EngineerExperience level = (EngineerExperience)s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());
            int? engineerId = allEngineers == null? null : allEngineers[s_rand.Next(0, allEngineers.Count())]!.Id;
            DO.Task newTask = new(id, task.description, task.taskAlias, false, createDate, startDate, ScheduledDate, deadLineDate, complete, null, null, engineerId, level,null);
            s_dal!.Task.Create(newTask);
        }
    }
    private static void CreateDependences()//Initializes a list of Dependences
    {
        (int dependentOnTask, int dependentTask)[] dependencesNums =
          {//array
           (1,2),
           (2,3),
           (3,4),
           (4, 5)
        };
        int id;

        foreach (var dependencesNum in dependencesNums)
        {
            do
            {
                id = s_rand.Next(1, 10);
            }
            while (s_dal!.Dependence.Read(id) != null);//Temporary ID until the running number is initialized
            Dependence newDependence = new(id, dependencesNum.dependentOnTask, dependencesNum.dependentTask);
            s_dal!.Dependence.Create(newDependence);
        }
    }

}
