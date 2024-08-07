using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloFilme;
using ControleDeCinema.Infra.ModuloGenero;
using ControleDeCinema.Infra.ModuloSala;
using ControleDeCinema.Infra.ModuloSessao;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeCinema.WebApp.Controllers
{
    public class SessaoController : Controller
    {
        public ViewResult Listar()
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSessao = new RepositorioSessaoEmOrm(db);

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

            return View(listaSessaoVm);
        }

        public ViewResult Inserir()
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioSala = new RepositorioSalaEmOrm(db);

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

            return View(inserirSessaoVm);
        }

        [HttpPost]
        public ViewResult Inserir(InserirSessaoViewModel inserirSessaoVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioSala = new RepositorioSalaEmOrm(db);
            var repositorioSessao = new RepositorioSessaoEmOrm(db);

            var filme = repositorioFilme.SelecionarPorId(inserirSessaoVm.IdFilme);
            var sala = repositorioSala.SelecionarPorId(inserirSessaoVm.IdSala);

            var sessao = new Sessao(inserirSessaoVm.NumeroMaximoIngresso, inserirSessaoVm.Data, sala, filme);

            repositorioSessao.Inserir(sessao);

            HttpContext.Response.StatusCode = 201;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{sessao.Id}] foi cadastrado com sucesso!",
                LinkRedirecionamento = "/sessao/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Editar(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioSala = new RepositorioSalaEmOrm(db);
            var repositorioSessao = new RepositorioSessaoEmOrm(db);

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
                NumeroMaximoIngresso = sessao.NumeroMaximoIngresso,
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
        public ViewResult Editar(EditarSessaoViewModel editarSessaoVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioSala = new RepositorioSalaEmOrm(db);
            var repositorioSessao = new RepositorioSessaoEmOrm(db);

            //var filme = repositorioFilme.SelecionarPorId(editarSessaoVm.IdFilme);
            //var sala = repositorioSala.SelecionarPorId(editarSessaoVm.IdSala);

            var sessaoOriginal = repositorioSessao.SelecionarPorId(editarSessaoVm.Id);

            sessaoOriginal.Filme.Id = editarSessaoVm.IdFilme;
            sessaoOriginal.Sala.Id = editarSessaoVm.IdSala;
            sessaoOriginal.Data = editarSessaoVm.Data;
            sessaoOriginal.NumeroMaximoIngresso = editarSessaoVm.NumeroMaximoIngresso;

            repositorioSessao.Editar(sessaoOriginal);

            HttpContext.Response.StatusCode = 200;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{sessaoOriginal.Id}] foi atualizado com sucesso!",
                LinkRedirecionamento = "/sessao/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Excluir(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSessao = new RepositorioSessaoEmOrm(db);
            var repositorioFilme = new RepositorioFilmeEmOrm(db);
            var repositorioSala = new RepositorioSalaEmOrm(db);

            var sessao = repositorioSessao.SelecionarPorId(id);
            var filme = repositorioFilme.SelecionarPorId(sessao.Filme.Id);
            var sala = repositorioSala.SelecionarPorId(sessao.Sala.Id);

            var excluirSessaoVm = new ExcluirSessaoViewModel
            {
                Id = sessao.Id,
                Filme = filme.Titulo,
                Sala = sala.Numero,
                Data = sessao.Data,
                NumeroMaximoIngresso = sessao.NumeroMaximoIngresso
            };

            return View(excluirSessaoVm);
        }

        [HttpPost, ActionName("excluir")]
        public ViewResult ExcluirConfirmado(ExcluirSessaoViewModel excluirSessaoVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSessao = new RepositorioSessaoEmOrm(db);

            var sessao = repositorioSessao.SelecionarPorId(excluirSessaoVm.Id);

            repositorioSessao.Excluir(sessao);

            HttpContext.Response.StatusCode = 200;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{sessao.Id}] foi excluído com sucesso!",
                LinkRedirecionamento = "/sessao/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Detalhes(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSessao = new RepositorioSessaoEmOrm(db);
            var repositorioGenero = new RepositorioGeneroEmOrm(db);

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
                NumeroMaximoIngresso = sessao.NumeroMaximoIngresso
            };

            return View(detalhesSessaoVm);
        }

        public ViewResult Ingresso(int sessaoId)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSessao = new RepositorioSessaoEmOrm(db);

            var sessao = repositorioSessao.SelecionarPorId(sessaoId);

            var vendaIngressoVm = new VendaIngressoViewModel
            {
                SessaoId = sessao.Id,
                Filme = sessao.Filme.Titulo,
                Sala = sessao.Sala.Numero,
                Data = sessao.Data,
                //MeiaEntrada = 
            };

            return View(vendaIngressoVm);
        }

    }
}
