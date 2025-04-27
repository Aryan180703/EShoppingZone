using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EShoppingZone.DTOs;
using EShoppingZone.DTOs.CartDTOs;
using EShoppingZone.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShoppingZone.Controllers
{
    [Route("api/CartController")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest cartRequest)
        {
            try
            {
                var profileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _service.AddToCartAsync(profileId, cartRequest);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateCartItem(int itemId, [FromBody] UpdateCartItemRequest updateRequest)
        {
            try
            {
                var profileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _service.UpdateCartItemAsync(profileId, itemId, updateRequest);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            try
            {
                var profileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _service.RemoveFromCartAsync(profileId, itemId);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var profileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _service.GetCartAsync(profileId);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Clear")]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var profileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _service.ClearCartAsync(profileId);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}