using System.Drawing;

namespace Nexus.Models.ModelView
{
    public class ServicePlanView
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public bool Status { get; set; }
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? Options { get; set; }
        public int OptionsID { get; set; }
        public string? OptionsName { get; set; }

    }
}
