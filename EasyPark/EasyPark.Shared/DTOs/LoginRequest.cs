using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPark.Shared.DTOs
{
    public class LoginRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}
