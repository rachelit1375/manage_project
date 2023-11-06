
namespace DalTest;
using DalApi;
using DO;
/// <summary>
/// 
/// </summary>
public static class Initialization
{
    private static ITask? s_dalTask;//מה זה אומר? איזה סוג אובייקט זה?
    private static IDependence? s_dalDependence;
    private static IEngineer? s_dalEngineer;
    private static readonly Random s_rand = new();

    private static void createTasks()
    {
        string[] tasksAliases =
        {
            "wash the dishes",
            "put the dishes in their place",
            "do homework",
            "tide the bed",
            "go to shoping",
            "buy milk"
        };

        foreach (var taskAlias in tasksAliases)//לוודא שלא בעיה לקרוא לרבים עם es
        {
            int id;
            do
            {
                id = s_rand.Next(1000, 9999);
            }
            while (s_dalTask!.Read(id) != null);
            int year=s_rand.Next(2020,2023);
            int month=s_rand.Next(1,12);
            int day=s_rand.Next(1,30);
            DateTime createDate= new DateTime(year, month, day);
            int dMonth=month+1;
            int dYear=year;
            if (month==12)
            {
                dMonth = 1;
                dYear=year+1;
            }         
            DateTime deadLineDate = new DateTime(dYear, dMonth, day);
            Task newTask=new(id,null, taskAlias,false, createDate,null, null, null, deadLineDate,null,null,null...);
            s_dalTask!.Create(newTask);

        }
    }
    private static void createDependences()
    {

    }
    private static void createEngineers()
    {
        (string name, string email)[] engineerNames =
        {
           ( "Racheli Toledano", "racheli@gmail.com"),
           ( "Osnat Shachor", "osnaty@gmail.com"),
           ( "Dani Levi", "Dani@gmail.com"),
           ( "Eli Amar", "Eli@gmail.com"),
           ("Yair Cohen","Yair@gmail.com"),
           ("Ariela Levin","Ariel@gmail.com"),
           ("Dina Klein", "Dina@gmail.com"),
           ("Zundel Baruch", "Zundel@gmail.com")
        };
    }
}
