using Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Base
{
    public class StoreImage : EntityModalBase
    {
        public Byte[] ImageData { get; set; }
    }
}
