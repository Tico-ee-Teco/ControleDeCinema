using ControleDeCinema.Dominio.ModuloFilme;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeCinema.Infra.ModuloFilme;

public class MapeadorFilmeEmOrm : IEntityTypeConfiguration<Filme>
{
    public void Configure(EntityTypeBuilder<Filme> fBuilder)
    {
        fBuilder.ToTable("TBFilme");

        fBuilder.Property(f => f.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        fBuilder.Property(f => f.Titulo)
            .IsRequired()
            .HasColumnType("varchar(200)");

        fBuilder.Property(f => f.Duracao)
            .IsRequired()
            .HasColumnType("int");

        fBuilder.Property(f => f.Estreia)
            .IsRequired()
            .HasColumnType("bit");

        fBuilder.HasOne(f => f.Genero)
            .WithMany(g => g.Filmes)
            .HasForeignKey("Genero_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        fBuilder.HasData(ObterRegistrosPadrao());
    }
    
    //populando dados no banco
    private object[] ObterRegistrosPadrao()
    {
        return
        [
            new
            {
                Id = 1,
                Titulo = "Aladdin",
                Duracao = 90,
                Lancamento = false,
                Genero_Id = 2,
                Estreia = false
            },
            new
            {
                Id = 2,
                Titulo = "Wolverine vs. Deadpool",
                Duracao = 127,
                Lancamento = true,
                Genero_Id = 1,
                Estreia = true
            },
        ];
    }
}