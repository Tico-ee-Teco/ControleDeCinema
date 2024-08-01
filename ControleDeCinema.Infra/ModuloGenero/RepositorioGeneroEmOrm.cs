using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Infra.Compartilhado;

namespace ControleDeCinema.Infra.ModuloGenero
{
    public class RepositorioGeneroEmOrm : IRepositorioGenero
    {
        ControleDeCinemaDbContext dbContext;
        public RepositorioGeneroEmOrm(ControleDeCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Inserir(Genero registro)
        {
            dbContext.Generos.Add(registro);

            dbContext.SaveChanges();
        }

        public bool Editar(Genero registroOriginal, Genero registroAtualizado)
        {
           if(registroOriginal == null || registroAtualizado == null)
                return false;

            registroOriginal.AtualizarInformacoes(registroAtualizado);

            dbContext.Generos.Update(registroOriginal);

            dbContext.SaveChanges();

            return true;
        }

        public bool Excluir(Genero registro)
        {
            if(registro == null)
                return false;

            dbContext.Generos.Remove(registro);

            dbContext.SaveChanges();

            return true;
        }

        public Genero SelecionarPorId(int id)
        {
            return dbContext.Generos.Find(id)!;
        }

        public List<Genero> SelecionarTudo()
        {
            return dbContext.Generos.ToList();
        }
    }
}
