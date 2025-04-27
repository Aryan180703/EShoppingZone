using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Images { get; set; } 
        public string? Ratings { get; set; }
        public string? Reviews { get; set; }
        public string? Specifications { get; set; }
        public int OwnerId { get; set; } 
        public UserProfile Owner { get; set; } = null!;
    }
}