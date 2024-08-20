using minimal_api2.Entities;

namespace Teste.Entities;

[TestClass]
public class AdministradorTest
{
    [TestMethod]
    public void TestasGetSetProps()
    {
        var adm = new Usuario();

        adm.Id = 1;
        adm.Email = "teste@teste.com";
        adm.Password = "teste";
        adm.Poderes = EnumPoderes.Administrador;

        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("teste@teste.com", adm.Email);
        Assert.AreEqual("teste", adm.Password);
        Assert.AreEqual(EnumPoderes.Administrador, adm.Poderes);

        var car = new Veiculo();
        car.Id = 1;
        car.Marca = "Volks";
        car.Modelo = "kombi";
        car.Ano = 1998;
        car.KmRodados = 123415;

        Assert.AreEqual(1, car.Id);
        Assert.AreEqual("Volks", car.Marca);
        Assert.AreEqual("kombi", car.Modelo);
        Assert.AreEqual(1998, car.Ano);
        Assert.AreEqual(123415, car.KmRodados);

   }
}