using ControleDeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModulosSala;

namespace ControleDeCinema.Dominio.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        public int NumeroMaximoIngressos { get; set; }
        public DateTime Data { get; set; }
        public Sala Sala { get; set; }
        public Filme Filme { get; set; }
        public bool Encerrada { get; set; }
        public List<Ingresso> Ingressos { get; set; }

        public Sessao()
        {
            Ingressos = new List<Ingresso>();
        }

        public Sessao(int numeroMaximoIngressos, DateTime data, Sala sala, Filme filme): this()
        {
            NumeroMaximoIngressos = numeroMaximoIngressos;
            Data = data;
            Sala = sala;
            Filme = filme;
        }
        

        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Sessao sessaoAtualizada = (Sessao)registroAtualizado;

            NumeroMaximoIngressos = sessaoAtualizada.NumeroMaximoIngressos;
            Data = sessaoAtualizada.Data;
            Sala = sessaoAtualizada.Sala;
            Filme = sessaoAtualizada.Filme;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (NumeroMaximoIngressos <= 0)
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