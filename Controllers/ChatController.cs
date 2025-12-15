using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_housing.Data;
// ****************************  THIS IS STILL UNDER DEVELOPMENT - CHAT FUNCTIONALITY NOT COMPLETE ***********************

namespace Student_housing.Controllers
{
    [Authorize] // must be logged in to access anything here
    public class ChatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // **********  put the chat's UI here  ********
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "LandlordOnly")]
        public IActionResult BanUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return NotFound();

            // can't ban landlord accounts
            if (user.Role == "Landlord") return BadRequest("Cannot ban landlord.");

            user.IsBanned = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "LandlordOnly")]
        public IActionResult UnbanUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return NotFound();

            user.IsBanned = false;
            _context.SaveChanges();

            return RedirectToAction("Index"); 
        }
    }
}
