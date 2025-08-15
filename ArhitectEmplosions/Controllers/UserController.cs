using ArhitectEmplosions.Database;
using ArhitectEmplosions.Models;
using ArhitectEmplosions.Models.HazarNabeg;
using ArhitectEmplosions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ArhitectEmplosions.Controllers
{
    public class UserController : Controller
    {
        private readonly string _symbols;
        private readonly ApplicationContext _context;
        private readonly Random _random;
        public UserController(ApplicationContext context) 
        {
            _context = context;
            _symbols = "qwertyuiopasdfghjklzxcvbnm";
            _random = new Random();
        }
        [HttpGet]
        public IActionResult CreateUser(int id)
        {
            return View(id);
        }
        [HttpGet]
        public async Task<IActionResult> UserList(int hazarId)
        {
            HazarNabeg? hazar = await _context.HazarNabegs.FirstOrDefaultAsync();
            HazarUsers hazarUsers = new HazarUsers();
            if (hazar != null) 
            {
                var list = await _context.Users.Where(x=>x.HazarId == hazarId && x.RoleId == _context.Roles.FirstOrDefault(x=>x.Name=="user")!.Id).ToListAsync();
                hazarUsers.Users = list;
                hazarUsers.HazarNabegsId = hazarId;
            }
            return View(hazarUsers);
        }

		[HttpPost]
		public async Task GetUsers([FromBody] UserList userList)
        {
            if (userList.Users!.Count > 0)
            {
                List<User> users = new List<User>();
				Role userRole = (await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user"))!;
				foreach (var user in userList.Users)
                {
                    User us = new User { Login = user.Login, Name = user.Name, Password = GetPassword(), Role = userRole, HazarId = user.HazarId };
                    users.Add(us);
				}
                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }
        }
        private string GetPassword()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                sb.Append(_symbols[_random.Next(_symbols.Length)]);
            }
            return sb.ToString();
        }
    }
}
