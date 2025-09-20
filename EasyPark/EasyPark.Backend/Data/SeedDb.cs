using EasyPark.Backend; // tu namespace Backend
using EasyPark.Shared.Entities; // si tus entidades están aquí
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
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
                _context.TblRols.Add(new TblRol { Nombre = "Administrador" });
                _context.TblRols.Add(new TblRol { Nombre = "Cajero" });
                _context.TblRols.Add(new TblRol { Nombre = "Vigilante" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckEmpleadosAsync()
        {
            if (!_context.TblEmpleados.Any())
            {
                var adminRol = await _context.TblRols.FirstOrDefaultAsync(r => r.Nombre == "Administrador");
                var cajeroRol = await _context.TblRols.FirstOrDefaultAsync(r => r.Nombre == "Cajero");
                var vigilanteRol = await _context.TblRols.FirstOrDefaultAsync(r => r.Nombre == "Vigilante");

                _context.TblEmpleados.Add(new TblEmpleado
                {
                    Nombre = "Juan Pérez",
                    Documento = "1001",
                    Telefono = "3001234567",
                    IdRol = adminRol.IdRol
                });

                _context.TblEmpleados.Add(new TblEmpleado
                {
                    Nombre = "Ana Gómez",
                    Documento = "1002",
                    Telefono = "3109876543",
                    IdRol = cajeroRol.IdRol
                });

                _context.TblEmpleados.Add(new TblEmpleado
                {
                    Nombre = "Luis Torres",
                    Documento = "1003",
                    Telefono = "3115554321",
                    IdRol = vigilanteRol.IdRol
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckUsuariosAsync()
        {
            if (!_context.TblUsuarios.Any())
            {
                var admin = await _context.TblEmpleados.FirstOrDefaultAsync(e => e.Documento == "1001");
                var cajero = await _context.TblEmpleados.FirstOrDefaultAsync(e => e.Documento == "1002");
                var vigilante = await _context.TblEmpleados.FirstOrDefaultAsync(e => e.Documento == "1003");

                _context.TblUsuarios.Add(new TblUsuario
                {
                    Nombre = "admin",
                    Contrasena = "admin123", // ⚠️ en la práctica deberías usar hash
                    IdEmpleado = admin.IdEmpleado,
                    Estado = "Activo"
                });

                _context.TblUsuarios.Add(new TblUsuario
                {
                    Nombre = "cajero1",
                    Contrasena = "cajero123",
                    IdEmpleado = cajero.IdEmpleado,
                    Estado = "Activo"
                });

                _context.TblUsuarios.Add(new TblUsuario
                {
                    Nombre = "vigilante1",
                    Contrasena = "vigilante123",
                    IdEmpleado = vigilante.IdEmpleado,
                    Estado = "Activo"
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckTarifasAsync()
        {
            if (!_context.TblTarifas.Any())
            {
                _context.TblTarifas.Add(new TblTarifa { TipoVehiculo = "Carro", ValorHora = 2500 });
                _context.TblTarifas.Add(new TblTarifa { TipoVehiculo = "Moto", ValorHora = 1500 });
                _context.TblTarifas.Add(new TblTarifa { TipoVehiculo = "Bicicleta", ValorHora = 500 });

                await _context.SaveChangesAsync();
            }
        }
    }
}
