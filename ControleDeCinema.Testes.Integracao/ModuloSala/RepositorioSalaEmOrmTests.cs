using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloSala;

namespace ControleDeCinema.Testes.Integracao.ModuloSala
{
    [TestClass]
    [TestCategory("Testes de integracao de Sala")]
    public class RepositorioSalaEmOrmTests
    {
        RepositorioSalaEmOrm repositorioSala;
        ControleDeCinemaDbContext dbContext;

        [TestInitialize]
        public void ConfigurarTestes()
        {
            dbContext = new ControleDeCinemaDbContext();
            repositorioSala = new RepositorioSalaEmOrm(dbContext);

            dbContext.Salas.RemoveRange(dbContext.Salas);
        }

        [TestMethod]
        public void Deve_Inserir_Sala()
        {
            // Arrange
            var sala = new Sala("Sala 1", 100);

            // Act
            repositorioSala.Inserir(sala);

            // Assert
            var salaInserida = dbContext.Salas.FirstOrDefault();
            Assert.IsNotNull(salaInserida);
            Assert.AreEqual(sala.Nome, salaInserida.Nome);
            Assert.AreEqual(sala.Capacidade, salaInserida.Capacidade);
        }

        [TestMethod]
        public void Deve_Editar_Sala()
        {
            // Arrange
            Sala salaOriginal = new Sala("Sala 1", 100);

            repositorioSala.Inserir(salaOriginal);

            Sala salaEditada = repositorioSala.SelecionarPorId(salaOriginal.Id);

            salaEditada.Nome = "Sala 2";
            salaEditada.Capacidade = 200;

            // Act
            repositorioSala.Editar(salaEditada);

            // Assert
            var salaEditadaNoBanco = dbContext.Salas.FirstOrDefault();
            Assert.IsNotNull(salaEditadaNoBanco);
            Assert.AreEqual(salaEditada.Nome, salaEditadaNoBanco.Nome);
            Assert.AreEqual(salaEditada.Capacidade, salaEditadaNoBanco.Capacidade);
        }

        [TestMethod]
        public void Deve_Excluir_Sala()
        {
            // Arrange
            Sala sala = new Sala("Sala 1", 100);

            repositorioSala.Inserir(sala);

            // Act
            repositorioSala.Excluir(sala);

            // Assert
            var salaExcluida = dbContext.Salas.FirstOrDefault();
            Assert.IsNull(salaExcluida);
        }

        [TestMethod]
        public void Deve_Selecionar_Sala_Por_Id()
        {
            // Arrange
            Sala sala = new Sala("Sala 1", 100);

            repositorioSala.Inserir(sala);

            // Act
            var salaSelecionada = repositorioSala.SelecionarPorId(sala.Id);

            // Assert
            Assert.IsNotNull(salaSelecionada);
            Assert.AreEqual(sala.Id, salaSelecionada.Id);
            Assert.AreEqual(sala.Nome, salaSelecionada.Nome);
            Assert.AreEqual(sala.Capacidade, salaSelecionada.Capacidade);
        }

        [TestMethod]
        public void Deve_Selecionar_Todas_As_Salas()
        {
            // Arrange
            Sala sala1 = new Sala("Sala 1", 100);
            Sala sala2 = new Sala("Sala 2", 200);

            repositorioSala.Inserir(sala1);
            repositorioSala.Inserir(sala2);

            // Act
            var salas = repositorioSala.SelecionarTodos();

            // Assert
            Assert.AreEqual(2, salas.Count);
        }
    }

}
