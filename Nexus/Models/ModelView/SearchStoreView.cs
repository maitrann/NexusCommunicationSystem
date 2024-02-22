namespace Nexus.Models.ModelView
{
    public class SearchStoreView
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DistrictID { get; set; }
        public string? DistrictName { get; set; }
        public string? Ward { get; set; }
        public bool Status { get; set; }
    }
}
