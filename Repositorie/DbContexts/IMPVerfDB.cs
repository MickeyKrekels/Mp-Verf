using System.Data.Entity;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;

namespace Repositorie.DbContexts
{
    public interface IMPVerfDB
    {
        DbSet<Admin> Admin { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<StoreItem> StoreItem { get; set; }
        DbSet<StoreImage> StoreImage { get; set; }
    }
}