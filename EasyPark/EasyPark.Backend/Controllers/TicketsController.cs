using EasyPark.Backend;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly DataContext _context;

        public TicketsController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTicketEntradum>>> Get() =>
            await _context.TblTicketEntrada
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdBahiaNavigation)
                .Include(t => t.PlacaNavigation)
                .ToListAsync();

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
    }
}
