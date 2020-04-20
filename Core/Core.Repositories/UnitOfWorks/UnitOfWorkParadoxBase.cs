using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtosoft.Core.Repositories.UnitOfWorks
{
    public class UnitOfWorkParadoxBase<TContext>: IUnitOfWorkParadoxBase
    {
        protected TContext DbContext;

        public UnitOfWorkParadoxBase(TContext context)
        {
            this.DbContext = context;
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
