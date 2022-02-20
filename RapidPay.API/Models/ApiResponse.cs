using System.Collections.Generic;

namespace RapidPay.API.Models
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
