using Microsoft.AspNetCore.Mvc;
using Student_housing.Models;

namespace Student_housing.Controllers
{
    public class AccountController : Controller
    {
        // get acc login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // post acc log in
        [HttpPost]
        public IActionResult Login(LogInViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.UserName) &&
                !string.IsNullOrWhiteSpace(model.Password))
            {
                // For now just go to dashboard no real auth yet
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError(string.Empty, "Please enter a username and password.");
            return View(model);
        }

        // get acc register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // post acc register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Later: save to New DB.
            return RedirectToAction("Login");
        }
        
        // TEMPORARY logout action!!!
        [HttpPost]
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }

    }
}
