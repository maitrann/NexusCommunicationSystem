namespace Nexus.Models
{
    public class RazorPayOrder
    {
        public string? OrderId { get; set; }
        public string? RazorPayAPIKey { get; set; }
        public int Amount { get; set; }
        public string? Currency { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public int ServicePlanId { get; set; }
        public int ExpiryDateId { get; set; }
    }
}
