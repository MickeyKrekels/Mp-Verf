using Core.Repositories.Entities;
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
        public decimal Price { get; set; }

        [Display(Name = "Product Discount")]
        [Range(0, 100,ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Discount { get; set; }

        [Display(Name = "Products InStock")]
        [Range(1, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int InStock { get; set; }
        public decimal PriceWithDiscount
        {
            get
            {
                if (Discount <= 0)
                {
                    return Price;
                }
                return Decimal.Round(Price - ((Discount / 100m) * Price),2);
            }
        }

        public List<ImageModel> Images { get; set; }
        public List<SpecificationModel> Specifications { get; set; }
        public List<CommentModel> Comments { get; set; }

    }
}
