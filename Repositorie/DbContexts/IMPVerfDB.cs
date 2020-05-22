using Repositorie.Entities.Base;
using Repositorie.Entities.Users;
using System.Data.Entity;

namespace Repositorie.DbContexts
{
    public interface IMPVerfDB
    {
        DbSet<Admin> Admin { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<StoreImage> StoreImage { get; set; }
        DbSet<StoreItem> StoreItem { get; set; }
    }
}