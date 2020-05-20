using Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Business_Logic.Models
{
    public class StoreItemModel : EntityModalBase
    {
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Display(Name = "Product Discription")]
        [Required(ErrorMessage = "This field is required")]
        public string Discription { get; set; }

        [Display(Name = "Brand Name")]
        [Required(ErrorMessage = "This field is required")]
        public string Brand { get; set; }

        [Display(Name = "Product Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public List<Byte[]> Images { get; set; }
        public List<SpecificationModel> Specifications { get; set; }
    }
}
