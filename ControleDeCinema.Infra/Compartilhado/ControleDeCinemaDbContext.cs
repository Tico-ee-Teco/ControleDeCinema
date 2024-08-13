using ControleDeCinema.Dominio;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.ModuloFilme;
using ControleDeCinema.Infra.ModuloFuncionario;
using ControleDeCinema.Infra.ModuloGenero;
using ControleDeCinema.Infra.ModuloSala;
using ControleDeCinema.Infra.ModuloSessao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControleDeCinema.Infra.Compartilhado
{
    public class ControleDeCinemaDbContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        
        public DbSet<Ingresso> Ingressos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("SqlServer")!;

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapeadorGeneroEmOrm());
            modelBuilder.ApplyConfiguration(new MapeadorFilmeEmOrm());
            modelBuilder.ApplyConfiguration(new MapeadorFuncionarioEmOrm());
            modelBuilder.ApplyConfiguration(new MapeadorSalaEmOrm());
            modelBuilder.ApplyConfiguration(new MapeadorSessaoEmOrm());
            modelBuilder.ApplyConfiguration(new MapeadorIngressoEmOrm());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}