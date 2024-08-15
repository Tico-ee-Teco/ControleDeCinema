using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class FilmeController : Controller
    {
        private readonly IRepositorioFilme repositorioFilme;
        private readonly IRepositorioGenero repositorioGenero;

        public FilmeController(IRepositorioFilme repositorioFilme, IRepositorioGenero repositorioGenero)
        {
            this.repositorioFilme = repositorioFilme;
            this.repositorioGenero = repositorioGenero;
        }

        //[AllowAnonymous] //libera a rota para qualquer usuário
        public IActionResult Listar()
        {
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

            ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

            return View(listarFilmesVm);
        }

        public IActionResult Inserir()
        {
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

            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult Inserir(InserirFilmeViewModel inserirFilmeVm)
        {
            var genero = repositorioGenero.SelecionarPorId(inserirFilmeVm.IdGenero);

            var filme = new Filme(inserirFilmeVm.Nome, genero, inserirFilmeVm.Duracao, inserirFilmeVm.Estreia);

            repositorioFilme.Inserir(filme);

            HttpContext.Response.StatusCode = 201;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{filme.Id}] foi inserido com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Editar(int id)
        {
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

            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult Editar(EditarFilmeViewModel editarFilmeVm)
        {
            var genero = repositorioGenero.SelecionarPorId(editarFilmeVm.IdGenero);

            var filme = new Filme(editarFilmeVm.Nome, genero, editarFilmeVm.Duracao, editarFilmeVm.Estreia);

            repositorioFilme.Editar(filme);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{filme.Id}] foi editado com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Excluir(int id)
        {
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

            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(ExcluirFilmeViewModel excluirFilmeVm)
        {
            var filme = repositorioFilme.SelecionarPorId(excluirFilmeVm.Id);

            repositorioFilme.Excluir(filme);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{filme.Id}] foi excluido com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }
        
    }
}
