using EasyPark.Backend.Data;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EasyPark.Shared.DTOs;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador, Cajero")]
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
        public async Task<ActionResult<TblVehiculo>> Post(CrearVehiculoRequest request)
        {
            // 1. Creamos la entidad completa que se guardará en la BD.
            var nuevoVehiculo = new TblVehiculo
            {
                // 2. Asignamos la placa que viene del DTO.
                Placa = request.Placa.ToUpper(),

                // 3. Asignamos los valores opcionales si vienen en el DTO, si no, se quedan nulos.
                Color = request.Color,
                Marca = request.Marca,

                // 4. Asignamos un valor por defecto para el campo obligatorio IdTipoVehiculo
                //    si el usuario no lo envió. Usamos el ID 1 (Carro) como fallback.
                IdTipoVehiculo = request.IdTipoVehiculo ?? 1
            };

            // 5. Guardamos la nueva entidad en la base de datos.
            _context.TblVehiculos.Add(nuevoVehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { placa = nuevoVehiculo.Placa }, nuevoVehiculo);
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
