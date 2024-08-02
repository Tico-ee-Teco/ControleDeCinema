using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloFilme;
using ControleDeCinema.Infra.ModuloGenero;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Controllers
{
    public class FilmeController : Controller
    {
        public ViewResult Listar()
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);

            var filmes = repositorioFilme.SelecionarTodos();

            var listarFilmesVm = filmes
                .Select(f => new ListarFilmeViewModel
                {
                    Id = f.Id,
                    Nome = f.Titulo,
                    Duracao = f.Duracao,
                    Estreia = f.Estreia,
                });

            return View(listarFilmesVm);
        }

        public ViewResult Inserir()
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var generosDeFilme = repositorioGenero.SelecionarTodos();

            var inserirFilmeVm = new InserirFilmeViewModel
            {
                Generos = generosDeFilme
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Nome
                    })
            };

            return View(inserirFilmeVm);
        }

        [HttpPost]
        public ViewResult Inserir(InserirFilmeViewModel inserirFilmeVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var genero = repositorioGenero.SelecionarPorId(inserirFilmeVm.IdGenero);

            var filme = new Filme(inserirFilmeVm.Nome, genero, inserirFilmeVm.Duracao, inserirFilmeVm.Estreia);

            repositorioFilme.Inserir(filme);

            HttpContext.Response.StatusCode = 201;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{filme.Id}] foi cadastrado com sucesso!",
                LinkRedirecionamento = "/filme/listar"
            };

            return View();
        }
    }
}
