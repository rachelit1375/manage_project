using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
sealed public class DalXmlClass : IDal
{
    public ITask Task => new TaskImplementation();

    public IDependence Dependence => new DependenceImplementation();

    public IEngineer Engineer => new EngineerImplementation();
}