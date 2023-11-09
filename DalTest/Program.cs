using Dal;
using DalApi;
using DO;
using System.Xml.Linq;

namespace DalTest;

class Program
{
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependence? s_dalDependence = new DependenceImplementation();
    private static IEngineer? s_dalEngineer = new EngineerImplementation();

    public static void InfoOfEngineer(char x)
    {
        switch (x)
        {
            case 'a'://add
                Console.WriteLine("enter Engineer's id to add");
                int idAdd = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter Engineer's name");
                string? nameAdd = Console.ReadLine();
                Console.WriteLine("enter Engineer's email");
                string? emailAdd = (Console.ReadLine());
                Console.WriteLine("enter Engineer's level(0-for expert,1-for Junior,2-for Rookie)");
                EngineerExperience? levelAdd = (EngineerExperience)int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter Engineer's Cost");
                double? costAdd = double.Parse(Console.ReadLine()!);
                Engineer engineerAdd = new Engineer(idAdd, nameAdd!, emailAdd, (EngineerExperience)levelAdd, costAdd);

                try
                {
                    s_dalEngineer!.Create(engineerAdd);
                    Console.WriteLine("the engineer added");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'b'://read
                Console.WriteLine("enter engineer's id to read");
                int idRead = int.Parse(Console.ReadLine()!);
                Console.WriteLine(s_dalEngineer?.Read(idRead));              
                break;

            case 'c'://read all
                Console.WriteLine("all the engineers:");
                List<Engineer> allEngineers = s_dalEngineer!.ReadAll();
                foreach (var item in allEngineers)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of engineer to update");
                int idUpdate = int.Parse(Console.ReadLine()!);
                try
                {
                    Console.WriteLine(s_dalEngineer?.Read(idUpdate));

                    Console.WriteLine("enter Engineer's name");
                    string? nameUpdate = Console.ReadLine();
                    Console.WriteLine("enter Engineer's email");
                    string? emailUpdate = (Console.ReadLine());
                    Console.WriteLine("enter Engineer's level(0-for expert,1-for Junior,2-for Rookie)");
                    EngineerExperience? levelUpdate = (EngineerExperience)int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter Engineer's Cost");
                    double? costUpdate = double.Parse(Console.ReadLine()!);
                    Engineer engineerUpdate = new Engineer(idUpdate, nameUpdate!, emailUpdate, (EngineerExperience)levelUpdate, costUpdate);
                    s_dalEngineer?.Update(engineerUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'e'://delete
                Console.WriteLine("enter id of engineer to delete");
                int idDelete = int.Parse(Console.ReadLine()!);
                try
                {
                    s_dalEngineer?.Delete(idDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            default:
                break;
        }
    }

    public static void InfoOfTask(char x)
    {
        switch (x)
        {

            case 'a'://add
                Console.WriteLine("enter task's description");
                string descriptionAdd = Console.ReadLine()!;
                Console.WriteLine("enter task's alias");
                string aliasAdd = Console.ReadLine()!;
                Console.WriteLine("enter task's milestone");
                bool milestoneAdd = bool.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's date of created");
                DateTime createAdd = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of start");
                DateTime? startAdd = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of scheduled");
                DateTime? scheduledAdd = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of deadline");
                DateTime? deadlineAdd = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of complete");
                DateTime? completeAdd = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's deliverables");
                string? deliverablesAdd = Console.ReadLine();
                Console.WriteLine("enter task's remarks");
                string? remarksAdd = Console.ReadLine();
                Console.WriteLine("enter task's engineerld");
                int engineerldAdd = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's level(0-for expert,1-for Junior,2-for Rookie)");
                EngineerExperience? levelAdd = (EngineerExperience)int.Parse(Console.ReadLine()!);

                DO.Task task = new DO.Task(111, descriptionAdd, aliasAdd, milestoneAdd, createAdd, startAdd, scheduledAdd, deadlineAdd, completeAdd, deliverablesAdd, remarksAdd, engineerldAdd, levelAdd);
               
                s_dalTask!.Create(task);
                break;

            case 'b'://read
                Console.WriteLine("enter task's number to read");
                int idRead = int.Parse(Console.ReadLine()!);            
                Console.WriteLine(s_dalTask?.Read(idRead));              
                break;

            case 'c'://read all
                Console.WriteLine("all the tasks with their customers:");
                List<DO.Task> allTasks = s_dalTask!.ReadAll();
                foreach (var item in allTasks)
                    Console.WriteLine(item);
                break;
            case 'd'://update!!
                Console.WriteLine("enter id of task to update");
                int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    Console.WriteLine(s_dalTask?.Read(idUpdate));

                    Console.WriteLine("enter task's description");
                    string descriptionUpdate = Console.ReadLine()!;
                    Console.WriteLine("enter task's alias");
                    string aliasUpdate = Console.ReadLine()!;                    
                    Console.WriteLine("enter task's milestone");
                    bool milestoneUpdate = bool.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task's date of created");
                    DateTime createUpdate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of start");
                    DateTime? startUpdate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of scheduled");
                    DateTime? scheduledUpdate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of deadline");
                    DateTime? deadlineUpdate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of complete");
                    DateTime? completeUpdate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's deliverables");
                    string? deliverablesUpdate = Console.ReadLine();
                    Console.WriteLine("enter task's remarks");
                    string? remarksUpdate = Console.ReadLine();
                    Console.WriteLine("enter task's engineerld");
                    int engineerldUpdate = int.Parse(Console.ReadLine()!); 
                    Console.WriteLine("enter task's level(0-for expert,1-for Junior,2-for Rookie)");
                    EngineerExperience? levelUpdate = (EngineerExperience)int.Parse(Console.ReadLine()!);

                    DO.Task taskUpdate = new DO.Task(idUpdate, descriptionUpdate, aliasUpdate, milestoneUpdate, createUpdate, startUpdate, scheduledUpdate, deadlineUpdate, completeUpdate, deliverablesUpdate, remarksUpdate, engineerldUpdate, levelUpdate);
                    s_dalTask!.Update(taskUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'e'://delete
                Console.WriteLine("enter id of task to delete");
                int idDelete = int.Parse(Console.ReadLine()!);
                try
                {
                    s_dalTask?.Delete(idDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            default:
                break;
        }
    }
    public static void InfoOfDependence(char x)
    {
        switch (x)
        {
            case 'a'://add
                Console.WriteLine("enter dependent task's id");
                int dependentTaskAdd = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter depends on task's id");
                int dependsOnTaskAdd = int.Parse(Console.ReadLine()!);
                Dependence dependenceAdd = new Dependence(121, dependentTaskAdd, dependsOnTaskAdd);              
                s_dalDependence!.Create(dependenceAdd);// ?/!              
                break;

            case 'b'://read
                Console.WriteLine("enter dependence's id to read");
                int idRead = int.Parse(Console.ReadLine()!);               
                Console.WriteLine(s_dalDependence?.Read(idRead));              
                break;

            case 'c'://read all
                Console.WriteLine("all the dependences:");
                List<DO.Dependence> listReadAllDependences = s_dalDependence!.ReadAll();
                foreach (var item in listReadAllDependences)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of dependence to update");
                int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    Console.WriteLine(s_dalDependence?.Read(idUpdate));

                    Console.WriteLine("enter dependent task's id");
                    int dependentTaskUpdate = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter depends on task's id");
                    int dependsOnTaskUpdate = int.Parse(Console.ReadLine()!);
                    Dependence depUpdate = new Dependence(idUpdate, dependentTaskUpdate, dependsOnTaskUpdate);
                    s_dalDependence!.Update(depUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'e'://delete
                Console.WriteLine("enter dependency's id to delete");
                int idDelete = int.Parse(Console.ReadLine()!);
                try
                {
                    s_dalDependence!.Delete(idDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            default:
                break;
        }
    }
    public static void Main(string[] args)
    {
        Initialization.Do(s_dalTask, s_dalDependence, s_dalEngineer);
        Console.WriteLine("for engineer press 1");
        Console.WriteLine("for task press 2");
        Console.WriteLine("for Dependence of tasks press 3");
        Console.WriteLine("for exit press 0");
        int choose = int.Parse(Console.ReadLine()!);
        char x;
        while (choose != 0)
        {
            switch (choose)
            {
                case 1:
                    Console.WriteLine("for add an engineer press a");
                    Console.WriteLine("for read an engineer press b");
                    Console.WriteLine("for read all engineers press c");
                    Console.WriteLine("for update an engineer press d");
                    Console.WriteLine("for delete an engineer press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfEngineer(x);
                    break;
                case 2:
                    Console.WriteLine("for add a task press a");
                    Console.WriteLine("for read a task press b");
                    Console.WriteLine("for read all tasks press c");
                    Console.WriteLine("for update a task press d");
                    Console.WriteLine("for delete a task press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfTask(x);  
                    break;
                case 3:
                    Console.WriteLine("for add an dependence of tasks press a");
                    Console.WriteLine("for read dependence of tasks press b");
                    Console.WriteLine("for read all dependences of tasks press c");
                    Console.WriteLine("for update a dependence of task press d");
                    Console.WriteLine("for delete a dependence of tasks press e");
                 
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfDependence(x);
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter your choose");
            choose = int.Parse(Console.ReadLine()!);
        }

    }
}///initialization לא עובד- לא קיים במאגר