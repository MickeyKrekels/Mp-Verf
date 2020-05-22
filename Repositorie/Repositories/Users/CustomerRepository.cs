﻿using Repositorie.DbContexts;
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

        public void UpdateShoppingCart(Guid id, List<StoreItem> storeItems)
        {
            var user = context.Customer.Where(x => x.Id == id).First();
            var shoppingCart = context.ShoppingCart.Where(x => x.Id == id).ToList();

            if (user == null)
                return;

            if (storeItems != null)
            {
                foreach (var storeItem in storeItems)
                {
                    if (shoppingCart.Count == 0 || shoppingCart.Where(x => x.StoreItemId == storeItem.Id).First() == null)
                    {
                        ShoppingCart sc = new ShoppingCart
                        {
                            Id = Guid.NewGuid(),
                            StoreItemId = storeItem.Id,
                        };

                        user.ShoppingCart.Add(sc);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
