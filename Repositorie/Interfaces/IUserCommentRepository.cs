using Repositorie.Entities.Users;
using System;
using System.Collections.Generic;

namespace Repositorie.Interfaces.Repositories
{
    public interface IUserCommentRepository
    {
        List<UserComment> Get();
        UserComment Get(Guid id);
        List<UserComment> GetAllFromUser(Guid Userid);
        void Remove(Guid id);
        void Update(Guid id, string newComment);
        void UpdateRating(Guid id, int rating);
    }
}