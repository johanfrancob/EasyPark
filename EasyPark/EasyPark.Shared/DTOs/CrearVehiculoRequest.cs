using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPark.Shared.DTOs;

public class CrearVehiculoRequest
{
    // El único campo realmente obligatorio para crear un vehículo
    public string Placa { get; set; } = null!;

    // Estos campos son opcionales
    public string? Color { get; set; }
    public string? Marca { get; set; }
    public int? IdTipoVehiculo { get; set; } // Lo hacemos nullable para que sea opcional
}
