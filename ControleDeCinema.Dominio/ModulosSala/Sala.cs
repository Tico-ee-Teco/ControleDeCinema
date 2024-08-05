using ControleDeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloSessao;

namespace ControleDeCinema.Dominio.ModulosSala
{
    public class Sala : EntidadeBase
    {
        public int Numero { get; set; }
        public int Capacidade { get; set;}
        public int NumeroAssentosDisponiveis { get; set; } // Quantidade de assentos disponíveis na sala
        public List<Sessao> Sessoes { get; set; } // Lista de sessões que ocorrem na sala

        public Sala()
        {
            Sessoes = new List<Sessao>();
        }
        public Sala(int numero, int capacidade) : this ()
        {
            Numero = numero;
            Capacidade = capacidade;
        }

        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Sala salaAtualizada = (Sala)registroAtualizado;

            Numero = salaAtualizada.Numero;
            Capacidade = salaAtualizada.Capacidade;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (Numero <= 0)
                erros.Add("O campo \"Número\" deve ser maior que zero!");

            if (Capacidade <= 0)
                erros.Add("O campo \"Capacidade\" deve ser maior que zero!");

            return erros;
        }
    }

}