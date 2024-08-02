using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloGenero;

namespace ControleDeCinema.Testes.Integracao.ModuloGenero;

[TestClass]
[TestCategory("Testes de Integração de Genero")]
public class RepositorioGeneroemOrmTests
{
    RepositorioGeneroEmOrm repositorioGenero;
    ControleDeCinemaDbContext dbContext;

    [TestInitialize]
    public void ConfigurarTestes()
    {
        dbContext = new ControleDeCinemaDbContext(); 
        repositorioGenero = new RepositorioGeneroEmOrm(dbContext);

        dbContext.Generos.RemoveRange(dbContext.Generos);
    }

    [TestMethod]
    public void Deve_inserir_um_genero()
    {
        //Arrange
        Genero genero = new Genero("Ação");

        //Act
        repositorioGenero.Inserir(genero);
        
        //Assert
        Assert.IsTrue(genero.Id > 0);
    }

    [TestMethod]
    public void Deve_editar_um_genero()
    {
        //Arrange
        Genero generoOriginal = new Genero("Ação");

        repositorioGenero.Inserir(generoOriginal);

        Genero generoAtualizado = repositorioGenero.SelecionarPorId(generoOriginal.Id);

        generoAtualizado.Nome = "Ação e Aventura";

        //Act
        repositorioGenero.Editar(generoOriginal, generoAtualizado);

        //Assert
        Assert.AreEqual(generoOriginal.Nome, generoAtualizado.Nome);
    }

    [TestMethod]
    public void Deve_excluir_um_genero()
    {
        //Arrange
        Genero genero = new Genero("Ação");

        repositorioGenero.Inserir(genero);

        //Act
        repositorioGenero.Excluir(genero);

        //Assert
        Assert.IsNull(repositorioGenero.SelecionarPorId(genero.Id));
    }
}

