using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api2.Entities;

namespace minimal_api2.Context
{
    public class VeiculosContext : DbContext
    {
        public VeiculosContext(DbContextOptions<VeiculosContext> options) : base(options){

        }

        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}