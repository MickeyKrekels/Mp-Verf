using Repositorie.DbContexts;
using Repositorie.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Repositories.StoreItems
{
    public class ShoppingCartRepository
    {
        public MPVerfDB context;

        public ShoppingCartRepository(MPVerfDB context)
        {
            this.context = context;
        }

        public void Add(ShoppingCart shoppingCart)
        {
            context.ShoppingCart.Add(shoppingCart);
            context.SaveChanges();
        }
        public void Add(List<ShoppingCart> shoppingCarts)
        {
            foreach (var shoppingCart in shoppingCarts)
            {
                Add(shoppingCart);
            }
            context.SaveChanges();
        }
        public List<ShoppingCart> Get()
        {
            var shoppingCarts = context.ShoppingCart.ToList();
            return shoppingCarts;
        }
        public ShoppingCart Get(Guid id)
        {
            var shoppingCart = context.ShoppingCart.Where(x=> x.Id == id).SingleOrDefault();
            return shoppingCart;
        }

        public List<ShoppingCart> GetByUser(Guid userId)
        {
            var shoppingCart = context.ShoppingCart.Where(x => x.Id == userId).ToList();
            return shoppingCart;
        }

    }
}
