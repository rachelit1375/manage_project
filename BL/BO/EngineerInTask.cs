
namespace BO;

public class EngineerInTask
{
    public int Id { get; init; }/// id of engineer
    public string? Name { get; set; }// engineer's name

    public override string ToString() => this.ToStringProperty();
}
