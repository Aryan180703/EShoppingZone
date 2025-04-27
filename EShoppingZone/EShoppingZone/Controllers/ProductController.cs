using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShoppingZone.DTOs.ProductDTOs;
using EShoppingZone.Interfaces;
using EShoppingZone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShoppingZone.Controllers
{
    [ApiController]
    [Route("api/ProductController")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service){
            _service = service;
        }
        [HttpPost]
        [Authorize(Roles = "Merchant")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest){
            var profileId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _service.AddProductAsync(profileId , productRequest);
            if(response.Success){
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("MerchantGetProdut")]
        [Authorize(Roles = "Merchant")]
        public async Task<IActionResult> GetMerchantProducts(){
            var profileId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _service.GetMerchantsProductAsync(profileId);
            if(response.Success){
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetProduct/{productId}")]
        public async Task<IActionResult> GetProduct(int productId){
            var response = await _service.GetProductAsync(productId);
            if(response.Success){
                return Ok(response);
            }
            return BadRequest(response);
        }

        [Authorize(Roles = "Merchant")]
        [HttpPut("UpdateProduct/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductRequest updateProductRequest){
            var profileId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _service.UpdateProductAsync(profileId,productId,updateProductRequest);
            if(response.Success){
                return Ok(response);
            }
            return BadRequest(response);
        }

        // [Authorize(Roles = "Merchant")]
        // [HttpPut("DeleteProduct/{productId}")]
    }
}