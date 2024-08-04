using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;

namespace ControleDeCinema.Testes.Integracao.ModuloSala
{
    [TestClass]
    [TestCategory("Testes de integracao de Sala")]
    public class RepositorioSalaEmOrmTests
    {
        RepositorioSalaEmOrm repositorioSalaEmOrm;
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
            Assert.AreEqual(sala.QuantidadeDeLugares, salaInserida.QuantidadeDeLugares);
        }

        [TestMethod]
        public void Deve_Editar_Sala()
        {
            // Arrange
            Sala salaOriginal = new Sala("Sala 1", 100);

            repositorioSala.Inserir(salaOriginal);

            Sala salaEditada = repositorioSala.SelecionarPorid(salaOriginal.Id);

            salaEditada.Nome = "Sala 2";
            salaEditada.QuantidadeDeLugares = 200;

            // Act
            repositorioSala.Editar(salaEditada);

            // Assert
            var salaEditadaNoBanco = dbContext.Salas.FirstOrDefault();
            Assert.IsNotNull(salaEditadaNoBanco);
            Assert.AreEqual(salaEditada.Nome, salaEditadaNoBanco.Nome);
            Assert.AreEqual(salaEditada.QuantidadeDeLugares, salaEditadaNoBanco.QuantidadeDeLugares);
        }
    }
}
