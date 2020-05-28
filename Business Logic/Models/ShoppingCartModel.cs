using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Models
{
    public class ShoppingCartModel : StoreItemModel
    {
        [Display(Name = "Product Amount")]
        [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Amount { get; set; }
        public DateTime DataCreated { get; set; }

        public Guid StoreItemId { get; set; }

        public decimal PriceTimesAmount
        {
            get
            {
                if (Discount <= 0)
                {
                    return Price;
                }
                return PriceWithDiscount * Amount;
            }
        }
    }
}
