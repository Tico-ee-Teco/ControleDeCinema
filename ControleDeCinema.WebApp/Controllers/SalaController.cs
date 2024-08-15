using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDeCinema.WebApp.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class SalaController : AuthController
    {
        private readonly IRepositorioSala repositorioSala;
        public SalaController(IRepositorioSala repositorioSala)
        {
            this.repositorioSala = repositorioSala;
        }
        public IActionResult Listar()
        { 
            var salas = repositorioSala
                .Filtrar(s => s.UsuarioId == UsuarioId);

            var listaSalasVm = salas
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
            

            var sala = new Sala()
            {
                Numero = inserirSalaVm.Numero,
                Capacidade = inserirSalaVm.Capacidade,
                UsuarioId = UsuarioId.GetValueOrDefault()
            };
            
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

       

    }

    
}
