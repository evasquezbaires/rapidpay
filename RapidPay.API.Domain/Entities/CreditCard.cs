using System;
using System.Collections.Generic;

#nullable disable

namespace RapidPay.API.Domain.Entities
{
    public partial class CreditCard
    {
        public CreditCard()
        {
            PaymentCards = new HashSet<PaymentCard>();
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string Cvv { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }

        public virtual ICollection<PaymentCard> PaymentCards { get; set; }
    }
}
