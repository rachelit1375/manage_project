
namespace BlApi;
/// <summary>
/// Declaration of functions for Milestone class
/// </summary>
public interface IMilestone
{
    public int Create(BO.Milestone item);
    public BO.Milestone? Read(int id);
    public void Update(int id);
}
