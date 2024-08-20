using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minimal_api2.Context;
using minimal_api2.Controllers;
using minimal_api2.Entities;

namespace Teste.Mocks;

public class VeiculosServiceMock : VeiculoController
{
    private readonly VeiculosContext _context;

    public VeiculosServiceMock(VeiculosContext context) : base (context)
    {
        _context = context;
    }


    private static List<Veiculo> veiculos = new List<Veiculo>(){
        new Veiculo{
            Id = 1,
            Modelo = "A3",
            Marca = "Audi",
            Ano = 2024,
            KmRodados = 2
        },
        new Veiculo{
            Id = 2,
            Modelo = "A4",
            Marca = "Audi",
            Ano = 2024,
            KmRodados = 10
        }
    };
    
        [Authorize(Roles = "Usuario, Supervisor, Gerente, Administrador")]
        [HttpGet("ListarVeiculosCadastrados")]
        public IActionResult ListarVeiculos(){
        var listaVeiculos = _context.Veiculos;
        return Ok(listaVeiculos);
        }

        [Authorize(Roles = "Usuario, Supervisor, Gerente, Administrador")]
        [HttpGet("{id}")]
        public IActionResult VeiculoPorId(int id){
        var veiculo = _context.Veiculos.Find(id);
        return Ok(veiculo);
        }

        [Authorize(Roles = "Supervisor, Gerente, Administrador")]
        [HttpPost("CadastrarVeiculos")]
        IActionResult CadastrarVeiculo(Veiculo veiculo){
        _context.Veiculos.Add(veiculo);
        _context.SaveChanges();
        return Ok("Cadastrado com sucesso");
        }

        [Authorize(Roles = "Supervisor, Gerente, Administrador")]
        [HttpPut("{id}")]
        IActionResult AlterarVeiculo(int id, Veiculo veiculo){
        var veiculoAlterado = _context.Veiculos.Find(id);
        if(veiculoAlterado == null){
            return NotFound("Veiculo não encontrado");
        }
        veiculoAlterado.Modelo = veiculo.Modelo;
        veiculoAlterado.Marca = veiculo.Marca;
        veiculoAlterado.Ano = veiculo.Ano;
        veiculoAlterado.KmRodados = veiculo.KmRodados;

        _context.Veiculos.Update(veiculoAlterado);
        _context.SaveChanges();
        return Ok("Veiculo alterado com sucesso!");
        }

        [Authorize(Roles = "Gerente, Administrador")]
        [HttpDelete("{id}")]
        IActionResult DeletarVeiculo(int id){
        var veiculoBanco = _context.Veiculos.Find(id);
        if(veiculoBanco == null){
            return NotFound("Veiculo não encontrado");
        }

        _context.Veiculos.Remove(veiculoBanco);
        _context.SaveChanges();
        return Ok("Veiculo removido com sucesso");
        }
}