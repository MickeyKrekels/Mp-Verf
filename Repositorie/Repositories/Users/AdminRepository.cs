using Repositorie.DbContexts;
using Repositorie.Entities.Users;
using Repositorie.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Repositories.Users
{
    public class AdminRepository : IAdminRepository
    {
        public MPVerfDB context;

        public AdminRepository(MPVerfDB context)
        {
            this.context = context;
        }

        public List<Admin> Get()
        {
            List<Admin> admins = new List<Admin>();
            admins = context.Admin.ToList();

            return admins;
        }
        public void Add(Admin admin)
        {
            context.Admin.Add(admin);
            context.SaveChanges();
        }
    }
}
