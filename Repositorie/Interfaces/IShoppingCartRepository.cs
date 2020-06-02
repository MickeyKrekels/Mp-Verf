using Repositorie.Entities.Base;
using System;
using System.Collections.Generic;

namespace Repositorie.Repositories.StoreItems
{
    public interface IShoppingCartRepository
    {
        void Add(List<ShoppingCart> shoppingCarts);
        void Add(ShoppingCart shoppingCart);
        void Update(ShoppingCart shoppingCart);
        List<ShoppingCart> Get();
        ShoppingCart Get(Guid id);
        List<ShoppingCart> GetByUser(Guid userId);
    }
}