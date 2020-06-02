using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Base
{
    public class ShoppingCart : EntityModalBase
    {
        public Guid StoreItemId { get; set; }
        public Guid Customer_Id { get; set; }
        public int Amount { get; set; }
        public DateTime DataCreated { get; set; }
        public ShoppingCart()
        {
            DataCreated = DateTime.Now;
        }
    }
}
