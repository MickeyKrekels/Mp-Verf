using System.Collections.Generic;
using Repositorie.Entities.Users;

namespace Repositorie.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        void Add(Admin admin);
        List<Admin> Get();
    }
}