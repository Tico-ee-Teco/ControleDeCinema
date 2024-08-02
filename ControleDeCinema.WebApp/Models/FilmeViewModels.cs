using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Models
{
    public class ListarFilmeViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<GeneroFilmeViewModel> Generos { get; set; }
        public DateTime Duracao { get; set; }
        public bool Estreia { get; set; }
    }

    public class GeneroFilmeViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class InserirFilmeViewModel
    {
        public string Nome { get; set; }
        public int IdGenero { get; set; }
        public DateTime Duracao { get; set; }
        public bool Estreia { get; set; }
        public IEnumerable<SelectListItem> Generos { get; set; }
    }
}
