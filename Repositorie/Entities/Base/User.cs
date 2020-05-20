using Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Base.Repositories
{
    public class User : EntityModalBase
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }

        public User()
        {
            CreationDate = DateTime.Now;
        }

    }
}
