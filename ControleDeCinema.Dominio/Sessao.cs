using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeCinema.Dominio
{
    public class Sessao
    {
        public System.Collections.Generic.List<Filme> Filme;
        public DateTime Data;
        public DateTime Hora;
        public int NumeroMaximoIngresso;
        private int Id;
        public Sala Sala;
    }
}