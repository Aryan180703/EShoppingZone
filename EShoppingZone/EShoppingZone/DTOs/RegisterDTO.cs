using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.DTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long MobileNumber { get; set; }
        public string Password { get; set; } = string.Empty;
        public string RoleName { get; set; } = "Customer"; // by default is customer
    }
}
