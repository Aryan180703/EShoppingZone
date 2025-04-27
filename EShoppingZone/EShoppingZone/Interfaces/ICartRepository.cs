using System;
using System.Collections.Generic;
using System.Linq;
using EShoppingZone.Models;

namespace EShoppingZone.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart> GetCartAsync(int profileId);
        Task<Product> GetProductAsync(int productId);
        Task UpdateCartAsync(Cart cart);
    }
}