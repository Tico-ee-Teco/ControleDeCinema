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

        dbContext.Filmes.RemoveRange(dbContext.Filmes);
        dbContext.Generos.RemoveRange(dbContext.Generos);
    }

    [TestMethod]
    public void Deve_inserir_um_genero()
    {
        //Arrange
        Genero novoGenero = new Genero("Suspense");

        //Act
        repositorioGenero.Inserir(novoGenero);
        
        //Assert
        Assert.IsTrue(novoGenero.Id > 0);
    }

    [TestMethod]
    public void Deve_editar_um_genero()
    {
        //Arrange
        Genero generoOriginal = new Genero("Suspense");

        repositorioGenero.Inserir(generoOriginal);

        Genero generoAtualizado = repositorioGenero.SelecionarPorId(generoOriginal.Id);

        generoAtualizado.Nome = "Terror";

        //Act
        bool resultado = repositorioGenero.Editar(generoOriginal, generoAtualizado);

        //Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    public void Deve_excluir_um_genero()
    {
        //Arrange
        Genero genero = new Genero("Suspense");

        repositorioGenero.Inserir(genero);

        //Act
        bool resultado = repositorioGenero.Excluir(genero);

        //Assert
        Assert.IsTrue(resultado);
    }

   
}

