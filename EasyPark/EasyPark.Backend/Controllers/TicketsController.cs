using EasyPark.Backend.Data;
using EasyPark.Shared.Entities;
using EasyPark.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador, Cajero")]
    public class TicketsController : ControllerBase
    {
        private readonly DataContext _context;

        public TicketsController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTicketEntradum>>> Get()
        {
            var ticketsFacturadosIds = await _context.TblFacturas
                .Select(f => f.IdTicket)
                .ToListAsync();

            return await _context.TblTicketEntrada
                .Where(t => !ticketsFacturadosIds.Contains(t.IdTicket))
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdBahiaNavigation)
                .Include(t => t.PlacaNavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblTicketEntradum>> Get(int id)
        {
            var ticket = await _context.TblTicketEntrada
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdBahiaNavigation)
                .Include(t => t.PlacaNavigation)
                .FirstOrDefaultAsync(t => t.IdTicket == id);

            return ticket is null ? NotFound() : ticket;
        }

        [HttpPost]
        public async Task<ActionResult<TblTicketEntradum>> Post(TblTicketEntradum ticket)
        {
            _context.TblTicketEntrada.Add(ticket);

            var bahia = await _context.TblBahia.FindAsync(ticket.IdBahia);
            if (bahia != null)
            {
                bahia.Estado = "Ocupada";
                _context.TblBahia.Update(bahia);
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = ticket.IdTicket }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblTicketEntradum ticket)
        {
            if (id != ticket.IdTicket) return BadRequest();
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.TblTicketEntrada.FindAsync(id);
            if (ticket is null) return NotFound();

            _context.TblTicketEntrada.Remove(ticket);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("registrar-salida/{idTicket}")]
        public async Task<ActionResult<FacturaDTO>> RegistrarSalida(int idTicket)
        {
            var ticket = await _context.TblTicketEntrada
                .Include(t => t.PlacaNavigation)
                .FirstOrDefaultAsync(t => t.IdTicket == idTicket);

            if (ticket == null)
                return NotFound("El ticket no fue encontrado.");

            var bahia = await _context.TblBahia.FindAsync(ticket.IdBahia);
            if (bahia == null)
                return NotFound("La bahía asociada al ticket no fue encontrada.");

            var tarifa = await _context.TblTarifas
                .FirstOrDefaultAsync(t => t.IdTipoVehiculo == ticket.PlacaNavigation.IdTipoVehiculo);
            if (tarifa == null)
                return NotFound("No se encontró una tarifa para este tipo de vehículo.");

            var empleadoNombre = User.FindFirst(ClaimTypes.Name)?.Value;
            var usuario = await _context.TblUsuarios
                .Include(u => u.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(u => u.Nombre == empleadoNombre);

            if (usuario == null)
                return Unauthorized("No se pudo identificar al empleado.");

            var idEmpleado = usuario.IdEmpleado;

            var tiempoEstacionado = DateTime.Now - ticket.FechaHoraEntrada;
            var horasACobrar = (decimal)Math.Ceiling(tiempoEstacionado.TotalHours);
            var monto = horasACobrar * tarifa.ValorHora;

            var nuevaFactura = new TblFactura
            {
                FechaHoraSalida = DateTime.Now,
                Monto = monto,
                IdTicket = ticket.IdTicket,
                IdEmpleado = idEmpleado,
                IdTarifa = tarifa.IdTarifa,
                Estado = "Pagada"
            };

            bahia.Estado = "Disponible";
            _context.TblFacturas.Add(nuevaFactura);
            _context.TblBahia.Update(bahia);
            await _context.SaveChangesAsync();

            var facturaDTO = new FacturaDTO
            {
                IdFactura = nuevaFactura.IdFactura,
                FechaHoraSalida = nuevaFactura.FechaHoraSalida,
                Monto = nuevaFactura.Monto,
                IdTicket = ticket.IdTicket,
                Placa = ticket.Placa,
                FechaHoraEntrada = ticket.FechaHoraEntrada,
                Cliente = "Consumidor Final",
                Documento = "222222222222"
            };

            return Ok(facturaDTO);
        }
    }
}
