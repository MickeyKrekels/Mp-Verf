﻿using Repositorie.DbContexts;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;
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
            var StoreItems = context.StoreItem.Where(x => x.Id == id).FirstOrDefault();

            if (StoreItems == null)
                return null;

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
        public void UpdateStoreImages(StoreItem storeItem)
        {
            var result = context.StoreItem.Where(x => x.Id == storeItem.Id).First();

            //log error
            if (result == null)
                return;

            result.Images = storeItem.Images;
            context.Entry(result).CurrentValues.SetValues(storeItem);
            context.SaveChanges();
        }
        public void AddComments(Guid id,UserComment comment)
        {
            var result = context.StoreItem.Where(x => x.Id == id).First();

            //log error
            if (result == null)
                return;

            result.UserComments.Add(comment);
            context.SaveChanges();
        }
        public void Update(StoreItem storeItem)
        {
            var result = context.StoreItem.Where(x => x.Id == storeItem.Id).FirstOrDefault();

            //log error
            if (result == null)
                return;

            context.Entry(result).CurrentValues.SetValues(storeItem);
            context.SaveChanges();
        }

        //Delete
        public void Remove(Guid id)
        {
            var storeItem = Get(id);
            var images = storeItem.Images;
            var comments = storeItem.UserComments;
            //log error
            if (storeItem == null)
                return;

            if (images != null && images.Count > 0)
            {
                for (int i = 0; i < images.Count; i++)
                {
                    context.StoreImage.Remove(images[i]);
                }
            }
            if (comments != null && comments.Count > 0)
            {
                for (int i = 0; i < comments.Count; i++)
                {
                    context.UserComment.Remove(comments[i]);
                }
            }

            context.StoreItem.Remove(storeItem);
            context.SaveChanges();
        }
    }
}
