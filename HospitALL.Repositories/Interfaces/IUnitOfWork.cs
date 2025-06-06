using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitALL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GenericReporitory<T>() where T : class;
        void Save();
    }
}
