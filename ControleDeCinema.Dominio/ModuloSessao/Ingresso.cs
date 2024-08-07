using ControleDeCinema.Dominio.Compartilhado;

namespace ControleDeCinema.Dominio.ModuloSessao
{
    public class Ingresso : EntidadeBase
    {
        public int NumeroAssento { get; set; }
        public bool MeiaEntrada { get; set; }

        public Sessao Sessao { get; set; }

        public Ingresso() { }

       public Ingresso(int numeroAssento, bool meiaEntrada)
       {
           NumeroAssento = numeroAssento;
           MeiaEntrada = meiaEntrada;
       }

       public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
       {
           throw new NotImplementedException();
       }

       public override List<string> Validar()
       {
           throw new NotImplementedException();
       }
    }

}