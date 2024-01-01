
namespace DalApi;

public interface IDal
{
    ITask Task {  get; }
    IDependence Dependence { get; }
    IEngineer Engineer { get; }
}
