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

    [TestMethod]
    public void Deve_editar_um_filme()
    {
        //Arrange
        Genero genero = new Genero("Ação");

        repositorioGenero.Inserir(genero);

        Filme filmeOriginal = new Filme("Fui e Ja Volto",genero,DateTime.MinValue, false);

        repositorioFilme.Inserir(filmeOriginal);

        Filme filmeAtualizado = repositorioFilme.SelecionarPorId(filmeOriginal.Id);

        filmeAtualizado.Titulo = "Fui e Voltei";

        //Act
        repositorioFilme.Editar(filmeOriginal, filmeAtualizado);

        //Assert
        Assert.AreEqual(filmeOriginal.Titulo, filmeAtualizado.Titulo);
    }

    [TestMethod]
    public void Deve_excluir_um_filme()
    {
        //Arrange
        Genero genero = new Genero("Ação");

        repositorioGenero.Inserir(genero);

        Filme filme = new Filme("Fui e Ja Volto",genero,DateTime.MinValue, false);

        repositorioFilme.Inserir(filme);

        //Act
        repositorioFilme.Excluir(filme);

        //Assert
        Filme filmeSelecinado = repositorioFilme.SelecionarPorId(filme.Id);

        Assert.IsNull(filmeSelecinado);
    }

    [TestMethod]
    public void Deve_selecionar_um_filme_por_id()
    {
        //Arrange
        Genero genero = new Genero("Ação");

        repositorioGenero.Inserir(genero);

        Filme filme = new Filme("Fui e Ja Volto",genero,DateTime.MinValue, false);

        repositorioFilme.Inserir(filme);

        //Act
        Filme filmeSelecinado = repositorioFilme.SelecionarPorId(filme.Id);

        //Assert
        Assert.IsNotNull(filmeSelecinado);
    }

    [TestMethod]
    public void Deve_selecionar_todos_os_filmes()
    {
        //Arrange
        Genero genero = new Genero("Ação");

        repositorioGenero.Inserir(genero);

        List<Filme> filmesParainseir =
        [
            new Filme("Fui e Ja Volto",genero,DateTime.MinValue, false),
            new Filme("Fui e Voltei",genero,DateTime.MinValue, false),
            new Filme("Fui e Fiquei", genero, DateTime.MinValue, true)

        ];

        foreach( Filme filme in filmesParainseir)
            repositorioFilme.Inserir(filme);

        //Act
        List<Filme> filmesSelecionados = repositorioFilme.SelecionarTodos();

        //Assert
        Assert.AreEqual(3, filmesSelecionados.Count);
        CollectionAssert.AreEqual(filmesParainseir, filmesSelecionados);
        
    }
}