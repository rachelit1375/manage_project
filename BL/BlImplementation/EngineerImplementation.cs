
using BlApi;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer item)
    {
        if (item.Id <= 0)
            throw new ArgumentException();//
        if (item.Name?.Length <= 0)
            throw new ArgumentException();//
        if (item.Cost < 0)//<=
            throw new ArgumentException();//
        if (!item.Email!.Contains("@"))
            throw new ArgumentException();//
        DO.Engineer doEngineer = new DO.Engineer(item.Id, item.Name!, item.Email, (DO.EngineerExperience)item.Level!, item.Cost, item.Active);
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }
}
