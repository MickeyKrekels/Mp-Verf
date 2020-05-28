using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Models
{
    public class CommentModel : EntityModalBase
    {
        public string Text { get; set; }
        public DateTime DataCreated { get; set; }

        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int ProductRating { get; set; } // 1 = 0.5 star - 2 = 1 star
        public Guid OwnerId { get; set; }
    }
}
