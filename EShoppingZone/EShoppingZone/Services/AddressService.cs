using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShoppingZone.DTOs;
using EShoppingZone.DTOs.AddressDTOs;
using EShoppingZone.Interfaces;
using EShoppingZone.Models;
using Microsoft.AspNetCore.Authorization;

namespace EShoppingZone.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseDTO<AddressResponse>> AddAddressAsync(int profileId, AddressRequest addressRequest)
        {

            var address = _mapper.Map<Address>(addressRequest);
            address.UserProfileId = profileId;
            var addedAddress = await _repository.AddAddress(address);
            var response = _mapper.Map<AddressResponse>(addedAddress);
            response.Id = addedAddress.Id;
            return new ResponseDTO<AddressResponse>
            {
                Success = true,
                Message = "Address Added",
                Data = response
            };
        }

        public async Task<ResponseDTO<List<AddressResponse>>> GetAllAddressAsync(int profileId)
        {
            var addresses = await _repository.GetAllAddress(profileId);
            if (addresses.Count == 0)
            {
                return new ResponseDTO<List<AddressResponse>>
                {
                    Success = false,
                    Message = "Zero Address"
                };
            }
            List<AddressResponse> allAddress = new List<AddressResponse>();
            foreach (var a in addresses)
            {
                AddressResponse response = _mapper.Map<AddressResponse>(a);
                allAddress.Add(response);
            }
            return new ResponseDTO<List<AddressResponse>>
            {
                Success = true,
                Message = "All Addresses Retrieved",
                Data = allAddress
            };
        }

        public async Task<ResponseDTO<AddressResponse>> GetAddressAsync(int profileId, int addressId)
        {
            Address address = await _repository.GetAddress(profileId, addressId);
            if (address != null)
            {
                var response = _mapper.Map<AddressResponse>(address);
                return new ResponseDTO<AddressResponse>
                {
                    Success = true,
                    Message = "Address Retrieved",
                    Data = response
                };
            }
            return new ResponseDTO<AddressResponse>
            {
                Success = false,
                Message = "Address not found"
            };
        }
        public async Task<ResponseDTO<AddressResponse>> UpdateAddressAsync(int profileId, int AddressId, UpdateAddressRequest updateAddress)
        {
            var address = await _repository.GetAddress(profileId, AddressId);
            if (address == null)
            {
                return new ResponseDTO<AddressResponse>
                {
                    Success = false,
                    Message = "Address not found"
                };
            }
        
            address.HouseNumber = (updateAddress.HouseNumber!=0)? updateAddress.HouseNumber : address.HouseNumber;
            address.StreetName = updateAddress.StreetName ?? address.StreetName;
            address.ColonyName = updateAddress.ColonyName ?? address.ColonyName;
            address.City = updateAddress.City ?? address.City;
            address.State = updateAddress.State ?? address.State;
            address.Pincode = (updateAddress.Pincode!=0) ? updateAddress.Pincode : address.Pincode;
            await _repository.UpdateAddress(address);

            var response = _mapper.Map<AddressResponse>(address);
            return new ResponseDTO<AddressResponse>{
                Success = true,
                Message = "Address Updated",
                Data = response
            };
        }

        public async Task<ResponseDTO<AddressResponse>> DeleteAddressAsync(int profileId, int addressId){
            var address = await _repository.GetAddress(profileId, addressId);
            if(address==null){
                return new ResponseDTO<AddressResponse>{
                    Success = false,
                    Message = "Address not found"
                };
            }
            await _repository.DeleteAddress(address);
            var response = _mapper.Map<AddressResponse>(address);
            return new ResponseDTO<AddressResponse>{
                Success = true,
                Message = "Address deleted",
                Data = response 
            };
        }


    }
}