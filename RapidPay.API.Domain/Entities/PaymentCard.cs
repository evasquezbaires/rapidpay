using System;

#nullable disable

namespace RapidPay.API.Domain.Entities
{
    public partial class PaymentCard
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }

        public virtual CreditCard CreditCard { get; set; }
    }
}
