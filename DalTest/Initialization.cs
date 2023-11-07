
namespace DalTest;
using DalApi;
using DO;
using System.Reflection.Emit;

/// <summary>
/// 
/// </summary>
public static class Initialization
{
    private static ITask? s_dalTask;
    private static IDependence? s_dalDependence;
    private static IEngineer? s_dalEngineer;
    private static readonly Random s_rand = new();
    private static void createEngineers()
    {
        (string name, string email)[] engineerNames =
        {
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
            while (s_dalEngineer!.Read(id) != null);
            level = (EngineerExperience)s_rand.Next(0, 3);
            Engineer engineer = new(id, engineerName.name, engineerName.email, level, null);
        }
    }
    private static void createTasks()
    {
        (string taskAlias, string description)[] tasks =
          {
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

        foreach (var task in tasks)//לוודא שלא בעיה לקרוא לרבים עם es
        {
            do
            {
                id = s_rand.Next(1000, 10000);
            }
            while (s_dalTask!.Read(id) != null);

            DateTime createDate = DateTime.Now;
            DateTime startDate = createDate.AddDays(s_rand.Next(0, 11));
            DateTime ScheduledDate= createDate.AddDays(s_rand.Next(11, 61));
            DateTime ForecastDate=createDate.AddDays(s_rand.Next(11, 61));
            DateTime deadLineDate = ForecastDate.AddDays(s_rand.Next(0, 14));
            level = (EngineerExperience)s_rand.Next(0, 3);
            Task newTask =new(id,task.description, task.taskAlias, false, createDate,startDate, ScheduledDate, ForecastDate, deadLineDate,null,null,null,level);
            s_dalTask!.Create(newTask);

        }
    }

   
}
