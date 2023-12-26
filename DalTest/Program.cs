using Dal;
using DalApi;
using DO;
using DalList;
using System.Xml.Linq;
namespace DalTest;
using DalXml;
internal class Program
{
    // private static ITask? s_dalTask = new TaskImplementation();
    // private static IDependence? s_dalDependence = new DependenceImplementation();
    // private static IEngineer? s_dalEngineer = new EngineerImplementation();

    //static readonly IDal s_dal = new Dal.DalList(); //stage 2
    //static readonly IDal s_dal = new Dal.DalXml(); //stage 3
    static readonly IDal s_dal = Factory.Get;//stage 4
    public static void InfoOfEngineer(char x)////The function that manages the Engineer's menu
    {
        switch (x)
        {
            case 'a'://add
                Console.WriteLine("enter Engineer's id to add");
                int idAdd = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter Engineer's name");
                string? nameAdd = Console.ReadLine();
                Console.WriteLine("enter Engineer's email");
                string emailAdd = (Console.ReadLine()!);
                Console.WriteLine("enter Engineer's level(0-for expert,1-for Junior,2-for Rookie)");
                EngineerExperience? levelAdd = (EngineerExperience)int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter Engineer's Cost");
                double? costAdd = double.Parse(Console.ReadLine()!);
                Engineer engineerAdd = new Engineer(idAdd, nameAdd!, emailAdd, (EngineerExperience)levelAdd, costAdd, true);

                try
                {
                    s_dal!.Engineer.Create(engineerAdd);//Creating a new engineer
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
                Console.WriteLine(s_dal?.Engineer?.Read(idRead));//Introducing the engineer              
                break;

            case 'c'://read all
                Console.WriteLine("all the engineers:");
                IEnumerable<Engineer> allEngineers = s_dal!.Engineer.ReadAll()!;
                foreach (var item in allEngineers)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of engineer to update");
                int idUpdate = int.Parse(Console.ReadLine()!);
                try
                {
                    Engineer? lastEngineer = s_dal.Engineer?.Read(idUpdate);
                    if (lastEngineer==null)//If no engineer is found
                    {
                        Console.WriteLine("An Engineers With such Id Does Not Exist");
                        break;
                    }
                    Console.WriteLine(lastEngineer);

                    Console.WriteLine("enter Engineer's name");
                    string? nameUpdate = Console.ReadLine();
                    if (nameUpdate == "")//If the user has not added details - will take from the engineer before the update
                        nameUpdate = lastEngineer.Name;
                    Console.WriteLine("enter Engineer's email");
                    string emailUpdate = (Console.ReadLine()!);
                    if (emailUpdate == "")
                        emailUpdate = lastEngineer.Email;
                    Console.WriteLine("enter Engineer's level(0-for Beginner,1-for AdvancedBeginner,2-for Competent,3-for Proficient,4-for Expert)");
                    EngineerExperience levelUpdate = (EngineerExperience)int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter Engineer's Cost");
                    double? costUpdate = double.Parse(Console.ReadLine()!);
                    costUpdate ??= lastEngineer.Cost;
                    Engineer engineerUpdate = new Engineer(idUpdate, nameUpdate!, emailUpdate, (EngineerExperience)levelUpdate, costUpdate, true);
                    s_dal.Engineer?.Update(engineerUpdate);//Updates the engineer
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
                    s_dal.Engineer?.Delete(idDelete);//Deletes the engineer
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

    public static void InfoOfTask(char x)//The function that manages the task's menu
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
                DateTime deadlineAdd = Convert.ToDateTime(Console.ReadLine()!);
                Console.WriteLine("enter task's date of complete");
                DateTime? completeAdd = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's deliverables");
                string? deliverablesAdd = Console.ReadLine();
                Console.WriteLine("enter task's remarks");
                string? remarksAdd = Console.ReadLine();
                Console.WriteLine("enter task's engineerld");
                int engineerldAdd = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's level(0-for Beginner, 1-for AdvancedBeginner, 2-for Competent, 3-Proficient, 4-Expert)");
                EngineerExperience? levelAdd = (EngineerExperience)int.Parse(Console.ReadLine()!);

                DO.Task task = new DO.Task(111, descriptionAdd, aliasAdd, milestoneAdd, createAdd, startAdd, scheduledAdd, deadlineAdd, completeAdd, deliverablesAdd, remarksAdd, engineerldAdd, levelAdd);//Create a new task
                s_dal.Task!.Create(task);
                break;

            case 'b'://read
                Console.WriteLine("enter task's number to read");
                int idRead = int.Parse(Console.ReadLine()!);            
                Console.WriteLine(s_dal.Task?.Read(idRead));// Presents the task             
                break;

            case 'c'://read all
                Console.WriteLine("all the tasks with their customers:");
                IEnumerable<DO.Task> allTasks = s_dal.Task!.ReadAll()!;
                foreach (var item in allTasks)
                    Console.WriteLine(item);
                break;
            case 'd'://update!!
                Console.WriteLine("enter number of task to update");
                int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    DO.Task? upTask = s_dal.Task?.Read(idUpdate);
                    if (upTask == null)//If the task number is not found
                    {
                        Console.WriteLine("A Task With such number Does Not Exist");
                        break;
                    }

                    Console.WriteLine(upTask);
                    Console.WriteLine("enter task's description");
                    string? descriptionUpdate = Console.ReadLine();
                    if (descriptionUpdate == "")// If the user has not added details - will take from the task before the update
                        descriptionUpdate = upTask.Description;
                    Console.WriteLine("enter task's alias");
                    string? aliasUpdate = Console.ReadLine();
                    if (aliasUpdate == "")
                        aliasUpdate = upTask.Alias;
                    Console.WriteLine("enter task's milestone");
                    bool milestoneUpdate = bool.Parse(Console.ReadLine()!);//אם לא הוכנס לתא, מה עושים?
                    Console.WriteLine("enter task's date of created");
                    DateTime? createUpdate = Convert.ToDateTime(Console.ReadLine());
                    if (createUpdate == null)
                        createUpdate = upTask.CreateAt;
                    Console.WriteLine("enter task's date of start");
                    DateTime? startUpdate = Convert.ToDateTime(Console.ReadLine());                    
                    Console.WriteLine("enter task's date of scheduled");
                    DateTime? scheduledUpdate = Convert.ToDateTime(Console.ReadLine());                   
                    Console.WriteLine("enter task's date of deadline");
                    DateTime? deadlineUpdate = Convert.ToDateTime(Console.ReadLine());
                    if (deadlineUpdate == null)
                        deadlineUpdate = upTask.Deadline;
                    Console.WriteLine("enter task's date of complete");
                    DateTime? completeUpdate = Convert.ToDateTime(Console.ReadLine());                  
                    Console.WriteLine("enter task's deliverables");
                    string? deliverablesUpdate = Console.ReadLine();
                    if (deliverablesUpdate == "")
                        deliverablesUpdate = upTask.Deliverables;
                    Console.WriteLine("enter task's remarks");
                    string? remarksUpdate = Console.ReadLine();
                    if (remarksUpdate == "")
                        remarksUpdate = upTask.Remarks;
                    Console.WriteLine("enter task's engineerld");
                    int? engineerldUpdate = int.Parse(Console.ReadLine()!);
                    if (engineerldUpdate == null)
                        engineerldUpdate = upTask.EngineerId;
                    Console.WriteLine("enter task's level(0-for Beginner, 1-for AdvancedBeginner, 2-for Competent, 3-Proficient, 4-Expert)");
                    EngineerExperience? levelUpdate = (EngineerExperience)int.Parse(Console.ReadLine()!);                 
                    DO.Task taskUpdate = new DO.Task(idUpdate, descriptionUpdate, aliasUpdate, milestoneUpdate, (DateTime)createUpdate, startUpdate, scheduledUpdate, (DateTime)deadlineUpdate, completeUpdate, deliverablesUpdate, remarksUpdate, engineerldUpdate, levelUpdate);
                    s_dal.Task!.Update(taskUpdate);
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
                    s_dal.Task?.Delete(idDelete);
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
    public static void InfoOfDependence(char x)//The function that manages the dependence's menu
    {
        switch (x)
        {
            case 'a'://add
                Console.WriteLine("enter dependence task's id");
                int dependentTaskAdd = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter dependent on task's id");
                int dependsOnTaskAdd = int.Parse(Console.ReadLine()!);
                Dependence dependenceAdd = new Dependence(121, dependentTaskAdd, dependsOnTaskAdd);              
                s_dal.Dependence!.Create(dependenceAdd);// ?/!              
                break;

            case 'b'://read
                Console.WriteLine("enter dependence's id to read");
                int idRead = int.Parse(Console.ReadLine()!);               
                Console.WriteLine(s_dal.Dependence?.Read(idRead));              
                break;

            case 'c'://read all
                Console.WriteLine("all the dependences:");
               IEnumerable<DO.Dependence> listReadAllDependences = s_dal.Dependence!.ReadAll()!;
                foreach (var item in listReadAllDependences)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of dependence to update");
                int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    Console.WriteLine(s_dal.Dependence?.Read(idUpdate));

                    Console.WriteLine("enter dependent task's id");
                    int dependentTaskUpdate = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter depends on task's id");
                    int dependsOnTaskUpdate = int.Parse(Console.ReadLine()!);
                    Dependence depUpdate = new Dependence(idUpdate, dependentTaskUpdate, dependsOnTaskUpdate);
                    s_dal.Dependence!.Update(depUpdate);
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
                    s_dal.Dependence!.Delete(idDelete);
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
     static void Main(string[] args)
    {
        Console.WriteLine("for engineer press 1");
        Console.WriteLine("for task press 2");
        Console.WriteLine("for dependence of tasks press 3");
        Console.WriteLine("for initialization press 4");
        Console.WriteLine("for exit press 0");
        int choose = int.Parse(Console.ReadLine()!);
        char x;
        while (choose != 0)//As long as 0 was not pressed to exit
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
                    InfoOfEngineer(x);//The function that manages the engineer's menu
                    break;
                case 2:
                    Console.WriteLine("for add a task press a");
                    Console.WriteLine("for read a task press b");
                    Console.WriteLine("for read all tasks press c");
                    Console.WriteLine("for update a task press d");
                    Console.WriteLine("for delete a task press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfTask(x);//The function that manages the task's menu  
                    break;
                case 3:
                    Console.WriteLine("for add an dependence of tasks press a");
                    Console.WriteLine("for read dependence of tasks press b");
                    Console.WriteLine("for read all dependences of tasks press c");
                    Console.WriteLine("for update a dependence of task press d");
                    Console.WriteLine("for delete a dependence of tasks press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfDependence(x);//The function that manages the Dependence's menu
                    break;
                 case 4:
                    Console.WriteLine("Would you like to create Initial data? (Y/N)"); //stage 3
                    string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                    if (ans == "Y") //stage 3
                    { //Initialization.Do(s_dal); //stage 2
                        Initialization.Do();//stage 4
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine("for engineer press 1");
            Console.WriteLine("for task press 2");
            Console.WriteLine("for Dependence of tasks press 3");
            Console.WriteLine("for initialization press 4");
            Console.WriteLine("for exit press 0");
            choose = int.Parse(Console.ReadLine()!);
        }

    }
}