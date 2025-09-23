using EasyPark.Backend;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly DataContext _context;

        public VehiculosController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblVehiculo>>> Get() =>
            await _context.TblVehiculos.ToListAsync();

        [HttpGet("{placa}")]
        public async Task<ActionResult<TblVehiculo>> Get(string placa)
        {
            var vehiculo = await _context.TblVehiculos.FindAsync(placa);
            return vehiculo is null ? NotFound() : vehiculo;
        }

        [HttpPost]
        public async Task<ActionResult<TblVehiculo>> Post(TblVehiculo vehiculo)
        {
            _context.TblVehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { placa = vehiculo.Placa }, vehiculo);
        }

        [HttpPut("{placa}")]
        public async Task<IActionResult> Put(string placa, TblVehiculo vehiculo)
        {
            if (placa != vehiculo.Placa) return BadRequest();
            _context.Entry(vehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{placa}")]
        public async Task<IActionResult> Delete(string placa)
        {
            var vehiculo = await _context.TblVehiculos.FindAsync(placa);
            if (vehiculo is null) return NotFound();
            _context.TblVehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
