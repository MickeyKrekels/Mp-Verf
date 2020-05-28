using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Models
{
    public class ImageModel : EntityModalBase
    {
        public Byte[] Image { get; set; }
    }
}
