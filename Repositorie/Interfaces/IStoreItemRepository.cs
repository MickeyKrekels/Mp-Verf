using System;
using System.Collections.Generic;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;

namespace Repositorie.Repositories.StoreItems
{
    public interface IStoreItemRepository
    {
        void Add(StoreItem storeItem);
        List<StoreItem> Get();
        StoreItem Get(Guid id);
        StoreItem Get(Specification specification);
        void Remove(Guid id);
        void UpdateStoreImages(StoreItem storeItem);
        void AddComments(Guid id, UserComment comment);
        void Update(StoreItem storeItem);
    }
}