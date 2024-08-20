using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api2.Entities
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Modelo { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public int Ano { get; set; }
        public int KmRodados { get; set; }
    }
}