using Common.ExtensionMethods;
using Core.Repositories.Conventions;
using Repositorie.Entities;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.DbContexts
{
    public class MPVerfDB : DbContext, IMPVerfDB
    {
        public MPVerfDB()
           : base("mp-database")
        {
            Database.SetInitializer<MPVerfDB>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.AddBefore<ForeignKeyAssociationMultiplicityConvention>(new CustomForeignKeyDiscoveryConvention());
        }
        //users
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<StoreItem> StoreItem { get; set; }



    }
}
