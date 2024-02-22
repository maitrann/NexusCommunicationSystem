using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("Feedback")]
    public class Feedback
    {
        public int Id { get; set; }
        public string ServicePlanID { get; set; }
        public string Content { get; set; }
    }
}