using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Models
{
    public class ListarSessaoViewModel
    {
        public int Id { get; set; }
        public string Filme { get; set; }
        public int Sala { get; set; }
        public DateTime Data { get; set; }
    }

    public class InserirSessaoViewModel
    {
        public int IdFilme{ get; set; }
        public string NomeGenero { get; set; }
        public int IdSala { get; set; }
        public DateTime Data { get; set; }
        public int NumeroMaximoIngresso { get; set; }

        public IEnumerable<SelectListItem> Filmes { get; set; }
        public IEnumerable<SelectListItem> Salas { get; set; }
    }

    public class EditarSessaoViewModel
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public string NomeFilme { get; set; }
        public string NomeGenero { get; set; }
        public int IdSala { get; set; }
        public int Sala { get; set; }
        public DateTime Data { get; set; }
        public int NumeroMaximoIngresso { get; set; }

        public IEnumerable<SelectListItem> Filmes { get; set; }
        public IEnumerable<SelectListItem> Salas { get; set; }

    }

    public class ExcluirSessaoViewModel
    {
        public int Id { get; set; }
        public string Filme { get; set; }
        public int Sala { get; set; }
        public DateTime Data { get; set; }
        public int NumeroMaximoIngresso { get; set; }
    }

    public class AgruparSessaoViewModel
    {
        public string Filme { get; set; }
        public IEnumerable<ListarSessaoViewModel> Sessoes { get; set; }
    }

    public class DetalhesSessaoViewModel
    {
        public int Id { get; set; }
        public string Filme { get; set; }
        public string Genero { get; set; }
        public int Sala { get; set; }
        public DateTime Data { get; set; }
        public int NumeroMaximoIngresso { get; set; }
    }

    public class VendaIngressoViewModel
    {
        public int SessaoId { get; set; }
        public string Filme { get; set; }
        public int Sala { get; set; }
        public DateTime Data { get; set; }
        public int QuantidadeIngressos { get; set; }

        public bool MeiaEntrada { get; set; }
    }
}
