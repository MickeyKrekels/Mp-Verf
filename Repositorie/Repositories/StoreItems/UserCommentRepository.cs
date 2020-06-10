using Repositorie.DbContexts;
using Repositorie.Entities.Users;
using Repositorie.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorie.Repositories.StoreItems
{
    public class UserCommentRepository : IUserCommentRepository
    {
        public MPVerfDB context;

        public UserCommentRepository(MPVerfDB context)
        {
            this.context = context;
        }

        public UserComment Get(Guid id)
        {
            var comment = context.UserComment.Where(x => x.Id == id).FirstOrDefault();

            if (comment == null)
                return null;

            return comment;
        }
        public List<UserComment> Get()
        {
            var comments = context.UserComment.ToList();

            if (comments == null)
                return null;

            return comments;
        }
        public List<UserComment> GetAllFromUser(Guid Userid)
        {
            var comments = context.UserComment.Where(x => x.OwnerId == Userid).ToList();

            if (comments == null)
                return null;

            return comments;
        }

        public void Update(Guid id, string newCommentText)
        {
            var comment = context.UserComment.Where(x => x.Id == id).FirstOrDefault();

            if (comment == null)
                return;

            comment.Text = newCommentText;
            context.SaveChanges();
        }
        public void UpdateRating(Guid id, int rating)
        {
            var comment = context.UserComment.Where(x => x.Id == id).FirstOrDefault();

            if (comment == null)
                return;

            comment.ProductRating += rating;
            comment.TimesVoted++;

            context.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var comment = context.UserComment.Where(x => x.Id == id).FirstOrDefault();

            if (comment == null)
                return;

            context.UserComment.Remove(comment);
            context.SaveChanges();

        }
    }
}
