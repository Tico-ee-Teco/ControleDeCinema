using ControleDeCinema.Dominio;
using ControleDeCinema.Dominio.ModuloFilme;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControleDeCinema.Infra.Compartilhado
{
    public class ControleDeCinemaDbContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }

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
            modelBuilder.Entity<Genero>(generoBuilder =>
            {
                generoBuilder.ToTable("TBGenero");

                generoBuilder.Property(g => g.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                generoBuilder.Property(g => g.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Filme>(filmeBuilder =>
            {
                filmeBuilder.ToTable("TBFilme");

                filmeBuilder.Property(f => f.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                filmeBuilder.Property(f => f.Titulo)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                filmeBuilder.Property(f => f.Duracao)
                    .HasColumnType("datetime2");

                filmeBuilder.Property(f => f.Estreia)
                    .IsRequired()
                    .HasColumnType("bit");

                filmeBuilder.HasOne(f => f.Genero)
                    .WithMany()
                    .HasForeignKey("Genero_Id")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

