using Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Base
{
    public class StoreItem : EntityModalBase
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }

        public virtual List<Specification> Specification { get; set; }
        public virtual List<StoreImage> Images { get; set; }
    }
}
