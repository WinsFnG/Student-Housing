using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Student_housing.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Landlord"))
                return RedirectToAction(nameof(Landlord));

            return RedirectToAction(nameof(Tenant));
        }

        [Authorize(Roles = "Tenant")]
        public IActionResult Tenant()
        {
            return View("TenantIndex");
        }

        [Authorize(Roles = "Landlord")]
        public IActionResult Landlord()
        {
            return View("LandlordIndex");
        }
    }
}
