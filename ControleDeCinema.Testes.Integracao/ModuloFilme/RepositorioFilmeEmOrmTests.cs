using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloFilme;
using ControleDeCinema.Infra.ModuloGenero;

namespace ControleDeCinema.Testes.Integracao.moduloFilme;

[TestClass]
[TestCategory("Testes de Integração de Filme")]
public class RepositorioFilmeEmOrmTests
{
    RepositorioFilmeEmOrm repositorioFilme;
    RepositorioGeneroEmOrm repositorioGenero;
    ControleDeCinemaDbContext dbContext;

    [TestInitialize]
    public void ConfigurarTestes()
    {
       dbContext = new ControleDeCinemaDbContext(); 
       repositorioFilme = new RepositorioFilmeEmOrm(dbContext);
       repositorioGenero = new RepositorioGeneroEmOrm(dbContext);

       dbContext.Filmes.RemoveRange(dbContext.Filmes);
       dbContext.Generos.RemoveRange(dbContext.Generos);
    }

    [TestMethod]
    public void Deve_inserir_um_filme()
    {
        //Arrange
        Genero genero = new Genero("Ação");

        repositorioGenero.Inserir(genero);

        Filme filme = new Filme("Fui e Ja Volto",genero,DateTime.MinValue, false);

        //Act
        repositorioFilme.Inserir(filme);
        
        //Assert
        Assert.IsTrue(filme.Id > 0);
    }
}