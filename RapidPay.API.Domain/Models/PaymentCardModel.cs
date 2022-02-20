using System.ComponentModel.DataAnnotations;

namespace RapidPay.API.Domain.Models
{
    public class PaymentCardModel
    {
        [Display(Name = "Card Id")]
        [Required(ErrorMessage = "{0} is required.")]
        public int CardId { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public decimal Amount { get; set; }
    }
}
