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
            await CheckTipoVehiculosAsync();
            await CheckTarifasAsync();
            await CheckBahiasAsync();
            await CheckVehiculosAsync();
            await CheckClientesAsync(); 

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

        private async Task CheckTipoVehiculosAsync()
        {
            if (!_context.TblTipoVehiculos.Any())
            {
                _context.TblTipoVehiculos.AddRange(
                    new TblTipoVehiculo { Nombre = "Carro" },
                    new TblTipoVehiculo { Nombre = "Moto" }
                );
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckTarifasAsync()
        {
            if (!_context.TblTarifas.Any())
            {
                var carro = await _context.TblTipoVehiculos.FirstAsync(tv => tv.Nombre == "Carro");
                var moto = await _context.TblTipoVehiculos.FirstAsync(tv => tv.Nombre == "Moto");

                _context.TblTarifas.AddRange(
                    new TblTarifa { IdTipoVehiculo = carro.IdTipoVehiculo, ValorHora = 2500 }, // Carro
                    new TblTarifa { IdTipoVehiculo = moto.IdTipoVehiculo, ValorHora = 1500 }   // Moto
                );

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckBahiasAsync()
        {
            if (!_context.TblBahia.Any())
            {
                var carro = await _context.TblTipoVehiculos.FirstAsync(tv => tv.Nombre == "Carro");
                var moto = await _context.TblTipoVehiculos.FirstAsync(tv => tv.Nombre == "Moto");

                var bahias = new List<TblBahium>();

                for (int i = 1; i <= 10; i++)
                {
                    bahias.Add(new TblBahium
                    {
                        Estado = "Disponible",
                        IdTipoVehiculo = carro.IdTipoVehiculo,
                        Ubicacion = $"C{i}"
                    });
                }


                for (int i = 1; i <= 5; i++)
                {
                    bahias.Add(new TblBahium
                    {
                        Estado = "Disponible",
                        IdTipoVehiculo = moto.IdTipoVehiculo,
                        Ubicacion = $"M{i}"
                    });
                }

                _context.TblBahia.AddRange(bahias);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckVehiculosAsync()
        {
            if (!_context.TblVehiculos.Any())
            {
                var carro = await _context.TblTipoVehiculos.FirstAsync(tv => tv.Nombre == "Carro");
                var moto = await _context.TblTipoVehiculos.FirstAsync(tv => tv.Nombre == "Moto");
                _context.TblVehiculos.AddRange(
                    new TblVehiculo { Placa = "ABC123", Color = "Rojo", Marca = "Toyota", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "XYZ789", Color = "Azul", Marca = "Honda", IdTipoVehiculo = moto.IdTipoVehiculo }
                );
                await _context.SaveChangesAsync();
            }

        }

        private async Task CheckClientesAsync()
        {
            if (!_context.TblClientes.Any())
            {
                _context.TblClientes.AddRange(
                    new TblCliente { Nombre = "Consumidor final", Documento = "222222222222", Telefono = "00000000000" },
                    new TblCliente { Nombre = "Juan Ruiz", Documento = "2001", Telefono = "3201234567" },
                    new TblCliente { Nombre = "Laura Martínez", Documento = "2002", Telefono = "3309876543" }
                );
                await _context.SaveChangesAsync();
            }
        }
    }
}