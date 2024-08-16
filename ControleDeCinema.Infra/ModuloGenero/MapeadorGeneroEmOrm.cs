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

        gBuilder.Property(g => g.Descricao)
            .IsRequired()
            .HasColumnType("varchar(50)");

        gBuilder.Property(g => g.UsuarioId)
            .IsRequired()
            .HasColumnType("int")
            .HasColumnName("Usuario_Id");

        gBuilder.HasOne(g => g.Usuario)
            .WithMany()
            .HasForeignKey("Usuario_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        //gBuilder.HasData(ObterRegistrosPadrao());
    }

    //populando tabela no banco
    private Genero[] ObterRegistrosPadrao()
    {
        return
        [
            new Genero { Id = 1, Descricao = "Ação" },
            new Genero { Id = 2, Descricao = "Animação" },
            new Genero { Id = 3, Descricao = "Aventura" },
            new Genero { Id = 4, Descricao = "Comédia" },
            new Genero { Id = 5, Descricao = "Romance" },
            new Genero { Id = 6, Descricao = "Terror" }
        ];
    }
}