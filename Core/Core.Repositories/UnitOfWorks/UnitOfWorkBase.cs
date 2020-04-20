using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtosoft.Core.Repositories.UnitOfWorks
{
    public class UnitOfWorkBase<TContext>: IUnitOfWorkBase
        where TContext: DbContext
    {
        protected TContext DbContext;

        public UnitOfWorkBase(TContext context)
        {
            this.DbContext = context;
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
