

namespace Dal;

  internal static class DataSource
{
    internal static class Config
    {
        internal const int startTaskId = 1;//Initialize the running number
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
       
        internal const int startDependenceId = 1;//Initialize the running number
        private static int nextDependenceId = startDependenceId;
        internal static int NextDependenceId { get => nextDependenceId++; }
    }

    internal static List<DO.Dependence> Dependences { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();

}
