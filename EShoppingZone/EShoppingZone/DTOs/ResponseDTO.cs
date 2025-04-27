using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShoppingZone.DTOs
{
    public class ResponseDTO<T>
    {
        public bool Success {get; set;} = false;
        public string Message {get; set;} = string.Empty;
        public T Data {get; set;} = default(T);
    }
}