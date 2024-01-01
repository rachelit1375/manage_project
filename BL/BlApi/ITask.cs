
namespace BlApi;
/// <summary>
/// Declaration of functions for Task class
/// </summary>
public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInList> ReadAll(Func<Task, bool>? filter = null);////
    public void Update(BO.Task item);
    public void Delete(int id);

}
