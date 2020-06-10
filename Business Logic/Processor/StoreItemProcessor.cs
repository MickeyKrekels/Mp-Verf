using Business_Logic.Models;
using Common.ExtensionMethods;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Business_Logic.Processor
{
    public static class StoreItemProcessor
    {
        #region CRUD StoreItem

        public static void CreateStoreItem(StoreItemModel model)
        {
            if (model == null)
                return;

            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            var storeItem = ConvertModelToStoreItem(model);
     

            unitOfWork.StoreItemRepository.Add(storeItem);
        }

        public static void RemoveStoreItem(StoreItemModel model)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            StoreItem storeItem = ConvertModelToStoreItem(model);

            if (storeItem == null)
                return;

            unitOfWork.StoreItemRepository.Remove(model.Id);
        }

        public static void UpdateStoreItemImages(StoreItemModel model)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            StoreItem storeItem = ConvertModelToStoreItem(model);

            if (storeItem == null)
                return;

            unitOfWork.StoreItemRepository.UpdateStoreImages(storeItem);
        }
        public static void UpdateStoreItem(StoreItemModel model)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            StoreItem storeItem = ConvertModelToStoreItem(model);

            if (storeItem == null)
                return;

            unitOfWork.StoreItemRepository.Update(storeItem);
        }
        public static void UpdateStoreItem(StoreItem storeItem)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            if (storeItem == null)
                return;

            unitOfWork.StoreItemRepository.Update(storeItem);
        }

        #endregion

        #region ConvertFunctions

        public static StoreItemModel GetStoreItemModelbyId(Guid id)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var result = unitOfWork.StoreItemRepository.Get(id);

            if (result == null)
                return null;

            StoreItemModel model = ConvertStoreItemToModel(result);

            return model;
        }
        public static StoreItem GetStoreItembyId(Guid id)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var result = unitOfWork.StoreItemRepository.Get(id);

            if (result == null)
                return null;

            return result;
        }

        public static List<StoreItemModel> GetStoreItemModelbyId(List<Guid> ids)
        {
            List<StoreItemModel> models = new List<StoreItemModel>();

            if (ids == null)
                return null;

            foreach (var id in ids)
            {
                var model = GetStoreItemModelbyId(id);
                models.Add(model);
            }

            return models;
        }


        public static StoreItem ConvertModelToStoreItem(StoreItemModel model)
        {

            if (model == null)
                return null;

            List<StoreImage> StoreImages = new List<StoreImage>();
            List<Specification> Specifications = new List<Specification>();

            if (model.Images != null)
            {
                foreach (var imageData in model.Images)
                {
                    StoreImage storeImage = new StoreImage
                    {
                        Id = imageData.Id,
                        ImageData = imageData.Image,
                    };

                    StoreImages.Add(storeImage);
                }
            }
            if (model.Specifications != null)
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
                Id = model.Id,
                Name = model.Name,
                Discription = model.Discription,
                Price = model.Price,
                DiscountPercentage = model.Discount,
                Brand = model.Brand,
                Images = StoreImages,
                InStock = model.InStock,
                Specification = Specifications
            };

            return storeItem;
        }
    

        public static List<StoreItem> ConvertModelToStoreItem(List<StoreItemModel> models)
        {

            if (models == null)
                return null;

            List<StoreItem> StoreItems = new List<StoreItem>();

            foreach (var model in models)
            {
                var storeItem = ConvertModelToStoreItem(model);
                StoreItems.Add(storeItem);
            }

            return StoreItems;

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
                StoreItemModel model = ConvertStoreItemToModel(storeItem);

                models.Add(model);
            }

            return models;
        }


        public static StoreItemModel ConvertStoreItemToModel(StoreItem storeItem)
        {

            if (storeItem == null)
                return null;

            List<ImageModel> storeImages = new List<ImageModel>();
            List<SpecificationModel> specifications = new List<SpecificationModel>();
            List<CommentModel> comments = new List<CommentModel>();

            if (storeItem.Images != null)
            {
                foreach (var image in storeItem.Images)
                {
                    var imageFileBase = ConvertToImageModel(image);

                    if (imageFileBase == null)
                        continue;

                    storeImages.Add(imageFileBase);
                }
            }

            if (storeItem.Specification != null)
            {
                foreach (var specification in storeItem.Specification)
                {
                    SpecificationModel specificationModel = ConvertToSpecificationModel(specification);
                    specifications.Add(specificationModel);
                }
            }

            if (storeItem.UserComments != null)
            {
                foreach (var userComment in storeItem.UserComments)
                {
                    CommentModel commentModel = CommentProcessor.ConvertToCommentModel(userComment);
                    comments.Add(commentModel);
                }
            }

            StoreItemModel model = new StoreItemModel
            {
                Id = storeItem.Id,
                Name = storeItem.Name,
                Discription = storeItem.Discription,
                Brand = storeItem.Brand,
                Price = storeItem.Price,
                Discount = storeItem.DiscountPercentage,
                Images = storeImages,
                InStock = storeItem.InStock,
                Specifications = specifications,
                Comments = comments,
            };

            return model;
        }

        public static List<StoreItemModel> ConvertStoreItemToModel(List<StoreItem> storeItems)
        {
            List<StoreItemModel> models = new List<StoreItemModel>();

            foreach (var storeItem in storeItems)
            {
                var model = ConvertStoreItemToModel(storeItem);
                models.Add(model);
            }

            return models;
        }     

        private static ImageModel ConvertToImageModel(StoreImage storeImage)
        {
            ImageModel imageModel = new ImageModel
            {
                Id = storeImage.Id,
                Image = storeImage.ImageData,
            };

            return imageModel;
        }

        private static SpecificationModel ConvertToSpecificationModel(Specification specification)
        {
            SpecificationModel specificationModel = new SpecificationModel
            {
                Name = specification.Name,
                Description = specification.Description
            };

            return specificationModel;
        }

        public static List<ImageModel> ConvertToImageModel(List<byte[]> imageDatas)
        {
            List<ImageModel> models = new List<ImageModel>();

            foreach (var imageData in imageDatas)
            {
               var model = ConvertToImageModel(imageData);
                models.Add(model);
            }
            return models;
        }
        public static ImageModel ConvertToImageModel(byte[] imageData)
        {
            ImageModel imageModel = new ImageModel
            {
                Id = Guid.NewGuid(),
                Image = imageData,
            };
            return imageModel;
        }

        public static byte[] ConverToBytes(HttpPostedFileBase file)
        {
            if (file == null)
                return null;

            //var length = file.InputStream.Length; //Length: 103050706
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }
            return fileData;
        }

        public static List<byte[]> ConverToBytes(List<HttpPostedFileBase> files)
        {
            if (files == null)
                return null;

            List<Byte[]> imageData = new List<Byte[]>();
            foreach (var imageFileBase in files)
            {
                var bytes = ConverToBytes(imageFileBase);
                imageData.Add(bytes);
            }
            return imageData;
        }

        #endregion

    }
}
