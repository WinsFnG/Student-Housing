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
            if (!string.IsNullOrWhiteSpace(model.Username) &&
                !string.IsNullOrWhiteSpace(model.Password))
            {
                // Here you put your Main/Index page's name so it goes there after logging in
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Please enter a username and password.");
            return View(model);
        }

        // get acc register
        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Validation failed - stay on page, do NOT show terms
                ViewData["ShowTerms"] = "true";
                return View(model);
            }

            // TODO HERE: hash password, save user to DB, etc.
            // var hashed = PasswordHasher.Hash(model.Password);
            // _context.Users.Add(new User { ... });
            // _context.SaveChanges();

            // Registration success - show terms modal
            ViewData["ShowTerms"] = "true";

            // Stay on Register view so the modal can appear
            return View(model);
        }

        // TEMPORARY logout action!!! *This is currently overriden by the goodbye page*
        [HttpPost]
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }

    }
}
