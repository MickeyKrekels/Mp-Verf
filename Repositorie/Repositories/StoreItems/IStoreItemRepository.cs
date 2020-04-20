using System;
using System.Collections.Generic;
using Repositorie.Entities.Base;

namespace Repositorie.Repositories.StoreItems
{
    public interface IStoreItemRepository
    {
        void Add(StoreItem storeItem);
        List<StoreItem> Get();
        StoreItem Get(Guid id);
        StoreItem Get(Specification specification);
        void Remove(Guid id);
        void Update(Guid id, StoreItem storeItem);
    }
}