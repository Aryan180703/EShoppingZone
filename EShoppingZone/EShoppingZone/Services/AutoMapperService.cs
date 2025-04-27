using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShoppingZone.DTOs.AddressDTOs;
using EShoppingZone.DTOs.CartDTOs;
using EShoppingZone.DTOs.ProductDTOs;
using EShoppingZone.Models;

namespace EShoppingZone.Services
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService(){
            CreateMap<AddressRequest , Address>();
            CreateMap<Address , AddressResponse>();
            CreateMap<Product , ProductResponse>();
            CreateMap<ProductRequest , Product>();
            CreateMap<Cart, CartResponse>();
            CreateMap<CartItem, CartItemResponse>();
            CreateMap<CartRequest, CartItem>();
        }
    }
}