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

        public static void EditStoreItem(StoreItemModel model)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            StoreItem storeItem = ConvertModelToStoreItem(model);

            if (storeItem == null)
                return;

            unitOfWork.StoreItemRepository.Update(storeItem);
        }


        #endregion

        #region CRUD Comments

        public static void AddComment(Guid userId, Guid storeItemId, string text)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            var storeItem = GetStoreItembyId(storeItemId);

            UserComment comment = new UserComment
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                Text = text,
                ChildComments = new List<UserComment>(),       
            };
            storeItem.UserComments.Add(comment);

            unitOfWork.StoreItemRepository.Update(storeItem);
        }

        public static void UpdateComment(Guid CommentId, Guid storeItemId, string newText)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            var storeItem = GetStoreItembyId(storeItemId);

            unitOfWork.UserCommentRepository.Update(CommentId, newText);
        }

        public static void RemoveComment(Guid CommentId)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            unitOfWork.UserCommentRepository.Remove(CommentId);
        }
        public static void GetUserComments(Guid userId)
        {

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
                        Id = Guid.NewGuid(),
                        ImageData = imageData
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

            List<Byte[]> storeImages = new List<Byte[]>();
            List<SpecificationModel> specifications = new List<SpecificationModel>();
            List<CommentModel> comments = new List<CommentModel>();

            if (storeItem.Images != null)
            {
                foreach (var image in storeItem.Images)
                {
                    var imageFileBase = image.ImageData;

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
                    CommentModel commentModel = ConvertToCommentModel(userComment);
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
        private static CommentModel ConvertToCommentModel(UserComment userComment)
        {

            CommentModel commentModel = new CommentModel
            {
                Id = userComment.Id,
                Text = userComment.Text,
                DataCreated = userComment.DataCreated,
                //ChildComments = userComment.ChildComments,
                OwnerId = userComment.OwnerId,
            };
            return commentModel;
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
