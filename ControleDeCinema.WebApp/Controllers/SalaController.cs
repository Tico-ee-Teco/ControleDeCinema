using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloSala;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers
{
    public class SalaController : Controller
    {
        public ViewResult Listar()
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSala = new RepositorioSalaEmOrm(db);

            var salas = repositorioSala.SelecionarTodos();

            var listaSalasVm = salas
                .Select(s => new ListarSalaViewModel
                {
                    Id = s.Id,
                    Numero = s.Numero,
                    Capacidade = s.Capacidade,
                });

            return View(listaSalasVm);
        }

        public ViewResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Inserir(InserirSalaViewModel inserirSalaVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSala = new RepositorioSalaEmOrm(db);

            var sala = new Sala(inserirSalaVm.Numero, inserirSalaVm.Capacidade);

            repositorioSala.Inserir(sala);

            HttpContext.Response.StatusCode = 201;

            var notificacaoVm = new NotificacaoViewModel
            {
               Mensagem = $"O registro com o ID [{sala.Id}] foi cadastrado com sucesso!",
               LinkRedirecionamento = "/sala/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Editar(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSala = new RepositorioSalaEmOrm(db);

            var sala = repositorioSala.SelecionarPorId(id);

            var editarSalaVm = new EditarSalaViewModel
            {
                Id = sala.Id,
                Numero = sala.Numero,
                Capacidade = sala.Capacidade,
            };

            return View(editarSalaVm);
        }

        [HttpPost]
        public ViewResult Editar(EditarSalaViewModel editarSalaVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSala = new RepositorioSalaEmOrm(db);

            var salaOriginal = repositorioSala.SelecionarPorId(editarSalaVm.Id);

            salaOriginal.Numero = editarSalaVm.Numero;
            salaOriginal.Capacidade = editarSalaVm.Capacidade;

            repositorioSala.Editar(salaOriginal);

            HttpContext.Response.StatusCode = 200;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{salaOriginal.Id}] foi atualizado com sucesso!",
                LinkRedirecionamento = "/sala/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Excluir(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSala = new RepositorioSalaEmOrm(db);

            var sala = repositorioSala.SelecionarPorId(id);

            var excluirSalaVm = new ExcluirSalaViewModel
            {
                Id = sala.Id,
                Numero = sala.Numero,
                Capacidade = sala.Capacidade,
            };

            return View(excluirSalaVm);
        }

        [HttpPost, ActionName("excluir")]
        public ViewResult ExcluirConfirmado(ExcluirSalaViewModel excluirSalaVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioSala = new RepositorioSalaEmOrm(db);

            var sala = repositorioSala.SelecionarPorId(excluirSalaVm.Id);

            repositorioSala.Excluir(sala);

            HttpContext.Response.StatusCode = 200;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{sala.Id}] foi excluído com sucesso!",
                LinkRedirecionamento = "/sala/listar"
            };

            return View("mensagens", notificacaoVm);
        }

    }
}
