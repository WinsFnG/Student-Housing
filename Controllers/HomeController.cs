using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NexusThuisWeb.Models;
using Student_housing.Data;
using Student_housing.Models;

namespace Student_housing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Application()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Profiles.FirstOrDefault(u => u.email == email && u.password == password);
            if (user == null)
            {
                return View();
            }

            var userId = user.id;
            var userName = user.name;
            var userVCode = user.vcode;
            int userIsLandlord = user.is_landlord;

            HttpContext.Session.SetInt32("userId", userId);
            HttpContext.Session.SetString("userName", userName);
            HttpContext.Session.SetInt32("userVCode", userVCode);
            HttpContext.Session.SetInt32("userIsLandlord", userIsLandlord);

            if (userIsLandlord == 1)
            {
                return RedirectToAction("Landlord", "Portal");
            }

            else
            {
                return RedirectToAction("Tenant", "Portal");
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(string name, string email, string password, int vcode)
        {
            var userName = name;
            var emailAddress = email;
            var userPassword = password;
            var userVCode = vcode;

            var profiles = new Profiles
            {
                name = userName,
                email = emailAddress,
                password = userPassword,
                vcode = userVCode
            };

            _context.Profiles.Add(profiles);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
    }
}
