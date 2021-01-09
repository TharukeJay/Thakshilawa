using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thakshilawa.ViewModels;
using XYZLaundry.Data;
using XYZLaundry.Models;

namespace Thakshilawa.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var students = await _context.Student
                .Select(s => new studentViewModel
                {
                    StudentName = s.StudentName,
                    NICNo = s.NICNo,
                    DateofBirth = s.DateofBirth,
                    School = s.School,
                    Grade = s.Grade,
                    ContactNumber = s.ContactNumber
                }).ToListAsync();

            return View(students);
        }
    }
}
