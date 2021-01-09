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
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    NICNo = s.NICNo,
                    DateofBirth = s.DateofBirth,
                    School = s.School,
                    Grade = s.Grade,
                    ContactNumber = s.ContactNumber
                }).ToListAsync();

            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Select(s => new studentViewModel
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    NICNo = s.NICNo,
                    DateofBirth = s.DateofBirth,
                    School = s.School,
                    Grade = s.Grade,
                    ContactNumber = s.ContactNumber
                })
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(studentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await GetCurrentUserAsync();

            var student = new Student
            {
                StudentId = viewModel.StudentId,
                StudentName = viewModel.StudentName,
                NICNo = viewModel.NICNo,
                DateofBirth = viewModel.DateofBirth,
                School = viewModel.School,
                Grade = viewModel.Grade,
                ContactNumber = viewModel.ContactNumber,
                Email = viewModel.Email,
                Address = viewModel.Address,
                GuardianName = viewModel.GuardianName,
                GuardianContactNumber = viewModel.GuardianContactNumber,
                GuardianAddress = viewModel.GuardianAddress

            };

            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
