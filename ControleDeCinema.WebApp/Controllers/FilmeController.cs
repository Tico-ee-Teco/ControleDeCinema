using ControleDeCinema.Dominio.ModuloFilme;
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
                    Genero = f.Genero.Nome,
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

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Editar(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var filme = repositorioFilme.SelecionarPorId(id);
            var generosDeFilme = repositorioGenero.SelecionarTodos();

            var editarFilmeVm = new EditarFilmeViewModel
            {
                Id = filme.Id,
                Nome = filme.Titulo,
                IdGenero = filme.Genero.Id,
                Duracao = filme.Duracao,
                Estreia = filme.Estreia,
                Generos = generosDeFilme
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Nome
                    })
            };

            return View(editarFilmeVm);
        }

        [HttpPost]
        public ViewResult Editar(EditarFilmeViewModel editarFilmeVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var genero = repositorioGenero.SelecionarPorId(editarFilmeVm.IdGenero);

            var filme = new Filme(editarFilmeVm.Nome, genero, editarFilmeVm.Duracao, editarFilmeVm.Estreia);

            repositorioFilme.Editar(filme);

            HttpContext.Response.StatusCode = 200;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{filme.Id}] foi atualizado com sucesso!",
                LinkRedirecionamento = "/filme/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Excluir(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

            var filme = repositorioFilme.SelecionarPorId(id);
            var genero = repositorioGenero.SelecionarPorId(filme.Genero.Id);

           var excluirFilmeVm = new ExcluirFilmeViewModel
            {
                Id = filme.Id,
                Nome = filme.Titulo,
                NomeGenero = genero.Nome,
                Duracao = filme.Duracao,
                Estreia = filme.Estreia
            };

            return View(excluirFilmeVm);
        }

        [HttpPost, ActionName("excluir")]
        public ViewResult ExcluirConfirmado(ExcluirFilmeViewModel excluirFilmeVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);

            var filme = repositorioFilme.SelecionarPorId(excluirFilmeVm.Id);

            repositorioFilme.Excluir(filme);

            HttpContext.Response.StatusCode = 200;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{filme.Id}] foi excluído com sucesso!",
                LinkRedirecionamento = "/filme/listar"
            };

            return View("mensagens", notificacaoVm);
        }
        
    }
}
