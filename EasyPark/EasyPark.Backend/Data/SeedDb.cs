using EasyPark.Backend;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly PasswordHasher<TblUsuario> _hasher;

        public SeedDb(DataContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<TblUsuario>();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await CheckRolesAsync();
            await CheckEmpleadosAsync();
            await CheckUsuariosAsync();
            await CheckTarifasAsync();
        }

        private async Task CheckRolesAsync()
        {
            if (!_context.TblRols.Any())
            {
                _context.TblRols.AddRange(
                    new TblRol { Nombre = "Administrador" },
                    new TblRol { Nombre = "Cajero" }
                );

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckEmpleadosAsync()
        {
            if (!_context.TblEmpleados.Any())
            {
                var adminRol = await _context.TblRols.FirstAsync(r => r.Nombre == "Administrador");
                var cajeroRol = await _context.TblRols.FirstAsync(r => r.Nombre == "Cajero");

                _context.TblEmpleados.AddRange(
                    new TblEmpleado { Nombre = "Juan Pérez", Documento = "1001", Telefono = "3001234567", IdRol = adminRol.IdRol },
                    new TblEmpleado { Nombre = "Ana Gómez", Documento = "1002", Telefono = "3109876543", IdRol = cajeroRol.IdRol }

                );

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckUsuariosAsync()
        {
            if (!_context.TblUsuarios.Any())
            {
                var admin = await _context.TblEmpleados.FirstAsync(e => e.Documento == "1001");
                var cajero = await _context.TblEmpleados.FirstAsync(e => e.Documento == "1002");

                var u1 = new TblUsuario { Nombre = "admin", IdEmpleado = admin.IdEmpleado, Estado = "Activo" };
                u1.Contrasena = _hasher.HashPassword(u1, "admin123");

                var u2 = new TblUsuario { Nombre = "cajero1", IdEmpleado = cajero.IdEmpleado, Estado = "Activo" };
                u2.Contrasena = _hasher.HashPassword(u2, "cajero123");

              

                _context.TblUsuarios.AddRange(u1, u2);

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckTarifasAsync()
        {
            if (!_context.TblTarifas.Any())
            {
                _context.TblTarifas.AddRange(
                    new TblTarifa { TipoVehiculo = "Carro", ValorHora = 2500 },
                    new TblTarifa { TipoVehiculo = "Moto", ValorHora = 1500 },
                    new TblTarifa { TipoVehiculo = "Bicicleta", ValorHora = 500 }
                );

                await _context.SaveChangesAsync();
            }
        }
    }
}
