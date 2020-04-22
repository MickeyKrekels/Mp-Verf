using Business_Logic.Models;
using Repositorie.Entities.Base;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;

namespace Business_Logic.Processor
{
    public static class StoreItemProcessor
    {
        public static void CreateStoreItem(StoreItemModel model)
        {
            if (model == null)
                return;

            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            StoreItem storeItem = new StoreItem
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Brand = model.Brand,
                Discription = model.Discription,
                Price = model.Price,
                Images = model.Images,
                Specification = model.Specifications
            };

            unitOfWork.StoreItemRepository.Add(storeItem);
        }

        public static StoreItemModel ConvertStoreItemToModel(Guid id)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var result = unitOfWork.StoreItemRepository.Get(id);

            if (result == null)
                return null;

            StoreItemModel model = new StoreItemModel
            {
                Id = result.Id,
                Name = result.Name,
                Discription = result.Discription,
                Brand = result.Brand,
                Price = result.Price,
                Images = result.Images,
                Specifications = result.Specification
            };

            return model;
        }

        public static StoreItem ConvertModelToStoreItem(StoreItemModel model)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            if (model == null)
                return null;

            StoreItem storeItem = new StoreItem {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Discription = model.Discription,
                Price = model.Price,
                Brand = model.Brand,
                Images = model.Images,
                Specification = model.Specifications
            };

            return storeItem;
        }

        public static List<StoreItemModel> ConvertAllStoreItemToModels()
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var result = unitOfWork.StoreItemRepository.Get();

            if (result == null)
                return null;
            List<StoreItemModel> models = new List<StoreItemModel>();

            foreach (var storeItem in result)
            {
                StoreItemModel model = new StoreItemModel
                {
                    Id = storeItem.Id,
                    Name = storeItem.Name,
                    Discription = storeItem.Discription,
                    Brand = storeItem.Brand,
                    Price = storeItem.Price,
                    Images = storeItem.Images,
                    Specifications = storeItem.Specification
                };

                models.Add(model);
            }

            return models;
        }


    }
}
