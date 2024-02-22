using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("ProductExchange")]
    public class ProductExchange
    {
        public int ID { get; set; }
        public int BillID { get; set; }
        public Boolean Status { get; set; }
        public DateTime? ExchangeDate { get; set; }
        public int QuantityChange { get; set; }
        public string? Reason { get; set; }
    }
}