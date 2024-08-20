using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using minimal_api2.Context;
using minimal_api2.Entities;

namespace minimal_api2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly VeiculosContext _context;

        public UsuarioController(VeiculosContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginrequest)
        {
            var userCheck = _context.Usuarios.FirstOrDefault(x => x.Email == loginrequest.Email);
            if (userCheck == null)
            {
                return NotFound("Usuário não encontrato!");
            }

            if (loginrequest.Senha == userCheck.Password)
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("dabas-minimal-api-aula-uma-vez-sempre-voltamos-atras-de-nosso-sonhos-texto-aleatorio");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Email", userCheck.Email),
                        new Claim("Poderes", userCheck.Poderes.ToString()),
                        new Claim(ClaimTypes.Role, userCheck.Poderes.ToString())
                        
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                Console.WriteLine(userCheck.Poderes.ToString());
                return Ok(tokenString);

            }
            else
            {
                return Unauthorized("Senha incorreta!");
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost("Novo Usuario")]
        public IActionResult NovoUsuario(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
            return Ok("Usuario Criado com Sucesso");

        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscarUsuarios")]
        public IActionResult BuscarUsuarios()
        {
            var usuarios = _context.Usuarios;
            return Ok(usuarios);
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscarPorPoder")]
        public IActionResult BuscarPorPoderes(EnumPoderes poderes)
        {
            var poder = _context.Usuarios.Where(x => x.Poderes == poderes);
            return Ok(poder);

        }
    }
}