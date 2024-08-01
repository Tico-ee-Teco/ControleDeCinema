using ControleDeCinema.Dominio.Compartilhado;

namespace ControleDeCinema.Dominio.ModuloFilme
{
    public class Filme : EntidadeBase
    {
        public string Titulo { get; set; }
        public Genero Genero { get; set; }
        public DateTime Duracao { get; set; }
        public bool Estreia { get; set; }
        public Filme()
        {
        }

        public Filme(string titulo, Genero genero, DateTime duracao, bool estreia)
        {
            Titulo = titulo;
            Genero = genero;
            Duracao = duracao;
            Estreia = estreia;
        }
        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Filme filmeAtualizado = (Filme)registroAtualizado;

            Titulo = filmeAtualizado.Titulo;
            Genero = filmeAtualizado.Genero;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if(string.IsNullOrEmpty(Titulo.Trim()))
                erros.Add("O campo \"Titulo\" é obrigatório");

            if(Genero == null)
                erros.Add("O campo \"Genero\" é obrigatório");

            if(Duracao == null)
                erros.Add("O campo \"Duracao\" é obrigatório");

            return erros;
        }
    }
}