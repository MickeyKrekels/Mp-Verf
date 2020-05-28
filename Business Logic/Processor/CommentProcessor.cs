using Business_Logic.Models;
using Repositorie.Entities.Users;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Processor
{
    public static class CommentProcessor
    {
        public static void AddComment(Guid userId, Guid StoreItemId, CommentModel commentModel)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            var storeItem = StoreItemProcessor.GetStoreItembyId(StoreItemId);

            UserComment comment = new UserComment
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                Text = commentModel.Text,
                ProductRating = commentModel.ProductRating,
            };
            storeItem.UserComments.Add(comment);

            unitOfWork.StoreItemRepository.UpdateStoreImages(storeItem);
        }

        public static void UpdateComment(Guid CommentId, string newText)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

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

    }
}
