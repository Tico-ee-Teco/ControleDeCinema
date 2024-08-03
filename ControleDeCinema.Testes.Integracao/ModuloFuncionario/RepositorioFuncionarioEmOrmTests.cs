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
        
    }
}