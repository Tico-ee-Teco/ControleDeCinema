using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.ModuloSessao
{
    public class RepositorioSessaoEmOrm : RepositorioBaseEmOrm<Sessao>, IRepositorioSessao
    {
        public RepositorioSessaoEmOrm(ControleDeCinemaDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Sessao> ObterRegistros()
        {
            return dbContext.Sessoes;
        }
    }
}
