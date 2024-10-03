using ArhitectEmplosions.Database;
using ArhitectEmplosions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ArhitectEmplosions.Controllers
{
    public class UserController : Controller
    {
        private string _symbols;
        private ApplicationContext _context;
        private Random _random;
        public UserController(ApplicationContext context) 
        {
            _context = context;
            _symbols = "qwertyuiopasdfghjklzxcvbnm";
            _random = new Random();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
		[HttpPost]
		public async Task GetUsers([FromBody] UserList userList)
        {
            if (userList.Users.Count > 0)
            {
                List<User> users = new List<User>();
				Role userRole = (await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user"))!;
				foreach (var user in userList.Users)
                {
                    User us = new User { Login = user.Login, Name = user.Name, Password = GetPassword(), Role = userRole };
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
