using EasyPark.Backend;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUsuario>>> Get() =>
            await _context.TblUsuarios.Include(u => u.IdEmpleadoNavigation).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TblUsuario>> Get(int id)
        {
            var usuario = await _context.TblUsuarios.FindAsync(id);
            return usuario is null ? NotFound() : usuario;
        }

        [HttpPost]
        public async Task<ActionResult<TblUsuario>> Post(TblUsuario usuario)
        {
            _context.TblUsuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblUsuario usuario)
        {
            if (id != usuario.IdUsuario) return BadRequest();
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.TblUsuarios.FindAsync(id);
            if (usuario is null) return NotFound();
            _context.TblUsuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
