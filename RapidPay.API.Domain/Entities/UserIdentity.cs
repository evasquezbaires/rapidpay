using System;

#nullable disable

namespace RapidPay.API.Domain.Entities
{
    public partial class UserIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
