﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}