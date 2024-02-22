using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("Bill")]
    public class Bill
    {
        public int Id { get; set; }
        public string? IDPayment { get; set; }
        public string? OrderID { get; set; }
        //[ForeignKey("OrderID")]
        //public Order BillOrder { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ConnectionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}