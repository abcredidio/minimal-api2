using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using minimal_api2.Context;
using System.Text;
using minimal_api2.Controllers;
using minimal_api2.Entities;
using System.Security.Claims;

namespace Teste.Mocks;

public class AdministradorServiceMock : UsuarioController
{
    private readonly VeiculosContext _context;

    public AdministradorServiceMock(VeiculosContext context) : base (context)
    {
        _context = context;
    }

    private static List<Usuario> usuarios = new List<Usuario>(){
        new Usuario{
            Id = 1,
            Email = "adm@teste.com",
            Password = "123456",
            Poderes = EnumPoderes.Administrador
        },
        new Usuario{
            Id = 2,
            Email = "gerente@teste.com",
            Password = "123456",
            Poderes = EnumPoderes.Gerente
        }
    };
    
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