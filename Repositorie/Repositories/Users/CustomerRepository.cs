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
    public class CustomerRepository : ICustomerRepository
    {
        public MPVerfDB context;

        public CustomerRepository(MPVerfDB context)
        {
            this.context = context;
        }

        public List<Customer> Get()
        {
            List<Customer> customers = new List<Customer>();
            customers = context.Customer.ToList();

            return customers;
        }
        public void Add(Customer customer)
        {
            context.Customer.Add(customer);
            context.SaveChanges();
        }
    }
}
