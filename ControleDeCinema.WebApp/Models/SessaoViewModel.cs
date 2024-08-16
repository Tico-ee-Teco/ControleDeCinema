using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Models
{
    public class ListarSessaoViewModel
    {
        public int Id { get; set; }
        public string Filme { get; set; }
        public int Sala { get; set; }
        public string Data { get; set; }
        public string Encerrada { get; set; }
        public int IngressosDisponiveis { get; set; }
    }

    public class InserirSessaoViewModel
    {
        [Required(ErrorMessage = "O filme é obrigatório")]
        public int IdFilme{ get; set; }

        [Required(ErrorMessage = "A sala é obrigatória")]
        public int IdSala { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Range(0, 1000, ErrorMessage = "O número máximo de ingressos deve ser entre 0 e 1000")]
        public int NumeroMaximoIngresso { get; set; }

        public IEnumerable<SelectListItem> Filmes { get; set; }
        public IEnumerable<SelectListItem> Salas { get; set; }
    }

    public class EditarSessaoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O filme é obrigatório")]
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "A sala é obrigatória")]
        public int IdSala { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Range(0, 1000, ErrorMessage = "O número máximo de ingressos deve ser entre 0 e 1000")]
        public int NumeroMaximoIngresso { get; set; }

        public IEnumerable<SelectListItem> Filmes { get; set; }
        public IEnumerable<SelectListItem> Salas { get; set; }
    }

    public class AgrupamentoSessoesPorFilmeViewModel
    {
        public string Filme { get; set; }
        public IEnumerable<ListarSessaoViewModel> Sessoes { get; set; }
    }

    public class DetalhesSessaoViewModel
    {
        public int Id { get; set; }
        public int Sala { get; set; }
        public string Filme { get; set; }
        public string Data { get; set; }
        public string Encerrada { get; set; }
        public int NumeroMaximoIngresso { get; set; }
        public int IngressosDisponiveis { get; set; }
    }

    public class VendaIngressoViewModel
    {
        public DetalhesSessaoViewModel Sessao { get; set; }   
        public bool MeiaEntrada { get; set; }
        [Required(ErrorMessage = "É obrigatório selecionar um assento!")]
        public int AssentoSelecionado { get; set; }

        public IEnumerable<SelectListItem>? Assentos { get; set; }

    }
}
