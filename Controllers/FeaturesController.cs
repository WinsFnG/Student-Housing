using Microsoft.AspNetCore.Mvc;
using Student_housing.Data;
using NexusThuisWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace NexusThuisWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Events")]
        public async Task<ActionResult<List<Events>>> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        [HttpPost("Events")]
        public async Task<IActionResult> PostEvents(GetEvents request)
        {
            var createdBy = HttpContext.Session.GetInt32("userId");
            var verifyCode = HttpContext.Session.GetInt32("userVCode");

            var PostEvents = new Events
            {
                title = request.title,

                event_date = DateTime.SpecifyKind(request.date, DateTimeKind.Utc),

                description = request.description,

                created_by = createdBy,

                vcode = verifyCode

            };

            _context.Events.Add(PostEvents);
            await _context.SaveChangesAsync();

            return Ok();


        }

        [HttpPost("CleaningSchedule")]
        public async Task<ActionResult> GetCleaning(GetCleaning request)
        {
            var userVCode = HttpContext.Session.GetInt32("UserVCode");
            var userName = HttpContext.Session.GetString("userName");

            var cleaningSchedule = new Cleaning_schedule
            {
                date = DateTime.SpecifyKind(request.date, DateTimeKind.Utc),
                kitchen = request.kitchen,
                living_room = request.livingroom,
                bathroom = request.bathroom,
                storage = request.storageroom,
                vcode = userVCode,
                name = userName
            };

            _context.Cleaning_schedule.Add(cleaningSchedule);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("CleaningSchedule")]
        public async Task<ActionResult<List<Cleaning_schedule>>> PostCleaning()
        {
            var cleaningSchedule = await _context.Cleaning_schedule.ToListAsync();
            return Ok(cleaningSchedule);
        }
    }
}
