using Business_Logic.Models;
using Repositorie.Entities.Users;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Processor
{
    public static class CommentProcessor
    {
        public static void AddComment(Guid userId, Guid storeItemId, string text)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            UserComment comment = new UserComment
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                Text = text,
            };

            unitOfWork.StoreItemRepository.AddComments(storeItemId, comment);
        }

        public static void AddCommentRating(CommentRatingModel model)
        {
            var commentRating = ConvertToCommentRating(model);

            if (commentRating == null)
                return;

            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            unitOfWork.UserCommentRepository.AddCommentRating(commentRating);
        }

        public static void UpdateCommentText(Guid commentId, string newText)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            unitOfWork.UserCommentRepository.Update(commentId, newText);
        }

        public static void RemoveComment(Guid commentId)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            unitOfWork.UserCommentRepository.Remove(commentId);
        }

        public static void UpdateCommentRating(CommentRatingModel model)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            if (model == null)
                return;

            var commentRating = ConvertToCommentRating(model);
            unitOfWork.CommentRatingRepository.Update(commentRating);
        }

        public static void RemoveCommentRating(Guid commentRatingId)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            unitOfWork.CommentRatingRepository.Remove(commentRatingId);
        }

        public static List<CommentRatingModel> GetCommentRating()
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();
            var commentRatings = unitOfWork.CommentRatingRepository.Get();

            if (commentRatings == null)
                return null;

            var models = ConvertToCommentRatingModel(commentRatings);

            return models;
        }

        #region ConvertFunctions
        public static CommentRatingModel GetCommentRating(Guid commentRatingId)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            var commentRating = unitOfWork.CommentRatingRepository.Get(commentRatingId);

            if (commentRating == null)
                return null;

            var model = ConvertToCommentRatingModel(commentRating);

            return model;

        }
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
                StoreItemId = userComment.StoreItem_Id,
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
                StoreItemId = commentRating.storeItemId,
                DataCreated = commentRating.DataCreated,
                Rating = commentRating.Rating,
                DownVote = commentRating.DownVote,
                UpVote = commentRating.UpVote,
                Report = commentRating.Reported,
                OpinionText = commentRating.ReportText,
            };

            return model;
        }

        public static List<CommentRatingModel> ConvertToCommentRatingModel(List<CommentRating> commentRatings)
        {
            if (commentRatings == null)
                return null;

            List<CommentRatingModel> models = new List<CommentRatingModel>();

            foreach (var commentRating in commentRatings)
            {
                var model = ConvertToCommentRatingModel(commentRating);
                models.Add(model);
            }

            return models;
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
                storeItemId = model.StoreItemId,
                DataCreated = model.DataCreated,
                Rating = model.Rating,
                DownVote = model.DownVote,
                UpVote = model.UpVote,
                Reported = model.Report,
                ReportText = model.OpinionText,
            };

            return commentRating;
        }

        #endregion
    }
}
