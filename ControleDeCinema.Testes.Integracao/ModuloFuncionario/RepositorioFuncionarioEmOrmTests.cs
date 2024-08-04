using ControleDeCinema.Dominio;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloFuncionario;

namespace ControleDeCinema.Testes.Integracao.ModuloFuncionario;

[TestClass]
[TestCategory("Testes de Integração de Funcionario")]
public class RepositorioFuncionarioEmOrmTests
{
    RepositorioFuncionarioEmOrm repositorioFuncionario;
    ControleDeCinemaDbContext dbContext;

    [TestInitialize]
    public void ConfigurarTestes()
    {
        dbContext = new ControleDeCinemaDbContext();
        repositorioFuncionario = new RepositorioFuncionarioEmOrm(dbContext);

        dbContext.Funcionarios.RemoveRange(dbContext.Funcionarios);
    }

    [TestMethod]
    public void Deve_inserir_um_funcionario()
    {
        //Arrange
        Funcionario funcionario = new Funcionario("Alice", "12345678900", "123456", "123456");

        //Act
        repositorioFuncionario.Inserir(funcionario);

        //Assert
        Assert.IsTrue(funcionario.Id > 0);
    }

    [TestMethod]
    public void Deve_editar_um_funcionario()
    {
        //Arrange
        Funcionario funcionarioOriginal = new Funcionario("Alice", "12345678900", "123456", "123456");

        repositorioFuncionario.Inserir(funcionarioOriginal);

        Funcionario funcionarioAtualizado = repositorioFuncionario.SelecionarPorId(funcionarioOriginal.Id);

        funcionarioAtualizado.Nome = "Alice Editada";

        //Act
        repositorioFuncionario.Editar(funcionarioAtualizado);

        //Assert
        Assert.AreEqual("Alice Editada", funcionarioAtualizado.Nome);
    }

    [TestMethod]
    public void Deve_excluir_um_funcionario()
    {
        //Arrange
        Funcionario funcionario = new Funcionario("Alice", "12345678900", "123456", "123456");

        repositorioFuncionario.Inserir(funcionario);

        //Act
        repositorioFuncionario.Excluir(funcionario);

        //Assert
        Assert.IsNull(repositorioFuncionario.SelecionarPorId(funcionario.Id));
    }

    [TestMethod]
    public void Deve_selecionar_um_funcionario_por_id()
    {
        //Arrange
        Funcionario funcionario = new Funcionario("Alice", "12345678900", "123456", "123456");

        repositorioFuncionario.Inserir(funcionario);

        //Act
        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarPorId(funcionario.Id);

        //Assert
        Assert.AreEqual(funcionario.Id, funcionarioSelecionado.Id);
    }

    [TestMethod]
    public void Deve_selecionar_todos_os_funcionarios()
    {
        //Arrange
        List<Funcionario> funcinariosParaInserir =
        [
            new Funcionario("Alice", "12345678900", "123456", "123456"),
            new Funcionario("Bob", "12345678900", "123456", "123456")

        ];

        foreach (var f in funcinariosParaInserir)
        {
            repositorioFuncionario.Inserir(f);
        }

        //Act
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        //Assert
        CollectionAssert.AreEqual(funcinariosParaInserir, funcionarios);
    }

}