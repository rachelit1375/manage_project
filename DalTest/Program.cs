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
                int idEngineer = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter Engineer's name");
                string? nameEngineer = Console.ReadLine();
                Console.WriteLine("enter Engineer's email");
                string? emailEngineer = (Console.ReadLine());
                Console.WriteLine("enter Engineer's level(0-for expert,1-for Junior,2-for Rookie)");
                EngineerExperience? levelEngineer = (EngineerExperience)int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter Engineer's Cost");
                double? costEngineer = double.Parse(Console.ReadLine()!);
                Engineer engineer = new Engineer(idEngineer, nameEngineer!, emailEngineer, (EngineerExperience)levelEngineer, costEngineer);

                try
                {
                    s_dalEngineer!.Create(engineer);
                    Console.WriteLine("the engineer added");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'b'://read by id
                Console.WriteLine("enter engineer's id to read");
                int id = int.Parse(Console.ReadLine()!);
                Console.WriteLine(s_dalEngineer?.Read(id));              
                break;

            case 'c'://read all
                Console.WriteLine("all the engineers:");
                List<Engineer> allEngineers = s_dalEngineer!.ReadAll();
                foreach (var item in allEngineers)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of engineer to update");
                int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    Console.WriteLine(s_dalEngineer?.Read(idUpdate));
                    int _id = idUpdate;
                    Console.WriteLine("enter Engineer's name");
                    string? name = Console.ReadLine();
                    Console.WriteLine("enter Engineer's email");
                    string? email = (Console.ReadLine());
                    Console.WriteLine("enter Engineer's level(0-for expert,1-for Junior,2-for Rookie)");
                    EngineerExperience? level = (EngineerExperience)int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter Engineer's Cost");
                    double? cost = double.Parse(Console.ReadLine()!);
                    Engineer eUpdate = new Engineer(_id, name!, email, (EngineerExperience)level, cost);
                    s_dalEngineer?.Update(eUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'e'://delete a product
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
                string _description = Console.ReadLine()!;
                Console.WriteLine("enter task's alias");
                string _alias = Console.ReadLine()!;
                Console.WriteLine("enter task's milestone");
                bool _milestone = bool.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's date of created");
                DateTime _createdAt = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of start");
                DateTime? _start = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of scheduled");
                DateTime? _scheduledDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of deadline");
                DateTime? _deadline = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of complete");
                DateTime? _complete = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's deliverables");
                string? _deliverables = Console.ReadLine();
                Console.WriteLine("enter task's remarks");
                string? _remarks = Console.ReadLine();
                Console.WriteLine("enter task's engineerld");
                int _engineerld = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's level(0-for expert,1-for Junior,2-for Rookie)");
                EngineerExperience? _complexityLevel = (EngineerExperience)int.Parse(Console.ReadLine()!);

                DO.Task task = new DO.Task(111, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _deadline, _complete, _deliverables, _remarks, _engineerld, _complexityLevel);
               
                s_dalTask!.Create(task);
                break;

            case 'b'://read by id
                Console.WriteLine("enter task's number to read");
                int id = int.Parse(Console.ReadLine()!);            
                Console.WriteLine(s_dalTask?.Read(id));              
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
                    int _id = idUpdate;
                    Console.WriteLine("enter task's description");
                    string description = Console.ReadLine()!;
                    Console.WriteLine("enter task's alias");
                    string alias = Console.ReadLine()!;
                    Console.WriteLine("enter task's engineerld");
                    int engineerld = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task's milestone");
                    bool milestone = bool.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task's date of created");
                    DateTime createdAt = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of start");
                    DateTime? start = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of scheduled");
                    DateTime? scheduledDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of deadline");
                    DateTime? deadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of complete");
                    DateTime? complete = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's deliverables");
                    string? deliverables = Console.ReadLine();
                    Console.WriteLine("enter task's remarks");
                    string? remarks = Console.ReadLine();
                    Console.WriteLine("enter task's level(0-for expert,1-for Junior,2-for Rookie)");
                    EngineerExperience? complexityLevel = (EngineerExperience)int.Parse(Console.ReadLine());
                    DO.Task taskUpdate = new DO.Task(_id, description, alias,  milestone, createdAt, start, scheduledDate, deadline, complete, deliverables, remarks, engineerld, complexityLevel);
                    s_dalTask!.Update(taskUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'e'://delete an order
                Console.WriteLine("enter id of task to delete");
                int idDelete = int.Parse(Console.ReadLine()!);
                try
                {
                    s_dalTask?.Delete(idDelete);//לבדוק
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
                int _dependentTask = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter depends on task's id");
                int _dependsOnTask = int.Parse(Console.ReadLine()!);
                Dependence dep = new Dependence(121, _dependentTask, _dependsOnTask);              
                s_dalDependence!.Create(dep);// ?/!              
                break;

            case 'b'://read by id
                Console.WriteLine("enter dependency's id to read");
                int id = int.Parse(Console.ReadLine()!);               
                Console.WriteLine(s_dalDependence?.Read(id));              
                break;

            case 'c'://read all
                Console.WriteLine("all the dependencys:");
                List<DO.Dependence> listReadAllDependencys = s_dalDependence!.ReadAll();
                foreach (var item in listReadAllDependencys)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of dependency to update");
                int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    Console.WriteLine(s_dalDependence?.Read(idUpdate));
                    int _id = idUpdate;
                    Console.WriteLine("enter dependent task's id");
                    int dependentTask = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter depends on task's id");
                    int dependsOnTask = int.Parse(Console.ReadLine()!);
                    Dependence depUpdate = new Dependence(_id, dependentTask, dependsOnTask);
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
        Console.WriteLine("for Dependence in order press 3");
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
                    Console.WriteLine("for update a dependence in task press d");
                    Console.WriteLine("for delete a dependence of tasks press e");
                 
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfDependence(x);
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter a number");
            choose = int.Parse(Console.ReadLine()!);
        }

    }
}