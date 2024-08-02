using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloGenero;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers
{
    public class GeneroController : Controller
    {
        public ViewResult Listar()
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var generos = repositorioGenero.SelecionarTodos();

            var listarGenerosVm = generos
                .Select(g => new ListarGeneroViewModel
                {
                    Id = g.Id,
                    Nome = g.Nome
                });

            return View(listarGenerosVm);
        }

        public ViewResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Inserir(InserirGeneroViewModel inserirGeneroVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var genero = new Genero(inserirGeneroVm.Nome);

            repositorioGenero.Inserir(genero);

            HttpContext.Response.StatusCode = 201;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{genero.Id}] foi cadastrado com sucesso!",
                LinkRedirecionamento = "/genero/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Editar(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var genero = repositorioGenero.SelecionarPorId(id);

            var editarGeneroVm = new EditarGeneroViewModel
            {
                Id = genero.Id,
                Nome = genero.Nome
            };

            return View(editarGeneroVm);
        }

        [HttpPost]
        public ViewResult Editar(EditarGeneroViewModel editarGeneroVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var generoOriginal = repositorioGenero.SelecionarPorId(editarGeneroVm.Id);

            generoOriginal.Nome = editarGeneroVm.Nome;

            repositorioGenero.Editar(generoOriginal);

            HttpContext.Response.StatusCode = 200;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{generoOriginal.Id}] foi atualizado com sucesso!",
                LinkRedirecionamento = "/genero/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        
    }

  
}
