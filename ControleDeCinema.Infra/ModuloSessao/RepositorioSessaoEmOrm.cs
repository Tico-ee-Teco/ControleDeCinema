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

        public override Sessao? SelecionarPorId(int id)
        {
            return dbContext.Sessoes
                .Include(s => s.Filme)
                .Include(s => s.Sala)
                .FirstOrDefault(s => s.Id == id);
        }

        public override List<Sessao> SelecionarTodos()
        {
            return dbContext.Sessoes
                .Include(s => s.Filme)
                .Include(s => s.Sala)
                .AsNoTracking()
                .ToList();
        }

        public List<Sessao> Filtrar(Func<Sessao, bool> predicate)
        {
            return dbContext.Sessoes
                .Include(s => s.Filme)
                .Include(s => s.Sala)
                .AsNoTracking().AsEnumerable()
                .Where(predicate)
                .ToList();
        }

        public List<IGrouping<string, Sessao>> ObterSessoesAgrupadas()
        {
            return dbContext.Sessoes
                .Where(s => !s.Encerrada)
                .Include(s => s.Filme)
                .ThenInclude(f => f.Genero)
                .Include(s => s.Sala)
                .Include(s => s.Ingressos)
                .GroupBy(s => s.Filme.Titulo)
                .AsNoTracking()
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

        public List<Ingresso> SelecionarTodosIngressos(int usuarioSessaoId)
        {
            return dbContext.Ingressos
                .Include(i => i.Sessao)
                .Where(i => i.Sessao.UsuarioId == usuarioSessaoId)
                .ToList();
        }

        public List<Ingresso> SelecionarTodosIngressos()
        {
            return dbContext.Ingressos
                .ToList();
        }

        public List<int> ObterNumerosAssentosOcupados(int sessaoId)
        {
            return dbContext.Ingressos
                .Where(s => s.Id == sessaoId)
                .Include(s => s.Sessao)
                .Select(s => s.NumeroAssento)
                .ToList();
        }
       
    }
    
}
