using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Core.Repositories.Entities;

namespace Business_Logic.Models
{
    public class UserModel : EntityModalBase
    {
        [Display (Name = "Full Name")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password needs to be at least 8 characters long")]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "The Password and ConfirmPassword must match.") ]
        public string ConfirmPassword { get; set; }

        public List<StoreItemModel> ShoppingCart { get; set; }

}
}
