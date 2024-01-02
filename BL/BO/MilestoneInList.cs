
namespace BO;

public class MilestoneInList
{
    public int Id { get; init; }//Milestone ID number
    public string? Description { get; set; }//Milestone description
    public string? Alias { get; set; }//Nickname of the milestone
    public Status? Status { get; set; }//The status of the milestone - what is its status
    public double? CompletionPercentage { get; set; }//How many percent are completed until execution

    public override string ToString() => this.ToStringProperty();
}
