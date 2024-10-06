using ArhitectEmplosions.Database;
using ArhitectEmplosions.Models;
using ArhitectEmplosions.Models.HazarNabeg;
using ArhitectEmplosions.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ArchEmplosion.Controllers
{
    [Authorize]
    public class HazarNabegController : Controller
    {
        private ApplicationContext db;
        private static int _idHazar;
        public HazarNabegController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> MainHazar()
        {
            _idHazar = 0;
            HazarNabegsUser hazarNabegs = new HazarNabegsUser();
            hazarNabegs.HazarNabegs = await db.HazarNabegs.ToListAsync();
            hazarNabegs.Role = User.FindFirstValue(ClaimTypes.Role) ?? "guest";
            return View(hazarNabegs);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateHazar()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddHazar([FromBody] HazarNabeg hazarNabeg)
        {
            HazarNabeg hazar = new HazarNabeg(hazarNabeg.Name, hazarNabeg.X, hazarNabeg.Y);
            await db.HazarNabegs.AddAsync(hazar);
            await db.SaveChangesAsync();
            return RedirectToAction("MainHazar","HazarNabeg");
        }

        [HttpGet]
        public IActionResult EditHazar()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ViewHazar(int id)
        {
            _idHazar = id;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetQuestionnaires()
        {
			HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == _idHazar);
            if (hazarNabeg != null)
			{
				List<Quastionnaire>? quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == _idHazar).ToListAsync();
				HazarNabegData hazarNabegData = new HazarNabegData(hazarNabeg);
                if (hazarNabegData.SetQuastionnaires(quastionnaires))
                {
				    string resultJSON = JsonConvert.SerializeObject(hazarNabegData);
				    return Json(resultJSON);
                }
			}
			return Json(null);
		}

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetHttpData(int idHazar)
        {
            return await JsonHazar(idHazar);
        }

        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
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

		[HttpGet]
        [AllowAnonymous]
        public IActionResult LookConflict(int id)
		{
			_idHazar = id;
            return View();
		}

		[HttpPost]
        public async Task<IActionResult> GiveQuestionnaire([FromBody] ListQuest listQuest)
        {
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == _idHazar);
            if (listQuest.Quastionnaires != null && hazarNabeg != null)
            {
                List<Quastionnaire> quastion = new List<Quastionnaire>();
                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                foreach (var item in listQuest.Quastionnaires)
                {
                    quastion.Add(new Quastionnaire
                    {
                        Differentiation = item.Differentiation,
                        Comm = item.Comm,
                        HazarNabegId = item.HazarNabegId,
                        HazarUserId = id,
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
        [AllowAnonymous]
        public async Task<IActionResult> Test(string val)
        {
            Testing testHazar = new Testing { Type = "Hazar", Value = val };
            await db.Testing.AddAsync(testHazar);
            await db.SaveChangesAsync();
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
