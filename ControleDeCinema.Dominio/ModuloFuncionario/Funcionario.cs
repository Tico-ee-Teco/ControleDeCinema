using ControleDeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloSessao;

namespace ControleDeCinema.Dominio;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }

    public List<Ingresso> Ingressos { get; set; }

    public Funcionario()
    {
        Ingressos = new List<Ingresso>();
    }

    public Funcionario(string nome, string cpf, string login, string senha) : this()
    {
        Nome = nome;
        CPF = cpf;
        Login = login;
        Senha = senha;
    }

    public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
    {
        Funcionario funcionarioAtualizado = (Funcionario)registroAtualizado;

        Nome = funcionarioAtualizado.Nome;
        CPF = funcionarioAtualizado.CPF;
        Login = funcionarioAtualizado.Login;
        Senha = funcionarioAtualizado.Senha;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();
        
        if(string.IsNullOrEmpty(Nome.Trim()))
            erros.Add("O campo \"Nome\" é obrigatório");
        
        if(string.IsNullOrEmpty(CPF.Trim()))
            erros.Add("O campo \"Cpf\" é obrigatório");
        
        if(string.IsNullOrEmpty(Login.Trim()))
            erros.Add("O campo \"Login\" é obrigatório");
        
        if(string.IsNullOrEmpty(Senha.Trim()))
            erros.Add("O campo \"Senha\" é obrigatório");

        return erros;
    }
}
