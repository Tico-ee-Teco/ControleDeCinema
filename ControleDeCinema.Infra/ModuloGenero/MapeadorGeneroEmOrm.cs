using ControleDeCinema.Dominio.ModuloGenero;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace ControleDeCinema.Infra.ModuloGenero;

public class MapeadorGeneroEmOrm : IEntityTypeConfiguration<Genero>
{
    public void Configure(EntityTypeBuilder<Genero> gBuilder)
    {
        gBuilder.ToTable("TBGenero");

        gBuilder.Property(g => g.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        gBuilder.Property(g => g.Nome)
            .IsRequired()
            .HasColumnType("varchar(50)");

        gBuilder.HasOne(g => g.Usuario)
            .WithMany()
            .HasForeignKey("Usuario_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        //fBuilder.HasData(ObterRegistrosPadrao());
    }

    //populando tabela no banco
    private Genero[] ObterRegistrosPadrao()
    {
        return
        [
            new Genero { Id = 1, Nome = "Ação" },
            new Genero { Id = 2, Nome = "Animação" },
            new Genero { Id = 3, Nome = "Aventura" },
            new Genero { Id = 4, Nome = "Comédia" },
            new Genero { Id = 5, Nome = "Romance" },
            new Genero { Id = 6, Nome = "Terror" }
        ];
    }
}