using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.DTOs.CartDTOs
{
    public class CartResponse
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemResponse> Items { get; set; } = new();
    }
}