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

        fuBuilder.Property(f => f.CPF)
            .IsRequired()
            .HasColumnType("varchar(11)");

        fuBuilder.Property(f => f.Login)
            .IsRequired()
            .HasColumnType("varchar(200)");

        fuBuilder.Property(f => f.Senha)
            .IsRequired()
            .HasColumnType("varchar(200)");

        fuBuilder.HasData(ObterRegistrosPadrao());
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
                CPF = "12345678900",
                Login = "c.tanaka",
                Senha = "sFQZT5W2kK8BUAO8uhhQ"
            },
            new
            {
                Id = 2,
                Nome = "Júnior Teixeira",
                CPF = "98765432100",
                Login = "junior.teixeira201",
                Senha = "eNsoNQxmzglCOs3OK76a"
            },
            new
            {
                Id = 3,
                Nome = "Márcia Silva",
                CPF = "45678912300",
                Login = "marcia.silva0306",
                Senha = "AW6m9OHzgB28v4ZNS5jY"
            }
        ];
    }
}