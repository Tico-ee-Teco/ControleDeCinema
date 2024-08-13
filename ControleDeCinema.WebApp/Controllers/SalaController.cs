using ControleDeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloSala;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers
{
    public class SalaController : Controller
    {
        private readonly IRepositorioBase<Sala> repositorioSala;
        public SalaController(IRepositorioBase<Sala> repositorioSala)
        {
            this.repositorioSala = repositorioSala;
        }
        public IActionResult Listar()
        {
            var salasCadastradas = repositorioSala.SelecionarTodos();

            var listaSalasVm = salasCadastradas
                .Select(s => new ListarSalaViewModel
                {
                    Id = s.Id,
                    Numero = s.Numero,
                    Capacidade = s.Capacidade,
                });

            ViewBag.Mensagem = TempData.DesserializarMensagemViewModel(); 

            return View(listaSalasVm);
        }

        public IActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inserir(InserirSalaViewModel inserirSalaVm)
        {
            var sala = new Sala(inserirSalaVm.Numero, inserirSalaVm.Capacidade);
            
            repositorioSala.Inserir(sala);

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{sala.Id}] foi inserido com sucesso!"
            });
            HttpContext.Response.StatusCode = 201;
            
            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Editar(int id)
        {
            var sala = repositorioSala.SelecionarPorId(id);

            if (sala is null)
                return MensagemRegistroNaoEnconrtado(id);

            var editarSalaVm = new EditarSalaViewModel
            {
                Id = sala.Id,
                Numero = sala.Numero,
                Capacidade = sala.Capacidade,
            };

            return View(editarSalaVm);
        }

        [HttpPost]
        public IActionResult Editar(EditarSalaViewModel editarSalaVm)
        {
            var salaOriginal = repositorioSala.SelecionarPorId(editarSalaVm.Id);

            salaOriginal.Numero = editarSalaVm.Numero;
            salaOriginal.Capacidade = editarSalaVm.Capacidade;

            repositorioSala.Editar(salaOriginal);
            
            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{salaOriginal.Id}] foi editado com sucesso!"
            });

            HttpContext.Response.StatusCode = 200;

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Excluir(int id)
        {
            var sala = repositorioSala.SelecionarPorId(id);
            
            if (sala is null)
                return MensagemRegistroNaoEnconrtado(id);

            var detalhesSalaViewModel = new DetalhesSalaViewModel()
            {
                Id = sala.Id,
                Numero = sala.Numero,
                Capacidade = sala.Capacidade,
            };
            
            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{sala.Id}] foi excluído com sucesso!"
            });

            return View(detalhesSalaViewModel);
        }

        [HttpPost, ActionName("excluir")]
        public IActionResult ExcluirConfirmado(DetalhesSalaViewModel detalhesSalaVm)
        {
            var sala = repositorioSala.SelecionarPorId(detalhesSalaVm.Id);
            
            if (sala is null)
                return RedirectToAction(nameof(Listar));

            repositorioSala.Excluir(sala);

            HttpContext.Response.StatusCode = 200;
            
            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Detalhes(int id)
        {
            var sala = repositorioSala.SelecionarPorId(id);

            if (sala is null)
                return MensagemRegistroNaoEnconrtado(id);
            return RedirectToAction(nameof(Listar));
        }

        public IActionResult MensagemRegistroNaoEnconrtado(int idRegsitro)
        {
            TempData.SerializarMensagemViewModel(new MensagemViewModel
            {
                Titulo = "Erro",
                Mensagem = $"Não foi possivel encontrar ID [{idRegsitro}]!"
            });

            return RedirectToAction(nameof(Listar));
        }

    }

    
}
