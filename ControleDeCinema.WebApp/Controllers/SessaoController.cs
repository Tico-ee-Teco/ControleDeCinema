using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloSessao;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Empresa")]
        public IActionResult Listar()
        {
            var agrupamentos = repositorioSessao.ObterSessoesAgrupadas();

            var AgrupamentoSessoesVm = agrupamentos
                .Select(MapearAgrupamentoSessoes);
            
            ViewBag.Mensagem = TempData.DesserializarMensagemViewModel(); 
            
            return View(AgrupamentoSessoesVm);
        }

        [Authorize(Roles = "Empresa")]
        public IActionResult Inserir()
        {
            var filmes = repositorioFilme.SelecionarTodos();
            var salas = repositorioSala.SelecionarTodos();
            
            var inserirSessaoVm = new InserirSessaoViewModel
            {
                Salas = salas.Select(s =>
                    new SelectListItem(s.Numero.ToString(), s.Id.ToString())),
                Filmes = filmes.Select(f =>
                    new SelectListItem(f.Titulo, f.Id.ToString())),
            };

            return View(inserirSessaoVm);
        }

        [Authorize(Roles = "Empresa")]
        [HttpPost]
        public IActionResult Inserir(InserirSessaoViewModel inserirSessaoVm)
        {
            if (!ModelState.IsValid)
            {
                var filmes = repositorioFilme.SelecionarTodos();
                var salas = repositorioSala.SelecionarTodos();

                inserirSessaoVm.Salas = salas.Select(s =>
                    new SelectListItem(s.Numero.ToString(), s.Id.ToString()));
                inserirSessaoVm.Filmes = filmes.Select(f =>
                    new SelectListItem(f.Titulo, f.Id.ToString()));

                return View(inserirSessaoVm);
            }

            var filmeSelecionado = repositorioFilme.SelecionarPorId(inserirSessaoVm.IdFilme);

            var salaSelecionada = repositorioSala.SelecionarPorId(inserirSessaoVm.IdSala);

            var sessao = new Sessao()
            {
                Sala = salaSelecionada!,
                Filme = filmeSelecionado!,
                Data = inserirSessaoVm.Data,
                NumeroMaximoIngressos = inserirSessaoVm.NumeroMaximoIngresso
            };

            repositorioSessao.Inserir(sessao);
            
            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{sessao.Id}] foi inserido com sucesso!"
            });
            
            HttpContext.Response.StatusCode = 201;

            return RedirectToAction(nameof(Listar));
        }

        [Authorize(Roles = "Empresa")]
        public IActionResult Encerrar(int id)
        {
            var sessao = repositorioSessao.SelecionarPorId(id);

            if(sessao is null)
                return MensagemRegistroNaoEncontrado(id);

            var detalhesSessaoVm = MapearDetalhesSessao(sessao);

            return View(detalhesSessaoVm);
        }

        [Authorize(Roles = "Empressa")]
        [HttpPost]
        public IActionResult Encerrar(DetalhesSessaoViewModel detalhesSessaoVm)
        {
            var sessao = repositorioSessao.SelecionarPorId(detalhesSessaoVm.Id);

            if (sessao is null)
                return MensagemRegistroNaoEncontrado(detalhesSessaoVm.Id);

            sessao.Encerrar();

            repositorioSessao.Editar(sessao);

            TempData.SerializarMensagemViewModel(new MensagemViewModel()

            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{sessao.Id}] foi encerrado com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        [Authorize(Roles = "Empresa")]
        public IActionResult Excluir(int id)
        {
            var sessao = repositorioSessao.SelecionarPorId(id);

            if(sessao is null)
                return MensagemRegistroNaoEncontrado(sessao);

            var detalhesSessaoVm = MapearDetalhesSessao(sessao);

            return View(detalhesSessaoVm);
        }

        [Authorize(Roles = "Empresa")]
        [HttpPost]
        public IActionResult Excluir(DetalhesSessaoViewModel detalhesSessaoVm)
        {
           var sessao = repositorioSessao.SelecionarPorId(detalhesSessaoVm.Id);

           if(sessao is null)
                return MensagemRegistroNaoEncontrado(detalhesSessaoVm.Id);

            repositorioSessao.Excluir(sessao);

            HttpContext.Response.StatusCode = 200;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{sessao.Id}] foi excluído com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        [Authorize(Roles = "Empresa, Cliente")]
        public IActionResult Detalhes(int id)
        {
            var sessao = repositorioSessao.SelecionarPorId(id);
            
            if(sessao is null)
                return MensagemRegistroNaoEncontrado(id);

            var detalhesSessaoVm = MapearDetalhesSessao(sessao);

            return View(detalhesSessaoVm);
        }

        [Authorize(Roles = "Empresa, Cliente")]
        [HttpGet, Route("/sessao/comprar-ingresso/{sessaoId;int}")]
        public ViewResult Ingresso(int id)
        {
            var sessao = repositorioSessao.SelecionarPorId(sessaoId);

            if(sessao is null)
                return MensagemRegistroNaoEncontrado(sessaoId);

            var detalhesSessaoVm = MapearDetalhesSessao(sessao);

            var vendaIngressoVm = new VendaIngressoViewModel
            {
               Sessao = detalhesSessaoVm,
               Assentos = sessao.ObterAssentosDisponiveis()
                   .Select(a => new SelectListItem(a.ToString(), a.ToString()))
            };

            return View(vendaIngressoVm);
        }

        [Authorize(Roles = "Empresa, Cliente")]
        [HttpPost, Route("/sessao/comprar-ingresso/{sessaoId;int}")]
        public IActionResult Ingresso(int sessaoId, VendaIngressoViewModel vendaIngressoVm)
        {
            var sessao = repositorioSessao.SelecionarPorId(sessaoId);

            if(sessao is null)
                return MensagemRegistroNaoEncontrado(sessaoId);

            var novoIngresso = sessao.GerarIngresso(
                comprarIngressoVm.AssentoSelecionado,
                comprarIngressoVm.MeiaEntrada
            );

            repositorioSessao.Editar(sessao);

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O ingresso [{novoIngresso.Id}] foi gerado com sucesso. Obrigado por sua compra!"
            });

            return RedirectToAction(nameof(Listar));
        }

        private IActionResult MensagemRegistroNaoEncontrado(int idRegistro)
        {
            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Erro",
                Mensagem = $"Não foipossível encontraro registro ID [{idRegistro}]!",
            });

            return RedirectToAction(nameof(Listar));
        }

        private static DetalhesSessaoViewModel MapearDetalhesSessao(Sessao sessao)
        {
            return new DetalhesSessaoViewModel
            {
                Id = sessao.Id,
                Sala = sessao.Sala.Numero,
                Filme = sessao.Filme.Titulo,
                Data = sessao.Data.ToString("dd/MM/yyyy HH:mm"),
                Encerrada = sessao.Encerrada ? "Encerrada" : "Disponível",
                NumeroMaximoIngresso = sessao.NumeroMaximoIngressos,
                IngressosDisponiveis = sessao.ObterIngressosDisponiveis(),
            };
        }

        private static AgrupamentoSessoesPorFilmeViewModel MapearAgrupamentoSessoes(IGrouping<string, Sessao> agrupamento)
        {
            return new AgrupamentoSessoesPorFilmeViewModel
            {
                Filme = agrupamento.Key,
                Sessoes = agrupamento.Select(s => new ListarSessaoViewModel
                {
                    Id = s.Id,
                    Filme = agrupamento.Key,
                    Sala = s.Sala.Numero,
                    IngressosDisponiveis = s.ObterQuantidadeIngressosDisponiveis(),
                    Data = s.Data.ToString("dd/MM/yyyy HH:mm"),
                    Encerrada = s.Encerrada ? "Encerrada" : "Disponível"
                })
                .OrderBy(s => s.Encerrada)
                .ThenBy(s => s.Data)
            };
        }
    }
}
