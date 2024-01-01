
namespace BlApi;
/// <summary>
/// Declaration of functions for Milestone class
/// </summary>
public interface IMilestone
{
    public BO.Milestone? Read(int id);
    public void Update(int id);
}
