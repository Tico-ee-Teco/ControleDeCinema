using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.ModuloFilme
{
    public class RepositorioFilmeEmOrm : IRepositorioFilme
    { 
        ControleDeCinemaDbContext dbContext;

        public RepositorioFilmeEmOrm(ControleDeCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       public void Inserir(Filme registro)
       {
           dbContext.Filmes.Add(registro);

           dbContext.SaveChanges();
       }

       public bool Editar(Filme registroOriginal, Filme registroAtualizado)
       {
           if(registroOriginal == null || registroAtualizado == null)
               return false;

           registroOriginal.AtualizarInformacoes(registroAtualizado);

           dbContext.Filmes.Update(registroOriginal);

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
           return dbContext.Filmes.Find(id)!;
       }

       public List<Filme> SelecionarTodos()
       {
           return dbContext.Filmes
               .Include(f => f.Genero)
               .ToList();
        }

       
    }
}
