using EasyPark.Backend.Data;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;

        public RolesController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblRol>>> Get() =>
            await _context.TblRols.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TblRol>> Get(int id)
        {
            var rol = await _context.TblRols.FindAsync(id);
            return rol is null ? NotFound() : rol;
        }

        [HttpPost]
        public async Task<ActionResult<TblRol>> Post(TblRol rol)
        {
            _context.TblRols.Add(rol);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = rol.IdRol }, rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblRol rol)
        {
            if (id != rol.IdRol) return BadRequest();
            _context.Entry(rol).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _context.TblRols.FindAsync(id);
            if (rol is null) return NotFound();
            _context.TblRols.Remove(rol);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
