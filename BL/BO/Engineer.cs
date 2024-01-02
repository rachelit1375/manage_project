
namespace BO;

public class Engineer
{
    public int Id { get; init; }///personal id of engineer
    public string? Name { get; set; }///engineer's name
    public string? Email { get; set; }///engineer's email
    public EngineerExperience? Level { get; set; }///The difficulty level of the task he is responsible for
    public double? Cost { get; set; }///How much do you get per hour of work?
    public BO.TaskInEngineer? Task { get; set; }//
    public bool Active { get; set; }

    public override string ToString() => this.ToStringProperty();
}
