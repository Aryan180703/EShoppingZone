using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public int HouseNumber { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string? ColonyName { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Pincode { get; set; }
        public UserProfile UserProfile { get; set; } = null!;
    }
}