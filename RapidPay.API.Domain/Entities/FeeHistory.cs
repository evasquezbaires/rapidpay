using System;

#nullable disable

namespace RapidPay.API.Domain.Entities
{
    public partial class FeeHistory
    {
        public int Id { get; set; }
        public decimal FeeRate { get; set; }
        public decimal FeeExchange { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
    }
}
