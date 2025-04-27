using System.Threading.Tasks;
using EShoppingZone.Data;
using EShoppingZone.Interfaces;
using EShoppingZone.Models;
using Microsoft.EntityFrameworkCore;

namespace EShoppingZone.Repository

{
    public class CartRepository : ICartRepository
    {
        private readonly EShoppingZoneDBContext _context;

        public CartRepository(EShoppingZoneDBContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> GetCartAsync(int profileId)
        {
            return await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserProfileId == profileId);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}