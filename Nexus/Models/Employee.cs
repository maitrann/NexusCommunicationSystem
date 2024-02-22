using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Boolean Status { get; set; }
        public int Position { get; set; }
    }
}
