using ControleDeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModulosSala;

namespace ControleDeCinema.Dominio.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        public int NumeroMaximoIngresso { get; set; }
        public DateTime Data { get; set; }
        public Sala Sala { get; set; }
        public Filme Filme { get; set; }

        public Sessao()
        {
            
        }

        public Sessao(int numeroMaximoIngresso, DateTime data, Sala sala, Filme filme)
        {
            NumeroMaximoIngresso = numeroMaximoIngresso;
            Data = data;
            Sala = sala;
            Filme = filme;
        }
        

        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Sessao sessaoAtualizada = (Sessao)registroAtualizado;

            NumeroMaximoIngresso = sessaoAtualizada.NumeroMaximoIngresso;
            Data = sessaoAtualizada.Data;
            Sala = sessaoAtualizada.Sala;
            Filme = sessaoAtualizada.Filme;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (NumeroMaximoIngresso <= 0)
                erros.Add("Número máximo de ingressos deve ser maior que zero.");

            if (Data == null)
                erros.Add("A sessão precisa de uma data e hora.");

            if (Sala == null)
                erros.Add("A sessão precisa de uma sala.");

            if (Filme == null )
                erros.Add("A sessão precisa de um filme.");

            return erros;
        }
    }
}