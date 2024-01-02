
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BO;

public class TaskInList
{
    public int Id { get; init; }// The ID number of the task
    public string? Description { get; set; }///Task description
    public string? Alias { get; set; }///The name of the task
    public Status? Status { get; set; }//what is the task status

    public override string ToString() => this.ToStringProperty();
}
