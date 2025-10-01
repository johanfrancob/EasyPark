using System;
using System.Threading.Tasks;
using EasyPark.Backend.Repositories;
using EasyPark.Backend.Services.Abstractions;
using EasyPark.Shared.Entities;

namespace EasyPark.Backend.Services
{
    public sealed class BillingService : IBillingService
    {
        private readonly IRepository<TblTicketEntradum> _tickets;
        private readonly IRepository<TblVehiculo> _vehiculos;
        private readonly IRepository<TblTarifa> _tarifas;
        private readonly IRepository<TblBahium> _bahias;
        private readonly ITarifaStrategy _strategy;

        public BillingService(
            IRepository<TblTicketEntradum> tickets,
            IRepository<TblVehiculo> vehiculos,
            IRepository<TblTarifa> tarifas,
            IRepository<TblBahium> bahias,
            ITarifaStrategy strategy)
        {
            _tickets = tickets;
            _vehiculos = vehiculos;
            _tarifas = tarifas;
            _bahias = bahias;
            _strategy = strategy;
        }

        public async Task<decimal> CalcularMontoAsync(int idTicket)
        {
            var ticket = await _tickets.GetByIdAsync(idTicket)
                         ?? throw new InvalidOperationException($"Ticket {idTicket} no existe.");

            var vehiculo = await _vehiculos.FirstOrDefaultAsync(v => v.Placa == ticket.Placa)
                           ?? throw new InvalidOperationException("Vehículo no encontrado.");

            var tarifa = await _tarifas.FirstOrDefaultAsync(t => t.IdTipoVehiculo == vehiculo.IdTipoVehiculo)
                         ?? throw new InvalidOperationException("Tarifa no configurada para el tipo de vehículo.");

            var salida = DateTime.Now;
            return _strategy.Calcular(ticket.FechaHoraEntrada, salida, tarifa);
        }
    }
}
