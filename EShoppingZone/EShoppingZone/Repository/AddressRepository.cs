using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShoppingZone.Data;
using EShoppingZone.DTOs.AddressDTOs;
using EShoppingZone.Interfaces;
using EShoppingZone.Models;
using Microsoft.EntityFrameworkCore;

namespace EShoppingZone.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly EShoppingZoneDBContext _context;

        public AddressRepository(EShoppingZoneDBContext context){
            _context = context;
        }
        public async Task<Address> AddAddress(Address address){
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address> GetAddress(int profileId, int AddressId){
            var address = await _context.Addresses.FirstOrDefaultAsync(a=> a.UserProfileId == profileId && a.Id == AddressId);
            return address;
        }
        public async Task<List<Address>> GetAllAddress(int profileId){
            var addresses = await _context.Addresses.Where(a=> a.UserProfileId == profileId).Select(a=> a).ToListAsync();
            return addresses;
        }

        public async Task UpdateAddress(Address address){
            var Existing = await GetAddress(address.UserProfileId, address.Id);
            Existing.HouseNumber = address.HouseNumber;
            Existing.StreetName = address.StreetName;
            Existing.ColonyName = address.ColonyName;
            Existing.City=address.City;
            Existing.State=address.State;
            Existing.Pincode=address.Pincode;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddress(Address address){
            _context.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}