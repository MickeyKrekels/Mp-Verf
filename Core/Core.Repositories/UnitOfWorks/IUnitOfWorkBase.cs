using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtosoft.Core.Repositories.UnitOfWorks
{
    public interface IUnitOfWorkBase
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
