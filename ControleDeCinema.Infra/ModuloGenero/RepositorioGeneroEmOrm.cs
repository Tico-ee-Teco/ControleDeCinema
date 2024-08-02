using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.ModuloGenero
{
    public class RepositorioGeneroEmOrm : RepositorioBaseEmOrm<Genero>, IRepositorioGenero
    {
        public RepositorioGeneroEmOrm(ControleDeCinemaDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Genero> ObterRegistros()
        {
           return dbContext.Generos;
        }

        public void Inserir(Genero registro)
        {
            dbContext.Generos.Add(registro);

            dbContext.SaveChanges();
        }

        public override bool Editar(Genero registroAtualizado)
        {
           if(registroAtualizado == null)
                return false;

            dbContext.Generos.Update(registroAtualizado);

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
            return dbContext.Generos
                .FirstOrDefault(g => g.Id == id)!;
        }

       
    }
}
