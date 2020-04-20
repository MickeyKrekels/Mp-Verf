﻿using Core.Shared.Entities;
using Repositorie.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Models
{
    public class StoreItemModel : EntityModalBase
    {

        public string Name { get; set; }
        public string Discription { get; set; }
        public string Brand { get; set; }
        public float Price { get; set; }

        public List<StoreImage> Images { get; set; }
        public List<Specification> Specifications { get; set; }

        public StoreItemModel(string name, string discription, string brand, 
            float price, List<StoreImage> images, List<Specification> specifications)
        {
            Id = Guid.NewGuid();
            Name = name;
            Discription = discription;
            Brand = brand;
            Price = price;
            Images = images;
            Specifications = specifications;
        }

        
    }
}
