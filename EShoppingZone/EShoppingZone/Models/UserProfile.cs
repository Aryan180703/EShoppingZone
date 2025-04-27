using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EShoppingZone.Models
{
    public class UserProfile : IdentityUser<int>
    {
        public string FullName {get; set;}
        public long MobileNumber {get; set;}
        public string? Image {get; set;}
        public string? About {get; set;}
        public DateTime DateOfBirth {get; set;}
        public string? Gender {get; set;}
        public List<Address> Addresses {get; set;}
    }
}