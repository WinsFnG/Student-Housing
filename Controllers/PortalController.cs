using Microsoft.AspNetCore.Mvc;

namespace NexusThuisWeb.Controllers
{
    public class PortalController : Controller
    {
        public IActionResult Tenant()
        {
            return View();
        }

        public IActionResult Landlord()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Userdisplay()
        {
            return Redirect("Tenant");
        }
    }
}
