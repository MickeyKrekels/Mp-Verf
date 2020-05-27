using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Models
{
    public class CommentModel : EntityModalBase
    {
        public string Text { get; set; }
        public DateTime DataCreated { get; set; }
        public List<CommentModel> ChildComments { get; set; }
        public Guid OwnerId { get; set; }
    }
}
