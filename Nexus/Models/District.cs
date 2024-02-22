using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("District")]
    public class District
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}