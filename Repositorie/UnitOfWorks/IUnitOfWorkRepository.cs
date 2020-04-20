using Repositorie.Interfaces.Repositories;
using Repositorie.Repositories.StoreItems;

namespace Repositorie.UnitOfWorks
{
    public interface IUnitOfWorkRepository
    {
        IAdminRepository AdminRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IStoreItemRepository StoreItemRepository { get; }
    }
}