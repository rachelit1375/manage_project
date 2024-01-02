
namespace BO;

public class TaskInEngineer
{
    public int Id { get; init; }//The ID number of the task
    public string? Alias { get; set; }///The nicKname of the task

    public override string ToString() => this.ToStringProperty();
}
