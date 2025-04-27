using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.DTOs.ProductDTOs
{
    public class ProductResponse
    {
        public int Id {get; set;}
        public string Name { get; set; } 
        public string? Type { get; set; } 
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Images { get; set; } 
        public string? Ratings { get; set; } 
        public string? Reviews { get; set; } 
        public string? Specifications { get; set; } 
    }
}