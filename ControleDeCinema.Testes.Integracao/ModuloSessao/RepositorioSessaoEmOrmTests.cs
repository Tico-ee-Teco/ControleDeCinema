using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloFilme;
using ControleDeCinema.Infra.ModuloGenero;
using ControleDeCinema.Infra.ModuloSala;
using ControleDeCinema.Infra.ModuloSessao;

namespace ControleDeCinema.Testes.Integracao.ModuloSessao
{
    [TestClass]
    [TestCategory("Testes de integracao de Sessao")]
    public class RepositorioSalaEmOrmTests
    {
        ControleDeCinemaDbContext dbContext = null;

        RepositorioSalaEmOrm repositorioSala = null;
        RepositorioFilmeEmOrm repositorioFilme = null;
        RepositorioGeneroEmOrm repositorioGenero = null;
        RepositorioSessaoEmOrm repositorioSessao = null;

        [TestInitialize]
        public void ConfigurarTestes()
        {
            dbContext = new ControleDeCinemaDbContext();

            repositorioSala = new RepositorioSalaEmOrm(dbContext);
            repositorioFilme = new RepositorioFilmeEmOrm(dbContext);
            repositorioGenero = new RepositorioGeneroEmOrm(dbContext);
            repositorioSessao = new RepositorioSessaoEmOrm(dbContext);

            dbContext.Salas.RemoveRange(dbContext.Salas);
            dbContext.Filmes.RemoveRange(dbContext.Filmes);
            dbContext.Generos.RemoveRange(dbContext.Generos);
            dbContext.Sessoes.RemoveRange(dbContext.Sessoes);
        }

        [TestMethod]
        public void Deve_Inserir_Sessao()
        {
            Genero genero = new Genero("Ação");
            Filme filme = new Filme("Vingadores", genero, 120, false);
            Sala sala = new Sala(1, 10);

            repositorioGenero.Inserir(genero);
            repositorioFilme.Inserir(filme);
            repositorioSala.Inserir(sala);

            Sessao sessao = new Sessao(10, DateTime.Now, sala, filme);

            repositorioSessao.Inserir(sessao);

            Sessao sessaoEncontrada = repositorioSessao.SelecionarPorId(sessao.Id);

            Assert.IsNotNull(sessaoEncontrada);
            Assert.AreEqual(sessao.Id, sessaoEncontrada.Id);
            Assert.AreEqual(sessao.NumeroMaximoIngresso, sessaoEncontrada.NumeroMaximoIngresso);
            Assert.AreEqual(sessao.Data, sessaoEncontrada.Data);
            Assert.AreEqual(sessao.Sala.Id, sessaoEncontrada.Sala.Id);
            Assert.AreEqual(sessao.Filme.Id, sessaoEncontrada.Filme.Id);
        }

        [TestMethod]
        public void Deve_Editar_Sessao()
        {
            Genero genero = new Genero("Ação");
            Filme filme = new Filme("Vingadores", genero, 120, false);
            Sala sala = new Sala(1, 10);

            repositorioGenero.Inserir(genero);
            repositorioFilme.Inserir(filme);
            repositorioSala.Inserir(sala);

            Sessao sessao = new Sessao(10, DateTime.Now, sala, filme);

            repositorioSessao.Inserir(sessao);

            Sessao sessaoEditada = repositorioSessao.SelecionarPorId(sessao.Id);

            sessaoEditada.NumeroMaximoIngresso = 20;
            sessaoEditada.Data = DateTime.Now.AddDays(1);

            repositorioSessao.Editar(sessaoEditada);

            Sessao sessaoEditadaNoBanco = repositorioSessao.SelecionarPorId(sessao.Id);

            Assert.IsNotNull(sessaoEditadaNoBanco);
            Assert.AreEqual(sessaoEditada.Id, sessaoEditadaNoBanco.Id);
            Assert.AreEqual(sessaoEditada.NumeroMaximoIngresso, sessaoEditadaNoBanco.NumeroMaximoIngresso);
            Assert.AreEqual(sessaoEditada.Data, sessaoEditadaNoBanco.Data);
            Assert.AreEqual(sessaoEditada.Sala.Id, sessaoEditadaNoBanco.Sala.Id);
            Assert.AreEqual(sessaoEditada.Filme.Id, sessaoEditadaNoBanco.Filme.Id);
        }

        [TestMethod]
        public void Deve_Excluir_Sessao()
        {
            Genero genero = new Genero("Ação");
            Filme filme = new Filme("Vingadores", genero, 120, false);
            Sala sala = new Sala(1, 10);

            repositorioGenero.Inserir(genero);
            repositorioFilme.Inserir(filme);
            repositorioSala.Inserir(sala);

            Sessao sessao = new Sessao(10, DateTime.Now, sala, filme);

            repositorioSessao.Inserir(sessao);

            repositorioSessao.Excluir(sessao);

            Sessao sessaoExcluida = repositorioSessao.SelecionarPorId(sessao.Id);

            Assert.IsNull(sessaoExcluida);
        }


    }
}
