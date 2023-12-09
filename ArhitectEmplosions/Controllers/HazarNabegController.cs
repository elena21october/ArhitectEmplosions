using Microsoft.AspNetCore.Mvc;
using Models.HazarNabeg;

namespace ArchEmplosion.Controllers
{
    public class HazarNabegController : Controller
    {
        private int _idHazar;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateHazar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddHazar([FromBody] HazarNabeg hazarNabeg)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditHazar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddQuestionnare(int id)
        {
            _idHazar = id;
            return View();
        }

    }
}
