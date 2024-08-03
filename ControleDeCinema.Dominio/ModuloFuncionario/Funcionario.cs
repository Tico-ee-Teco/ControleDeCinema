using ControleDeCinema.Dominio.Compartilhado;

namespace ControleDeCinema.Dominio;

public class Funcionario : EntidadeBase
{
    public string Nome;
    public string CPF;
    public string Login;
    public string Senha;

    public Funcionario() { }

    public Funcionario(string nome, string cpf, string login, string senha)
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
