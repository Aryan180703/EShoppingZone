using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShoppingZone.DTOs.ProductDTOs;
using EShoppingZone.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace EShoppingZone.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<List<Product>> GetMerchantProducts(int profileId);
        Task<Product> GetProduct(int productId);
        Task<Product> UpdateProduct(int profileId,int productId,UpdateProductRequest updateProductRequest);
    }
}