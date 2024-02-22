﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("Customer")]
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}