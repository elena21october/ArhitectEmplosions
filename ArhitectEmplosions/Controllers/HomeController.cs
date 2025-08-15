using ArhitectEmplosions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ArhitectEmplosions.Database;
using Microsoft.AspNetCore.Authorization;

namespace ArhitectEmplosions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult InfoProject()
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

        [Authorize(Roles = "admin")]
        public void DropDb()
        {
            _db.DropDataBase();
        }
    }
}
