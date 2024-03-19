using DataBaseContext;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models.HazarNabeg;
using Newtonsoft.Json;
using Services.HazarNabeg;
using System.Diagnostics;

namespace ArchEmplosion.Controllers
{
    public class HazarNabegController : Controller
    {
        ApplicationContext db;
        private static int _idHazar;

        public HazarNabegController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> MainHazar()
        {
            _idHazar = 0;
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
        public IActionResult ViewHazar(int id)
        {
            _idHazar = id;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetQuestionnaires()
        {
			HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == _idHazar);
            await Console.Out.WriteLineAsync();
            if (hazarNabeg != null)
			{
				List<Quastionnaire>? quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == _idHazar).ToListAsync();
				HazarNabegData hazarNabegData = new HazarNabegData(hazarNabeg);
				hazarNabegData.SetQuastionnaires(quastionnaires);
				string resultJSON = JsonConvert.SerializeObject(hazarNabegData);
				return Json(resultJSON);
			}
			return Json(null);
		}

        [EnableCors("AllowAllOrigin")]
        [HttpGet]
        public async Task<JsonResult> GetHttpData(int idHazar)
        {
            return await JsonHazar(idHazar);
        }

        [HttpGet]
        public IActionResult AddQuestionnaire(int id)
        {
            _idHazar = id;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddConflict(int id)
        {
            _idHazar = id;
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == id);
            return View(hazarNabeg);
        }

        [HttpGet]
        public async Task<JsonResult> GetCoordinates()
        {
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == _idHazar);
            if (hazarNabeg != null)
            {
                string resultJSON = JsonConvert.SerializeObject(hazarNabeg);
                return Json(resultJSON);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> GiveQuestionnaire([FromBody] ListQuest listQuest)
        {
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == _idHazar);
            if (listQuest.Quastionnaires != null && hazarNabeg != null)
            {
                List<Quastionnaire> quastion = new List<Quastionnaire>();
                foreach (var item in listQuest.Quastionnaires)
                {
                    quastion.Add(new Quastionnaire
                    {
                        Differentiation = item.Differentiation,
                        Comm = item.Comm,
                        NazarNabeg = hazarNabeg,
                        HazarNabegId = _idHazar,
                        DateTime = DateTime.Now,
                        Emotions = item.Emotions,
                    });
                }
                await db.Quastionnaires.AddRangeAsync(quastion);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("MainHazar");
        }
        [HttpPost]
        public IActionResult Test(string val)
        {
            TestHazar testHazar = new TestHazar {Type = "Hazar", Value = val };
            db.TestHazars.Add(testHazar);
            db.SaveChanges();
            return RedirectToAction("MainHazar");
        }
        
        public async Task<JsonResult> JsonHazar(int idHazar)
        {
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == idHazar);
            if (hazarNabeg != null)
            {
                List<Quastionnaire>? quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == idHazar).ToListAsync();
                HazarNabegData hazarNabegData = new HazarNabegData(hazarNabeg);
                hazarNabegData.SetQuastionnaires(quastionnaires);
                string resultJSON = JsonConvert.SerializeObject(hazarNabegData);
                return Json(resultJSON);
            }
            return Json(null);
        }
    }
}
