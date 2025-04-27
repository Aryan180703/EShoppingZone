using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShoppingZone.DTOs;
using EShoppingZone.DTOs.AddressDTOs;
using EShoppingZone.DTOs.ProductDTOs;
using EShoppingZone.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShoppingZone.Controllers
{
    [Route("api/AddressController")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {

        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressRequest addressRequest)
        {
            try
            {
                var profileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _service.AddAddressAsync(profileId, addressRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddress(int addressId){
            var profileId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _service.GetAddressAsync(profileId ,addressId);
            if(response.Success){
                return Ok(response);
            }
            return BadRequest(response);
        }

        
        [HttpGet("GetAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            try
            {
                var profileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _service.GetAllAddressAsync(profileId);
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

        [HttpPut("UpdateAddress/{AddressId}")]
        public async Task<IActionResult> UpdateAddress(int AddressId, [FromBody] UpdateAddressRequest updateAddressRequest)
        {
            var profileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var response = await _service.UpdateAddressAsync(profileId ,AddressId, updateAddressRequest);
            if(response.Success){
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("DeleteAddress/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId){
            var profileId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _service.DeleteAddressAsync(profileId , addressId);
            if(response.Success){
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}