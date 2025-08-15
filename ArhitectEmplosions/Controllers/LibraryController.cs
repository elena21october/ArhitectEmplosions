using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ArhitectEmplosions.Models.Poke;
using ArhitectEmplosions.Database;
using ArhitectEmplosions.Models;
using ArhitectEmplosions.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ArhitectEmplosions.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly List<string> Components;
        public LibraryController(ApplicationContext context)
        {
            _context = context;
            Components = new List<string>(){"Концепт", "Ингредиенты", "Рецепт", "Специальное предложение для УГНТУ" };
        }

        [HttpGet]
        public IActionResult LibraryCollection()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string type)
        {
            var emotionForms = (await _context.EmotionForms.ToListAsync())
                                                           .Where(x=> x.Type == type)
                                                           .ToList();
            List<Zabeyka> result = emotionForms
                                            .Select(x => new Zabeyka(x, "Test"))
                                            .ToList();
            return View(null);
        }
        [HttpGet]
        public async Task<IActionResult> ZabeykaPlus()
        {
            var emotionForms = (await _context.EmotionForms.ToListAsync())
                .Where(x=> x.Type == "Zabeyka")
                .ToList();
            List<Zabeyka> result = emotionForms
                .Select(x => new Zabeyka(x, "Test"))
                .ToList();
            result.ForEach(x=>x.SetMainPic());
            string role = User.FindFirstValue(ClaimTypes.Role) ?? "guest";
            return View((result, role));
        }
        [HttpGet]
        public async Task<IActionResult> MuralView()
        {
            var emotionForms = (await _context.EmotionForms.ToListAsync())
                .Where(x=> x.Type == "Mural")
                .ToList();
            List<EmotionMuralView> result = emotionForms
                .Select(x => new EmotionMuralView(x, "Test"))
                .ToList();
            result.ForEach(x=>x.SetMainPic());
            string role = User.FindFirstValue(ClaimTypes.Role) ?? "guest";
            return View((result, role));
        }
        
        [HttpGet]
        public IActionResult CreateForm()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateFormMural()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateFormZabeyka()
        {
            return View(Components);
        }

        [HttpGet]
        public async Task<ActionResult> EmotForm(int id)
        {
            var dbEmotForm = (await _context.EmotionForms.FirstOrDefaultAsync(x => x.Id == id))!;
            var res = new Zabeyka(dbEmotForm, "Anastasia");
            return View(res);
        }
        
        [HttpPost]
        public async Task <ActionResult> CreateMural(EmotionForm model, IFormFileCollection uploadImage)
        {
            var modelpath = $"{model.Title}_{Guid.NewGuid()}";
            var dirPath = $@"{Directory.GetCurrentDirectory()}\wwwroot\images\{modelpath}";
            var dir = Directory.CreateDirectory(dirPath);
            List<Picture> imageList = new List<Picture>();

            foreach (var item in uploadImage)
            {
                long fileSize = item.Length;
                string fileType = item.ContentType;

                if (fileSize > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        item.CopyTo(stream);
                        var bytes = stream.ToArray();
                        imageList.Add(new Picture{Image = bytes});
                    }
                }
            }
            model.ImageList = JsonConvert.SerializeObject(imageList);
            model.CreationDate = DateTime.Now;
            model.Type = "Mural";
            model.Folder = modelpath;
            await _context.EmotionForms.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("CreateFormMural");
        }
        [HttpPost]
        public async Task <ActionResult> CreateZabeyka(EmotionForm model, IFormFileCollection uploadImage)
        {
            Dictionary<string, Picture> zabeyPic = new Dictionary<string, Picture>();
            int i = 0;
            foreach (var item in uploadImage)
            {
                long fileSize = item.Length;
                string fileType = item.ContentType;

                if (fileSize > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        item.CopyTo(stream);
                        var bytes = stream.ToArray();
                        zabeyPic.Add($"{Components[i++]}", new Picture{Image = bytes});
                    }
                }
            }
            model.ImageList = JsonConvert.SerializeObject(zabeyPic);
            model.CreationDate = DateTime.Now;
            model.Type = "Zabeyka";
            await _context.EmotionForms.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("CreateFormZabeyka");
        }

        [HttpPost]
        public async Task PositiveOne(int id)
        {
            var model = (await _context.EmotionForms.FirstOrDefaultAsync(x => x.Id == id))!;
            model.Positive++;
            _context.EmotionForms.Update(model);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task Vdoh([FromBody] LibraryView lib)
        {
            var model = (await _context.EmotionForms.FirstOrDefaultAsync(x => x.Id == lib.Id))!;
            model.Positive++;
            _context.EmotionForms.Update(model);
            await _context.SaveChangesAsync();
        }
        [HttpPost]
        public async Task NeVdoh([FromBody] LibraryView lib)
        {
            var model = (await _context.EmotionForms.FirstOrDefaultAsync(x => x.Id == lib.Id))!;
            model.Negative++;
            _context.EmotionForms.Update(model);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task DeleteProj(int id)
        {
            var model = (await _context.EmotionForms.FirstOrDefaultAsync(x => x.Id == id))!;
            _context.EmotionForms.Remove(model);
            await _context.SaveChangesAsync();
        }
        
    }
}
