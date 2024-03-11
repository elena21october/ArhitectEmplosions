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
            return await JsonHazar(_idHazar);
        }
        [EnableCors("AllowAllOrigin")]
        [HttpGet]
        public async Task<JsonResult> GetHttpData(int idHazar)
        {
            return await JsonHazar(idHazar);
        }

        [HttpGet]
        public async Task<IActionResult> AddQuestionnaire(int id)
        {
            _idHazar = id;
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == id);
            return View(hazarNabeg);
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
                        PositiveComm = item.PositiveComm,
                        NegativeComm = item.NegativeComm,
                        NeutralComm = item.NeutralComm,
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
        public async Task<List<Quastionnaire>> QuastionnairesSelect(int hazarId)
        {
            List<Quastionnaire> quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == hazarId).ToListAsync();
            List<Emotion> emotionsDb = await db.Emotions.ToListAsync();
            List<PointHN> pointsDb = await db.Points.ToListAsync();
            for (int i = 0; i < quastionnaires.Count; i++)
            {
                quastionnaires[i].Differentiation = await db.Differentiations.FirstOrDefaultAsync(p => p.QuastionnaireId == quastionnaires[i].Id);
                quastionnaires[i].Emotions = emotionsDb.Where(p => p.QuastionnaireId == quastionnaires[i].Id).ToList();
                for (int j = 0; j < quastionnaires[i].Emotions.Count; j++)
                {
                    quastionnaires[i].Emotions[j].Points = pointsDb.Where(p => p.EmotionId == quastionnaires[i].Emotions[j].Id).ToList();
                }
            }
            return quastionnaires;
        }
        public async Task<List<Emotion>> EmotionsHazarId(int hazarId)
        {
            List<Quastionnaire> quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == _idHazar).ToListAsync();
            List<Emotion> emotionsdb = await db.Emotions.ToListAsync();
            List<PointHN> pointdb = await db.Points.ToListAsync();
            List<Emotion> emotions = new List<Emotion>();
            foreach (var item in quastionnaires)
            {
                item.Differentiation = await db.Differentiations.FirstOrDefaultAsync(p => p.QuastionnaireId == item.Id);
                item.Emotions = emotionsdb.Where(p => p.QuastionnaireId == item.Id).ToList();
                for (int i = 0; i < item.Emotions.Count; i++)
                {
                    item.Emotions[i].Points = pointdb.Where(p => p.EmotionId == item.Emotions[i].Id).ToList();
                }
                emotions.AddRange(item.Emotions);
            }
            return emotions;
        }
        public async Task<JsonResult> JsonHazar(int idHazar)
        {
            HazarNabeg? hazarNabeg = await db.HazarNabegs.FirstOrDefaultAsync(p => p.Id == idHazar);
            if (hazarNabeg != null)
            {
                List<Quastionnaire> quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == idHazar).ToListAsync();
                List<Emotion> emotionsdb = await db.Emotions.ToListAsync();
                List<PointHN> pointdb = await db.Points.ToListAsync();
                List<Emotion> emotions = new List<Emotion>();
                foreach (var item in quastionnaires)
                {
                    item.Differentiation = await db.Differentiations.FirstOrDefaultAsync(p => p.QuastionnaireId == item.Id);
                    item.Emotions = emotionsdb.Where(p => p.QuastionnaireId == item.Id).ToList();
                    for (int i = 0; i < item.Emotions.Count; i++)
                    {
                        item.Emotions[i].Points = pointdb.Where(p => p.EmotionId == item.Emotions[i].Id).ToList();
                    }
                    emotions.AddRange(item.Emotions);
                }
                HazarNabegData hazarNabegData = new HazarNabegData();
                hazarNabegData.Name = hazarNabeg.Name;
                hazarNabegData.X = hazarNabeg.X;
                hazarNabegData.Y = hazarNabeg.Y;
                hazarNabegData.PositiveEmotions = emotions.Where(p => p.Color == "#58FF008F").ToList();
                hazarNabegData.NegativeEmotions = emotions.Where(p => p.Color == "#FF00008F").ToList();
                hazarNabegData.NeutralEmotions = emotions.Where(p => p.Color == "#FFF200AB").ToList();
                hazarNabegData.ConflictEmotions = emotions.Where(p => p.Color == "#9300FF8F").ToList();
                hazarNabegData.Quastionnaires = quastionnaires;
                string resultJSON = JsonConvert.SerializeObject(hazarNabegData);
                return Json(resultJSON);
            }
            return Json(null);
        }
    }
}
