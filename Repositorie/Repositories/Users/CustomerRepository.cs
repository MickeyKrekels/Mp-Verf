using Repositorie.DbContexts;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;
using Repositorie.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    var shoppingCart = context.ShoppingCart.Where(x => x.Id == customer.Id).ToList();
                    customer.ShoppingCart = shoppingCart;
                }
            }


            return customers;
        }
        public void Add(Customer customer)
        {
            context.Customer.Add(customer);
            context.SaveChanges();
        }

        public void AddToShoppingCart(Guid id, ShoppingCart shoppingCart)
        {
            var user = context.Customer.Where(x => x.Id == id).First();

            if (user == null)
                return;

            user.ShoppingCart.Add(shoppingCart);


            context.SaveChanges();
        }
        public void RemoveFromShoppingCart(Guid id, StoreItem storeItem)
        {
            var user = context.Customer.Where(x => x.Id == id).First();

            if (user == null)
                return;

            var userItem = user.ShoppingCart.Where(x => x.StoreItemId == storeItem.Id).First();

            if (userItem == null)
                return;

            context.ShoppingCart.Remove(userItem);
            context.SaveChanges();
        }
        public void UpdateShoppingCart(Guid id, Guid shoppingCartId,int amount)
        {
            var user = context.Customer.Where(x => x.Id == id).First();

            if (user == null)
                return;

            var result = context.ShoppingCart.Where(x => x.Id == shoppingCartId).First();

            if (result == null)
                return;

            result.Amount = amount;
            context.SaveChanges();
        }

    }
}
