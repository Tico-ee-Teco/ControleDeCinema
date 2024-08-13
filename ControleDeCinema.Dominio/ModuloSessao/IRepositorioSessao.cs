using ControleDeCinema.Dominio.Compartilhado;

namespace ControleDeCinema.Dominio.ModuloSessao
{
    public interface IRepositorioSessao : IRepositorioBase<Sessao>
    {
        List<IGrouping<string, Sessao>> ObterSessoesAgrupadas();
        List<IGrouping<string, Sessao>> ObterSessoesDisponiveisAgrupadas();
    }
}
