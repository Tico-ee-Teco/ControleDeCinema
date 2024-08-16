using ControleDeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloFilme;

namespace ControleDeCinema.Dominio.ModuloGenero;

public class Genero : EntidadeBase
{
    public string Descricao { get; set; }
    public List<Filme> Filmes { get; set; }

    public Genero()
    {
        
    }
    public Genero(string descricao)
    {
        Descricao = descricao;
    }
    public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
    {
        Genero generoAtualizado = (Genero)registroAtualizado;

        Descricao = generoAtualizado.Descricao;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrEmpty(Descricao.Trim()))
            erros.Add("O campo \"Genero\" é obrigatório!");


        return erros;
    }

    public override string ToString()
    {
        return Descricao;
    }
}