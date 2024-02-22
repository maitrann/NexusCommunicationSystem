namespace Nexus.Models.ModelView
{
    public class StorageView
    {
        public int IdBill { get; set; }
        public int? OrderID { get; set; }

        public int IdServicePlan { get; set; }
        public string? NameService { get; set; }
        public string? OrderDate { get; set; }
        public string ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public string? ExchangeDate { get; set; }
        public int? QuantityChange { get; set; }
        public string? Reason { get; set; }
        public bool StatusChange { get; set; }

        public decimal TheAmountPaid { get; set; }
        public decimal Total { get; set; }
        public bool StatusPaid { get; set; }
        public int StatusService { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public string? IDPayment { get; set; }
        public string? RegistrationDate { get; set; }
        public string? ConnectionDate { get; set; }
        public string? ExpirationDate { get; set; }
    }
}
