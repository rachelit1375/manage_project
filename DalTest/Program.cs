using Dal;
using DalApi;
using DO;

namespace DalTest;

internal class Program
{
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependence? s_dalDependence = new DependenceImplementation();
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    static void Main(string[] args)
    {
        Initialization.Do(s_dalTask, s_dalDependence, s_dalEngineer);
    }
}