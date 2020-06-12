using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Models
{
    public class CommentRatingModel : EntityModalBase
    {
        public Guid OwnerId { get; set; }
        public Guid CommentId { get; set; }
        public bool UpVote { get; set; }

        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int ProductRating { get; set; } //input player // 1 = 0.5 star - 2 = 1 star
        public bool DownVote { get; set; }
        public DateTime DataCreated { get; set; }
    }
}
