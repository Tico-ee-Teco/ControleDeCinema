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

        public override Sessao SelecionarPorId(int id)
        {
            return dbContext.Sessoes
                .Include(s => s.Filme)
                .Include(s => s.Sala)
                .FirstOrDefault(s => s.Id == id);
        }

        public List<IGrouping<string, Sessao>> ObterSessoesAgrupadas()
        {
            return ObterRegistros()
                .Include(s => s.Filme)
                .ThenInclude(f => f.Genero)
                .Include(s => s.Sala)
                .GroupBy(s => s.Filme.Titulo)
                .ToList();
        }
    }
}
