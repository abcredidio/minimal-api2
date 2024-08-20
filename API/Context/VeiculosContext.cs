using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using minimal_api2.Entities;

namespace minimal_api2.Context
{
    public class VeiculosContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Email = "adm@teste.com",
                    Password = "123456",
                    Poderes = EnumPoderes.Administrador
                }
            );
        }
        public VeiculosContext(DbContextOptions<VeiculosContext> options) : base(options){

        }

        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}