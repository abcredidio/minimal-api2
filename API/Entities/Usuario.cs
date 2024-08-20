using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api2.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public EnumPoderes Poderes { get; set; }
    }
}