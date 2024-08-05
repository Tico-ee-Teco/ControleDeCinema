using System.Collections;

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
        public int Numero { get; set; }
        public int Capacidade { get; set; }
    }

    public class EditarSalaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidade { get; set; }
    }

    public class ExcluirSalaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidade { get; set; }
    }
}
