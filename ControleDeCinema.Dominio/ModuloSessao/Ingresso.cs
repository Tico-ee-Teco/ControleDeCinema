using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeCinema.Dominio.ModuloSessao
{
    public class Ingresso
    {
       
        public int Id;
        public Sessao Sessao;

        public Funcionario Funcionario;
        public Cliente Cliente;
    }
}