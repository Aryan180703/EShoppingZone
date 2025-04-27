using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required] 
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Cart Cart { get; set; } 
        public Product Product { get; set; } 
    }
}