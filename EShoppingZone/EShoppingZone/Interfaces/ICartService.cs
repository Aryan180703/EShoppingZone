using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShoppingZone.DTOs;
using EShoppingZone.DTOs.CartDTOs;

namespace EShoppingZone.Interfaces
{
    public interface ICartService
    {
        Task<ResponseDTO<CartResponse>> AddToCartAsync(int profileId, CartRequest cartRequest);
        Task<ResponseDTO<CartResponse>> UpdateCartItemAsync(int profileId, int itemId, UpdateCartItemRequest updateRequest);
        Task<ResponseDTO<CartResponse>> RemoveFromCartAsync(int profileId, int itemId);
        Task<ResponseDTO<CartResponse>> GetCartAsync(int profileId);
        Task<ResponseDTO<CartResponse>> ClearCartAsync(int profileId);
    }
}
