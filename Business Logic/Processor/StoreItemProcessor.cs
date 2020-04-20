using Business_Logic.Models;
using Repositorie.Entities.Base;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;

namespace Business_Logic.Processor
{
    public static class StoreItemProcessor
    {
        public static StoreItemModel ConvertStoreItemToModel(Guid id)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var result = unitOfWork.StoreItemRepository.Get(id);

            if (result == null)
                return null;

            StoreItemModel model = new StoreItemModel(result.Name,result.Discription, result.Brand, result.Price, result.Images, result.Specification);
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

        public static List<StoreItemModel> ConvertAllStoreItemToModel()
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var result = unitOfWork.StoreItemRepository.Get();

            if (result == null)
                return null;
            List<StoreItemModel> models = new List<StoreItemModel>();

            foreach (var storeItem in result)
            {
                StoreItemModel model = new StoreItemModel(storeItem.Name, storeItem.Discription, storeItem.Brand, storeItem.Price, storeItem.Images, storeItem.Specification);
                models.Add(model);
            }

            return models;
        }


    }
}
