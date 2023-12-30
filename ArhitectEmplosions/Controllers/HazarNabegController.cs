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
            if (hazarNabeg != null)
            {
                List<Quastionnaire> quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == _idHazar).ToListAsync();
                List<Emotion> emotions = new List<Emotion>();
                foreach (var item in quastionnaires)
                {
                    item.Differentiation = await db.Differentiations.FirstOrDefaultAsync(p => p.QuastionnaireId == item.Id);
                    item.Emotions = await db.Emotions.Where(p => p.QuastionnaireId == item.Id).ToListAsync();
                    for (int i = 0; i < item.Emotions.Count; i++)
                    {
                        item.Emotions[i].Points = await db.Points.Where(p => p.EmotionId == item.Emotions[i].Id).ToListAsync();
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

        [HttpGet]
        public async Task<IActionResult> AddQuestionnaire(int id)
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
        public void GiveQuestionnaire([FromBody] ListQuest listQuest)
        {
            HazarNabeg? hazarNabeg = db.HazarNabegs.FirstOrDefault(p => p.Id == _idHazar);
            Console.WriteLine(listQuest);
            if (listQuest.Quastionnaires != null && hazarNabeg != null)
            {
                Console.WriteLine(listQuest);
                List<Quastionnaire> quastionnaires = new List<Quastionnaire>();
                foreach (var item in listQuest.Quastionnaires)
                {

                    quastionnaires.Add(new Quastionnaire
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
                db.Quastionnaires.AddRange(quastionnaires);
                db.SaveChanges();
            }
            //return RedirectToAction("Index");
        }
        public async Task<List<Quastionnaire>> QuastionnairesSelect(int hazarId)
        {
            List<Quastionnaire> quastionnaires = await db.Quastionnaires.Where(p => p.HazarNabegId == hazarId).ToListAsync();
            for (int i = 0; i < quastionnaires.Count; i++)
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
