using Microsoft.AspNetCore.Mvc;

namespace ArhitectEmplosions.Controllers;

public class NewsController : Controller
{
    public IActionResult Menu()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
}