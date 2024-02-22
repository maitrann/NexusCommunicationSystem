﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("ExpiryDate")]
    public class ExpiryDate
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
