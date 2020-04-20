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
        public float  Price { get; set; }

        public List<Specification> Specification { get; set; }
        public List<StoreImage> Images { get; set; }
    }
}
