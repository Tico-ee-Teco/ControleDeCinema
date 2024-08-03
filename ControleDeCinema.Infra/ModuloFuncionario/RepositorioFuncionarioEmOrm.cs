using ControleDeCinema.Dominio;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.ModuloFuncionario;

public class RepositorioFuncionarioEmOrm : RepositorioBaseEmOrm<Funcionario>,IRepositorioFuncionario
{
    public RepositorioFuncionarioEmOrm(ControleDeCinemaDbContext dbContext) : base(dbContext)
    {
    }
    protected override DbSet<Funcionario> ObterRegistros()
    {
        return dbContext.Funcionarios;
    }
}