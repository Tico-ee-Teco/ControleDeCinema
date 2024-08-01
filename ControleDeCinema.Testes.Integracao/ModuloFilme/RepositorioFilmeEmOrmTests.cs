using ControleDeCinema.Dominio;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloFilme;
using ControleDeCinema.Infra.ModuloGenero;

namespace ControleDeCinema.Testes.Integracao;

[TestClass]
public class RepositorioFilmeEmOrmTests
{
    [TestMethod]
    public void Deve_inserir_um_filme()
    {
        ControleDeCinemaDbContext dbContext = new ControleDeCinemaDbContext();

        var genero = new Genero("Ação");
        RepositorioGeneroEmOrm repositorioGenero = new RepositorioGeneroEmOrm(dbContext);
        repositorioGenero.Inserir(genero);

        Filme filme = new Filme();
        RepositorioFilmeEmOrm repositorioFilme = new RepositorioFilmeEmOrm(dbContext);
        repositorioFilme.Inserir(filme);

        Assert.IsTrue(filme.Id > 0);
    }
}