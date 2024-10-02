using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ArhitectEmplosions.Database;
using ArhitectEmplosions.Models.Poke;
using Microsoft.EntityFrameworkCore;
using ArhitectEmplosions.Models;

namespace ArhitectEmplosions.Controllers
{
    public class PokeController : Controller
    {
        private ApplicationContext db;
        private Random random;
        private List<string> Sush = new List<string>() { "ПОРЕБРИК", "КАМПУСИК", "НЕФТЕПРОВОДИК", "РЕКТИФИКАТИК", "СОЛЯРОЧКА", "МАЗУТИК", "ФРОНТОНЧИК", "РИЗАЛИТИК", "АНТАБЛИМЕНТИК" };
        private List<string> Pril = new List<string>() { "КРАСНЫЙ", "ЗЕЛЕНЫЙ", "НЕЖНЫЙ", "ГРУСТНЫЙ", "ФАНТАСТИЧЕСКИЙ", "ОЧАРОВАТЕЛЬНЫЙ", "ВЕСЕЛЫЙ", "ЗАДУМЧИВЫЙ", "ГРОЗНЫЙ" };

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
        public async Task<JsonResult?> Getdata()
        {
            List<EmotionPoke> allcurrentPoke = await db.EmotionPokes.ToListAsync();
            PokeData pokeData = new PokeData();
            if (pokeData.SetPoints(allcurrentPoke))
            {
                string serializePoke = JsonConvert.SerializeObject(pokeData);
                return Json(serializePoke);
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Test(string val)
        {
            Testing testHazar = new Testing { Type = "Poke", Value = val };
            await db.Testing.AddAsync(testHazar);
            await db.SaveChangesAsync();
            return RedirectToAction("MainPoke");
        }
        [HttpPost]
        public async Task Givemedata([FromBody] EmotionPoke poke)
        {
            if (string.IsNullOrEmpty(poke.Points))
            { 
                EmotionPoke ep = new EmotionPoke(poke.Points!);
                await db.EmotionPokes.AddAsync(ep);
                await db.SaveChangesAsync();
            }
        }
    }
}
