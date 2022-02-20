namespace RapidPay.API.Domain.Models
{
    public class PaymentCardResponse
    {
        public int PaymentId { get; set; }

        public int CardId { get; set; }

        public decimal BalanceAmount { get; set; }
    }
}
