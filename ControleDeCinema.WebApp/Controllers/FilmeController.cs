using ControleDeCinema.Dominio.Extensions;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

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
                    Titulo = f.Titulo,
                    Duracao = f.Duracao.FormatarEmHorasEMinutos(),
                    Lancamento = f.Lancamento ? "Lançamento" : "Re-Exibição",
                    Genero = f.Genero.Descricao
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
                    .Select(g => new SelectListItem(g.Descricao, g.Id.ToString()))
                  
            };

            return View(inserirFilmeVm);
        }

        [HttpPost]
        public IActionResult Inserir(InserirFilmeViewModel inserirFilmeVm)
        {

            if (!ModelState.IsValid)
            {
                var generoDeFilme = repositorioGenero.SelecionarTodos();

                inserirFilmeVm.Generos = generoDeFilme
                    .Select(g => new SelectListItem(g.Descricao, g.Id.ToString()));

                return View(inserirFilmeVm);
            }
            
            var genero = repositorioGenero.SelecionarPorId(inserirFilmeVm.IdGenero.GetValueOrDefault());

            var filme = new Filme()
            {
                Titulo = inserirFilmeVm.Titulo,
                Lancamento = inserirFilmeVm.Lancamento,
                Duracao = inserirFilmeVm.Duracao,
                Genero = genero
            };

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

            if (filme is null)
                return MensagemRegistroNaoEncontrado(id);

            var generos = repositorioGenero.SelecionarTodos();

            var editarFilmeVm = new EditarFilmeViewModel
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Duracao = filme.Duracao,
                Lancamento = filme.Lancamento,
                Generos = generos
                    .Select(g => new SelectListItem(g.Descricao, g.Id.ToString())),
            };

            return View(editarFilmeVm);
        }

        [HttpPost]
        public IActionResult Editar(EditarFilmeViewModel editarFilmeVm)
        {
            if(!ModelState.IsValid)
            {
                var generos = repositorioGenero.SelecionarTodos();

                editarFilmeVm.Generos = generos
                    .Select(g => new SelectListItem(g.Descricao, g.Id.ToString()));

                return View(editarFilmeVm);
            }

            var generoSelecionado = repositorioGenero.SelecionarPorId(editarFilmeVm.IdGenero);

            var filme = repositorioFilme.SelecionarPorId(editarFilmeVm.Id);

            filme!.Titulo = editarFilmeVm.Titulo;
            filme!.Duracao = editarFilmeVm.Duracao;
            filme!.Lancamento = editarFilmeVm.Lancamento;
            filme!.Genero = generoSelecionado;

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
           
           if(filme is null)
                return MensagemRegistroNaoEncontrado(id);

            var detalhesFilmeViewModel = new DetalhesFilmeViewModel
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Duracao = filme.Duracao.FormatarEmHorasEMinutos(),
                Lancamento = filme.Lancamento ? "Lançamento" : "Re-Exibição",
                Genero = filme.Genero.Descricao
            };

            return View(detalhesFilmeViewModel);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(DetalhesFilmeViewModel detalhesFilmeVm)
        {
            var filme = repositorioFilme.SelecionarPorId(detalhesFilmeVm.Id);

            if(filme is null)
                return MensagemRegistroNaoEncontrado(detalhesFilmeVm.Id);

            repositorioFilme.Excluir(filme);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{filme.Id}] foi excluido com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Detalhes(int id)
        {
            var filme = repositorioFilme.SelecionarPorId(id);

            if (filme is null)
                return MensagemRegistroNaoEncontrado(id);

            var detalhesFilmeViewModel = new DetalhesFilmeViewModel
            {
                Id = id,
                Titulo = filme.Titulo,
                Duracao = filme.Duracao.FormatarEmHorasEMinutos(),
                Lancamento = filme.Lancamento ? "Lançamento" : "Re-Exibição",
                Genero = filme.Genero.Descricao
            };

            return View(detalhesFilmeViewModel);
        }

        private IActionResult MensagemRegistroNaoEncontrado(int idRegistro)
        {
            TempData.SerializarMensagemViewModel(new MensagemViewModel
            {
                Titulo = "Erro",
                Mensagem = $"Não foi possível encontrar o registro ID [{idRegistro}]!"
            });

            return RedirectToAction(nameof(Listar));
        }

    }
}
