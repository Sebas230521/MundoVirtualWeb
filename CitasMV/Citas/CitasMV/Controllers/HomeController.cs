using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CitasMV.Models;

namespace CitasMV.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Crear()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult TestSession()
{
    HttpContext.Session.SetString("usuario", "Prueba");
    var nombre = HttpContext.Session.GetString("usuario");
    return Content($"Usuario en sesión: {nombre}");
}

}

