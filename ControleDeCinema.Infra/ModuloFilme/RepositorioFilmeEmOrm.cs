using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.ModuloFilme
{
    public class RepositorioFilmeEmOrm : RepositorioBaseEmOrm<Filme>, IRepositorioFilme
    { 
        public RepositorioFilmeEmOrm(ControleDeCinemaDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Filme> ObterRegistros()
        {
           return dbContext.Filmes;
        }

        public void Inserir(Filme registro)
       {
           dbContext.Filmes.Add(registro);

           dbContext.SaveChanges();
       }

       public override bool Editar(Filme registroAtualizado)
       {
           if(registroAtualizado == null)
               return false;

           dbContext.Filmes.Update(registroAtualizado);

           dbContext.SaveChanges();

           return true;
       }

       public bool Excluir(Filme registro)
       {
           if(registro == null)
               return false;

           dbContext.Filmes.Remove(registro);

           dbContext.SaveChanges();

           return true;
       }

       public Filme SelecionarPorId(int id)
       {
           return dbContext.Filmes
               .Include(f => f.Genero)
               .FirstOrDefault(F => F.Id == id);
       }

       public List<Filme> SelecionarTodos()
       {
           return dbContext.Filmes
               .Include(f => f.Genero)
               .ToList();
        }

       
    }
}
