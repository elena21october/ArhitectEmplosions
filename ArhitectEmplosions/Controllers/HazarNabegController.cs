using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.HazarNabeg;

namespace ArchEmplosion.Controllers
{
    public class HazarNabegController : Controller
    {
        ApplicationContext db;
        private int _idHazar;

        public HazarNabegController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> MainHazar()
        {  
            return View(await db.HazarNabegs.ToListAsync());
        }

        [HttpGet]
        public IActionResult CreateHazar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddHazar([FromBody] HazarNabeg hazarNabeg)
        {
            db.HazarNabegs.Add(hazarNabeg);
            db.SaveChanges();
            return RedirectToAction("MainHazar");
        }

        [HttpGet]
        public IActionResult EditHazar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LookHazar(int id)
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
