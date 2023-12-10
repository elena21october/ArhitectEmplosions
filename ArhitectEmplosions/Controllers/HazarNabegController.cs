using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models.HazarNabeg;
using Newtonsoft.Json;

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
            return RedirectToAction("MainHazar","HazarNabeg");
        }

        [HttpGet]
        public IActionResult EditHazar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LookHazar(int id)
        {
            _idHazar = id;
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetQuestionnaires()
        {
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == _idHazar);
            if (hazarNabeg != null)
            {
                hazarNabeg.Quastionnaires = await QuastionnairesSelect(hazarNabeg.Id);
                string resultJSON = JsonConvert.SerializeObject(hazarNabeg);
                return Json(resultJSON);
            }
            return Json(null);
        }
        [HttpGet]
        public async Task<IActionResult> AddQuestionnare(int id)
        {
            _idHazar = id;
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == id);
            return View(hazarNabeg);
        }
        [HttpPost]
        public IActionResult 
        public async Task<List<Quastionnaire>> QuastionnairesSelect(int hazarId)
        {
            List<Quastionnaire> quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == hazarId).ToListAsync();
            for(int i =0; i < quastionnaires.Count; i++)
            {
                quastionnaires[i].Differentiation = await db.Differentiations.FirstOrDefaultAsync(p => p.QuastionnaireId == quastionnaires[i].Id);
                quastionnaires[i].Emotions = await db.Emotions.Where(p => p.QuastionnaireId == quastionnaires[i].Id).ToListAsync();
                for (int j = 0; j < quastionnaires[i].Emotions.Count; j++)
                {
                    quastionnaires[i].Emotions[j].Points = await db.Points.Where(p => p.EmotionId == quastionnaires[i].Emotions[j].Id).ToListAsync();
                }
            }
            return quastionnaires;
        }
    }
}
