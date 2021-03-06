﻿using Business_Logic.Models;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Processor
{
    public static class ShoppingCartProcessor
    {

        public static void AddToShoppingCart(Guid id, StoreItemModel storeItemModel)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            var StoreItem = StoreItemProcessor.ConvertModelToStoreItem(storeItemModel);
            var user = UserProcessor.GetUser(id);

            if (!(user is Customer))
                return;

            var customer = (Customer)user;

            if (StoreItem == null)
                return;

            ChangeStoreItemStock(StoreItem, -1);

            int amount = 0;

            for (int i = 0; i < customer.ShoppingCart.Count(); i++)
            {
                if (customer.ShoppingCart[i].StoreItemId == storeItemModel.Id)
                {
                    amount++;
                }
            }
            if (amount == 0)
            {
                var shoppingCart = ConvertStoreItemToShoppingCart(StoreItem);
                unitOfWork.CustomerRepository.AddToShoppingCart(id, shoppingCart);
            }
            else // there are multible items equal to storeItemModel 
            {
                //update storeItem amount
                var currenthoppingCart = customer.ShoppingCart.Where(x => x.StoreItemId == storeItemModel.Id).FirstOrDefault();
                int newAmount = currenthoppingCart.Amount + 1;

                unitOfWork.CustomerRepository.UpdateShoppingCart(id, currenthoppingCart.Id, newAmount);
            }

        }

        public static void RemoveFromShoppingCart(Guid id, ShoppingCartModel shoppingCartModel)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            var storeItem = StoreItemProcessor.GetStoreItembyId(shoppingCartModel.StoreItemId);

            if (storeItem == null)
                return;

            ChangeStoreItemStock(storeItem, shoppingCartModel.Amount);

            unitOfWork.CustomerRepository.RemoveFromShoppingCart(id, storeItem);
        }
        public static ShoppingCartModel GetShoppingModel(Guid id)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var ShoppingCart = unitOfWork.ShoppingCartRepository.Get(id);

            if (ShoppingCart == null)
                return null;

            var model = ConvertToShoppingCartModel(ShoppingCart);

            if (model == null)
                return null;

            return model;
        }
        public static void UpdateShoppingCart(ShoppingCartModel oldModel, ShoppingCartModel newModel)
        {
            var shoppingCart = ConvertToShoppingCart(newModel);

            if (shoppingCart == null)
                return;

            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            unitOfWork.ShoppingCartRepository.Update(shoppingCart);

            var storeItem = StoreItemProcessor.GetStoreItembyId(oldModel.StoreItemId);

            int newAmount = oldModel.Amount - newModel.Amount;
            ChangeStoreItemStock(storeItem, newAmount);
        }
        public static void ChangeStoreItemStock(StoreItem storeItem, int amount)
        {
            storeItem.InStock+= amount;
            StoreItemProcessor.UpdateStoreItem(storeItem);
        }

        #region Convert functions
        public static ShoppingCart ConvertToShoppingCart(ShoppingCartModel shoppingCartModel)
        {
            ShoppingCart shoppingCart = new ShoppingCart
            {
                Id = shoppingCartModel.Id,
                Amount = shoppingCartModel.Amount,
                StoreItemId = shoppingCartModel.Id,
            };

            return shoppingCart;
        }
        public static List<ShoppingCart> ConvertToShoppingCart(List<ShoppingCartModel> shoppingCartModel)
        {
            if (shoppingCartModel == null)
                return null;

            List<ShoppingCart> ShoppingCarts = new List<ShoppingCart>();
            foreach (var model in shoppingCartModel)
            {
                var shoppingCart = ConvertToShoppingCart(model);
                ShoppingCarts.Add(shoppingCart);
            }
            return ShoppingCarts;
        }
        public static ShoppingCart ConvertStoreItemToShoppingCart(StoreItem storeItem,int amount = 1)
        {
            ShoppingCart shoppingCart = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                StoreItemId = storeItem.Id,
            };

            return shoppingCart;
        }
        public static List<ShoppingCart> ConvertStoreItemToShoppingCart(List<StoreItem> StoreItems)
        {
            if (StoreItems == null)
                return null;

            List<ShoppingCart> ShoppingCarts = new List<ShoppingCart>();
            foreach (var storeItem in StoreItems)
            {
                var shoppingCart = ConvertStoreItemToShoppingCart(storeItem);
                ShoppingCarts.Add(shoppingCart);
            }
            return ShoppingCarts;
        }
        public static ShoppingCartModel ConvertToShoppingCartModel(ShoppingCart shoppingCart)
        {
            var storeItem = StoreItemProcessor.GetStoreItemModelbyId(shoppingCart.StoreItemId);

            if (storeItem == null)
                return null;

            ShoppingCartModel shoppingCartModel = new ShoppingCartModel
            {
                Id = shoppingCart.Id,
                Name = storeItem.Name,
                Discription = storeItem.Discription,
                Brand = storeItem.Brand,
                Price = storeItem.Price,
                Discount = storeItem.Discount,
                Images = storeItem.Images,
                Specifications = storeItem.Specifications,
                Comments = storeItem.Comments,

                Amount = shoppingCart.Amount,
                DataCreated = shoppingCart.DataCreated,
                StoreItemId = shoppingCart.StoreItemId,
            };
            return shoppingCartModel;
        }
        public static List<ShoppingCartModel> ConvertToShoppingCartModel(List<ShoppingCart> shoppingCarts)
        {
            if (shoppingCarts == null)
                return null;

            List<ShoppingCartModel> shoppingCartModels = new List<ShoppingCartModel>();
            foreach (var shoppingCart in shoppingCarts)
            {
                var model = ConvertToShoppingCartModel(shoppingCart);
                shoppingCartModels.Add(model);
            }
            return shoppingCartModels;
        }

        #endregion

    }
}
