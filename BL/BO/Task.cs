
namespace BO;

public class Task
{
    public int Id { get; init; }//The ID number of the task
    public string? Description { get; set; }///Task description
    public string? Alias { get; set; }///The name of the task
    public MilestoneInTask? MileStone { get; set; }///the name and id of the task's milestone 
    public Status? StatusTask { get; set; }//what is the task status
    public List<BO.TaskInList>? DependenceList { get; set; }//List of task dependencies
    public DateTime CreateAt { get; set; }///Task creation date
    public DateTime? Start { get; set; }///The date of the start of the execution of the task
    public DateTime? ScheduledDate { get; set; }//Estimated date for completion of the task
    public DateTime Deadline { get; set; }///end date
    public DateTime? Complete { get; set; }///Final date of completion
    public string? Deliverables { get; set; }///the result of the task
    public string? Remarks { get; set; }///Notes on the task
    public EngineerInTask? Engineer { get; set; }///The ID of the engineer responsible for the task////
    public EngineerExperience? ComplexityLevel { get; set; }//difficulty level
    
    public override string ToString() => this.ToStringProperty();
}
