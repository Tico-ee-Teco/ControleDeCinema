using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers;

public class InicioController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}