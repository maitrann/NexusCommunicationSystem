using System.ComponentModel.DataAnnotations;

namespace Nexus.Models
{
    public class PaymentRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        [Required]
        public int Amount { get; set; }
        public int ServicePlanId { get; set; }
        public int ExpiryDateId { get; set; }
    }
}
