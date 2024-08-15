using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class GeneroController : Controller
    {
        private readonly IRepositorioGenero repositorioGenero;

        public GeneroController(IRepositorioGenero repositorioGenero)
        {
            this.repositorioGenero = repositorioGenero;
        }
        public IActionResult Listar()
        {
            var generos = repositorioGenero.SelecionarTodos();

            var listarGenerosVm = generos
                .Select(g => new ListarGeneroViewModel
                {
                    Id = g.Id,
                    Nome = g.Nome
                });

            ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

            return View(listarGenerosVm);
        }

        public IActionResult Inserir()
        {
            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult Inserir(InserirGeneroViewModel inserirGeneroVm)
        {
            var genero = new Genero(inserirGeneroVm.Nome);

            repositorioGenero.Inserir(genero);

            HttpContext.Response.StatusCode = 201;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{genero.Id}] foi inserido com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Editar(int id)
        {
            var genero = repositorioGenero.SelecionarPorId(id);

            var editarGeneroVm = new EditarGeneroViewModel
            {
                Id = genero.Id,
                Nome = genero.Nome
            };

            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult Editar(EditarGeneroViewModel editarGeneroVm)
        {
            var generoOriginal = repositorioGenero.SelecionarPorId(editarGeneroVm.Id);

            generoOriginal.Nome = editarGeneroVm.Nome;

            repositorioGenero.Editar(generoOriginal);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{generoOriginal.Id}] foi editado com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Excluir(int id)
        {
            var genero = repositorioGenero.SelecionarPorId(id);

            var excluirGeneroVm = new ExcluirGeneroViewModel
            {
                Id = genero.Id,
                Nome = genero.Nome,
                Filmes = genero.Filmes
                    .Select(f => new ListarFilmeGeneroViewModel {Nome = f.Titulo})
            };

            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int id)
        {
            var genero = repositorioGenero.SelecionarPorId(id);

            repositorioGenero.Excluir(genero);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{genero.Id}] foi excluído com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        
    }

  
}
