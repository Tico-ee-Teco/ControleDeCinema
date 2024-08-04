using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControleDeCinema.Dominio.Compartilhado;

namespace ControleDeCinema.Dominio.ModulosSala
{
    public class Sala : EntidadeBase
    {
        public string Nome;
        public int Capacidade;
        public int NumeroAssentosDisponiveis; // Quantidade de assentos disponíveis na sala

        public Sala()
        {
            
        }
        public Sala(string nome, int capacidade)
        {
            Nome = nome;
            Capacidade = capacidade;
        }

        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Sala salaAtualizada = (Sala)registroAtualizado;

            Nome = salaAtualizada.Nome;
            Capacidade = salaAtualizada.Capacidade;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome.Trim()))
                erros.Add("O campo \"Nome\" é obrigatório!");

            if (Capacidade <= 0)
                erros.Add("O campo \"Capacidade\" deve ser maior que zero!");

            return erros;
        }
    }

}