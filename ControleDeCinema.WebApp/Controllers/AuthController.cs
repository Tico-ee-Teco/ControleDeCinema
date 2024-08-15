using System.Security.Claims;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers;

public abstract class AuthController : Controller
{
    protected int? UsuarioId
    {
        get
        {
            var usuarioAutenticado = User.FindFirst(ClaimTypes.NameIdentifier);

            if (usuarioAutenticado is null)
                return null;

            return Convert.ToInt32(usuarioAutenticado.Value);
        }
    }

    protected IActionResult MensagemRegistroNaoEnconrtado(int idRegsitro)
    {
        TempData.SerializarMensagemViewModel(new MensagemViewModel
        {
            Titulo = "Erro",
            Mensagem = $"Não foi possivel encontrar ID [{idRegsitro}]!"
        });

        return RedirectToAction("Index", "Inicio");
    }
}