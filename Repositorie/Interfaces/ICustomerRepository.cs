﻿using System;
using System.Collections.Generic;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;

namespace Repositorie.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        List<Customer> Get();
        void AddToShoppingCart(Guid id, ShoppingCart shoppingCart);
        void RemoveFromShoppingCart(Guid id, StoreItem storeItems);
        void UpdateShoppingCart(Guid id, Guid shoppingCartId, int amount);
    }
}