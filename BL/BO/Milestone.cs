
namespace BO;

public class Milestone
{
    public int Id { get; init; }//Milestone ID number
    public string? Description { get; set; }//Milestone description
    public string? Alias { get; set; }//Nickname of the milestone
    public Status? Status { get; set; }//The status of the milestone - what is its status
    public DateTime CreatedAtDate { get; set; }//when created
    public DateTime? Start { get; set; }///The date of the start of the execution of the task
    public DateTime? ScheduledDate { get; set; }//Estimated completion date
    public DateTime DeadlineDate { get; set; }//Final date for completion
    public DateTime? CompleteDate { get; set; }//Actual end date
    public double? CompletionPercentage { get; set; }//How many percent are completed until execution
    public string? Remarks { get; set; }//Notes on the milestone
    public List<BO.TaskInList>? DependenceList { get; set; }//The dependencies of the milestone

    public override string ToString() => this.ToStringProperty();
}
