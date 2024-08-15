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

        public List<Sessao> Filtrar(Func<Sessao, bool> predicate)
        {
            throw new NotImplementedException();
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

        public List<IGrouping<string, Sessao>> ObterSessoesAgrupadas(int usuarioId)
        {
            return dbContext.Sessoes
                .Where(s => s.UsuarioId == usuarioId)
                .Include(s => s.Filme)
                .ThenInclude(f => f.Genero)
                .Include(s => s.Sala)
                .Include(s => s.Ingressos)
                .GroupBy(s => s.Filme.Titulo)
                .AsNoTracking()
                .ToList();
        }

        public List<IGrouping<string, Sessao>> ObterSessoesDisponiveisAgrupadas()
        {
            throw new NotImplementedException();
        }
    }
}
