using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Student_housing.Data;
using System.Security.Claims;

namespace Student_housing.Filters
{
    public class AccountGateFilter : IAsyncActionFilter
    {
        private readonly ApplicationDbContext _db;

        public AccountGateFilter(ApplicationDbContext db) // db check for user status
        {
            _db = db;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) // entry point
        {
            var user = context.HttpContext.User;
            if (user?.Identity?.IsAuthenticated != true) // check auth - only for logged in users ( [Authorize] handles anonymous users)
            {
                await next();
                return;
            }

            var idStr = user.FindFirstValue(ClaimTypes.NameIdentifier); // get user id from claims
            if (!int.TryParse(idStr, out var userId)) // validate user id   
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            var dbUser = await _db.Users.FindAsync(userId);
            if (dbUser == null)
            {
                context.Result = new RedirectToActionResult("Register", "Account", null); // user not found in db
                return;
            }

            if (dbUser.IsBanned)
            {
                context.Result = new RedirectToActionResult("Dashboard", "Home", null); // user is banned in db
                return;
            }

            if (!dbUser.AcceptedTerms)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);// user did not accept terms
                return;
            }

            await next();
        }
    }
}