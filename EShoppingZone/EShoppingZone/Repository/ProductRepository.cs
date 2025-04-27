using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShoppingZone.Data;
using EShoppingZone.DTOs.ProductDTOs;
using EShoppingZone.Interfaces;
using EShoppingZone.Models;
using Microsoft.EntityFrameworkCore;

namespace EShoppingZone.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EShoppingZoneDBContext _context;
        
        public ProductRepository(EShoppingZoneDBContext context){
            _context = context;
        }
        public async Task<Product> AddProduct(Product product){
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetMerchantProducts(int profileId){
            var products = await _context.Products.Where(a => a.OwnerId == profileId).Select(a => a).ToListAsync();
            return products;
        }

        public async Task<Product> GetProduct(int productId){
            var product = await _context.Products.FirstOrDefaultAsync(a => a.Id == productId);
            return product;
        }

        public async Task<Product> UpdateProduct(int profileId,int productId,UpdateProductRequest updateProductRequest){
            var product = await _context.Products.FirstOrDefaultAsync(a => a.Id == productId && a.OwnerId == profileId);
            product.Name = updateProductRequest.Name;
            product.Type = updateProductRequest.Type;
            product.Category = updateProductRequest.Category;
            product.Price = updateProductRequest.Price;
            product.Description = updateProductRequest.Description;
            product.Images = updateProductRequest.Images;
            product.Ratings = updateProductRequest.Ratings;
            product.Reviews = updateProductRequest.Reviews;
            product.Specifications = updateProductRequest.Specifications;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
