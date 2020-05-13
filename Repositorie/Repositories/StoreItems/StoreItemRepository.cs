using Repositorie.DbContexts;
using Repositorie.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Repositories.StoreItems
{
    public class StoreItemRepository : IStoreItemRepository
    {
        public MPVerfDB context;

        public StoreItemRepository(MPVerfDB context)
        {
            this.context = context;
        }

        //Create
        public void Add(StoreItem storeItem)
        {
            context.StoreItem.Add(storeItem);

            context.SaveChanges();
        }

        //Read
        public List<StoreItem> Get()
        {
            var result = context.StoreItem.ToList();

            return result;
        }
        public StoreItem Get(Guid id)
        {
           var StoreItems = context.StoreItem.Where(x => x.Id == id).First();

            return StoreItems;
        }
        public StoreItem Get(Specification specification)
        {
            var StoreItems = context.
                StoreItem.
                Where(x => x.Specification.Contains(specification)).
                First();

            return StoreItems;
        }

        //Update
        public void Update(StoreItem storeItem)
        {
            var result = context.StoreItem.Where(x => x.Id == storeItem.Id).First();

            //log error
            if (result == null)
                return;
            result.Images = storeItem.Images;
            context.Entry(result).CurrentValues.SetValues(storeItem);
            context.SaveChanges();
        }

        //Delete
        public void Remove(Guid id)
        {
            var result = context.StoreItem.Where(x => x.Id == id).First();

            //log error
            if (result == null)
                return;

            context.StoreItem.Remove(result);
            context.SaveChanges();
        }
    }
}
