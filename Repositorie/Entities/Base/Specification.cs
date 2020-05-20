using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Base
{
    public class Specification : EntityModalBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
