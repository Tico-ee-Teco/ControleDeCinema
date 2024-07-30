namespace ControleDeCinema.Dominio.Compartilhado
{
    public interface IRepositorioBase<TEntidade> where TEntidade : EntidadeBase
    {
        void Inseir(TEntidade registro);
        bool Editar(TEntidade registroOriginal, TEntidade registroAtualizado);
        bool Excluir(TEntidade registro);
        TEntidade SelecionarPorId(int id);
        List<TEntidade> SelecionarTudo();
    }
}
