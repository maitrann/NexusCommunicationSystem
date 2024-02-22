using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("Position")]
    public class Position
    {
        public int Id { get; set; } 
        public string Name { get; set; }

    }
}