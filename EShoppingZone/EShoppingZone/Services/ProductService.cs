using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShoppingZone.DTOs;
using EShoppingZone.DTOs.AddressDTOs;
using EShoppingZone.DTOs.ProductDTOs;
using EShoppingZone.Interfaces;
using EShoppingZone.Models;
using EShoppingZone.Repository;

namespace EShoppingZone.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repository;
        public readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseDTO<ProductResponse>> AddProductAsync(int profileId, ProductRequest productRequest)
        {
            var productToAdd = _mapper.Map<Product>(productRequest);
            var product = await _repository.AddProduct(productToAdd);
            var response = _mapper.Map<ProductResponse>(product);
            return new ResponseDTO<ProductResponse>
            {
                Success = true,
                Message = "Product added",
                Data = response
            };
        }

        public async Task<ResponseDTO<List<ProductResponse>>> GetMerchantsProductAsync(int profileId)
        {
            var products = await _repository.GetMerchantProducts(profileId);
            if (products.Count == 0)
            {
                return new ResponseDTO<List<ProductResponse>>
                {
                    Success = true,
                    Message = "No product added yet for this merchant",
                    Data = null
                };
            }
            List<ProductResponse> productResponses = new List<ProductResponse>();
            for (int i = 0; i < products.Count; i++)
            {
                var productResponse = _mapper.Map<ProductResponse>(products[i]);
                productResponses.Add(productResponse);
            }
            return new ResponseDTO<List<ProductResponse>>
            {
                Success = true,
                Message = "Products Retrieved",
                Data = productResponses
            };
        }

        public async Task<ResponseDTO<ProductResponse>> GetProductAsync(int productId)
        {
            var product = await _repository.GetProduct(productId);
            if (product == null)
            {
                return new ResponseDTO<ProductResponse>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = null
                };
            }
            var response = _mapper.Map<ProductResponse>(product);
            return new ResponseDTO<ProductResponse>
            {
                Success = true,
                Message = "Product retrieved",
                Data = response
            };
        }

        public async Task<ResponseDTO<ProductResponse>> UpdateProductAsync(int profileId, int productId, UpdateProductRequest updateProductRequest)
        {
            var product = await _repository.GetProduct(productId);
            if (product == null || product.Id != profileId)
            {
                return new ResponseDTO<ProductResponse>
                {
                    Success = false,
                    Message = "Product does not exist or you don't have permission",
                };
            }
            var UProduct = await _repository.UpdateProduct(profileId,productId, updateProductRequest);
            var updatedProduct = _mapper.Map<ProductResponse>(UProduct);
            return new ResponseDTO<ProductResponse>
            {
                Success = true,
                Message = "Product updated",
                Data = updatedProduct
            };

        }
    }
}