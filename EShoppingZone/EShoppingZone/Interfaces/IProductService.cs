using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShoppingZone.DTOs;
using EShoppingZone.DTOs.AddressDTOs;
using EShoppingZone.DTOs.ProductDTOs;
using EShoppingZone.Models;
using EShoppingZone.Services;

namespace EShoppingZone.Interfaces
{
    public interface IProductService
    {
        Task<ResponseDTO<ProductResponse>> AddProductAsync(int profileId , ProductRequest productRequest);
        Task<ResponseDTO<List<ProductResponse>>> GetMerchantsProductAsync(int profileId); 
        Task<ResponseDTO<ProductResponse>> GetProductAsync(int productId);
        Task<ResponseDTO<ProductResponse>> UpdateProductAsync(int profileId, int productId, UpdateProductRequest updateProductRequest);
    }
}