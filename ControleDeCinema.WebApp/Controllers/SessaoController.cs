using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloSessao;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ControleDeCinema.WebApp.Controllers
{
    public class SessaoController : Controller
    {
        private readonly IRepositorioSessao repositorioSessao;
        private readonly IRepositorioSala repositorioSala;
        private readonly IRepositorioFilme repositorioFilme;
        private readonly IRepositorioGenero repositorioGenero;

        public SessaoController(
            IRepositorioSessao repositorioSessao,
            IRepositorioSala repositorioSala,
            IRepositorioFilme repositorioFilme,
            IRepositorioGenero repositorioGenero
        )
        {
            this.repositorioSessao = repositorioSessao;
            this.repositorioSala = repositorioSala;
            this.repositorioFilme = repositorioFilme;
            this.repositorioGenero = repositorioGenero;
        }
        public IActionResult Listar()
        {
            var sessoes = repositorioSessao.ObterSessoesAgrupadas();

            var listaSessaoVm = sessoes
                .Select(s => new AgruparSessaoViewModel
                {
                    Filme = s.Key,
                    Sessoes = s.Select(s => new ListarSessaoViewModel
                    {
                        Id = s.Id,
                        Filme = s.Filme.Titulo,
                        Sala = s.Sala.Numero,
                        Data = s.Data
                    })
                });
            
            ViewBag.Mensagem = TempData.DesserializarMensagemViewModel(); 
            
            return View(listaSessaoVm);
        }

        public IActionResult Inserir()
        {
            var filmes = repositorioFilme.SelecionarTodos();
            var salas = repositorioSala.SelecionarTodos();
            
            var inserirSessaoVm = new InserirSessaoViewModel
            {
                Filmes = filmes.Select(f => new SelectListItem
                {
                    Text = f.Titulo,
                    Value = f.Id.ToString()
                }),
                Salas = salas.Select(s => new SelectListItem
                {
                    Text = s.Numero.ToString(),
                    Value = s.Id.ToString()
                })
            };

            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult Inserir(InserirSessaoViewModel inserirSessaoVm)
        {
            var filme = repositorioFilme.SelecionarPorId(inserirSessaoVm.IdFilme);
            var sala = repositorioSala.SelecionarPorId(inserirSessaoVm.IdSala);

            var sessao = new Sessao(inserirSessaoVm.NumeroMaximoIngresso, inserirSessaoVm.Data, sala, filme);

            repositorioSessao.Inserir(sessao);
            
            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{sessao.Id}] foi inserido com sucesso!"
            });
            
            HttpContext.Response.StatusCode = 201;

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Editar(int id)
        {
            var sessao = repositorioSessao.SelecionarPorId(id);
            var filmes = repositorioFilme.SelecionarTodos();
            var salas = repositorioSala.SelecionarTodos();

            var editarSessaoVm = new EditarSessaoViewModel
            {
                Id = sessao.Id,
                IdFilme = sessao.Filme.Id,
                IdSala = sessao.Sala.Id,
                NomeFilme = sessao.Filme.Titulo,
                NomeGenero = sessao.Filme.Genero.Nome,
                Sala = sessao.Sala.Numero,
                Data = sessao.Data,
                NumeroMaximoIngresso = sessao.NumeroMaximoIngressos,
                Filmes = filmes.Select(f => new SelectListItem
                {
                    Text = f.Titulo,
                    Value = f.Id.ToString()
                }),
                Salas = salas.Select(s => new SelectListItem
                {
                    Text = s.Numero.ToString(),
                    Value = s.Id.ToString()
                })
            };
            return View(editarSessaoVm);
        }

        [HttpPost]
        public IActionResult Editar(EditarSessaoViewModel editarSessaoVm)
        {
            var sessaoOriginal = repositorioSessao.SelecionarPorId(editarSessaoVm.Id);

            sessaoOriginal.Filme.Id = editarSessaoVm.IdFilme;
            sessaoOriginal.Sala.Id = editarSessaoVm.IdSala;
            sessaoOriginal.Data = editarSessaoVm.Data;
            sessaoOriginal.NumeroMaximoIngressos = editarSessaoVm.NumeroMaximoIngresso;

            repositorioSessao.Editar(sessaoOriginal);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{sessaoOriginal.Id}] foi editado com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Excluir(int id)
        {
            var sessao = repositorioSessao.SelecionarPorId(id);
            var filme = repositorioFilme.SelecionarPorId(sessao.Filme.Id);
            var sala = repositorioSala.SelecionarPorId(sessao.Sala.Id);

            var excluirSessaoVm = new ExcluirSessaoViewModel
            {
                Id = sessao.Id,
                Filme = filme.Titulo,
                Sala = sala.Numero,
                Data = sessao.Data,
                NumeroMaximoIngresso = sessao.NumeroMaximoIngressos
            };

            return View(excluirSessaoVm);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(DetalhesSessaoViewModel detalhesSessaoVm)
        {
           var sessao = repositorioSessao.SelecionarPorId(detalhesSessaoVm.Id);

            repositorioSessao.Excluir(sessao);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{sessao.Id}] foi excluído com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Detalhes(int id)
        {
            var sessao = repositorioSessao.SelecionarPorId(id);
            var genero = repositorioGenero.SelecionarTodos()
                .Select(x => new SelectListItem(x.Nome, x.Id.ToString()));

            var detalhesSessaoVm = new DetalhesSessaoViewModel
            {
                Id = sessao.Id,
                Filme = sessao.Filme.Titulo,
                Genero = genero.First(x => x.Value == sessao.Filme.Genero.Id.ToString()).Text,
                Sala = sessao.Sala.Numero,
                Data = sessao.Data,
                NumeroMaximoIngresso = sessao.NumeroMaximoIngressos
            };

            return View(detalhesSessaoVm);
        }

        public ViewResult Ingresso(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSessao = new RepositorioSessaoEmOrm(db);

            var sessao = repositorioSessao.SelecionarPorId(id);

            var vendaIngressoVm = new VendaIngressoViewModel
            {
                Id = sessao.Id,
                Filme = sessao.Filme.Titulo,
                Sala = sessao.Sala.Numero,
                Data = sessao.Data,
                //MeiaEntrada = 
            };

            return View(vendaIngressoVm);
        }

    }
}
