using EasyPark.Backend.Data;
using EasyPark.Shared.DTOs;
using EasyPark.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyPark.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<TblUsuario> _hasher;

        public LoginController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _hasher = new PasswordHasher<TblUsuario>();
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var usuario = await _context.TblUsuarios
                .Include(u => u.IdEmpleadoNavigation)
                .ThenInclude(e => e.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.Nombre == request.Nombre);

            if (usuario == null)
                return Unauthorized(new LoginResponse { Success = false, Message = "Usuario no encontrado" });

            var result = _hasher.VerifyHashedPassword(usuario, usuario.Contrasena, request.Contrasena);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new LoginResponse { Success = false, Message = "Contraseña incorrecta" });

            var token = GenerateJwtToken(usuario.Nombre, usuario.IdEmpleadoNavigation.IdRolNavigation.Nombre);

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login correcto",
                Empleado = usuario.IdEmpleadoNavigation.Nombre,
                Rol = usuario.IdEmpleadoNavigation.IdRolNavigation.Nombre,
                Token = token
            });
        }

        private string GenerateJwtToken(string username, string role)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "ClaveSuperSecreta123456");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
