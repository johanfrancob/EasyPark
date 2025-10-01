using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPark.Shared.DTOs;
using EasyPark.Shared.Entities;

namespace EasyPark.Backend.Services.Abstractions
{
    public interface IParkingFacade
    {
        Task<IReadOnlyList<TblBahium>> GetBahiasDisponiblesAsync();
        Task<TblTicketEntradum> RegistrarEntradaAsync(RegistrarEntradaRequest request);
        Task<FacturaDTO> RegistrarSalidaAsync(int idTicket, int idEmpleado);
        Task<TblVehiculo> CrearVehiculoAsync(CrearVehiculoRequest request);
    }
}
