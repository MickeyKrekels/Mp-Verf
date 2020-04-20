using Core.Repositories.Interfaces;
using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Repositories
{
    public class RepositoryBase<TContext, TEntity> : IRepositoryBase<TEntity>
        where TContext : DbContext
        where TEntity : EntityModalBase
    {
        protected TContext DbContext;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase(TContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entity.Id = Guid.NewGuid();

            this.DbContext.Entry(entity).State = EntityState.Added;
        }
    }
}
