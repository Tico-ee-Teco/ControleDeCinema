using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeCinema.Dominio
{
    public class Ingresso
    {
        public Tipo Tipo;
        public int Id;
        public Sessao Sessao;
        public VendaIngresso Venda;
        public Funcionario Funcionario;
        public Cliente Cliente;
    }
}