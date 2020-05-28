using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Users
{
    public class UserComment : EntityModalBase
    {
        public string Text { get; set; }
        public DateTime DataCreated { get; set; }
        public Guid OwnerId { get; set; }
        public int ProductRating { get; set; }
        public UserComment()
        {
            DataCreated = DateTime.Now;
        }

    }
}
