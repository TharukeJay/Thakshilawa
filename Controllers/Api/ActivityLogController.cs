using XYZLaundry.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace XYZLaundry.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ActivityLog")]
    public class ActivityLogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get activity log
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActivityLog()
        {
            var activities = await _context.ActivityLog
                .ToListAsync();

            return Ok(new { Items = activities, Count = activities.Count() });
        }
    }
}
