using System.Collections.Generic;
using Repositorie.Entities.Users;

namespace Repositorie.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        List<Customer> Get();
    }
}