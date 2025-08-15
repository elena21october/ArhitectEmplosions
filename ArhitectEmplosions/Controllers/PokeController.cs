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
        private readonly ApplicationContext _context;
        private readonly Random _rnd;
        private readonly List<string> _sush;
        private readonly List<string> _pril;

        public PokeController(ApplicationContext context)
        {
            _context = context;
            _rnd = new Random();
            _sush = new List<string>() { "ПОРЕБРИК", "КАМПУСИК", "НЕФТЕПРОВОДИК", "РЕКТИФИКАТИК", "СОЛЯРОЧКА", "МАЗУТИК", "ФРОНТОНЧИК", "РИЗАЛИТИК", "АНТАБЛИМЕНТИК" };
            _pril = new List<string>() { "КРАСНЫЙ", "ЗЕЛЕНЫЙ", "НЕЖНЫЙ", "ГРУСТНЫЙ", "ФАНТАСТИЧЕСКИЙ", "ОЧАРОВАТЕЛЬНЫЙ", "ВЕСЕЛЫЙ", "ЗАДУМЧИВЫЙ", "ГРОЗНЫЙ" };
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
            string result = _sush[_rnd.Next(0, _sush.Count - 1)] + " " + _pril[_rnd.Next(0, _pril.Count - 1)];
            return result;
        }

        [HttpGet]
        public async Task<JsonResult?> Getdata()
        {
            List<EmotionPoke> allcurrentPoke = await _context.EmotionPokes.ToListAsync();
            PokeData pokeData = new PokeData();
            if (!pokeData.SetPoints(allcurrentPoke))
            {
                return null;
            }
            string serializePoke = JsonConvert.SerializeObject(pokeData);
            return Json(serializePoke);
        }

        [HttpPost]
        public async Task<IActionResult> Test(string val)
        {
            Testing testHazar = new Testing { Type = "Poke", Value = val };
            await _context.Testing.AddAsync(testHazar);
            await _context.SaveChangesAsync();
            return RedirectToAction("MainPoke");
        }
        [HttpPost]
        public async Task Givemedata([FromBody] EmotionPoke poke)
        {
            if (!string.IsNullOrEmpty(poke.Points))
            { 
                EmotionPoke ep = new EmotionPoke(poke.Points!);
                await _context.EmotionPokes.AddAsync(ep);
                await _context.SaveChangesAsync();
            }
        }
    }
}
