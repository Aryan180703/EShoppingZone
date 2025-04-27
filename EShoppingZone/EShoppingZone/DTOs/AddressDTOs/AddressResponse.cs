using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.DTOs.AddressDTOs
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public int HouseNumber { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string? ColonyName { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Pincode { get; set; }
    }
}