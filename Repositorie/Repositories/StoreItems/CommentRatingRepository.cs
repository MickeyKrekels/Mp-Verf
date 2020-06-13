using Repositorie.DbContexts;
using Repositorie.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Repositories.StoreItems
{
    public class CommentRatingRepository : ICommentRatingRepository
    {
        public MPVerfDB context;

        public CommentRatingRepository(MPVerfDB context)
        {
            this.context = context;
        }

        public CommentRating Get(Guid id)
        {
            var result = context.CommentRating.Where(x => x.Id == id).FirstOrDefault();

            if (result == null)
                return null;

            return result;
        }
        public List<CommentRating> Get()
        {
            var result = context.CommentRating.ToList();

            if (result == null)
                return null;

            return result;
        }
        public void Add(CommentRating commentRating)
        {
            if (commentRating == null)
                return;

            context.CommentRating.Add(commentRating);
            context.SaveChanges();
        }
        public void Remove(Guid id)
        {
            var result = context.CommentRating.Where(x => x.Id == id).FirstOrDefault();

            if (result == null)
                return;

            context.CommentRating.Remove(result);
            context.SaveChanges();
        }
        public void Update(CommentRating commentRating)
        {
            var result = context.CommentRating.Where(x => x.Id == commentRating.Id).FirstOrDefault();

            if (result == null)
                return;
            result.Rating = commentRating.Rating;
            result.DownVote = commentRating.DownVote;
            result.UpVote = commentRating.UpVote;

            result.Reported = commentRating.Reported;
            result.ReportText = commentRating.ReportText;

            context.SaveChanges();
        }
    }
}
