using EasyPark.Backend.Data;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifasController : ControllerBase
    {
        private readonly DataContext _context;

        public TarifasController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTarifa>>> Get() =>
            await _context.TblTarifas.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TblTarifa>> Get(int id)
        {
            var tarifa = await _context.TblTarifas.FindAsync(id);
            return tarifa is null ? NotFound() : tarifa;
        }

        [HttpPost]
        public async Task<ActionResult<TblTarifa>> Post(TblTarifa tarifa)
        {
            _context.TblTarifas.Add(tarifa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = tarifa.IdTarifa }, tarifa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblTarifa tarifa)
        {
            if (id != tarifa.IdTarifa) return BadRequest();
            _context.Entry(tarifa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarifa = await _context.TblTarifas.FindAsync(id);
            if (tarifa is null) return NotFound();
            _context.TblTarifas.Remove(tarifa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
