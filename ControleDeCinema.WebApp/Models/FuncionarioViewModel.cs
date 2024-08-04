﻿namespace ControleDeCinema.WebApp.Models
{
    public class ListarFuncionarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class InserirFuncionarioViewModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class EditarFuncionarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class ExcluirFuncionarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
        
}
