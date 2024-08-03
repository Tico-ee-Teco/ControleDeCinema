using System.ComponentModel.DataAnnotations;

namespace ControleDeCinema.WebApp.Models
{
    public class InserirGeneroViewModel 
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
    }

    public class EditarGeneroViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class ExcluirGeneroViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<ListarFilmeGeneroViewModel> Filmes { get; set; }
    }

    public class ListarGeneroViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class ListarFilmeGeneroViewModel
    {
       public string Nome { get; set; }
    }

   
}
