using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Base
{
    public class ShoppingCart : EntityModalBase
    {
        public Guid StoreItemId { get; set; }
    }
}
