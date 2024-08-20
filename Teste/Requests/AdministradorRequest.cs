using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using minimal_api2.Context;
using minimal_api2.Controllers;
using minimal_api2.Entities;
using System.Reflection;
using System.Text.Json;
using System.Text;
using System.Net;
using minimal_api2.Models;

namespace Teste.Requests;

[TestClass]
public class AdministradorRequest
{
    public static HttpClient client { get; set; } = default!;

    [ClassInitialize]
    [TestMethod]
    public async Task TestarGetSetProps(){
        var loginrequest = new LoginRequest{
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginrequest), Encoding.UTF8, "Application/json");

        var response = await client.PostAsync("/Usuario/Login", content);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var usuarioLogado = JsonSerializer.Deserialize<UsuarioLogadoView>(result, new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(usuarioLogado?.Email ?? "");
        Assert.IsNotNull(usuarioLogado?.Poderes.ToString() ?? "");
        Assert.IsNotNull(usuarioLogado?.Token ?? "");
    }
}