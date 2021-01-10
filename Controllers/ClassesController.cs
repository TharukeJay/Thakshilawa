using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thakshilawa.Models;
using Thakshilawa.ViewModels;
using XYZLaundry.Data;
using XYZLaundry.Models;

namespace Thakshilawa.Controllers
{
    public class ClassesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClassesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var cl = await _context.Classes
                .Select(s => new ClassViewModel
                {
                    ClassId = s.ClassId,
                    SubjectName =s.SubjectName,
                    Duration =s.Duration,
                    LecturerID =s.LecturerID,
                    SessionID= s.SessionID,
                    StudentCapacity =s.StudentCapacity,
                    CourseDuration =s.CourseDuration

                }).ToListAsync();

            return View(cl);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClassViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();

            var cl = new Classes
            {
                ClassId = viewModel.ClassId,
                SubjectName = viewModel.SubjectName,
                Duration = viewModel.Duration,
                LecturerID =viewModel.LecturerID,
                SessionID = viewModel.SessionID,
                StudentCapacity = viewModel.StudentCapacity,
                MonthlyFee = viewModel.MonthlyFee,
                CourseDuration = viewModel.CourseDuration,
            };

            _context.Classes.Add(cl);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
