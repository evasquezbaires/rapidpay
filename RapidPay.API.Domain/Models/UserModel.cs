using System.ComponentModel.DataAnnotations;

namespace RapidPay.API.Domain.Models
{
    public class UserModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(20, ErrorMessage = "{0} is too long.")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(20, ErrorMessage = "{0} is too long.")]
        public string Password { get; set; }
    }
}
