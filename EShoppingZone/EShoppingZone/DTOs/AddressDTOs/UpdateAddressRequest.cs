using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.DTOs.AddressDTOs
{
    public class UpdateAddressRequest
    {
        public int HouseNumber { get; set; }
        public string? StreetName { get; set; } 
        public string? ColonyName { get; set; }
        public string? City { get; set; } 
        public string? State { get; set; } 
        public int Pincode { get; set; }
    }
}