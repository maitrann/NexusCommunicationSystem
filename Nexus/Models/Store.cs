using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("Store")]
    public class Store
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DistrictID { get; set; }
        public District? StoreDistrict { get; set; }
        public string? Ward { get; set; }
        public bool Status { get; set; }
    }
}