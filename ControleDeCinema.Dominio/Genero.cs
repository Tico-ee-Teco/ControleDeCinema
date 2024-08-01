using ControleDeCinema.Dominio.Compartilhado;

namespace ControleDeCinema.Dominio;

public class Genero : EntidadeBase  
{
    public string Nome { get; set; }
    public Genero() { }
    public Genero(string nome)
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
        {
            erros.Add("Nome é obrigatório");
        }

        return erros;
    }

    public override string ToString()
    {
        return Nome;
    }
}