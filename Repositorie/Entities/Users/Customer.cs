﻿using Repositorie.Base.Repositories;
using Repositorie.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorie.Entities.Users
{
    public class Customer : User
    {
        public virtual List<StoreItem> ShoppingCart { get; set; }
    }
}
