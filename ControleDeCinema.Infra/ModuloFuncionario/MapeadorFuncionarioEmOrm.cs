using ControleDeCinema.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeCinema.Infra.ModuloFuncionario;

public class MapeadorFuncionarioEmOrm : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> fuBuilder)
    {
        fuBuilder.ToTable("TBFuncionario");

        fuBuilder.Property(f => f.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        fuBuilder.Property(f => f.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        fuBuilder.Property(f => f.Login)
            .IsRequired()
            .HasColumnType("varchar(200)");

        fuBuilder.Property(f => f.Senha)
            .IsRequired()
            .HasColumnType("varchar(200)");

        fuBuilder.Property(f => f.UsuarioId)
            .IsRequired()
            .HasColumnType("int")
            .HasColumnName("Usuario_Id");

        fuBuilder.HasOne(f => f.Usuario)
            .WithMany()
            .HasForeignKey("Usuario_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        //fuBuilder.HasData(ObterRegistrosPadrao());
    }

    //populando dados no banco
    private object[] ObterRegistrosPadrao()
    {
        return
        [
            new
            {
                Id = 1,
                Nome = "Caio Tanaka",
                Login = "c.tanaka",
                Senha = "sFQZT5W2kK8BUAO8uhhQ"
            },
            new
            {
                Id = 2,
                Nome = "Júnior Teixeira",
                Login = "junior.teixeira201",
                Senha = "eNsoNQxmzglCOs3OK76a"
            },
            new
            {
                Id = 3,
                Nome = "Márcia Silva",
                Login = "marcia.silva0306",
                Senha = "AW6m9OHzgB28v4ZNS5jY"
            }
        ];
    }
}