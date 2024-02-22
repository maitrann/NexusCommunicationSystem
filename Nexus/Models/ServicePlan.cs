using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("ServicePlan")]
    public class ServicePlan
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Decimal Price { get; set; }
        public string? Description { get; set; }
        public Boolean Status { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category ServicePlanCategory { get; set; }
        public string? Image { get; set; }
        public string? Options { get; set; }
    }
}