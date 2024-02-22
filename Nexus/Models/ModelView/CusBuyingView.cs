namespace Nexus.Models.ModelView
{
    public class CusBuyingView
    {
        public int id { get; set; }
        public int CustomerID { get; set; }
        public int ServicePlanID { get; set; }
        public int OrderID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }

        //Other
        public string? Name { get; set; }
        public string? Image { get; set; }
        public Decimal? Price { get; set; }
        public int Quantity { get; set; }
        public int ExpiryDateID { get; set; }
        public decimal Total { get; set; }
        public int CategoryID { get; set; }
        public string? Description { get; set; }
        public string? Option { get; set; }
        public Boolean? Status { get; set; }

    }
}
