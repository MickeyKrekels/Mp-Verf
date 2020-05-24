using Repositorie.Base.Repositories;
using Repositorie.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Users
{
    public class Customer : User
    {
        [ForeignKey("Customer_Id")]
        public virtual List<ShoppingCart> ShoppingCart { get; set; }
    }
}
