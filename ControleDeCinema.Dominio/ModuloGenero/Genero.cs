using ControleDeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloFilme;

namespace ControleDeCinema.Dominio.ModuloGenero;

public class Genero : EntidadeBase
{
    public string Nome { get; set; }
    public List<Filme> Filmes { get; set; }

    public Genero()
    {
        Filmes = new List<Filme>();
    }
    public Genero(string nome) : this()
    {
        Nome = nome;
    }
    public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
    {
        Genero generoAtualizado = (Genero)registroAtualizado;

        Nome = generoAtualizado.Nome;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrEmpty(Nome.Trim()))
            erros.Add("O campo \"Genero\" é obrigatório!");


        return erros;
    }

    public override string ToString()
    {
        return Nome;
    }
}