using EasyPark.Backend.Data;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientesController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCliente>>> Get() =>
            await _context.TblClientes.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TblCliente>> Get(int id)
        {
            var cliente = await _context.TblClientes.FindAsync(id);
            return cliente is null ? NotFound() : cliente;
        }

        [HttpPost]
        public async Task<ActionResult<TblCliente>> Post(TblCliente cliente)
        {
            _context.TblClientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TblCliente cliente)
        {
            if (id != cliente.IdCliente) return BadRequest();
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.TblClientes.FindAsync(id);
            if (cliente is null) return NotFound();
            _context.TblClientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
