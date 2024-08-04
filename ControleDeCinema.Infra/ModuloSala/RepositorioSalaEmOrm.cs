using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infra.ModuloSala
{
    public class RepositorioSalaEmOrm : RepositorioBaseEmOrm<Sala>, IRepositorioSala
    {
        public RepositorioSalaEmOrm(ControleDeCinemaDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Sala> ObterRegistros()
        {
            return dbContext.Salas;
        }
    }
}
