using System.ComponentModel.DataAnnotations;

namespace ControleDeCinema.WebApp.Models
{
    public class ListarSalaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidade { get; set; }
    }

    public class InserirSalaViewModel
    {
        [Required(ErrorMessage = "o número é obrigatório")]
        [Range(0, 1000, ErrorMessage = "informe um número maior que zero")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "a capacidade é obrigatória")]
        [Range(0, 1000, ErrorMessage = "informe um número de capacidade maior que zero")]
        public int Capacidade { get; set; }
    }

    public class EditarSalaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "o número é obrigatório")]
        [Range(0, 1000, ErrorMessage = "informe um número maior que zero")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "a capacidade é obrigatória")]
        [Range(0, 1000, ErrorMessage = "informe um número de capacidade maior que zero")]
        public int Capacidade { get; set; }
    }

    public class DetalhesSalaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidade { get; set; }
    }
}
