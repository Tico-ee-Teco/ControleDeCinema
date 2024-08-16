using ControleDeCinema.Dominio.Compartilhado;

namespace ControleDeCinema.Dominio.ModuloSessao
{
    public interface IRepositorioSessao : IRepositorioBase<Sessao>
    {
        List<IGrouping<string, Sessao>> ObterSessoesAgrupadas();
        List<IGrouping<string, Sessao>> ObterSessoesAgrupadas(int usuarioId);
        List<Ingresso>SelecionarTodosIngressos(int usuarioSessaoId);
        List<Ingresso> SelecionarTodosIngressos();
        List<int> ObterNumerosAssentosOcupados(int sessaoId);

    }
}
