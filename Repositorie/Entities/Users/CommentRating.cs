using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Users
{
    public class CommentRating : EntityModalBase
    {
        public Guid OwnerId { get; set; }
        public Guid CommentId { get; set; }
        public int Rating { get; set; }
        public bool UpVote { get; set; }
        public bool DownVote { get; set; }
        public DateTime DataCreated { get; set; }
        public CommentRating()
        {
            DataCreated = DateTime.Now;
        }

    }
}
