using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public decimal TotalPrice { get; set; }
        public UserProfile UserProfile { get; set; } 
        public List<CartItem> Items { get; set; } 
    }
}