using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeCinema.Dominio
{
    public class Ingresso
    {
        public enun Tipo;
        public int Id;
        private Sessao Sessao;
        private Venda Venda;
    }
}