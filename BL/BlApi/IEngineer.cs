
namespace BlApi;
/// <summary>
/// Declaration of functions for Engineer class
/// </summary>
public interface IEngineer
{
    public int Create(BO.Engineer item);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null);
    public void Update(BO.Engineer item);
    public void Delete(int id);
}
