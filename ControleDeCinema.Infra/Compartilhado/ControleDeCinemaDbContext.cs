using ControleDeCinema.Dominio;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Dominio.ModulosSala;
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
                    .IsRequired()
                    .HasColumnType("int");

                filmeBuilder.Property(f => f.Estreia)
                    .IsRequired()
                    .HasColumnType("bit");

                filmeBuilder.HasOne(f => f.Genero)
                    .WithMany(g => g.Filmes)
                    .HasForeignKey("Genero_Id")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Funcionario>(funcionarioBuilder =>
            {
                funcionarioBuilder.ToTable("TBFuncionario");

                funcionarioBuilder.Property(f => f.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                funcionarioBuilder.Property(f => f.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                funcionarioBuilder.Property(f => f.CPF)
                    .IsRequired()
                    .HasColumnType("varchar(11)");

                funcionarioBuilder.Property(f => f.Login)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                funcionarioBuilder.Property(f => f.Senha)
                    .IsRequired()
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Sala>(salaBuilder =>
            {
                salaBuilder.ToTable("TBSala");

                salaBuilder.Property(s => s.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                salaBuilder.Property(s => s.Numero)
                    .IsRequired()
                    .HasColumnType("int");

                salaBuilder.Property(s => s.Capacidade)
                    .IsRequired()
                    .HasColumnType("int");
            });

            modelBuilder.Entity<Sessao>(sessaoBuilder =>
            {
                sessaoBuilder.ToTable("TBSessao");

                sessaoBuilder.Property(s => s.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                sessaoBuilder.Property(s => s.NumeroMaximoIngresso)
                    .IsRequired()
                    .HasColumnType("int");

                sessaoBuilder.Property(s => s.Data)
                    .IsRequired()
                    .HasColumnType("datetime");

                sessaoBuilder.HasOne(s => s.Sala)
                    .WithMany(s => s.Sessoes)
                    .HasForeignKey("Sala_Id")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                sessaoBuilder.HasOne(s => s.Filme)
                    .WithMany(f => f.Sessoes)
                    .HasForeignKey("Filme_Id")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Ingresso>(ingressoBuilder =>
            {
                ingressoBuilder.ToTable("TBIngresso");

                ingressoBuilder.Property(i => i.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                ingressoBuilder.Property(i => i.MeiaEntrada)
                    .IsRequired()
                    .HasColumnType("bit");

               ingressoBuilder.Property(i => i.NumeroAssento)
                    .IsRequired()
                    .HasColumnType("int");
            });
        }
    }
}

