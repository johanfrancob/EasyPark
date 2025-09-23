using EasyPark.Backend;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly DataContext _context;

        public FacturasController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFactura>>> Get() =>
            await _context.TblFacturas
                .Include(f => f.IdEmpleadoNavigation)
                .Include(f => f.IdTarifaNavigation)
                .Include(f => f.IdTicketNavigation)
                .ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TblFactura>> Get(int id)
        {
            var factura = await _context.TblFacturas
                .Include(f => f.IdEmpleadoNavigation)
                .Include(f => f.IdTarifaNavigation)
                .Include(f => f.IdTicketNavigation)
                .FirstOrDefaultAsync(f => f.IdFactura == id);

            return factura is null ? NotFound() : factura;
        }

        [HttpPost]
        public async Task<ActionResult<TblFactura>> Post(TblFactura factura)
        {
            _context.TblFacturas.Add(factura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = factura.IdFactura }, factura);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblFactura factura)
        {
            if (id != factura.IdFactura) return BadRequest();
            _context.Entry(factura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var factura = await _context.TblFacturas.FindAsync(id);
            if (factura is null) return NotFound();
            _context.TblFacturas.Remove(factura);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
