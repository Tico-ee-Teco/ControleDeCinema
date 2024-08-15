using ControleDeCinema.Dominio.ModuloUsuario;

namespace ControleDeCinema.Dominio.Compartilhado
{
    public abstract class EntidadeBase
    {
        public int Id { get; set; }

        public abstract void AtualizarInformacoes(EntidadeBase registroAtualizado);

        public abstract List<string> Validar();

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
