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
        public static void AddComment(Guid userId, Guid StoreItemId, string text)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            UserComment comment = new UserComment
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                Text = text,
            };

            unitOfWork.StoreItemRepository.AddComments(StoreItemId, comment);
        }

        public static void AddCommentRating(CommentRatingModel model)
        {
            var commentRating = ConvertToCommentRating(model);

            if (commentRating == null)
                return;

            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            unitOfWork.UserCommentRepository.AddCommentRating(commentRating);
        }

        public static void UpdateCommentText(Guid CommentId, string newText)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            unitOfWork.UserCommentRepository.Update(CommentId, newText);
        }

        public static void RemoveComment(Guid CommentId)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            unitOfWork.UserCommentRepository.Remove(CommentId);
        }

        #region ConvertFunctions
        public static CommentModel ConvertToCommentModel(UserComment userComment)
        {
            List<CommentRatingModel> commentRatings = new List<CommentRatingModel>();

            if (userComment.CommentRatings != null)
            {
                foreach (var rating in userComment.CommentRatings)
                {
                    var model = ConvertToCommentRatingModel(rating);
                    commentRatings.Add(model);

                }
            }

            CommentModel commentModel = new CommentModel
            {
                Id = userComment.Id,
                Text = userComment.Text,
                DataCreated = userComment.DataCreated,
                OwnerId = userComment.OwnerId,
                commentRatings = commentRatings,
            };
            return commentModel;
        }

        public static CommentModel GetUserComment(Guid userId)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var comment = unitOfWork.UserCommentRepository.Get(userId);

            if (comment == null)
                return null;

            var model = ConvertToCommentModel(comment);

            return model;
        }

        public static CommentRatingModel ConvertToCommentRatingModel(CommentRating commentRating)
        {
            if (commentRating == null)
                return null;

            CommentRatingModel model = new CommentRatingModel()
            {
                Id = commentRating.Id,
                CommentId = commentRating.CommentId,
                OwnerId = commentRating.OwnerId,
                DataCreated = commentRating.DataCreated,
                Rating = commentRating.Rating,
                DownVote = commentRating.DownVote,
                UpVote = commentRating.UpVote,
                Report = commentRating.Reported,
            };

            return model;
        }

        public static CommentRating ConvertToCommentRating(CommentRatingModel model)
        {
            if (model == null)
                return null;

            CommentRating commentRating = new CommentRating()
            {
                Id = model.Id,
                CommentId = model.CommentId,
                OwnerId = model.OwnerId,
                DataCreated = model.DataCreated,
                Rating = model.Rating,
                DownVote = model.DownVote,
                UpVote = model.UpVote,
                Reported = model.Report,
            };

            return commentRating;
        }

        #endregion
    }
}
