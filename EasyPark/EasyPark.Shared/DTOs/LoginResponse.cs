using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPark.Shared.DTOs
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Empleado { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
