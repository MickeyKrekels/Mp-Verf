using Repositorie.Entities.Users;
using System;
using System.Collections.Generic;

namespace Repositorie.Repositories.StoreItems
{
    public interface ICommentRatingRepository
    {
        void Add(CommentRating commentRating);
        List<CommentRating> Get();
        CommentRating Get(Guid id);
        void Remove(Guid id);
        void Update(CommentRating commentRating);
    }
}