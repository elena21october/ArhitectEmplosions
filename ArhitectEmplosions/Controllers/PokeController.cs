using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Models.Poke;
using Services.Poke;
using Newtonsoft.Json;
using Models.HazarNabeg;

namespace ArhitectEmplosions.Controllers
{
    public class PokeController : Controller
    {

        ApplicationContext db;
        Random random;
        List<string> Sush = new List<string>() { "ПОРЕБРИК", "КАМПУСИК", "НЕФТЕПРОВОДИК", "РЕКТИФИКАТИК", "СОЛЯРОЧКА", "МАЗУТИК", "ФРОНТОНЧИК", "РИЗАЛИТИК", "АНТАБЛИМЕНТИК" };
        List<string> Pril = new List<string>() { "КРАСНЫЙ", "ЗЕЛЕНЫЙ", "НЕЖНЫЙ", "ГРУСТНЫЙ", "ФАНТАСТИЧЕСКИЙ", "ОЧАРОВАТЕЛЬНЫЙ", "ВЕСЕЛЫЙ", "ЗАДУМЧИВЫЙ", "ГРОЗНЫЙ" };
        public PokeController(ApplicationContext context)
        {
            db = context;
            random = new Random();
        }
        [HttpGet]
        public ActionResult MainPoke()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ResultPoke()
        {
            return View();
        }
        [HttpGet]
        public string RandName()
        {
            string result = Sush[random.Next(0, Sush.Count - 1)] + " " + Pril[random.Next(0, Pril.Count - 1)];
            return result;
        }
        [HttpGet]
        public JsonResult Getdata()
        {
            List<PointPoke> allcurrentUser = db.EmotionsPoke.ToList();
            EmotionPoke emotionPoints = new EmotionPoke
            {
                PositivePoints = allcurrentUser.Where(p => p.Color == "#58FF008F").ToList(),
                NegativePoints = allcurrentUser.Where(p => p.Color == "#FF00008F").ToList(),
                NeutralPoints = allcurrentUser.Where(p => p.Color == "#FFF200AB").ToList(),
            };
            string serializeUsers = JsonConvert.SerializeObject(emotionPoints);
            return Json(serializeUsers);
        }
        [HttpPost]
        public IActionResult Test(string val)
        {
            TestPoke testPoke = new TestPoke { Type = "Poke", Value = val };
            db.TestPokes.Add(testPoke);
            db.SaveChanges();
            return RedirectToAction("MainPoke");
        }
        [HttpPost]
        public void Givemedata([FromBody] UserPoke user)
        {
            List<PointPoke> points = db.EmotionsPoke.Where(p => p.Color != "#9C27B073").ToList();
            points.AddRange(user.Points);
            db.Users.Add(user);
            db.EmotionsPoke.AddRange(user.Points);
            db.SaveChangesAsync();
        }
    }
}
