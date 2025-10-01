using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyPark.Backend.Data;
using EasyPark.Backend.Services.Abstractions;
using EasyPark.Shared.DTOs;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador, Cajero")]
    public class TicketsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IParkingFacade _facade;

        public TicketsController(DataContext context, IParkingFacade facade)
        {
            _context = context;
            _facade = facade;
        }

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

        [HttpPost("registrar-entrada")]
        public async Task<ActionResult<TblTicketEntradum>> RegistrarEntrada([FromBody] RegistrarEntradaRequest request)
        {
            var ticket = await _facade.RegistrarEntradaAsync(request);
            return CreatedAtAction(nameof(Get), new { id = ticket.IdTicket }, ticket);
        }

        [HttpPost("registrar-salida/{idTicket}")]
        public async Task<ActionResult<FacturaDTO>> RegistrarSalida(int idTicket)
        {
            var empleadoNombre = User.FindFirst(ClaimTypes.Name)?.Value;
            var usuario = await _context.TblUsuarios
                .Include(u => u.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(u => u.Nombre == empleadoNombre);

            if (usuario == null) return Unauthorized("No se pudo identificar al empleado.");

            var facturaDTO = await _facade.RegistrarSalidaAsync(idTicket, usuario.IdEmpleado);
            return Ok(facturaDTO);
        }


    }
}
