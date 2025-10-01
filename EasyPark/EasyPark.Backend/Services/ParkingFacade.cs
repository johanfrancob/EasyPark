using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EasyPark.Backend.Repositories;
using EasyPark.Backend.Services.Abstractions;
using EasyPark.Shared.DTOs;
using EasyPark.Shared.Entities;

namespace EasyPark.Backend.Services
{
    public sealed class ParkingFacade : IParkingFacade
    {
        private readonly IBahiasService _bahiasService;
        private readonly IBillingService _billingService;
        private readonly IRepository<TblTicketEntradum> _tickets;
        private readonly IRepository<TblFactura> _facturas;
        private readonly IRepository<TblVehiculo> _vehiculos;
        private readonly IRepository<TblTarifa> _tarifas;

        public ParkingFacade(
            IBahiasService bahiasService,
            IBillingService billingService,
            IRepository<TblTicketEntradum> tickets,
            IRepository<TblFactura> facturas,
            IRepository<TblVehiculo> vehiculos,
            IRepository<TblTarifa> tarifas)
        {
            _bahiasService = bahiasService;
            _billingService = billingService;
            _tickets = tickets;
            _facturas = facturas;
            _vehiculos = vehiculos;
            _tarifas = tarifas;
        }

        public Task<IReadOnlyList<TblBahium>> GetBahiasDisponiblesAsync()
            => _bahiasService.DisponiblesAsync();

        public async Task<TblTicketEntradum> RegistrarEntradaAsync(RegistrarEntradaRequest request)
        {
            var disponibles = await _bahiasService.DisponiblesAsync();
            if (!disponibles.Any(b => b.IdBahia == request.IdBahia))
                throw new InvalidOperationException("La bahía no está disponible.");

            var vehiculo = await _vehiculos.FirstOrDefaultAsync(v => v.Placa == request.Placa);
            if (vehiculo is null)
                throw new InvalidOperationException("El vehículo no existe. Créalo antes de ingresar.");

            var ticket = new TblTicketEntradum
            {
                FechaHoraEntrada = DateTime.Now,
                Placa = request.Placa,
                IdCliente = request.IdCliente,
                IdBahia = request.IdBahia
            };

            await _tickets.AddAsync(ticket);
            await _tickets.SaveChangesAsync();

            await _bahiasService.OcuparAsync(request.IdBahia);
            return ticket;
        }

        public async Task<FacturaDTO> RegistrarSalidaAsync(int idTicket, int idEmpleado)
        {
            var ticket = await _tickets.GetByIdAsync(idTicket)
                         ?? throw new InvalidOperationException("Ticket no encontrado.");

            var vehiculo = await _vehiculos.FirstOrDefaultAsync(v => v.Placa == ticket.Placa)
                           ?? throw new InvalidOperationException("Vehículo no encontrado.");

            var tarifa = await _tarifas.FirstOrDefaultAsync(t => t.IdTipoVehiculo == vehiculo.IdTipoVehiculo)
                         ?? throw new InvalidOperationException("Tarifa no configurada.");

            var monto = await _billingService.CalcularMontoAsync(idTicket);

            var factura = new TblFactura
            {
                FechaHoraSalida = DateTime.Now,
                Monto = monto,
                IdTicket = ticket.IdTicket,
                IdEmpleado = idEmpleado,
                IdTarifa = tarifa.IdTarifa,
                Estado = "Pagada"
            };

            await _facturas.AddAsync(factura);
            await _facturas.SaveChangesAsync();

            await _bahiasService.LiberarAsync(ticket.IdBahia);

            return new FacturaDTO
            {
                IdFactura = factura.IdFactura,
                FechaHoraSalida = factura.FechaHoraSalida,
                Monto = factura.Monto,
                IdTicket = factura.IdTicket,
                Placa = ticket.Placa,
                FechaHoraEntrada = ticket.FechaHoraEntrada,
                Cliente = "Consumidor Final",
                Documento = "222222222222"
            };
        }

        public async Task<TblVehiculo> CrearVehiculoAsync(CrearVehiculoRequest request)
        {
            var existente = await _vehiculos.FirstOrDefaultAsync(v => v.Placa == request.Placa);
            if (existente != null) return existente;

            var vehiculo = new TblVehiculo
            {
                Placa = request.Placa.ToUpper(),
                Color = request.Color,
                Marca = request.Marca,
                IdTipoVehiculo = request.IdTipoVehiculo ?? 1
            };

            await _vehiculos.AddAsync(vehiculo);
            await _vehiculos.SaveChangesAsync();
            return vehiculo;
        }
    }
}
