using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShoppingZone.DTOs;
using EShoppingZone.DTOs.CartDTOs;
using EShoppingZone.Interfaces;
using EShoppingZone.Models;
using Microsoft.EntityFrameworkCore;

namespace EShoppingZone.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<CartResponse>> AddToCartAsync(int profileId, CartRequest cartRequest)
        {
            var product = await _repository.GetProductAsync(cartRequest.ProductId);
            if (product == null)
            {
                return new ResponseDTO<CartResponse>
                {
                    Success = false,
                    Message = "Product not found"
                };
            }

            var cart = await _repository.GetCartAsync(profileId);
            if (cart == null)
            {
                cart = new Cart { UserProfileId = profileId, TotalPrice = 0 };
                cart = await _repository.CreateCartAsync(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == cartRequest.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartRequest.Quantity;
                existingItem.Price = product.Price * existingItem.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = cartRequest.ProductId,
                    ProductName = product.Name,
                    Price = product.Price * cartRequest.Quantity,
                    Quantity = cartRequest.Quantity
                };
                cart.Items.Add(cartItem);
            }

            cart.TotalPrice = cart.Items.Sum(i => i.Price);
            await _repository.UpdateCartAsync(cart);

            var response = await GetCartResponseAsync(cart);
            return new ResponseDTO<CartResponse>
            {
                Success = true,
                Message = "Item added to cart",
                Data = response
            };
        }

        public async Task<ResponseDTO<CartResponse>> UpdateCartItemAsync(int profileId, int itemId, UpdateCartItemRequest updateRequest)
        {
            var cart = await _repository.GetCartAsync(profileId);
            if (cart == null)
            {
                return new ResponseDTO<CartResponse>
                {
                    Success = false,
                    Message = "Cart not found"
                };
            }

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return new ResponseDTO<CartResponse>
                {
                    Success = false,
                    Message = "Item not found"
                };
            }

            var product = await _repository.GetProductAsync(item.ProductId);
            item.Quantity = updateRequest.Quantity;
            item.Price = product.Price * item.Quantity;
            cart.TotalPrice = cart.Items.Sum(i => i.Price);

            await _repository.UpdateCartAsync(cart);

            var response = await GetCartResponseAsync(cart);
            return new ResponseDTO<CartResponse>
            {
                Success = true,
                Message = "Cart item updated",
                Data = response
            };
        }

        public async Task<ResponseDTO<CartResponse>> RemoveFromCartAsync(int profileId, int itemId)
        {
            var cart = await _repository.GetCartAsync(profileId);
            if (cart == null)
            {
                return new ResponseDTO<CartResponse>
                {
                    Success = false,
                    Message = "Cart not found"
                };
            }

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return new ResponseDTO<CartResponse>
                {
                    Success = false,
                    Message = "Item not found"
                };
            }

            cart.Items.Remove(item);
            cart.TotalPrice = cart.Items.Sum(i => i.Price);
            await _repository.UpdateCartAsync(cart);

            var response = await GetCartResponseAsync(cart);
            return new ResponseDTO<CartResponse>
            {
                Success = true,
                Message = "Item removed from cart",
                Data = response
            };
        }

        public async Task<ResponseDTO<CartResponse>> GetCartAsync(int profileId)
        {
            var cart = await _repository.GetCartAsync(profileId);
            if (cart == null)
            {
                return new ResponseDTO<CartResponse>
                {
                    Success = false,
                    Message = "Cart not found"
                };
            }

            var response = await GetCartResponseAsync(cart);
            return new ResponseDTO<CartResponse>
            {
                Success = true,
                Message = "Cart retrieved",
                Data = response
            };
        }

        public async Task<ResponseDTO<CartResponse>> ClearCartAsync(int profileId)
        {
            var cart = await _repository.GetCartAsync(profileId);
            if (cart == null)
            {
                return new ResponseDTO<CartResponse>
                {
                    Success = false,
                    Message = "Cart not found"
                };
            }

            cart.Items.Clear();
            cart.TotalPrice = 0;
            await _repository.UpdateCartAsync(cart);

            var response = await GetCartResponseAsync(cart);
            return new ResponseDTO<CartResponse>
            {
                Success = true,
                Message = "Cart cleared",
                Data = response
            };
        }

        private async Task<CartResponse> GetCartResponseAsync(Cart cart)
        {
            var response = _mapper.Map<CartResponse>(cart);
            response.Items = cart.Items.Select(i => _mapper.Map<CartItemResponse>(i)).ToList();
            return response;
        }
    }
}