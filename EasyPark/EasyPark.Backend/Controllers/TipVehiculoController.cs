using EasyPark.Backend.Data;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoVehiculosController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoVehiculosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoVehiculo>>> Get()
        {
            return await _context.TblTipoVehiculos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblTipoVehiculo>> Get(int id)
        {
            var tipoVehiculo = await _context.TblTipoVehiculos.FindAsync(id);

            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            return tipoVehiculo;
        }

        [HttpPost]
        public async Task<ActionResult<TblTipoVehiculo>> Post(TblTipoVehiculo tipoVehiculo)
        {
            _context.TblTipoVehiculos.Add(tipoVehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = tipoVehiculo.IdTipoVehiculo }, tipoVehiculo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblTipoVehiculo tipoVehiculo)
        {
            if (id != tipoVehiculo.IdTipoVehiculo)
            {
                return BadRequest();
            }

            _context.Entry(tipoVehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoVehiculo = await _context.TblTipoVehiculos.FindAsync(id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            _context.TblTipoVehiculos.Remove(tipoVehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
