using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShoppingZone.DTOs.AddressDTOs;
using EShoppingZone.Models;

namespace EShoppingZone.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> AddAddress(Address address);
        Task<Address> GetAddress(int profileId, int AddressId);
        Task<List<Address>> GetAllAddress(int profileId);
        Task UpdateAddress(Address address);
        Task DeleteAddress(Address address);
    }
}