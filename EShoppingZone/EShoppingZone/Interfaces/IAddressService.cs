using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShoppingZone.DTOs;
using EShoppingZone.DTOs.AddressDTOs;
using EShoppingZone.Models;

namespace EShoppingZone.Interfaces
{
    public interface IAddressService
    {
        Task<ResponseDTO<AddressResponse>> AddAddressAsync(int profileId,AddressRequest addressRequest);
        Task<ResponseDTO<List<AddressResponse>>> GetAllAddressAsync(int profileId);
        Task<ResponseDTO<AddressResponse>> UpdateAddressAsync(int profileId,int AddressId, UpdateAddressRequest updateAddress);
        Task<ResponseDTO<AddressResponse>> GetAddressAsync(int profileId, int addressId);

        Task<ResponseDTO<AddressResponse>> DeleteAddressAsync(int profileId , int AddressId); 
    }
}