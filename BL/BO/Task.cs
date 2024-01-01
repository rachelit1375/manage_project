
namespace BO;

public class Task
{
    public int Id { get; init;}
    public string? Description { get; set; }///Task description
    public string? Alias { get; set; }///The name of the task
    public bool MileStone { get; set; }
    public Status? Status { get; set; }
    public DateTime CreateAt { get; set; }///Task creation date
    public DateTime? Start { get; set; }///The date of the start of the execution of the task
    public DateTime? ScheduledDate { get; set; }//Estimated date for completion of the task
    public DateTime Deadline { get; set; }///end date
    public DateTime? Complete { get; set; }///Final date of completion
    public string? Deliverables { get; set; }///the result of the task
    public string? Remarks { get; set; }///Notes on the task
    public BO.EngineerInTask Engineer { get; set; }///The ID of the engineer responsible for the task////
    public EngineerExperience? ComplexityLevel { get; set; }//difficulty level

    public override string ToString() => this.ToStringProperty();
}
