using System.ComponentModel.DataAnnotations;

namespace RapidPay.API.Domain.Models
{
    public class CreditCardModel
    {
        [Display(Name = "Card number")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(15, ErrorMessage = "{0} is too long.")]
        [MinLength(15, ErrorMessage = "{0} is too short.")]
        [RegularExpression(@"^([0-9]+)$", ErrorMessage = "Only numeric characters are allowed.")]
        public string CardNumber { get; set; }

        [Display(Name = "Card holder name")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(40, ErrorMessage = "{0} is too long.")]
        public string CardHolder { get; set; }

        [Display(Name = "Card month expiration")]
        [Required(ErrorMessage = "{0} is required.")]
        [Range(1, 12, ErrorMessage = "{0} is out of range allowed (1-12).")]
        public int ExpirationMonth { get; set; }

        [Display(Name = "Card year expiration")]
        [Required(ErrorMessage = "{0} is required.")]
        [Range(0, 99, ErrorMessage = "{0} is out of range allowed (0-99).")]
        public int ExpirationYear { get; set; }

        [Display(Name = "Code verification")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(5, ErrorMessage = "{0} is too long.")]
        [RegularExpression(@"^([0-9]+)$", ErrorMessage = "Only numeric characters are allowed.")]
        public string Cvv { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "{0} is required.")]
        [Range(100, 100000, ErrorMessage = "{0} is out of range allowed ($100 - $100.000).")]
        public decimal TotalAmount { get; set; }
    }
}
