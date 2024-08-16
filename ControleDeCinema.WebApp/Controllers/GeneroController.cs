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
                    Descricao = g.Descricao
                });

            ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

            return View(listarGenerosVm);
        }

        public IActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inserir(InserirGeneroViewModel inserirGeneroVm)
        {
            if(!ModelState.IsValid)
                return View(inserirGeneroVm);

            var genero = new Genero()
            {
                Descricao = inserirGeneroVm.Descricao
            };

            repositorioGenero.Inserir(genero);

            HttpContext.Response.StatusCode = 201;

            TempData.SerializarMensagemViewModel(new MensagemViewModel
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

            return View(editarGeneroVm);
        }

        [HttpPost]
        public IActionResult Editar(EditarGeneroViewModel editarGeneroVm)
        {
            if(!ModelState.IsValid)
                return View(editarGeneroVm);

            var genero = repositorioGenero.SelecionarPorId(editarGeneroVm.Id);

            genero!.Descricao = editarGeneroVm.Descricao;

            repositorioGenero.Editar(genero);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{genero.Id}] foi editado com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Excluir(int id)
        {
            var genero = repositorioGenero.SelecionarPorId(id);

            if (genero is null)
                return MensagemRegistroNaoEncontrado(id);

            var detalhesGeneroViewModel = new DetalhesGeneroViewModel
            {
                Id = genero.Id,
                Descricao = genero.Descricao,
               
            };

            return View(detalhesGeneroViewModel);
        }

        [HttpPost]
        public IActionResult Excluir(DetalhesGeneroViewModel detalhesGeneroVm)
        {
            var genero = repositorioGenero.SelecionarPorId(detalhesGeneroVm.Id);

            if(genero is null)
                return MensagemRegistroNaoEncontrado(detalhesGeneroVm.Id);

            repositorioGenero.Excluir(genero);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{genero.Id}] foi excluído com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Detalhes(int id)
        {
            var genero = repositorioGenero.SelecionarPorId(id);

            if (genero is null)
                return MensagemRegistroNaoEncontrado(id);

            var detalhesGeneroViewModel = new DetalhesGeneroViewModel
            {
                Id = id,
                Descricao = genero.Descricao
            };

            return View(detalhesGeneroViewModel);
        }

        private IActionResult MensagemRegistroNaoEncontrado(int idRegistro)
        {
            TempData.SerializarMensagemViewModel(new MensagemViewModel
            {
                Titulo = "Erro",
                Mensagem = $"Não foi possível encontrar o registro ID [{idRegistro}]!",
            });

            return RedirectToAction(nameof(Listar));
        }


    }

  
}
