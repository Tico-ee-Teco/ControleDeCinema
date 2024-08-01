using ControleDeCinema.Dominio;
using ControleDeCinema.Infra.Compartilhado;

namespace ControleDeCinema.Testes.Integracao;

[TestClass]
public class RepositorioFilmeEmOrmTests
{
    [TestMethod]
    public void Deve_inserir_um_filme()
    {
        ControleDeCinemaDbContext dbContext = new ControleDeCinemaDbContext();

        var genero = new Genero("Ação");
        RepositorioGeneroOrm repositorioGenero = new RepositorioGeneroOrm(dbContext);
        repositorioGenero.Inserir(genero);

        Filme filme = new Filme();
        RepositorioFilmeOrm repositorioFilme = new RepositorioFilmeOrm(dbContext);
        repositorioFilme.Inserir(filme);

        Assert.IsTrue(filme.Id > 0);
    }
}