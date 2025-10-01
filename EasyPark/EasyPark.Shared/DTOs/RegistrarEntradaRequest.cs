using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPark.Shared.DTOs
{
    public sealed class RegistrarEntradaRequest
    {
        public string Placa { get; set; } = null!;
        public int IdCliente { get; set; }
        public int IdBahia { get; set; }
    }
}
