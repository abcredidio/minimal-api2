using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using minimal_api2.Context;

namespace minimal_api2.Controllers
{
    public class VeiculoController : ControllerBase
    {
        private readonly VeiculosContext _context;

        public VeiculoController(VeiculosContext context){
            _context = context;
        }

        
    }
}