using EasyPark.Backend.Data;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly DataContext _context;

        public EmpleadosController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEmpleado>>> Get() =>
            await _context.TblEmpleados.Include(e => e.IdRolNavigation).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TblEmpleado>> Get(int id)
        {
            var empleado = await _context.TblEmpleados.FindAsync(id);
            return empleado is null ? NotFound() : empleado;
        }

        [HttpPost]
        public async Task<ActionResult<TblEmpleado>> Post(TblEmpleado empleado)
        {
            _context.TblEmpleados.Add(empleado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = empleado.IdEmpleado }, empleado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblEmpleado empleado)
        {
            if (id != empleado.IdEmpleado) return BadRequest();
            _context.Entry(empleado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _context.TblEmpleados.FindAsync(id);
            if (empleado is null) return NotFound();
            _context.TblEmpleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
