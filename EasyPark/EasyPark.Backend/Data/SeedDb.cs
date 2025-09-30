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

                for (int i = 1; i <= 20; i++)
                {
                    bahias.Add(new TblBahium
                    {
                        Estado = "Disponible",
                        IdTipoVehiculo = carro.IdTipoVehiculo,
                        Ubicacion = $"C{i}"
                    });
                }

                for (int i = 1; i <= 30; i++)
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
                    // Inicialización de Carros
                    new TblVehiculo { Placa = "ABC123", Color = "Rojo", Marca = "Toyota", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "DEF456", Color = "Blanco", Marca = "Mazda", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "GHI789", Color = "Negro", Marca = "Chevrolet", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "JKL321", Color = "Gris", Marca = "Nissan", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "MNO654", Color = "Azul", Marca = "Ford", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "PQR987", Color = "Verde", Marca = "Hyundai", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "STU159", Color = "Amarillo", Marca = "Kia", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "VWX753", Color = "Plata", Marca = "Volkswagen", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "YZA852", Color = "Rojo", Marca = "Renault", IdTipoVehiculo = carro.IdTipoVehiculo },
                    new TblVehiculo { Placa = "BCD951", Color = "Azul", Marca = "Peugeot", IdTipoVehiculo = carro.IdTipoVehiculo },

                    // Inicialización de Motos
                    new TblVehiculo { Placa = "XYZ789", Color = "Azul", Marca = "Honda", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "EFG246", Color = "Negro", Marca = "Yamaha", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "HIJ357", Color = "Rojo", Marca = "Suzuki", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "KLM468", Color = "Blanco", Marca = "Kawasaki", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "NOP579", Color = "Gris", Marca = "BMW", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "QRS680", Color = "Amarillo", Marca = "Ducati", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "TUV791", Color = "Verde", Marca = "KTM", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "WXY802", Color = "Rojo", Marca = "Harley-Davidson", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "ZAB913", Color = "Negro", Marca = "Bajaj", IdTipoVehiculo = moto.IdTipoVehiculo },
                    new TblVehiculo { Placa = "CDE024", Color = "Azul", Marca = "Italika", IdTipoVehiculo = moto.IdTipoVehiculo }
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
                    new TblCliente { Nombre = "Laura Martínez", Documento = "2002", Telefono = "3309876543" },
                    new TblCliente { Nombre = "Andrés Gómez", Documento = "2003", Telefono = "3101112233" },
                    new TblCliente { Nombre = "María Torres", Documento = "2004", Telefono = "3114445566" },
                    new TblCliente { Nombre = "Carlos Rodríguez", Documento = "2005", Telefono = "3127778899" },
                    new TblCliente { Nombre = "Sofía Ramírez", Documento = "2006", Telefono = "3130001122" },
                    new TblCliente { Nombre = "Felipe Castro", Documento = "2007", Telefono = "3143334455" },
                    new TblCliente { Nombre = "Diana López", Documento = "2008", Telefono = "3156667788" },
                    new TblCliente { Nombre = "Ricardo Hernández", Documento = "2009", Telefono = "3169990011" },
                    new TblCliente { Nombre = "Valentina Morales", Documento = "2010", Telefono = "3171237890" },
                    new TblCliente { Nombre = "Miguel Ángel Ruiz", Documento = "2011", Telefono = "3183216547" },
                    new TblCliente { Nombre = "Camila Jiménez", Documento = "2012", Telefono = "3196541230" },
                    new TblCliente { Nombre = "Sebastián Vargas", Documento = "2013", Telefono = "3207418529" },
                    new TblCliente { Nombre = "Paula Restrepo", Documento = "2014", Telefono = "3219632587" },
                    new TblCliente { Nombre = "David Ramírez", Documento = "2015", Telefono = "3227894561" },
                    new TblCliente { Nombre = "Natalia Fernández", Documento = "2016", Telefono = "3238521479" }
                );
                await _context.SaveChangesAsync();
            }
        }
    }
}