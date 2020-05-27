using Imtosoft.Core.Repositories.UnitOfWorks;
using Repositorie.DbContexts;
using Repositorie.Entities;
using Repositorie.Interfaces.Repositories;
using Repositorie.Repositories;
using Repositorie.Repositories.StoreItems;
using Repositorie.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.UnitOfWorks
{
    public class UnitOfWorkRepository : UnitOfWorkBase<MPVerfDB>, IUnitOfWorkRepository
    {
        //customer
        private ICustomerRepository customerRepository;
        private IAdminRepository adminRepository;
        private IStoreItemRepository storeItemRepository;
        private IUserCommentRepository userCommentRepository;

        public UnitOfWorkRepository() : base(new MPVerfDB())
        {

        }

        //customer
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(DbContext);
                }
                return customerRepository;
            }
        }
        public IAdminRepository AdminRepository
        {
            get
            {
                if (adminRepository == null)
                {
                    adminRepository = new AdminRepository(DbContext);
                }
                return adminRepository;
            }
        }
        //storeitems
        public IStoreItemRepository StoreItemRepository
        {
            get
            {
                if (storeItemRepository == null)
                {
                    storeItemRepository = new StoreItemRepository(DbContext);
                }
                return storeItemRepository;
            }
        }
        public IUserCommentRepository UserCommentRepository
        {
            get
            {
                if (userCommentRepository == null)
                {
                    userCommentRepository = new UserCommentRepository(DbContext);
                }
                return userCommentRepository;
            }
        }


    }
}
