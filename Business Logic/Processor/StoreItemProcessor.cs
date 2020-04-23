using Business_Logic.Models;
using Repositorie.Entities.Base;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Business_Logic.Processor
{
    public static class StoreItemProcessor
    {
        public static void CreateStoreItem(StoreItemModel model)
        {
            if (model == null)
                return;

            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();


            List<StoreImage> StoreImages = new List<StoreImage>();
            List<Specification> Specifications = new List<Specification>();
            if (model.Images != null)
            {
                foreach (var imageData in model.Images)
                {
                    var imageBytes = ConverToBytes(imageData);

                    if (imageBytes == null)
                        continue;

                    StoreImage storeImage = new StoreImage
                    {
                        Id = Guid.NewGuid(),
                        ImageData = imageBytes
                    };

                    StoreImages.Add(storeImage);
                }
            }
            if(model.Specifications != null)
            {
            foreach (var specificationModel in model.Specifications)
            {
                Specification specification = new Specification
                {
                    Id = Guid.NewGuid(),
                    Name = specificationModel.Name,
                    Description = specificationModel.Description
                    
                };

                Specifications.Add(specification);
            }
            }

            StoreItem storeItem = new StoreItem
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Brand = model.Brand,
                Discription = model.Discription,
                Price = model.Price,
                Images = StoreImages,
                Specification = Specifications
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
                };

                models.Add(model);
            }

            return models;
        }

        private static byte[] ConverToBytes(HttpPostedFileBase file)
        {
            if (file == null)
                return null;

            var length = file.InputStream.Length; //Length: 103050706
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }
            return fileData;
        }

    }
}
