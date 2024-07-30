using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeCinema.Dominio
{
    public class Ingresso
    {
        public Enum Tipo;
        public int Id;
        private Sessao Sessao;
        private VendaIngresso Venda;
    }
}