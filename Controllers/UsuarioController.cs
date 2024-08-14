using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using minimal_api2.Context;
using minimal_api2.Entities;

namespace minimal_api2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly VeiculosContext _context;

        public UsuarioController(VeiculosContext context){
            _context = context;
        }

        [HttpPost("Login")]            
        public IActionResult Login(LoginRequest loginrequest){
            var userCheck = _context.Usuarios.FirstOrDefault(x => x.Email == loginrequest.Email);
            if(userCheck == null){
                return NotFound("Usuário não encontrato!");
            }

            if (loginrequest.Senha == userCheck.Password){
                return Ok("Logado com Sucesso!");
            }
            else{
                return Unauthorized("Senha incorreta!");
            }


        }
    }
}