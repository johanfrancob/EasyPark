using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPark.Shared.DTOs;

public class CrearVehiculoRequest
{
    public string Placa { get; set; } = null!;
    public string? Color { get; set; }
    public string? Marca { get; set; }
    public int? IdTipoVehiculo { get; set; }
}
