using XYZLaundry.Data;
using XYZLaundry.Models;
using System;
using System.Threading.Tasks;

namespace XYZLaundry.Helpers
{
    public class ActivityLogCreator
    {
        private readonly ApplicationDbContext _context;

        public ActivityLogCreator(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(string Action, ApplicationUser user)
        {
            var activityLog = new ActivityLog
            {
                Action = Action,
                ActionedBy = user.Email,
                ActionedOn = DateTime.Now
            };

            _context.ActivityLog.Add(activityLog);

            await _context.SaveChangesAsync();
        }
    }
}
