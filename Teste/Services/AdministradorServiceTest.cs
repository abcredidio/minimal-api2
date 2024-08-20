using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using minimal_api2.Context;
using minimal_api2.Controllers;
using minimal_api2.Entities;
using System.Reflection;

namespace Teste.Services;

[TestClass]
public class AdministradorTestServiceTest
{

    private VeiculosContext CriarContextoTest(){
        // Configuração do Builder
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<VeiculosContext>();
        optionsBuilder.UseMySql(configuration.GetConnectionString("ConexaoPadrao"), new MySqlServerVersion(new Version(8, 0, 39)));

        return new VeiculosContext(optionsBuilder.Options);
    }

    [TestMethod]
    public void TestandoSalverUsuario()
    {
        //Arrange
        var contexto = CriarContextoTest();
        contexto.Database.ExecuteSqlRaw("TRUNCATE TABLE Usuarios");
        contexto.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var usuarioControl = new UsuarioController(contexto);
        var veiculoControl = new VeiculoController(contexto);

        var adm = new Usuario();
        var car = new Veiculo();
        
        adm.Email = "teste@teste.com";
        adm.Password = "teste";
        adm.Poderes = EnumPoderes.Administrador;

        car.Marca = "Volks";
        car.Modelo = "kombi";
        car.Ano = 1998;
        car.KmRodados = 123415;

        //Act        
        usuarioControl.NovoUsuario(adm);
        veiculoControl.CadastrarVeiculo(car);
   }
}