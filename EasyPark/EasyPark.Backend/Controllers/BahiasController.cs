using EasyPark.Backend.Data;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BahiasController : ControllerBase
    {
        private readonly DataContext _context;

        public BahiasController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblBahium>>> Get() =>
            await _context.TblBahia.ToListAsync();

        [HttpGet("resumen")]
        public async Task<ActionResult<object>> GetResumenBahias()
        {
            var resumen = await _context.TblBahia
                .GroupBy(b => b.IdTipoVehiculo)
                .Select(g => new
                {
                    IdTipoVehiculo = g.Key,
                    Total = g.Count(),
                    Disponibles = g.Count(b => b.Estado == "Disponible")
                })
                .ToListAsync();

            int bh_disponibles_carro = 0;
            int bh_total_carro = 0;
            int bh_disponibles_moto = 0;
            int bh_total_moto = 0;

            foreach (var item in resumen)
            {
                if (item.IdTipoVehiculo == 1)
                {
                    bh_disponibles_carro = item.Disponibles;
                    bh_total_carro = item.Total;
                }
                else if (item.IdTipoVehiculo == 2)
                {
                    bh_disponibles_moto = item.Disponibles;
                    bh_total_moto = item.Total;
                }
            }

            return Ok(new
            {
                bh_disponibles_carro,
                bh_total_carro,
                bh_disponibles_moto,
                bh_total_moto
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblBahium>> Get(int id)
        {
            var bahia = await _context.TblBahia.FindAsync(id);
            return bahia is null ? NotFound() : bahia;
        }

        [HttpPost]
        public async Task<ActionResult<TblBahium>> Post(TblBahium bahia)
        {
            _context.TblBahia.Add(bahia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = bahia.IdBahia }, bahia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblBahium bahia)
        {
            if (id != bahia.IdBahia) return BadRequest();
            _context.Entry(bahia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bahia = await _context.TblBahia.FindAsync(id);
            if (bahia is null) return NotFound();
            _context.TblBahia.Remove(bahia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
